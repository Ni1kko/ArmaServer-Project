namespace ArmaServerBackend
{ 
    /// <summary>
    /// PBO Settings
    /// </summary>
    public class PBOFile : Helpers
    {
        /// <summary>
        /// PBO type [mission, client, server]
        /// </summary>
        public PboModType ModType { get; set; }

        /// <summary>
        /// Use PBO
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// PBO Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Git branch
        /// </summary>
        public string GitBranch { get; set; }

        /// <summary>
        /// Git URL
        /// </summary>
        public string GitUrl { get; set; }

        /// <summary>
        /// Git token
        /// </summary>
        public string GitToken { get; set; }

        /// <summary>
        /// Git server type
        /// </summary>
        public GitServer GitServer { get; set; }

        /// <summary>
        /// Path to arma3 server root
        /// </summary>
        public string ServerPath { get; set; }

        /// <summary>
        /// Mission Difficulty [recruit, regular, veteran, custom]
        /// </summary>
        public MissionDifficulty MissionDifficulty { get; set; }

        /// <summary>
        /// Randomize PBO Functions
        /// </summary>
        public bool RandomizeFunctions { get; set; }

        /// <summary>
        /// Randomize PBO Global Variables
        /// </summary>
        public bool RandomizeGlobalVariables { get; set; }

        /// <summary>
        /// Randomize PBO Local Variables
        /// </summary>
        public bool RandomizeLocalVariables { get; set; }

        /// <summary>
        /// SingleLine .sqf file in PBO
        /// </summary>
        public bool SingleLineFunctions { get; set; }
    }

    /// <summary>
    /// PBO Defualt Settings
    /// </summary>
    public class PboFilesDefault 
    {
        /// <summary>
        /// Creates default config with given params
        /// </summary>
        /// <param name="_Name"></param>
        /// <param name="_ServerPath"></param>
        /// <param name="_ModType"></param>
        /// <param name="_IsEnabled"></param>
        /// <param name="_GitBranch"></param>
        /// <param name="_GitUrl"></param>
        /// <param name="_GitToken"></param>
        /// <param name="_GitServer"></param>
        /// <param name="_MissionDifficulty"></param>
        /// <param name="_RandomizeFunctions"></param>
        /// <param name="_RandomizeGlobalVariables"></param>
        /// <param name="_RandomizeLocalVariables"></param>
        /// <param name="_SingleLineFunction"></param>
        /// <returns>new PboFile()</returns>
        public PBOFile Values(string _Name = "NewAddon", string _ServerPath = "c:\\Arma3\\@Server", PboModType _ModType = PboModType.ServerMod, bool _IsEnabled = true, string _GitBranch = "repo-main", string _GitUrl = "https://github.com/user/repo/archive/master.zip", string _GitToken = "xxxxx", GitServer _GitServer = GitServer.GitHub, MissionDifficulty _MissionDifficulty = MissionDifficulty.recruit,bool _RandomizeFunctions = false, bool _RandomizeGlobalVariables = false, bool _RandomizeLocalVariables = false, bool _SingleLineFunction = false) => new PBOFile() {
            ModType = _ModType,
            IsEnabled = _IsEnabled, 
            Name = _Name,
            GitBranch = _GitBranch,
            GitUrl = _GitUrl,
            GitToken = _GitToken,
            GitServer = _GitServer,
            ServerPath = _ServerPath,
            MissionDifficulty = _MissionDifficulty,
            RandomizeFunctions = _RandomizeFunctions,
            RandomizeGlobalVariables = _RandomizeGlobalVariables,
            RandomizeLocalVariables = _RandomizeLocalVariables,
            SingleLineFunctions = _SingleLineFunction
        };
    }
}
