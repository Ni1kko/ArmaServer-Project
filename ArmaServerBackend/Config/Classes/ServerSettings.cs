using System.Collections.Generic;
using System.Net;

namespace ArmaServerBackend
{
    /// <summary>
    /// https://community.bistudio.com/wiki/server.cfg
    /// </summary>
    public class ServerSettings
    {
        /// <summary>
        /// Armaserver root directory
        /// </summary>
        public string ServerDirectory { get; set; }

        /// <summary>
        /// Armaserver architecture
        /// </summary>
        public bool X64Architecture { get; set; }

        /// <summary>
        /// Pull PBO(S) from gitserver on server start/restart
        /// </summary>
        public bool PullOnStart { get; set; }

        /// <summary>
        /// Servername visible in the game browser. 
        /// </summary>
        public string hostName { get; set; }

        /// <summary>
        /// Port to have dedicated server listen on. 
        /// </summary>
        public int port { get; set; }

        /// <summary>
        /// Command to enable support for Multihome servers. Allows server process to use defined available IP address
        /// </summary>
        public bool UseIP { get; set; }

        /// <summary>
        /// IP to have dedicated server listen on. (UseIP) Must be set true first
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Password required to connect to server. 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Password to protect admin access. 
        /// </summary>
        public string passwordAdmin { get; set; }

        /// <summary>
        /// Password required by alternate syntax of serverCommand server-side scripting.
        /// </summary>
        public string serverCommandPassword { get; set; }

        /// <summary>
        /// Console logFile name, 
        /// Outputs dedicated server console into textfile. 
        /// Default location of log is same as crash dumps and other logs.
        /// </summary>
        public string logFile { get; set; }

        /// <summary>
        /// message of the day
        /// Empty messages "" will not be displayed at all but are only for increasing the interval
        /// </summary>
        public List<string> motd { get; set; }

        /// <summary>
        /// message of the day time interval (in seconds) between each message
        /// </summary>
        public int motdInterval { get; set; }

        /// <summary>
        /// Maximum amount of players. Civilians and watchers, beholder, bystanders and so on also count as player.
        /// </summary>
        public int maxPlayers { get; set; }

        /// <summary>
        /// Do not allow duplicate game IDs. Second player with an existing ID will be kicked automatically. 1 means active, 0 disabled. 
        /// </summary>
        public int kickDuplicate { get; set; }

        /// <summary>
        /// Enables or disables the signature verification for addons. 
        /// Default = 2.
        /// Verification disabled = 0. 
        /// From Arma 3 use only level 2 (level 1 is deprecated and defaults to 2) 
        /// </summary>
        public int verifySignatures { get; set; }

        /// <summary>
        /// Enables or disables the ability to place markers and draw lines in map. Default: 1
        /// </summary>
        public int drawingInMap { get; set; }

        /// <summary>
        /// Tells the server how many people must connect so that it displays the mission selection screen.
        /// </summary>
        public int voteMissionPlayers { get; set; }

        /// <summary>
        /// Enable - Disable voting in server
        /// </summary>
        public bool VotingEnabled { get; set; }

        /// <summary>
        /// Percentage of votes needed to confirm a vote. x% 
        /// </summary>
        public double voteThreshold { get; set; }

        /// <summary>
        /// Enables or disables the Voice over Net. Default = 0. 
        /// </summary>
        public int disableVoN { get; set; }

        /// <summary>
        /// Defines VoN codec type. Value range is from 0 to 1.
        /// Default = 0.
        /// Since Arma 3 update 1.58 supports value 1
        /// Value 0 uses older SPEEX codec, while 1 switches to new IETF standard OPUS codec
        /// </summary>
        public int vonCodec { get; set; }

        /// <summary>
        /// since 1.62.95417 supports range 1-20 
        /// since 1.63.x will supports range 1-30 
        /// 8kHz is 0-10, 
        /// 16kHz is 11-20, 
        /// 32kHz(48kHz) is 21-30
        /// </summary>
        public int vonCodecQuality { get; set; }

        /// <summary>
        /// Mission keeps running when all clients disconnect.. Default = 0. 
        /// </summary>
        public int persistent { get; set; }

        /// <summary>
        /// Set the timestamp format used on each report line in server-side RPT file. 
        /// Possible values are 
        /// "none" (default),
        /// "short",
        /// "full".
        /// </summary>
        public string timeStampFormat { get; set; }

