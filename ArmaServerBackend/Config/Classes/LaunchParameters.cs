namespace ArmaServerBackend
{
    public class LaunchParameters
    {
        /// <summary>
        /// Loads the specified sub-folders for different mods. Separated by semi-colons. Absolute path and multiple stacked folders are possible.
        /// </summary>
        public string clientMods { get; set; }
        
        /// <summary>
        /// Loads the specified sub-folders for different server-side (not broadcasted to clients) mods. Separated by semi-colons. Absolute path and multiple stacked folders are possible. 
        /// </summary>
        public string serverMods { get; set; }
        
        /// <summary>
        /// Selects the Server Basic Config file. Config file for server specific settings like network performance tuning. 
        /// </summary>
        public string configBasic { get; set; }

        /// <summary>
        /// Selects the Server Config File. Config file for server specific settings like admin password and mission selection. 
        /// </summary>
        public string configServer { get; set; }
        
        /// <summary>
        /// Port to have dedicated server listen on. 
        /// </summary>
        public int port { get; set; }
        
        /// <summary>
        /// string containing server profiles path
        /// </summary>
        public string profiles { get; set; }
        
        /// <summary>
        /// Location of user-profile folder. If a custom path is set, default files and folders (logFiles, AnimDataCache, DataCache, SteamPreviewCache....) located by default in %localappdata%/Arma 3 will be created in that new location. However, old files will stay in the old location and will not be copied. 
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Enables the use of all logical CPU cores for parallel tasks processing. If your CPU does not support Hyper-Threading or similar technology, this parameter is ignored. When disabled, only physical cores are used.
        /// </summary>
        public bool enableHT { get; set; }

        /// <summary>
        /// Enables hugepages with the default memory allocator (malloc) for both client/server 
        /// </summary>
        public bool hugepages { get; set; }

        /// <summary>
        /// Automatically initialize mission just like first client does
        /// </summary>
        public bool autoinit { get; set; }

        /// <summary>
        /// Convert Parameters to user friendly string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() 
        {
            //Start cmd line string
            string parameters = "";

            //Add port
            parameters += " -port=" + port.ToString();
            
            //Add profiles
            parameters += " -profiles=" + profiles;
            parameters += " -name=" + name.Replace(" ", "");

            //Add .cfgs to cmd line
            parameters += " -cfg=" + configBasic;
            parameters += " -config=" + configServer;

            //Add @mods to cmd line
            if (serverMods != "") parameters += " -servermod=" + serverMods;
            if (clientMods != "") parameters += " -mod=" + clientMods;

            //Add other parmas to cmd line
            if (enableHT) parameters += " -enableHT";
            if (hugepages) parameters += " -hugepages";

            //Add persistent to cmd line
            if (autoinit) parameters += " -autoinit";

            System.Console.WriteLine($"Launch Params: {parameters}");
             
            return parameters;
        }
    }
} 