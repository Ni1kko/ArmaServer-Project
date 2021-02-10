namespace ArmaServerBackend
{ 
    public class PboFiles
    {
        public bool IsMission { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string GitBranch { get; set; }
        public string GitUrl { get; set; }
        public string GitToken { get; set; }
        public GitServer GitServer { get; set; } // 1 = GitHub, 2 = GitLab, implement ur own if u need
        public string ServerPath { get; set; }
        public string MissionDifficulty { get; set; } 
        public bool RandomizeFunctions { get; set; }
        public bool RandomizeGlobalVariables { get; set; }
        public bool RandomizeLocalVariables { get; set; }
        public bool SingleLineFunctions { get; set; }
    }

    public class PboFilesDefault 
    {
        public PboFiles Values(string _Name, string _ServerPath, bool _IsMission) => new PboFiles()
        {
            IsMission = _IsMission,
            IsEnabled = true, 
            Name = _Name,
            GitBranch = "repo-main",
            GitUrl = "https://github.com/user/repo/archive/master.zip",
            GitToken = "xxxxx",
            GitServer = GitServer.GitHub,
            ServerPath = _ServerPath,
            MissionDifficulty = "recruit",
            RandomizeFunctions = false,
            RandomizeGlobalVariables = false,
            RandomizeLocalVariables = false,
            SingleLineFunctions = false
        };
    }
}
