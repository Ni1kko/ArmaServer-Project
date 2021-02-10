using System;
using System.Reflection;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    internal sealed class Program
    {
        internal static DLL BackendDLL = new DLL();
        public static Assembly assembly = typeof(Program).Assembly;
        internal static readonly string APPpackageName = assembly.GetName().Name;
        internal static readonly string DLLpackageName = DLL.AssemblyFunctions.assembly.GetName().Name;

        [STAThread]
        private static void Main()
        {
            if (DLL.ConfigFunctions.Load()){
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false); 
                Application.Run(new Home());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
