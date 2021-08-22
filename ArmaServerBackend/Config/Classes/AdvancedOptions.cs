using System.Collections.Generic;

namespace ArmaServerBackend
{
    /// <summary>
    /// https://community.bistudio.com/wiki/server.cfg
    /// </summary>
    public class AdvancedOptions : Helpers
    {
        public List<ConfigSetting> configs { get; set; }

        /// <summary>
        /// Converts Advanced Options to user friendly string
        /// </summary> 
        /// <returns>string</returns> 
        public override string ToString()
        {
            string options = "class AdvancedOptions {" + NewLine();
            if (configs.Count > 0) foreach (var config in configs) options += NewTab() + config.ToString() + NewLine();
            return options + "}";
        }
    }
}