        /// <summary>
        /// Enables or disables the BattlEye anti-cheat engine. 
        /// Default 0.
        /// since 1.09 beta, in Arma 3 default is 1. 
        /// Requires installed battleye on server and clients joining the server 
        /// </summary>
        public int BattlEye { get; set; }

        /// <summary>
        /// Defines if {<MaxPing>, <MaxPacketLoss>, <MaxDesync>, <DisconnectTimeout>} will be logged (0) or kicked (1)
        /// </summary>
        public int kickClientsOnSlowNetwork { get; set; }//todo: change to "public List<int> kickClientsOnSlowNetwork { get; set; }"

        /// <summary>
        /// Enable Max ping value
        /// </summary>
        public bool maxPingEnabled { get; set; }

        /// <summary>
        /// Max ping value until server kick the user (since Arma 3 1.56+) 
        /// maxPingEnabled Must be set true
        /// </summary>
        public int maxPing { get; set; }

        /// <summary>
        /// Enable Max packetloss value
        /// </summary>
        public bool maxPacketLossEnabled { get; set; }

        /// <summary>
        /// Max packetloss value until server kick the user
        /// </summary>
        public int maxPacketLoss { get; set; }

        /// <summary>
        /// Script to be ran on connecting users
        /// </summary>
        public string onUserConnected { get; set; }

        /// <summary>
        /// Script to be ran on disconnecting users
        /// </summary>
        public string onUserDisconnected { get; set; }

        /// <summary>
        /// Script to be ran when double Id detected
        /// </summary>
        public string doubleIdDetected { get; set; }

        /// <summary>
        /// Script to be ran when double Id detected
        /// </summary>
        public string onUnsignedData { get; set; }

        /// <summary>
        /// Script to be ran when hacked data found
        /// </summary>
        public string onHackedData { get; set; }

        /// <summary>
        /// Script to be ran when different data found
        /// </summary>
        public string onDifferentData { get; set; }

        /// <summary>
        /// Script to checks files from time to time by hashing them and comparing the hash to the hash values of the clients.
        /// !! Deprecated !!
        /// </summary>
        public string regularCheck { get; set; }// TODO: remove this

        /// <summary>
        /// Prevent or allow file patching for the clients (including the HC) (since Arma 3 1.49+)
        /// 0 is no clients(default)
        /// 1 is Headless Clients only
        /// 2 is all clients
        /// </summary>
        public int allowedFilePatching { get; set; }

        /// <summary>
        /// Whitelisted Steam IDs allowed filePatching 
        /// </summary>
        public List<string> filePatchingExceptions { get; set; }

        /// <summary>
        /// whitelisted client can use #login w/o password (since Arma 3 1.69+)
        /// https://community.bistudio.com/wiki/server.cfg#Logged_In_Admin
        /// </summary>
        public List<string> admins { get; set; }

        /// <summary>
        /// only allow files with those extensions to be loaded via loadFile command (since Arma 3 build 1.19.124216)
        /// </summary>
        public List<string> allowedLoadFileExtensions { get; set; }

        /// <summary>
        /// only allow files with those extensions to be loaded via preprocessFile/preprocessFileLineNumber commands (since Arma 3 build 1.19.124323)
        /// </summary>
        public List<string> allowedPreprocessFileExtensions { get; set; }

        /// <summary>
        /// define server-level restrictions for URIs
        /// </summary>
        public List<string> allowedHTMLLoadURIs { get; set; }

        /// <summary>
        /// Enable Server wait time before disconnecting client
        /// </summary>
        public bool disconnectTimeoutEnabled { get; set; }

        /// <summary>
        /// Server wait time before disconnecting client after loss of active traffic connection, 
        /// (disconnectTimeoutEnabled) Must be true, 
        /// default 90 seconds, 
        /// range 5 to 90 seconds.
        /// </summary>
        public int disconnectTimeout { get; set; }

        /// <summary>
        /// Logs players' bandwidth and desync info every 60 seconds, as well as "network message is pending" owner identity. 
        /// </summary>
        public int enablePlayerDiag { get; set; }

        /// <summary>
        /// If server initiated callExtension takes longer than specified limit in milliseconds, 
        /// the warning will be logged into server .rpt file as well as reflected in the extension return result. 
        /// Default: 1000.0 
        /// </summary>
        public double callExtReportLimit { get; set; }

