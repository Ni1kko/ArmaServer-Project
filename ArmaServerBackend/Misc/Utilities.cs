using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmaServerBackend
{
    public class Utilities
    { 
        public int ArmaProcessID = -1;
        public bool IsArmaMonitored = false;
        public bool IsFirstRun = true;
        private TextBox textBox = null;
        private string title = "";

        public bool PullAndRandomize(Form form, ProgressBar progressBar)
        {
            progressBar.Value = 0;//0%

            //purge directory if exists and create new
            try
            {
                if (Directory.Exists(DLL.ConfigValues.GitDirectory)) Helpers.DeleteDirectory(DLL.ConfigValues.GitDirectory);
                Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);
                form.Text = $"{title} | Purging local git directory";
                progressBar.Value += 10;//10% 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid git directory provided! Config is invalid?");
                Assert(ex);
            }

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
                    string newVar = "_" + DLL.HelperFunctions.RandomVariable(DLL.ConfigValues.RandomVariablesLength);
                    DLL._localVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 20;//40%

            //Create random global vars
            if (DLL.ConfigValues.GlobalVariables.Count > 0)
            {
                form.Text = $"{title} | Creating Random Global Variables";
                foreach (string varToChange in DLL.ConfigValues.GlobalVariables)
                {
                    string newVar = DLL.HelperFunctions.RandomVariable(DLL.ConfigValues.RandomVariablesLength);
                    DLL._globalVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 10;//50%

            //Create random function vars
            if (DLL.ConfigValues.Functions.Count > 0)
            {
                form.Text = $"{title} | Creating Random Functions";
                foreach (string varToChange in DLL.ConfigValues.Functions)
                {
                    string newVar = DLL.HelperFunctions.RandomVariable(DLL.ConfigValues.RandomFunctionsLength);
                    DLL._scriptFuncs[varToChange.Replace(DLL.ConfigValues.FunctionsTag + "_fnc_", "")] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 10;//60%

            //Download Folders, change all vars to random vars, Pack and move all pbos
            foreach (PboFiles pbo in DLL.ConfigValues.Pbos)
            {
                if (!pbo.IsEnabled) continue;
                form.Text = $"{title} | Downloading {pbo.Name} from git: {pbo.GitUrl}";
                Assert(Helpers.GitDownload(pbo), $"Downloading from git: {pbo.Name}/{pbo.GitUrl}");
                Thread.Sleep(2000);
                form.Text = $"{title} | Changing {pbo.Name} functions\variables too Randomized functions\variable";
                Helpers.RandomizeEverything(pbo);
                Thread.Sleep(1000);
                form.Text = $"{title} | Packing {pbo.Name}";
                Helpers.Pack(pbo);
                Assert(Helpers.Move(pbo), "Failed to move files");
            }
            progressBar.Value += 30;//90%
            Thread.Sleep(2000);


            progressBar.Value += 10;//100%  
            Thread.Sleep(2000);
            progressBar.Value = 0;//0% 
            form.Text = title;

            //Return
            return true;

        }
         
        public bool IsProcessRunning(int procID)
        {
            try { Process.GetProcessById(procID); }
            catch (InvalidOperationException) { return false; }
            catch (ArgumentException) { return false; }
            return true;
        }
        private int Run(TextBox _textBox, Form form, ProgressBar progressBar, bool gitPull, Button button)
        {
            _textBox.Invoke(new Action(() => { textBox = _textBox; }));

             if(title == "")  title = form.Text;
              
            if (IsFirstRun) 
            {
                IsFirstRun = false;
                foreach (int index in new List<int> { 0, 1 })
                {
                    form.Text = $"{title} | Creating {(index == 0 ? "Basic" : "Server")} Config";
                    DLL.ConfigFunctions.CreateA3ConfigFile(DLL.ConfigValues, index);
                }
                Thread.Sleep(5000);//Timeout

                //Update local with git
                if (gitPull) PullAndRandomize(form, progressBar);
            }

            string serverExe = Path.Combine(DLL.ConfigValues.serverSettings.ServerDirectory, (DLL.ConfigValues.serverSettings.X64Architecture ? "arma3server_x64.exe" : "arma3server.exe"));
            
            //Added ToString() Override
            LaunchParameters launchParameters = new LaunchParameters()
            {
                clientMods = DLL.ConfigFunctions.GetMods(DLL.ConfigValues.Pbos, PboModType.ClientMod),
                serverMods = DLL.ConfigFunctions.GetMods(DLL.ConfigValues.Pbos, PboModType.ServerMod),
                configBasic = DLL.ConfigFunctions.GetA3Config(DLL.ConfigValues, 0),
                configServer = DLL.ConfigFunctions.GetA3Config(DLL.ConfigValues, 1),
                port = DLL.ConfigValues.serverSettings.port,
                profiles = "A3Config",
                name = "Server",
                enableHT = (System.Environment.ProcessorCount > 4),
                hugepages = false,
                autoinit = (DLL.ConfigValues.serverSettings.persistent == 1)
            };
             
            //Prepare Process
            var serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = serverExe,
                    Arguments = launchParameters.ToString()
                },
                EnableRaisingEvents = true
            };

            //Subscribe Our Function To Exit Event Of Process
            serverProcess.Exited += OnDie;

            //Launch Process
            serverProcess.Start();

            //Unlock Button
            button.Enabled = true; 

            //Update Vars
            ArmaProcessID = serverProcess.Id;
            textBox.Text = ArmaProcessID.ToString();

            //Return ProcessID
            return ArmaProcessID;
        } 
        private void OnDie(object sender, EventArgs e) => textBox.Invoke(new Action(() =>
        {
            textBox.Text = /*Run().ToString()*/ "0";//was going to use the event for restarting but i decided a threaded async loop will be better since i have no idea if this event is reliable
        })); 
        
        private int MonitorArma(TextBox textBox, bool runOnce, Form form, ProgressBar progressBar, bool gitPull, Button button)
        {
            if (runOnce)
            {
                if (!IsProcessRunning(ArmaProcessID)) form.Invoke(new Action(() =>
                {
                    ArmaProcessID = Run(textBox, form, progressBar, gitPull, button);//Are we meant to be alive but ain't?
                }));
                return ArmaProcessID;
            }

            if (IsArmaMonitored) return ArmaProcessID;

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
                        if (!IsProcessRunning(ArmaProcessID) && IsArmaMonitored) form.Invoke(new Action(() =>
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
        private int Die(TextBox textBox)
        {
            if (ArmaProcessID == 0 || ArmaProcessID == -1) return ArmaProcessID;

            try
            {
                IsArmaMonitored = false;//kill stayAlive Loop
                IsFirstRun = true;
                Task.Delay(1000);//Wait a sec before killing Process

                Process.GetProcessById(ArmaProcessID).Kill();//Kill Arma Instance
                ArmaProcessID = -1;//Reset our var 
                textBox.Text = ArmaProcessID.ToString();
            }
            catch (ArgumentException) // Process already exited.
            {
                return 0;
            }

            return ArmaProcessID;
        }
        public string SwitchOnlineState(TextBox textBox, bool run, bool runOnce, Form form, ProgressBar progressBar, bool gitPull, Button button)//Handles Online State. Returns: given int as string
        {  
            int procID = run ? MonitorArma(textBox, runOnce, form, progressBar, gitPull, button) : Die(textBox);
            ArmaProcessID = procID;//set id
            return procID.ToString();
        }
        
        
        internal static void Assert(bool val, string msg = "")
        {
            if (!val)
            {
                Console.WriteLine("Fatal error occured!");
                if (msg != "")  Console.WriteLine("Error: " + msg);
                Environment.Exit(1);
            }
        }
        internal static void Assert(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine("Fatal error occured!");
                Console.WriteLine("Error: " + ex);
                Environment.Exit(1);
            }
        }
    }
}