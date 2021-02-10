using System;

namespace ArmaServerBackend
{
    public class Utilities
    {  
        internal static void Assert(bool val, string msg = "")
        {
            if (!val)
            {
                Console.WriteLine("Fatal error occured!");
                if (msg != "")  Console.WriteLine("Error: " + msg);
                Environment.Exit(1);
            }
        }
        internal static void Assert(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine("Fatal error occured!");
                Console.WriteLine("Error: " + ex);
                Environment.Exit(1);
            }
        }
    }
}