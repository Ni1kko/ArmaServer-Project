using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ArmaServerBackend
{
    /// <summary>
    /// Main application setting
    /// </summary>
    public class Settings : Helpers
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
        /// Server Profile Settings
        /// </summary>
        public ServerProfile serverProfile { get; set; }

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

        /// <summary>
        /// Creates .cfg from .json values
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool CreateA3ConfigFile(string path, Settings settings, int config = 0)
        {
            if (config != 0 && config != 1) return false;
            path =Path.Combine(path, "A3Config");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, (config == 0) ? "ArmaBasic.cfg" : "ArmaServer.cfg");
            if (config == 0)
                settings.BasicSetting.WriteFile(path);
            else
                settings.serverSettings.WriteFile(path);

            Thread.Sleep(1000);
            return File.Exists(path);
        }

        /// <summary>
        /// Creates .Arma3Profile from .json values
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool CreateA3ProfileFile(string path, Settings settings)
        {
            path = Path.Combine(path, "A3Config", "Users", "Server");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, "Server.Arma3Profile");
            settings.serverProfile.WriteFile(path);
            Thread.Sleep(1000);
            return File.Exists(path);
        }

        /// <summary>
        /// Write Config
        /// </summary>
        /// <param name="file"></param>
        internal override void WriteFile(string path, string _ = null)
        {
            foreach (int index in new List<int> { 0, 1 }) if(!CreateA3ConfigFile(path, DLL.ConfigValues, index)) return;
            if (!CreateA3ProfileFile(path, DLL.ConfigValues)) return;
            Thread.Sleep(5000);
        }
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
            serverProfile = new ServerProfile()
            {
                SkillAI = 0.75m,
                PrecisionAI = 0.55m,
                AILevelPreset = 3,
                DifficultyItems = new List<ConfigSetting>()
                {
                    new ConfigSetting("reducedDamage", 0),
                    new ConfigSetting("groupIndicators", 1),
                    new ConfigSetting("friendlyTags", 1),
                    new ConfigSetting("enemyTags", 1),
                    new ConfigSetting("detectedMines", 1),
                    new ConfigSetting("commands", 1),
                    new ConfigSetting("waypoints", 1),
                    new ConfigSetting("weaponInfo", 1),
                    new ConfigSetting("stanceIndicator", 1),
                    new ConfigSetting("staminaBar", 1),
                    new ConfigSetting("weaponCrosshair", 1),
                    new ConfigSetting("visionAid", 1),
                    new ConfigSetting("thirdPersonView", 1),
                    new ConfigSetting("cameraShake", 1),
                    new ConfigSetting("scoreTable", 1),
                    new ConfigSetting("deathMessages", 1),
                    new ConfigSetting("vonID", 1),
                    new ConfigSetting("mapContent", 1),
                    new ConfigSetting("autoReport", 1),
                    new ConfigSetting("multipleSaves", 1)
                }
            },
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
