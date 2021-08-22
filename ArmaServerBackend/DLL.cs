using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        /// <summary>
        /// List array of vars to obfuscate
        /// </summary>
        internal protected static Dictionary<string, string>
            _localVars   = new Dictionary<string, string>(),
            _globalVars  = new Dictionary<string, string>(),
            _scriptFuncs = new Dictionary<string, string>();
        
        
        private protected bool initDLL = false;
        /// <summary>
        /// DLL Entry point
        /// </summary>
        public DLL()
        {
            if (initDLL) return;
            initDLL = true;

            //Subscibe AssemblyResolve to resolve embedded assemblies
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyFunctions.AssemblyResolver;

            //Stack trace listener
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        /// <summary>
        /// Application Exit Event   
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <param name="killArmaOnExit">bool</param>
        public void OnDLLExit(object sender, EventArgs e, bool killArmaOnExit = false)
        {
            if (killArmaOnExit) UtilityFunctions.Die();
            UtilityFunctions.clearArmaCrap();
        }
    }
}