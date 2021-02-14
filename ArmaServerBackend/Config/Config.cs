using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace ArmaServerBackend
{
    /// <summary>
    /// Main methods for working with config system
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Path to output app config
        /// </summary>
        private protected readonly string _configPath = Path.Combine(Environment.CurrentDirectory, "ArmaServer.json");

        /// <summary>
        /// Load values from .json
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Write values to .json
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Write Default values to .json
        /// </summary>
        /// <returns></returns>
        private protected Settings SaveDefaults()
        {
            DLL.ConfigValues = new SettingsDefault().Values("Mission.Altis", "Tag");
            Save();
            return DLL.ConfigValues;
        }

        /// <summary>
        /// Write Json Config As String
        /// </summary>
        /// <param name="file"></param>
        /// <param name="basicSetting"></param>
        public void WriteBasicConfigFile(string file, ServerBasicSettings basicSetting) => File.WriteAllText(file, basicSetting.ToString());
        public void WriteServerConfigFile(string file, ServerSettings serverSettings) => File.WriteAllText(file, serverSettings.ToString());
        private void WriteServerProfileFile(string file, DifficultySetting difficultySetting) => File.WriteAllText(file, difficultySetting.ToString());

        /// <summary>
        /// Creates .cfg from .json values
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="config"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates .Arma3Profile from .json values
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool CreateA3ProfileFile(Settings settings)
        {
            var path = Path.Combine(settings.serverSettings.ServerDirectory, "A3Config", "Users", "Server"); 
            var file = Path.Combine(path, "Server.Arma3Profile");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            WriteServerProfileFile(file, settings.serverSettings.difficultySetting);
            Thread.Sleep(2000);
            return File.Exists(file);
        }

        /// <summary>
        /// Get Index of configList from given configName
        /// </summary>
        /// <param name="configSettings">list of configs</param>
        /// <param name="configName">name of config</param>
        /// <returns></returns>
        public int GetConfigSettingIndex(List<ConfigSetting> configSettings, string configName)
        {
            int index = 0;
            foreach (var item in configSettings)
            {
                if (item.Name == configName) return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Change specific value in configList from given index
        /// </summary>
        /// <param name="configSettings">list of configs</param>
        /// <param name="index">pos in configList</param>
        /// <param name="newValue">new value for config</param>
        /// <returns></returns>
        public List<ConfigSetting> ModifyConfigSetting(List<ConfigSetting> configSettings, int index, object newValue)
        {
            if (index != -1) configSettings[index].Value = newValue;
            return configSettings;
        }

        /// <summary>
        /// Change specific value in configList from given configName
        /// </summary>
        /// <param name="configSettings">list of configs</param>
        /// <param name="configName">name of config</param>
        /// <param name="newValue">new value for config</param>
        /// <returns></returns>
        public List<ConfigSetting> ModifyConfigSetting(List<ConfigSetting> configSettings, string configName, object newValue) => ModifyConfigSetting(configSettings, GetConfigSettingIndex(configSettings, configName), newValue);
  
    }
}
 