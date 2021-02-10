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
            string procName = (ConfigValues.Use64BitServer ? "arma3server_x64.exe" : "arma3server.exe");

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
            if (ConfigValues.KillArmaServer)
            {
                Console.WriteLine($"Stoping {procName}");
                Helpers.EndTask(procName);
            }

            //Timeout 
            Thread.Sleep(2000);

            //Create random local vars
            foreach (string varToChange in ConfigValues.ObfLocalVars)
            {
                string newVar = "_" + Helpers.RandomString(ConfigValues.RandomVarsLength);
                _localVars[varToChange] = newVar;
                File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
            }

            //Create random global vars
            foreach (string varToChange in ConfigValues.ObfGlobalVars)
            {
                string newVar = Helpers.RandomString(ConfigValues.RandomVarsLength);
                _globalVars[varToChange] = newVar;
                File.AppendAllText("ArmaServer.log", $"{varToChange} => {newVar}\r\n");
            }

            //Create random function vars
            foreach (string varToChange in ConfigValues.ObfFunctions)
            {
                string newVar = Helpers.RandomString(ConfigValues.RandomFuncsLength);
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
            Process.Start(ConfigValues.ServerDirectory + "/" + procName);

            //Return
            return true;
        }
    
    }
}