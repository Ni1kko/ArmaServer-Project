using System.Collections.Generic;

namespace ArmaServerBackend
{
    /// <summary>
    /// https://community.bistudio.com/wiki/server.cfg
    /// </summary>
    public class AdvancedOptions
    {
        public List<ConfigSetting> configs { get; set; }

        /// <summary>
        /// Converts Advanced Options to user friendly string
        /// </summary> 
        /// <returns>string</returns> 
        public override string ToString()
        {
            string options = "";
            foreach (var config in configs) options += Helpers.NewTab() + config.ToString() + Helpers.NewLine();
            if (options == "") options = Helpers.NewLine();
            return options;
        }
    }
}
