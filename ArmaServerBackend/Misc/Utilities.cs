using BisDll.Model;
using BisDll.Model.MLOD;
using BisDll.Model.ODOL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmaServerBackend
{
    /// <summary>
    /// More advanced helper methods, mainly contains methods that are less fequently used
    /// </summary>
    public class Utilities
    {   //TODO: Revise this class at somepoint got a litle messy getting it working


        #region Start ARMA Functions
        /// <summary>
        /// Current ProcessID of running arma3server
        /// </summary>
        private int ArmaProcessID = -1;

        /// <summary>
        /// Textbox refrence to where ArmaProcessID will be shown
        /// </summary>
        private TextBox textBox = null;

        /// <summary>
        /// Original app title
        /// </summary>
        private string title = "";

        /// <summary>
        /// Handles arma3server staus
        /// </summary>
        private bool isOnline = false, IsArmaMonitored = false, IsFirstRun = true;

        /// <summary>
        /// Downloads, Randomizes, Packs and Moves all files from gitserver
        /// </summary>
        /// <param name="form">mainForm where this was called from, required for title status</param>
        /// <param name="progressBar">progressBar to show current progress</param>
        /// <returns>true/false</returns>
        private bool PullAndRandomize(Form form, ProgressBar progressBar)
        {
            string message;
            progressBar.Value = 0;//0% 
            
            //purge directory if exists and create new
            try
            {
                message = "Purging local git directory";
                Trace.WriteLine(message + " | DIR: [" + DLL.ConfigValues.GitDirectory + "]");
                form.Text = $"{title} | {message}";

                progressBar.Value += 5;//5% 
                if (Directory.Exists(DLL.ConfigValues.GitDirectory)) Helpers.DeleteDirectory(DLL.ConfigValues.GitDirectory);
                progressBar.Value += 5;//10% 
                Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);


            }
            catch (IOException ex)
            {
                message = "Invalid git directory provided | Extecption:  ";
                Trace.WriteLine(message + ex.InnerException);
                MessageBox.Show(message + ex);
                return false;
            };

            //Timeout 
            Thread.Sleep(2000);
            progressBar.Value += 10;//20%  
            Thread.Sleep(1000);

            //Create random local vars
            if (DLL.ConfigValues.LocalVaribales.Count > 0)
            {
                form.Text = $"{title} | Creating Random Local Variables";
                foreach (string varToChange in DLL.ConfigValues.LocalVaribales)
                {
                    string newVar = "_" + DLL.HelperFunctions.CreateRandomVariable(DLL.ConfigValues.RandomVariablesLength);
                    DLL._localVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                };
            };
            progressBar.Value += 20;//40%

            //Create random global vars
            if (DLL.ConfigValues.GlobalVariables.Count > 0)
            {
                form.Text = $"{title} | Creating Random Global Variables";
                foreach (string varToChange in DLL.ConfigValues.GlobalVariables)
                {
                    string newVar = DLL.HelperFunctions.CreateRandomVariable(DLL.ConfigValues.RandomVariablesLength);
                    DLL._globalVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                };
            };
            progressBar.Value += 10;//50%

            //Create random function vars
            if (DLL.ConfigValues.Functions.Count > 0)
            {
                form.Text = $"{title} | Creating Random Functions";
                foreach (string varToChange in DLL.ConfigValues.Functions)
                {
                    string newVar = DLL.HelperFunctions.CreateRandomVariable(DLL.ConfigValues.RandomFunctionsLength);
                    DLL._scriptFuncs[varToChange.Replace(DLL.ConfigValues.FunctionsTag + "_fnc_", "")] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                };
            };
            progressBar.Value += 10;//60%
            
            
            //Download Folders, change all vars to random vars, Pack and move all pbos
            foreach (PBOFile pbo in DLL.ConfigValues.Pbos)
            {
                if (!pbo.IsEnabled) continue;
                
                //Download
                form.Text = $"{title} | Downloading {pbo.Name}"; 
                if (!Helpers.DownloadPBO(pbo, out Exception DownloadPBOException))
                {
                    MessageBox.Show($"Error Downloading: {pbo.Name}" + Helpers.NewLine() + $"From URL: {pbo.GitUrl}" + Helpers.NewLine() + $"Exception: {DownloadPBOException.Message}");
                    return false;
                };
                Thread.Sleep(2000);

                //Randomize
                if(DLL.ConfigValues.Functions.Count > 0 || DLL.ConfigValues.GlobalVariables.Count > 0 || DLL.ConfigValues.LocalVaribales.Count > 0 || pbo.SingleLineFunctions)
                {
                    form.Text = $"{title} | Randomizing {pbo.Name}";
                    if (!Helpers.RandomizePBO(pbo, out Exception RandomizePBOException))
                    {
                        MessageBox.Show($"Error Randomizing: {pbo.Name}" + Helpers.NewLine() + $"Exception: {RandomizePBOException.Message}");
                        return false;
                    };
                    Thread.Sleep(1000);
                };

                //Pack
                form.Text = $"{title} | Packing {pbo.Name}";
                if (!Helpers.PackPBO(pbo, out Exception PackPBOException))
                {
                    MessageBox.Show($"Error Packing: {pbo.Name}" + Helpers.NewLine() + $"Exception: {PackPBOException.Message}");
                    return false;
                };

                //Move
                form.Text = $"{title} | Moving {pbo.Name}";
                if (!Helpers.MovePBO(pbo, out Exception MovePBOException))
                { 
                    MessageBox.Show($"Error Moving: {pbo.Name}" + Helpers.NewLine() + $"From: {pbo.GitUrl}" + Helpers.NewLine() + $"Too: {pbo.ServerPath}" + Helpers.NewLine() + $"Exception: {MovePBOException.Message}");
                    return false;
                };
            };
            progressBar.Value += 30;//90%
            Thread.Sleep(2000);


            progressBar.Value += 10;//100%  
            Thread.Sleep(2000);
            progressBar.Value = 0;//0% 

            //Return
            return true;

        }

        /// <summary>
        /// Starts arma3server
        /// </summary>
        /// <param name="_textBox">textBox too witch ArmaProcessID will be displayed in</param>
        /// <param name="form">mainForm where this was called from, required for title status</param>
        /// <param name="progressBar">progressBar to show current progress</param>
        /// <param name="gitPull">true if to fetch git on launch</param>
        /// <param name="button">button of witch the method is called from, required to lock button to prevent multiple launches</param>
        /// <returns>int ArmaProcessID</returns>
        private int Run(TextBox _textBox, Form form, ProgressBar progressBar, bool gitPull, Button button)
        {
            isOnline = true;

            _textBox.Invoke(new Action(() => { textBox = _textBox; }));

            if(title == "")  title = form.Text;
              
            if (IsFirstRun) 
            {
                IsFirstRun = false;

                //Write Profile and configs
                string A3ConfigPath = DLL.ConfigValues.serverSettings.ServerDirectory;
                form.Text = $"{title} | Creating Profile/Configs";
                DLL.ConfigValues.WriteFile(A3ConfigPath);
                Thread.Sleep(5000);//Timeout
                form.Text = title;

                //Update local with git
                if (gitPull) 
                {
                    if (!PullAndRandomize(form, progressBar))
                    {
                        //reset everything
                        ArmaProcessID = Die();
                        form.Text = title;
                        progressBar.Value = 0;
                        button.Text = "Start";
                        button.Enabled = true;
                        return ArmaProcessID;
                    };
                };
                
                form.Text = title;
            }

            //Prepare Process
            var serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(DLL.ConfigValues.serverSettings.ServerDirectory, (DLL.ConfigValues.serverSettings.X64Architecture ? "arma3server_x64.exe" : "arma3server.exe")),
                    Arguments = new LaunchParameters()
                    {
                        language = DLL.ConfigValues.BasicSetting.language,
                        clientMods = LaunchParameters.GetMods(DLL.ConfigValues.Pbos, PboModType.ClientMod),
                        serverMods = LaunchParameters.GetMods(DLL.ConfigValues.Pbos, PboModType.ServerMod),
                        configBasic = LaunchParameters.GetA3Config(DLL.ConfigValues, 0),
                        configServer = LaunchParameters.GetA3Config(DLL.ConfigValues, 1),
                        IP = IPAddress.Parse(DLL.ConfigValues.serverSettings.IP),
                        port = DLL.ConfigValues.serverSettings.port,
                        profiles = "A3Config",
                        name = "Server",
                        enableHT = (Environment.ProcessorCount > 4),
                        hugepages = false,
                        autoinit = (DLL.ConfigValues.serverSettings.persistent == 1)
                    }.ToString()
                },
                EnableRaisingEvents = true
            };

            //Subscribe Our Exit/Crash method
            serverProcess.Exited += OnDie;

            //Launch Process
            serverProcess.Start();

            //Unlock Start-stop Button
            button.Enabled = true; 

            //Update Vars
            ArmaProcessID = serverProcess.Id;
            textBox.Text = ArmaProcessID.ToString();
             
            Thread.Sleep(2000);//Timeout
            clearArmaCrap()