        /// <summary>
        /// Enable lobbyIdleTimeout
        /// </summary>
        public bool lobbyIdleTimeoutEnabled { get; set; }

        /// <summary>
        /// Defines a lobbyIdleTimeout, 
        /// (lobbyIdleTimeoutEnabled) Must be set true
        /// default value is 300 seconds (+5 seconds vs any of above) > defined value
        /// Independent of set lobbyIdleTimeout in the config file, it will be at least
        /// MAX(votingTimeout, lobbyTimeout, briefingTimeout, debriefingTimeout) + 5 seconds
        /// by default, this makes it 300 seconds of time for people in lobby and 95 in roleTimeout or briefing screen
        /// (since Arma 3 1.90+ PerformanceBranch)
        /// </summary>
        public int lobbyIdleTimeout { get; set; }

        /// <summary>
        /// When requiredSecureId=1 is used, all playes are requested to provide a validated ID. 
        /// If the player does not provide a validate ID, he can still connect to the server, 
        /// but a warning message is shown and his ID is shown with a question mark in the #userList when admin issues this command.
        ///
        /// When requiredSecureId=2 is used, 
        /// all playes are requested to provide a validated ID and any player who does not provide it will not be allowed to connect on the server.
        /// https://www.bohemia.net/blog/secure-player-id-introduced
        /// </summary>
        public int requiredSecureId { get; set; }

        /// <summary>
        /// Automatically creates port mapping on UPNP/IGD enabled router.
        /// This option allows you to create a server behind NAT (your router must have public IP and support UPNP/IGD protocol).
        /// Default: false.
        /// Warning: When enabled then this setting may delay server start-up by 600s (standard UDP timeout of 10 minutes)
        /// if blocked on firewall or bad routing etc.Thus in such case is recommended to disable it.
        /// </summary>
        public bool upnp { get; set; }

        /// <summary>
        /// Enabling this option will force server into LAN mode.
        /// This will allow multiple local instances of the game to connect to the server for testing purposes.
        /// At the same time it will prevent all non-local instances from connecting.
        /// </summary>
        public bool loopback { get; set; }

        /// <summary>
        /// Minimum required client version.
        /// Clients with version lower than requiredBuild will not be able to connect.
        /// If requiredBuild is set to a large number,
        /// like requiredBuild = 999999999; for example, it will automatically be lowered to the current server version.
        /// </summary>
        public double requiredBuild { get; set; }

        /// <summary>
        /// Enable Max desync value
        /// </summary>
        public bool maxDesyncEnabled { get; set; }

        /// <summary>
        /// Max desync value until server kick the user
        /// </summary>
        public int maxDesync { get; set; }

        /// <summary>
        /// Enable Headless Clients
        /// </summary>
        public bool HeadlessEnabled { get; set; }

        /// <summary>
        /// The server doesn't allow arbitrary connections from headless clients if you do not define the headless clients IPs.
        /// Multiple Connections and Addresses are allowed in the case of more than one Headless Client.
        /// </summary>
        public List<string> headlessIps { get; set; }

        /// <summary>
        /// indicate clients with unlimited bandwidth and nearly no latency (https://dev-heaven.net/issues/62500)
        /// </summary>
        public List<string> localIps { get; set; }

        /// <summary>
        /// List of kickTimeouts
        /// </summary>
        public List<KickTimeout> kickTimeout { get; set; }

        /// <summary>
        /// AdvancedOptions
        /// Available since Arma 3 v2.02
        /// </summary>
        public AdvancedOptions advancedOptions { get; set; }

        /// <summary>
        /// Converts Message of the day to user friendly string
        /// </summary>
        /// <param name="Motd"></param>
        /// <returns>string</returns>
        private string GetMotd(List<string> Motd)
        {
            var motd = "";
            for (var i = 0; i < Motd.Count; i++)
            {
                motd += Helpers.NewTab() + "\"" + Motd[i] + "\"";
                motd += i < Motd.Count - 1 ? "," + Helpers.NewLine() : Helpers.NewLine();
            }
            return motd;
        }

