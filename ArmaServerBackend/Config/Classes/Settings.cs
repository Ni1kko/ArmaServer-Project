﻿using System.Collections.Generic;

namespace ArmaServerBackend
{
    /// <summary>
    /// Main application setting
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Local directory path where downloaded pbo will be stored
        /// </summary>
        public string GitDirectory { get; set; }
        
        /// <summary>
        /// BasicSetting for arma3server 
        /// </summary>
        public ServerBasicSettings BasicSetting { get; set; }
        
        /// <summary>
        /// serverSettings for arma3server 
        /// </summary>
        public ServerSettings serverSettings { get; set; }
        
        /// <summary>
        /// List of PBOS
        /// </summary>
        public List<PBOFile> Pbos { get; set; }
        
        /// <summary>
        /// Script FunctionsTag used when randomizing functions
        /// </summary>
        public string FunctionsTag { get; set; }
        
        /// <summary>
        /// List of functions to randomize
        /// </summary>
        public List<string> Functions { get; set; }
        
        /// <summary>
        /// List of global variables to randomize
        /// </summary>
        public List<string> GlobalVariables { get; set; }
        
        /// <summary>
        /// List of loacl variables to randomize
        /// </summary>
        public List<string> LocalVaribales { get; set; }
        
        /// <summary>
        /// Length of random function to create
        /// </summary>
        public int RandomFunctionsLength { get; set; }
        
        /// <summary>
        /// Length of random variables to create
        /// </summary>
        public int RandomVariablesLength { get; set; }
    }
    public class SettingsDefault
    {
        /// <summary>
        /// Default Directory
        /// </summary>
        public readonly string serverDirectory = @"C:\Arma3", gitDirectory = @"C:\Github";

        /// <summary>
        /// Creates default config
        /// </summary>
        /// <param name="PBOName">Name of pbo</param>
        /// <param name="functionTag">Script TAG</param>
        /// <returns>Settings</returns>
        public Settings Values(string PBOName, string functionTag) => new Settings()
        { 
            GitDirectory = @"C:\Github",
            BasicSetting = new ServerBasicSettingDefault().Values(),
            serverSettings = new ServerSettingsDefault().Values(serverDirectory,"Some Mission Name"),
            Pbos = new List<PBOFile>() {
                new PboFilesDefault().Values(PBOName, $"{serverDirectory}\\mpmissions", PboModType.Mission),
                //new PboFilesDefault().Values("client_functions", $"{serverDirectory}\\addons", PboModType.ClientMod),
                //new PboFilesDefault().Values("server_functions", $"{serverDirectory}\\addons", PboModType.ServerMod)
            },
            FunctionsTag = functionTag,
            Functions = new List<string>(),
            GlobalVariables = new List<string>(),
            LocalVaribales = new List<string>(),
            RandomFunctionsLength = 8,
            RandomVariablesLength = 8,
        };
    }
}
