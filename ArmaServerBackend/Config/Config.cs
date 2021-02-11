using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArmaServerBackend
{
    public class Config
    {
        private protected static readonly string _configPath = Path.Combine(Environment.CurrentDirectory, "ArmaServer.json");

        // Load values from .json
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

        // Write values to .json
        public bool Save()
        {
            try
            {
                File.WriteAllText(_configPath, JsonConvert.SerializeObject(DLL.ConfigValues, Formatting.Indented));
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error while writing config. Invalid permissions? | Exception: " + ex);
                return false;
            }
        }

        // Write Default values to .json
        private protected Settings SaveDefaults()
        {
            DLL.ConfigValues = new SettingsDefault().Values("Mission.Altis", "Tag");
            Save();
            return DLL.ConfigValues;
        }

        // Write Json Config As String
        public void WriteBasicConfigFile(string file, ServerBasicSettings basicSetting) => File.WriteAllText(file, basicSetting.ToString());
        public void WriteServerConfigFile(string file, ServerSettings serverSettings) => File.WriteAllText(file, serverSettings.ToString());
         
        // Creates .cfg from .json values
        public bool CreateA3ConfigFile(Settings settings, int config = 0)
        {
            if (config != 0 && config != 1) return false;
            var path = Path.Combine(settings.serverSettings.ServerDirectory, "A3Config");
            var file = Path.Combine(path, (config == 0) ? "ArmaBasic.cfg" : "ArmaServer.cfg");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if(config == 0) WriteBasicConfigFile(file, settings.BasicSetting); else WriteServerConfigFile(file, settings.serverSettings);
            Thread.Sleep(2000);
            return File.Exists(file);
        }
    }
     
}
 