        /// <summary>
        /// Converts Advanced Options to user friendly string
        /// </summary>
        /// <param name="advancedOptions"></param>
        /// <returns>string</returns>
        private string GetAdvancedOptions(AdvancedOptions advancedOptions)
        {
            string options = "";
            options += Helpers.NewTab() + "LogObjectNotFound = " + advancedOptions.LogObjectNotFound.ToString().ToLower() + ";" + Helpers.NewLine();
            options += Helpers.NewTab() + "SkipDescriptionParsing = " + advancedOptions.SkipDescriptionParsing.ToString().ToLower() + ";" + Helpers.NewLine();
            options += Helpers.NewTab() + "ignoreMissionLoadErrors = " + advancedOptions.ignoreMissionLoadErrors.ToString().ToLower() + ";";
            return options;
        }

        /// <summary>
        /// Gets Mission PBO(s) & Converts to user friendly string
        /// </summary>
        /// <param name="advancedOptions"></param>
        /// <returns>string</returns>
        private string GetMissions()
        {
            var missionString = "";
            var index = 0;

            foreach (PBOFile pbo in DLL.ConfigValues.Pbos)
            {
                if (pbo.ModType != PboModType.Mission) continue;//Not mission mod
                if (!pbo.IsEnabled) continue;

                index++;
                missionString +=
                    Helpers.NewLine() + Helpers.NewTab() + "class Mission_" + index + Helpers.NewLine() +
                    Helpers.NewTab() + "{" + Helpers.NewLine() +
                    Helpers.NewTab(2) + "template = \"" + pbo.Name + "\";" +
                    Helpers.NewLine() +
                    Helpers.NewTab(2) + "difficulty = \"" + pbo.MissionDifficulty + "\";" +
                    Helpers.NewLine() +
                    Helpers.NewTab() + "};" + Helpers.NewLine();

            }
            return missionString;
        }

        /// <summary>
        /// Convert Config to user friendly string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            //Start cfg string
            var configString = "";

            configString +=
                "hostName = \"" + hostName + "\";" + Helpers.NewLine() +
                "password = \"" + password + "\";" + Helpers.NewLine() +
                "passwordAdmin = \"" + passwordAdmin + "\";" + Helpers.NewLine() +
                "serverCommandPassword = \"" + serverCommandPassword + "\";" + Helpers.NewLine() +
                "logFile = \"" +logFile + ".log\";" + Helpers.NewLine(2) +
                "motd[] = {" + Helpers.NewLine() + GetMotd(motd) + "};" + Helpers.NewLine() +
                "motdInterval = " + motdInterval + ";" + Helpers.NewLine(2) +
                "maxPlayers = " + maxPlayers + ";" + Helpers.NewLine() +
                "kickduplicate = " + System.Convert.ToInt32(kickDuplicate) + ";" + Helpers.NewLine() +
                "verifySignatures = " + verifySignatures + ";" + Helpers.NewLine() +
                "allowedFilePatching = " + allowedFilePatching + ";" + Helpers.NewLine() +
                "requiredSecureId = " + requiredSecureId + ";" + Helpers.NewLine();

            if (upnp) configString += "upnp = 1;" + Helpers.NewLine();
            if (loopback) configString += "loopback = true;" + Helpers.NewLine();
            if (requiredBuild > 0) configString += "requiredBuild = " + requiredBuild + ";" + Helpers.NewLine();
      

            if (HeadlessEnabled)
            {
                configString += "headlessClients[]={" + string.Join(",", headlessIps) + "};" + Helpers.NewLine();
                configString += "localClient[]={" + string.Join(",", localIps) + "};" + Helpers.NewLine(2);
            }

            if (VotingEnabled)
            {
                //configString += Helpers.NewLine() + "allowedVoteCmds[] = { "+ string.Join(",", settings.allowedVoteCmds) +"};";
            }
            else
            {
                configString += Helpers.NewLine() + "allowedVoteCmds[] = {};";
            }

            double votingThreshold = VotingEnabled ? voteThreshold : 1.5;

            configString += Helpers.NewLine() +
                            "voteMissionPlayers = " + voteMissionPlayers + ";" + Helpers.NewLine() +
                            "voteThreshold = " + votingThreshold.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + Helpers.NewLine(2) +
                            "disableVoN = " + System.Convert.ToInt32(disableVoN) + ";" + Helpers.NewLine() +
                            "vonCodecQuality = " + vonCodecQuality + ";" + Helpers.NewLine() +
                            "persistent = " + System.Convert.ToInt32(persistent) + ";" + Helpers.NewLine() +
                            "timeStampFormat = \"" + timeStampFormat + "\";" + Helpers.NewLine() +
                            "BattlEye = " + System.Convert.ToInt32(BattlEye) + ";" + Helpers.NewLine();

