using System.Collections.Generic;

namespace ArmaServerBackend
{
    public class Settings
    {  
        public string GitDirectory { get; set; }
        public ServerBasicSettings BasicSetting { get; set; }
        public ServerSettings serverSettings { get; set; }
        public List<PboFiles> Pbos { get; set; }
        public string FunctionsTag { get; set; }
        public List<string> Functions { get; set; }
        public List<string> GlobalVariables { get; set; }
        public List<string> LocalVaribales { get; set; }
        public int RandomFunctionsLength { get; set; }
        public int RandomVariablesLength { get; set; }
    }
    public class SettingsDefault
    {
        private readonly string serverDirectory = @"C:\Arma3";
        public Settings Values(string missionName, string functionTag) => new Settings()
        { 
            GitDirectory = @"C:\Github",
            BasicSetting = new ServerBasicSettingDefault().Values(),
            serverSettings = new ServerSettingsDefault().Values(serverDirectory,"Some Mission Name", missionName),
            Pbos = new List<PboFiles>() {
                new PboFilesDefault().Values(missionName, $"{serverDirectory}\\mpmissions"),
                //new PboFilesDefault().Values("client_functions", "C:\\Arma3\\@ArmaServerCQCServer\\addons\\client_functions.pbo"),
                //new PboFilesDefault().Values("server_functions", "C:\\Arma3\\@ArmaServerCQCServer\\addons\\server_functions.pbo")
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
