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
        public bool LaunchServer()
        {
            string procName = (ConfigValues.serverSettings.X64Architecture ? "arma3server_x64.exe" : "arma3server.exe");

            //purge directory if exists and create new
            try
            {
                if (Directory.Exists(ConfigValues.GitDirectory)) Directory.Delete(ConfigValues.GitDirectory, true);
                Directory.CreateDirectory(ConfigValues.GitDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid git directory provided! Config is invalid?");
                Utilities.Assert(ex);
            }

            //Kill arma3 process
            Console.WriteLine($"Stoping {procName}");
            Helpers.EndTask(procName);
             

            //Timeout 
            Thread.Sleep(2000);

            //Create Configs
            //if (ConfigFunctions.CreateA3ConfigFile(ConfigValues, 0) && ConfigFunctions.CreateA3ConfigFile(ConfigValues, 1))
            
            foreach (int index in new List<int> {0,1})
            {
                ConfigFunctions.CreateA3ConfigFile(ConfigValues, index);
            }

            //Create random local vars
            foreach (string varToChange in ConfigValues.LocalVaribales)
            {
                string newVar = "_" + HelperFunctions.RandomVariable(ConfigValues.RandomVariablesLength);
                _localVars[varToChange] = newVar;
                File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
            }

            //Create random global vars
            foreach (string varToChange in ConfigValues.GlobalVariables)
            {
                string newVar = HelperFunctions.RandomVariable(ConfigValues.RandomVariablesLength);
                _globalVars[varToChange] = newVar;
                File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
            }

            //Create random function vars
            foreach (string varToChange in ConfigValues.Functions)
            {
                string newVar = HelperFunctions.RandomVariable(ConfigValues.RandomFunctionsLength);
                _scriptFuncs[varToChange.Replace(ConfigValues.FunctionsTag + "_fnc_", "")] = newVar;
                File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
            }

            //Download Folders, change all vars to random vars, Pack and move all pbos
            foreach (PboFiles pbo in ConfigValues.Pbos)
            {
                Utilities.Assert(Helpers.GitDownload(pbo), $"Downloading from git: {pbo.Name}/{pbo.GitUrl}");
                Helpers.RandomizeEverything(pbo);
                Helpers.Pack(pbo);
                Utilities.Assert(Helpers.Move(pbo), "Failed to move files");
            }

            //Start Arma3 server 
            MessageBox.Show($"Starting {procName}");
            Process.Start(ConfigValues.serverSettings.ServerDirectory + "/" + procName);

            //Return
            return true;
            
        }
    }
}