namespace ArmaServerBackend
{
    public enum GitType {
        GitHub = 1,
        GitLab = 2
    }

    public class PboFiles
    {
        public string Name { get; set; }
        public string GitPath { get; set; }
        public string GitUrl { get; set; }
        public string GitToken { get; set; }
        public int GitType { get; set; } // 1 = GitHub, 2 = GitLab, implement ur own if u need
        public string ServerPath { get; set; }
        public bool OneLine { get; set; }
        public bool RenameFuncs { get; set; }
        public bool RenameGlobalVars { get; set; }
        public bool RenameLocalVars { get; set; }
    }

    public class PboFilesDefault 
    {
        public PboFiles Values(string _Name, string _ServerPath) => new PboFiles()
        {
            Name = _Name,
            GitPath = "repo-main",
            GitUrl = "https://github.com/user/repo/archive/master.zip",
            GitToken = "xxxxx",
            GitType = (int)GitType.GitHub,
            ServerPath = _ServerPath,
            OneLine = false,
            RenameFuncs = false,
            RenameGlobalVars = false,
            RenameLocalVars = false
        };
    }
}
