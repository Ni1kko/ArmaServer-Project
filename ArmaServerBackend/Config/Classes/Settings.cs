using System.Collections.Generic;

namespace ArmaServerBackend
{
    public class Settings
    {
        public string ServerDirectory { get; set; }
        public string GitDirectory { get; set; }
        public string FunctionsTag { get; set; }
        public int RandomFuncsLength { get; set; }
        public int RandomVarsLength { get; set; }
        public bool KillArmaServer { get; set; }
        public bool Use64BitServer { get; set; }
        public List<string> ObfLocalVars { get; set; }
        public List<string> ObfGlobalVars { get; set; }
        public List<string> ObfFunctions { get; set; }
        public List<PboFiles> Pbos { get; set; }
    }
}
