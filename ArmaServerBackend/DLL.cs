using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
        public DLL() => AppDomain.CurrentDomain.AssemblyResolve += DLL.AssemblyFunctions.AssemblyResolver;
          
        //Download, Randomize & Pack everything
        public bool PackServer()
        {
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
                Console.WriteLine("Ending Arma3Server processes");

                if (ConfigValues.Use64BitServer)
                    Helpers.EndTask("arma3server_x64.exe");
                else
                    Helpers.EndTask("arma3server.exe");
            }

            //Timeout 
            Thread.Sleep(2000);

            //Create random local vars
            foreach (string s in ConfigValues.ObfLocalVars)
            {
                string newVar = "_" + Utilities.RandomString(ConfigValues.RandomVarsLength);
                _localVars[s] = newVar;
                File.AppendAllText("variables.log", $"{s} => {newVar}\r\n");
            }

            //Create random global vars
            foreach (string s in ConfigValues.ObfGlobalVars)
            {
                string newVar = Utilities.RandomString(ConfigValues.RandomVarsLength);
                _globalVars[s] = newVar;
                File.AppendAllText("variables.log", $"{s} => {newVar}\r\n");
            }

            //Create random function vars
            foreach (string s in ConfigValues.ObfFunctions)
            {
                string newFnc = Utilities.RandomString(ConfigValues.RandomFuncsLength);
                string fnc = s.Replace(ConfigValues.FunctionsTag + "_fnc_", "");
                _scriptFuncs[fnc] = newFnc;
                File.AppendAllText("variables.log", $"{s} => {newFnc}\r\n");
            }

            //Download Folders and change all vars to random vars
            foreach (PboFiles sm in ConfigValues.Pbos)
            {
                Utilities.Assert(Utilities.GitDownload(sm), $"Downloading from git: {sm.Name}/{sm.GitUrl}");

                Helpers.RandomizeEverything(sm);
                Helpers.Pack(sm);
            }

            //Obfuscate all pbos
            foreach (PboFiles sm in ConfigValues.Pbos)
            {
                if (Helpers.ObfuProcs.Count > 0)
                {
                    Console.WriteLine("Waiting for ObfuSQF to finish...");

                    while (Helpers.ObfuProcs.Count > 0)
                    {
                        List<Process> ps = new List<Process>(Helpers.ObfuProcs);

                        foreach (Process p in ps) if (p.HasExited && Helpers.ObfuProcs.Contains(p)) Helpers.ObfuProcs.Remove(p);

                        Thread.Sleep(100);
                    }

                    Console.WriteLine("ObfuSQF finished.");
                }

                Utilities.Assert(Helpers.Move(sm), "Failed to move files");
            }

            //Start Arma3 server
            Process.Start(ConfigValues.ServerDirectory + "/" + (ConfigValues.Use64BitServer ? "arma3server_x64.exe" : "arma3server.exe"));

            //Return
            return true;
        }
    
    }
}