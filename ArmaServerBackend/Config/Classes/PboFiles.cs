namespace ArmaServerBackend
{
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
}
