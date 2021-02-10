using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ArmaServerBackend
{
    public class DLL
    {
        //Create new instances that can be accsesed globaly
        public static Settings ConfigValues = null;
        public static Config ConfigFunctions = new Config();
        public static Utilities UtilityFunctions = new Utilities();
        public static Helpers HelperFunctions = new Helpers();
        public static Embedded AssemblyFunctions = new Embedded();

        //List array of vars to obfuscate
        internal protected static Dictionary<string, string> _localVars = new Dictionary<string, string>();
        internal protected static Dictionary<string, string> _globalVars = new Dictionary<string, string>();
        internal protected static Dictionary<string, string> _scriptFuncs = new Dictionary<string, string>();

        //Subscibe AssemblyResolve to resolve embedded assemblies
        public DLL() => AppDomain.CurrentDomain.AssemblyResolve += AssemblyFunctions.AssemblyResolver;
          
        //Download, Randomize & Pack everything
        private string title = "";
        public bool LaunchServer(Form form ,ProgressBar progressBar, bool gitPull = true)
        {
            if(title != "") title = form.Text;
            string procName = (ConfigValues.serverSettings.X64Architecture ? "arma3server_x64.exe" : "arma3server.exe");
             
            //Kill arma3 process
            form.Text = $"{title} | Stoping {procName}";
            Console.WriteLine($"Stoping {procName}");
            Helpers.EndTask(procName);
             
            //Timeout 
            Thread.Sleep(3000); 
            foreach (int index in new List<int> {0,1})
            {
                form.Text = $"{title} | Creating {(index == 0 ? "Basic" : "Server")} Config";
                ConfigFunctions.CreateA3ConfigFile(ConfigValues, index);
            }
            Thread.Sleep(1000);
            
            //Update local with git
            if (gitPull) PullAndRandomize(form, progressBar);
              
            //Start Arma3 server 
            form.Text = $"{title} | Starting {procName}";
            MessageBox.Show($"Starting {procName}"); 
            Process.Start(ConfigValues.serverSettings.ServerDirectory + "/" + procName);

            Thread.Sleep(2000);
            form.Text = $"{title} | {procName} Running";

            //Return
            return true;
            
        }
        
        public bool PullAndRandomize(Form form, ProgressBar progressBar)
        {  
            progressBar.Value = 0;//0%
             
            //purge directory if exists and create new
            try
            {
                if (Directory.Exists(ConfigValues.GitDirectory)) Directory.Delete(ConfigValues.GitDirectory, true);
                Directory.CreateDirectory(ConfigValues.GitDirectory);
                form.Text = $"{title} | Purging local git directory";
                progressBar.Value += 10;//10% 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid git directory provided! Config is invalid?");
                Utilities.Assert(ex);
            } 

            //Timeout 
            Thread.Sleep(2000);
            progressBar.Value += 10;//20%  
            Thread.Sleep(1000);

            //Create random local vars
            if (ConfigValues.LocalVaribales.Count > 0)
            {
                form.Text = $"{title} | Creating Random Local Variables";
                foreach (string varToChange in ConfigValues.LocalVaribales)
                {
                    string newVar = "_" + HelperFunctions.RandomVariable(ConfigValues.RandomVariablesLength);
                    _localVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 20;//40%

            //Create random global vars
            if (ConfigValues.GlobalVariables.Count > 0)
            {
                form.Text = $"{title} | Creating Random Global Variables";
                foreach (string varToChange in ConfigValues.GlobalVariables)
                {
                    string newVar = HelperFunctions.RandomVariable(ConfigValues.RandomVariablesLength);
                    _globalVars[varToChange] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 10;//50%

            //Create random function vars
            if (ConfigValues.Functions.Count > 0)
            {
                form.Text = $"{title} | Creating Random Functions";
                foreach (string varToChange in ConfigValues.Functions)
                {
                    string newVar = HelperFunctions.RandomVariable(ConfigValues.RandomFunctionsLength);
                    _scriptFuncs[varToChange.Replace(ConfigValues.FunctionsTag + "_fnc_", "")] = newVar;
                    File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
                }
            }
            progressBar.Value += 10;//60%

            //Download Folders, change all vars to random vars, Pack and move all pbos
            foreach (PboFiles pbo in ConfigValues.Pbos)
            {
                if (!pbo.IsEnabled) continue;
                form.Text = $"{title} | Downloading {pbo.Name} from git: {pbo.GitUrl}";
                Utilities.Assert(Helpers.GitDownload(pbo), $"Downloading from git: {pbo.Name}/{pbo.GitUrl}");
                Thread.Sleep(2000);
                form.Text = $"{title} | Changing {pbo.Name} functions\variables too Randomized functions\variable";
                Helpers.RandomizeEverything(pbo);
                Thread.Sleep(1000);
                form.Text = $"{title} | Packing {pbo.Name}";
                Helpers.Pack(pbo);
                Utilities.Assert(Helpers.Move(pbo), "Failed to move files");
            }
            progressBar.Value += 30;//90%
            Thread.Sleep(2000);

         
            progressBar.Value += 10;//100%  
            Thread.Sleep(2000);
            progressBar.Value = 0;//0% 

            //Return
            return true;

        }

    }
}