;            
            //Return ProcessID
            return ArmaProcessID;
        }

        /// <summary>
        /// Keeps arma3server running
        /// </summary>
        /// <param name="textBox">textBox too witch ArmaProcessID will be displayed in</param>
        /// <param name="runOnce">runOnce if true will not start the monitor loop</param>
        /// <param name="form">mainForm where this was called from, required for title status</param>
        /// <param name="progressBar">progressBar to show current progress</param>
        /// <param name="gitPull">true if to fetch git on launch</param>
        /// <param name="button">button of witch the method is called from, required to lock button to prevent multiple launches</param>
        /// <returns>int ArmaProcessID</returns>
        private int MonitorArma(TextBox textBox, bool runOnce, Form form, ProgressBar progressBar, bool gitPull, Button button)
        { 
            if (runOnce)
            {
                if (!DLL.HelperFunctions.IsProcessRunning(ArmaProcessID)) form.Invoke(new Action(() =>
                {
                    ArmaProcessID = Run(textBox, form, progressBar, gitPull, button);//Are we meant to be alive but ain't?
                }));
                return ArmaProcessID;
            }

            if (IsArmaMonitored) return ArmaProcessID;//exit so we dont keep creating threads
            IsArmaMonitored = true;//set as montiored

            try
            { 
                #region Monitor Thread  
                new Thread(async () =>
                {
                    Thread.CurrentThread.IsBackground = true;//Go away leave me alone lol
                    Thread.CurrentThread.Priority = ThreadPriority.Highest;//POWER!!!
                    while (IsArmaMonitored)
                    {
                        await Task.Delay(3000);//every 3 seconds give the cpu a little break 
                        if (!DLL.HelperFunctions.IsProcessRunning(ArmaProcessID) && IsArmaMonitored) form.Invoke(new Action(() =>
                        {
                            ArmaProcessID = Run(textBox, form, progressBar, gitPull, button);//Are we meant to be alive but ain't?
                        }));
                    }
                }).Start();//loop has be in a new thread or will hang application at loop
                #endregion

                return ArmaProcessID;
            }
            catch
            {
                return ArmaProcessID;
            }
        }

        /// <summary>
        /// Handles arma3server crashing/exiting without user stoping via Utilities.Die();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDie(object sender, EventArgs e) => textBox.Invoke(new Action(() =>
        {
            isOnline = false;
            ArmaProcessID = -1;//Reset our var 
            textBox.Text = Die().ToString();
        }));

        /// <summary>
        /// Stops arma3server and resets vars
        /// </summary>
        /// <returns>int ArmaProcessID</returns>
        internal int Die()
        { 
            try
            {
                IsArmaMonitored = false;//kill stayAlive Loop
                IsFirstRun = true;
                isOnline = false;
                if (ArmaProcessID == -1) return ArmaProcessID;
                Task.Delay(1000);//Wait a sec before killing Process
                Process.GetProcessById(ArmaProcessID).Kill();//Kill Arma Instance
                ArmaProcessID = -1;//Reset our var
            }
            catch (ArgumentException) // Process already exited.
            {
                return ArmaProcessID;
            }

            return ArmaProcessID;
        }

        /// <summary>
        /// Arma creates profile folder next to our exe every launch.... horrible fix for horrible arma code....
        /// </summary>
        internal void clearArmaCrap()
        { 
            string fuckingAmra = Path.Combine(Environment.CurrentDirectory, "A3Config");
            if (Directory.Exists(fuckingAmra)) Directory.Delete(Path.Combine(Environment.CurrentDirectory, "A3Config"));
        }

        /// <summary>
        /// Start/Stop arma3server instance
        /// </summary>
        /// <param name="textBox">textBox too witch ArmaProcessID will be displayed in</param>
        /// <param name="runOnce">runOnce if true will not start the monitor loop</param>
        /// <param name="form">mainForm where this was called from, required for title status</param>
        /// <param name="progressBar">progressBar to show current progress</param>
        /// <param name="gitPull">true if to fetch git on launch</param>
        /// <param name="button">button of witch the method is called from, required to lock button to prevent multiple launches</param>
        /// <returns>string ArmaProcessID</returns>
        public string SwitchOnlineState(TextBox textBox, bool runOnce, Form form, ProgressBar progressBar, bool gitPull, Button button)//Handles Online State. Returns: given int as string
        {
            if (!isOnline)
            { 
                button.Enabled = false;
                button.Text = "Stop";
                ArmaProcessID = MonitorArma(textBox, runOnce, form, progressBar, gitPull, button); 
            }
            else
            { 
                ArmaProcessID = Die();
                button.Text = "Start";
            }
    
            return ArmaProcessID.ToString();
        }
        #endregion

        #region convertP3D Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="dstPath"></param>
        /// <returns></returns>
        private static bool ConvertP3dFile(string srcPath, string dstPath = null)
        {
            Trace.WriteLine(string.Format("Reading the p3d ('{0}')...", (object)srcPath));
            P3D instance = P3D.GetInstance(srcPath);
            
            if (instance is MLOD)
            {
                Trace.WriteLine(string.Format("'{0}' is already in editable MLOD format", srcPath));
                return false;
            }
     
            ODOL odol = instance as ODOL;
            if (!(instance is ODOL))
            {
                Trace.WriteLine(string.Format("'{0}' could not be loaded.", srcPath));
                return false;
            }
            Trace.WriteLine("ODOL was loaded successfully.");

            Trace.WriteLine("Start conversion...");
            MLOD mlod = Conversion.ODOL2MLOD(odol);
            Trace.WriteLine("Conversion successful.");

            string file = Path.Combine(dstPath ?? Path.GetDirectoryName(srcPath), Path.GetFileNameWithoutExtension(srcPath) + ".p3d");
           
            Trace.WriteLine("Saving...");
            mlod.writeToFile(file + ".deBin");

            if (!File.Exists(file + ".deBin")) return false;
            
            Trace.WriteLine(string.Format("MLOD successfully saved to '{0}'", file));
            File.Delete(file);
            File.Move(file + ".deBin", file);
            return File.Exists(file);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ConvertP3D(string path) => (string.IsNullOrWhiteSpace(path)) ? false : ConvertP3dFile(path, null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool ConvertP3D() => ConvertP3D(DLL.HelperFunctions.GetP3DPathDialog());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static bool ConvertP3D(List<string> files)
        {
            foreach (var file in files)
            {  
                if (string.IsNullOrWhiteSpace(file)) return false; 
                if (!ConvertP3D(file)) return false;
            }
            return true;
        }

        /// <summary>
        /// Converts all P3DS
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ConvertALLP3D(PBOFile pbo) => ConvertP3D(Directory.EnumerateFiles(pbo.GitUrl, "*.p3d", SearchOption.AllDirectories).ToList());
        #endregion
    }

}