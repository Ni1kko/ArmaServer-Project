using System;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    internal static class Program
    {
        internal static DLL BackendDLL = new DLL();

        [STAThread]
        internal static void Main()
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
