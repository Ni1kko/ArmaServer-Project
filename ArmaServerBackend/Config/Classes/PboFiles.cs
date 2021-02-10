namespace ArmaServerBackend
{ 
    public class PboFiles
    {
        public PboModType ModType { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string GitBranch { get; set; }
        public string GitUrl { get; set; }
        public string GitToken { get; set; }
        public GitServer GitServer { get; set; }
        public string ServerPath { get; set; }
        public MissionDifficulty MissionDifficulty { get; set; } 
        public bool RandomizeFunctions { get; set; }
        public bool RandomizeGlobalVariables { get; set; }
        public bool RandomizeLocalVariables { get; set; }
        public bool SingleLineFunctions { get; set; }
    }

    public class PboFilesDefault 
    {
        public PboFiles Values(string _Name = "NewAddon", string _ServerPath = "c:\\Arma3\\@Server", PboModType _ModType = PboModType.ServerMod, bool _IsEnabled = true, string _GitBranch = "repo-main", string _GitUrl = "https://github.com/user/repo/archive/master.zip", string _GitToken = "xxxxx", GitServer _GitServer = GitServer.GitHub, MissionDifficulty _MissionDifficulty = MissionDifficulty.recruit,bool _RandomizeFunctions = false, bool _RandomizeGlobalVariables = false, bool _RandomizeLocalVariables = false, bool _SingleLineFunction = false) => new PboFiles() {
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