            if (HeadlessEnabled) configString += "battleyeLicense = 1;" + Helpers.NewLine();
            if (maxPingEnabled) configString += "maxPing = " + maxPing + ";" + Helpers.NewLine();
            if (maxDesyncEnabled) configString += "maxDesync = " + maxDesync + ";" + Helpers.NewLine();
            if (maxPacketLossEnabled) configString += "maxPacketloss = " + maxPacketLoss + ";" + Helpers.NewLine();
            if (disconnectTimeoutEnabled) configString += "disconnectTimeout = " + disconnectTimeout + ";" + Helpers.NewLine();
            if (kickClientsOnSlowNetwork == 1) configString += "kickClientsOnSlowNetwork = " + kickClientsOnSlowNetwork + ";" + Helpers.NewLine();
       

            configString += Helpers.NewLine() + "doubleIdDetected = \"" + doubleIdDetected + "\";" + Helpers.NewLine() +
                            "onUserConnected = \"" + onUserConnected + "\";" + Helpers.NewLine() +
                            "onUserDisconnected = \"" + onUserDisconnected + "\";" + Helpers.NewLine() +
                            "onHackedData = \"" +onHackedData + "\";" + Helpers.NewLine() +
                            "onDifferentData = \"" + onDifferentData + "\";" + Helpers.NewLine() +
                            "onUnsignedData = \"" + onUnsignedData + "\";" + Helpers.NewLine() +
                            "regularCheck = \"" + regularCheck + "\";" + Helpers.NewLine(2) +
                            "class AdvancedOptions" + Helpers.NewLine() + "{" + Helpers.NewLine() + GetAdvancedOptions(advancedOptions) + Helpers.NewLine() + "};" + Helpers.NewLine(2) +
                            "class Missions" + Helpers.NewLine() + "{" + Helpers.NewLine() + GetMissions() + Helpers.NewLine() + "};";


            return configString;
        }
    }

    public class ServerSettingsDefault
    {
        public readonly static string consoleLogFile = "server_console";
        public ServerSettings Values(string serverDirectory, string _hostName, string _missionName) => new ServerSettings()
        {
            ServerDirectory = serverDirectory,
            X64Architecture = false,
            PullOnStart = true,
            hostName = _hostName,
            port = 2302,
            UseIP = false,
            IP = IPAddress.Parse("192.168.0.1").ToString(),
            password = DLL.HelperFunctions.CreateRandomString(5),
            passwordAdmin = DLL.HelperFunctions.CreateRandomString(7),
            serverCommandPassword = DLL.HelperFunctions.CreateRandomVariable(8),
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
            vonCodecQuality = 15,
            persistent = 1,
            timeStampFormat = "short",
            BattlEye = 0,
            kickClientsOnSlowNetwork = 1,
            maxPingEnabled = false,
            maxPing = 500,
            maxPacketLossEnabled = false,
            maxPacketLoss = 300,
            onUserConnected = "",
            onUserDisconnected = "",
            doubleIdDetected = "",
            onUnsignedData = "kick (_this select 0)",
            onHackedData = "ban (_this select 0)",
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
            lobbyIdleTimeoutEnabled = false,
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
            advancedOptions = new AdvancedOptions()
            {
                LogObjectNotFound = true,
                SkipDescriptionParsing = false,
                ignoreMissionLoadErrors = false
            },
            kickTimeout = new List<KickTimeout>() { 
                new KickTimeout()
                {
                    type = KickID.manual,//manual kick (vote kick, admin kick, bruteforce detection etc.)
                    time = -2// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new KickTimeout()
                {
                    type = KickID.connectivity,//connectivity kick (ping, timeout, packetloss, desync)
                    time = 0// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new KickTimeout()
                {
                    type = KickID.BattlEye,//BattlEye kick
                    time = 180// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                },
                new KickTimeout()
                {
                    type = KickID.harmless,//harmless kick (wrong addons, steam timeout or checks, signatures, content etc.)
                    time = 0// -1 = until missionEnd | -2 = until serverRestart | 0 & > = seconds
                }
            }
        };
    }
}
