using System;
using System.Collections.Generic;

namespace ArmaServerBackend
{
    public class DLL
    {
        //Create new instances that can be accsesed globaly
        public static Settings ConfigValues = null;
        public static Config ConfigFunctions = new Config();
        public static Utilities UtilityFunctions = new Utilities();
        public static Helpers HelperFunctions = new Helpers();
        public static Embedded AssemblyFunctions = new Embedded();

        //List array of vars to obfuscate
        internal protected static Dictionary<string, string> _localVars = new Dictionary<string, string>();
        internal protected static Dictionary<string, string> _globalVars = new Dictionary<string, string>();
        internal protected static Dictionary<string, string> _scriptFuncs = new Dictionary<string, string>();

        //Subscibe AssemblyResolve to resolve embedded assemblies
        public DLL() => AppDomain.CurrentDomain.AssemblyResolve += AssemblyFunctions.AssemblyResolver; 
    }
}