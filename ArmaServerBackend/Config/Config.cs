using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ArmaServerBackend
{
    public class Config
    {
        private protected static readonly string _configPath = Path.Combine(Environment.CurrentDirectory, "ArmaServer.json");

        //Load values from .json
        public bool Load()
        { 
            try
            {
                if (!File.Exists(_configPath))
                {
                    Console.WriteLine("Config file not found, writing a new one!");
                    DLL.ConfigValues = SaveDefaults();
                }

                DLL.ConfigValues = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_configPath));
                return (DLL.ConfigValues != null ? true : false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! An error occured while reading the config file: " + ex);
                DLL.ConfigValues = SaveDefaults();

                Environment.Exit(0);
                return false;
            }
        }

        //Write values to .json
        public bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(DLL.ConfigValues, Formatting.Indented);
                File.WriteAllText(_configPath, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while writing config. Invalid permissions?");
                return false;
            }
        }

        //Write Default values to .json
        private Settings SaveDefaults()
        {
            DLL.ConfigValues = new Settings() {
                ServerDirectory = "C:\\Arma3",
                GitDirectory = "C:\\Github",
                FunctionsTag = "FSCQC",
                RandomFuncsLength = 8,
                RandomVarsLength = 8,
                KillArmaServer = false,
                Use64BitServer = true,
                ObfLocalVars = new List<string>(),
                ObfGlobalVars = new List<string>(),
                ObfFunctions = new List<string>(),
                Pbos = new List<PboFiles>() {
                    new PboFiles
                    {
                        Name = "Mission.Altis",
                        GitPath = "repo-main",
                        GitUrl = "https://github.com/user/repo/archive/master.zip",
                        GitToken = "xxxxx",
                        GitType = 1,
                        ServerPath = "C:\\Arma3\\mpMissions\\mission.pbo",
                        OneLine = false,
                        RenameFuncs = false,
                        RenameGlobalVars = false,
                        RenameLocalVars = false
                    },
                    new PboFiles
                    {
                        Name = "server_functions",
                        GitPath = "repo-main",
                        GitUrl = "https://github.com/user/repo/archive/master.zip",
                        GitToken = "xxxxx",
                        GitType = 1,
                        ServerPath = "C:\\Arma3\\@ArmaServerCQCServer\\addons\\server_functions.pbo",
                        OneLine = false,
                        RenameFuncs = false,
                        RenameGlobalVars = false,
                        RenameLocalVars = false
                    },
                    new PboFiles
                    {
                        Name = "client_functions",
                        GitPath = "repo-main",
                        GitUrl = "https://github.com/user/repo/archive/master.zip",
                        GitToken = "xxxxx",
                        GitType = 1,
                        ServerPath = "C:\\Arma3\\@ArmaServerCQCServerr\\addons\\client_functions.pbo",
                        OneLine = false,
                        RenameFuncs = false,
                        RenameGlobalVars = false,
                        RenameLocalVars = false
                    }
                }
            };

            Save();

            return DLL.ConfigValues;
        }
    }
}
 