namespace ArmaServerBackend
{
    /// <summary>
    /// https://community.bistudio.com/wiki/server.cfg
    /// </summary>
    public class AdvancedOptions
    {
        /// <summary>
        /// LogObjectNotFound - False to skip logging "Server: Object not found" messages
        /// </summary>
        public bool LogObjectNotFound { get; set; }

        /// <summary>
        /// SkipDescriptionParsing - True to skip parsing of description.ext/mission.sqm. 
        /// Will show pbo filename instead of configured missionName. 
        /// OverviewText and such won't work, but loading the mission list is a lot faster when you have many missions
        /// </summary>
        public bool SkipDescriptionParsing { get; set; }

        /// <summary>
        /// ignoreMissionLoadErrors - When server log gets filled with too many logs entries the mission loading will be aborted and jump bad to mission selection, 
        /// this forces the server to continue loading mission
        /// </summary>
        public bool ignoreMissionLoadErrors { get; set; }
    }
}
