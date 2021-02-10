namespace ArmaServerBackend
{
    public class LaunchParameters
    { 
        public string clientMods { get; set; }
        public string serverMods { get; set; }
        public string configBasic { get; set; }
        public string configServer { get; set; }
        public int port { get; set; }
        public string profiles { get; set; }
        public string name { get; set; }
        public bool enableHT { get; set; }
        public bool hugepages { get; set; }
        public bool autoinit { get; set; }

        public override string ToString() 
        {  
            //Start cmd line string
            string parameters = string.Format("-port={0} -profiles=A3Config -name=Server", port);

            //Add .cfgs to cmd line
            parameters += " -cfg=" + configBasic;
            parameters += " -config=" + configServer;

            //Add @mods to cmd line
            if (serverMods != "") parameters += " -servermod=" + serverMods;
            if (clientMods != "") parameters += " -mod=" + clientMods;

            //Add other parmas to cmd line
            if (enableHT) parameters += " -enableHT";
            if (hugepages) parameters += " -hugepages";

            //Add persistent to cmd line
            if (autoinit) parameters += " -autoinit";

            System.Console.WriteLine($"Launch Params: {parameters}");
             
            return parameters;
        }
    }
} 