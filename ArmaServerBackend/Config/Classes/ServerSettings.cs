using System.Collections.Generic;

namespace ArmaServerBackend
{
    public class ServerSettings
    {
        public string ServerDirectory { get; set; }
        public bool X64Architecture { get; set; }
        public string hostName { get; set; }
        public string password { get; set; }
        public string passwordAdmin { get; set; }
        public string serverCommandPassword { get; set; }
        public string logFile { get; set; }
        public List<string> motd { get; set; }
        public int motdInterval { get; set; }
        public int maxPlayers { get; set; }
        public int kickDuplicate { get; set; }
        public int verifySignatures { get; set; }
        public int drawingInMap { get; set; }
        public int voteMissionPlayers { get; set; }
        public bool VotingEnabled { get; set; }
        public double voteThreshold { get; set; }
        public int disableVoN { get; set; }
        public int vonCodec { get; set; }
        public int vonCodecQuality { get; set; }
        public int persistent { get; set; }
        public string timeStampFormat { get; set; }
        public int BattlEye { get; set; }
        public bool KickClientsOnSlowNetworkEnabled { get; set; }
        public int kickClientsOnSlowNetwork { get; set; }
        public bool maxPingEnabled { get; set; }
        public int maxPing { get; set; } 
        public bool maxPacketLossEnabled { get; set; }
        public int maxPacketLoss { get; set; }
        public string onUserConnected { get; set; }
        public string onUserDisconnected { get; set; }
        public string doubleIdDetected { get; set; }
        public string onUnsignedData { get; set; }
        public string onHackedData { get; set; }
        public string onDifferentData { get; set; }
        public string regularCheck { get; set; }
        public int allowedFilePatching { get; set; }
        public List<string> filePatchingExceptions { get; set; }
        public List<string> admins { get; set; }
        public List<string> allowedLoadFileExtensions { get; set; }
        public List<string> allowedPreprocessFileExtensions { get; set; }
        public List<string> allowedHTMLLoadURIs { get; set; }
        public bool disconnectTimeoutEnabled { get; set; }
        public int disconnectTimeout { get; set; }
        public int enablePlayerDiag { get; set; }
        public double callExtReportLimit { get; set; }
        public int lobbyIdleTimeout { get; set; }
        public int requiredSecureId { get; set; }
        public bool upnp { get; set; }
        public bool loopback { get; set; }
        public double requiredBuild { get; set; }
        public bool maxDesyncEnabled { get; set; }
        public int maxDesync { get; set; }
        public bool HeadlessEnabled { get; set; }
        public List<string> headlessIps { get; set; }
        public List<string> localIps { get; set; }
        public List<kickTimeouts> kickTimeout { get; set; }
        public List<Mission> Missions { get; set; }
    }

    public class kickTimeouts
    {
        public int type { get; set; }
        public int time { get; set; }
    }

    public class Mission
    {
        public bool enabled { get; set; }
        public string template { get; set; }
        public string difficulty { get; set; }
    }
    public class ServerSettingsDefault
    {
        public static string consoleLogFile = "server_console";
        public ServerSettings Values(string serverDirectory, string _hostName, string _missionName) => new ServerSettings()
        {
            ServerDirectory = serverDirectory,
            X64Architecture = false,
            hostName = _hostName,
            password = DLL.HelperFunctions.RandomString(5),
            passwordAdmin = DLL.HelperFunctions.RandomString(7),
            serverCommandPassword = DLL.HelperFunctions.RandomVariable(8),
            logFile = consoleLogFile,
            motd = new List<string>(),
            motdInterval = 5,
            maxPlayers = 130,
            kickDuplicate = 1,
            verifySignatures = 0,
            drawingInMap = 0,
            voteMissionPlayers = 150,
            VotingEnabled = true,
            voteThreshold = 0.99,
            disableVoN = 1,
            vonCodec = 1,
            vonCodecQuality = 1,
            persistent = 1,
            timeStampFormat = "short",
            BattlEye = 0,
            KickClientsOnSlowNetworkEnabled = false,
            kickClientsOnSlowNetwork = 1,
            maxPingEnabled = false,
            maxPing = 500,
            maxPacketLossEnabled = false,
            maxPacketLoss = 300,
            onUserConnected = "",
            onUserDisconnected = "",
            doubleIdDetected = "",
            onUnsignedData = "kick (_this select 0)",
            onHackedData = "kick (_this select 0)",
            onDifferentData = "",
            regularCheck = "",
            allowedFilePatching = 1,
            filePatchingExceptions = new List<string>(),
            admins = new List<string>(),
            allowedLoadFileExtensions = new List<string>(),
            allowedPreprocessFileExtensions = new List<string>(),
            allowedHTMLLoadURIs = new List<string>(),
            disconnectTimeoutEnabled = false,
            disconnectTimeout = 5,
            enablePlayerDiag = 1,
            callExtReportLimit = 2500.0,
            lobbyIdleTimeout = 300,
            requiredSecureId = 0,
            upnp = false,
            loopback = false,
            requiredBuild = 0,
            maxDesyncEnabled = false,
            maxDesync = 300,
            HeadlessEnabled = false,
            headlessIps = new List<string>(),
            localIps = new List<string>(),
            kickTimeout = new List<kickTimeouts>() { 
                new kickTimeouts()
                {
                    type = 0,//manual kick (vote kick, admin kick, bruteforce detection etc.)
                    time = -1// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new kickTimeouts()
                {
                    type = 1,//connectivity kick (ping, timeout, packetloss, desync)
                    time = 180// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new kickTimeouts()
                {
                    type = 2,//BattlEye kick
                    time = 180// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new kickTimeouts()
                {
                    type = 3,//harmless kick (wrong addons, steam timeout or checks, signatures, content etc.)
                    time = 180// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                }
            },
            Missions = new List<Mission>()
            {
                new Mission()
                {
                    enabled = true,
                    template = _missionName,
                    difficulty = "regular"
                }
            }
        };
    }
}
