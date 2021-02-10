using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace ArmaServerBackend
{
    public class Config
    {
        private protected static readonly string _configPath = Path.Combine(Environment.CurrentDirectory, "ArmaServer.json");

        // Load values from .json
        public bool Load()
        { 
            try
            {
                if (!File.Exists(_configPath))
                {
                    Console.WriteLine("Config file not found, writing a new one!");
                    DLL.ConfigValues = SaveDefaults();
                }

                DLL.ConfigValues = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_configPath));
                return (DLL.ConfigValues != null ? true : false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! An error occured while reading the config file: " + ex);
                DLL.ConfigValues = SaveDefaults();

                Environment.Exit(0);
                return false;
            }
        }

        // Write values to .json
        public bool Save()
        {
            try
            {
                File.WriteAllText(_configPath, JsonConvert.SerializeObject(DLL.ConfigValues, Formatting.Indented));
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error while writing config. Invalid permissions? | Exception: " + ex);
                return false;
            }
        }

        // Write Default values to .json
        private protected Settings SaveDefaults()
        {
            DLL.ConfigValues = new SettingsDefault().Values("Mission.Altis", "FSCQC");
            Save();
            return DLL.ConfigValues;
        }

        // Misc
        private string GetMotd(List<string> Motd)
        {
            var motd = "";
            for (var i = 0; i < Motd.Count; i++)
            {
                motd += "\t\"" + Motd[i] + "\"";
                motd += i < Motd.Count - 1 ? "," + Environment.NewLine : Environment.NewLine;
            }
            return motd;
        }
        private string GetMissions(List<Mission> Missions)
        {
            var missionString = "";
            var index = 0;

            foreach (Mission mission in Missions)
            {
                if (mission.enabled)
                {
                    index++;
                    missionString = missionString +
                        "\tclass Mission_" + index + Environment.NewLine +
                        "\t{" + Environment.NewLine +
                        "\t\ttemplate = \"" + mission.template + "\";" +
                        Environment.NewLine +
                        "\t\tdifficulty = \"" + mission.difficulty + "\";" +
                        Environment.NewLine +
                        "\t};" + Environment.NewLine + Environment.NewLine;
                }
            }
            return missionString;
        }

        // Return Configs As String
        private string GetBasicConfigString(ServerBasicSettings BasicSetting)
        {
            var basicConfig = "";

            basicConfig +=
                "language=\"" + BasicSetting.language + "\";" + Helpers.NewLine() +
                "MaxMsgSend = " + BasicSetting.MaxMsgSend + ";" + Helpers.NewLine() +
                "MaxSizeGuaranteed = " + BasicSetting.MaxSizeGuaranteed + ";" + Helpers.NewLine() +
                "MaxSizeNonguaranteed = " + BasicSetting.MaxSizeNonguaranteed + ";" + Helpers.NewLine() +
                "MinBandwidth = " + BasicSetting.MinBandwidth + ";" + Helpers.NewLine() +
                "MaxBandwidth = " + BasicSetting.MaxBandwidth + ";" + Helpers.NewLine() +
                "MinErrorToSend = " + BasicSetting.MinErrorToSend.ToString(CultureInfo.InvariantCulture) + ";" +
                Helpers.NewLine() +
                "MinErrorToSendNear = " + BasicSetting.MinErrorToSendNear.ToString(CultureInfo.InvariantCulture) + ";" +
                Helpers.NewLine() +
                "MaxCustomFileSize = " + BasicSetting.MaxCustomFileSize + ";" + Helpers.NewLine() +
                "class sockets{maxPacketSize = " + BasicSetting.MaxPacketSize + ";};" + Helpers.NewLine() +
                "adapter="+ BasicSetting.adapter + ";" + Helpers.NewLine() +
                "3D_Performance="+ BasicSetting.Performance_3D + ";" + Helpers.NewLine() +
                "Resolution_W= " + BasicSetting.Resolution_W + ";" + Helpers.NewLine() +
                "Resolution_H=" + BasicSetting.Resolution_H + ";" + Helpers.NewLine() +
                "Resolution_Bpp="+ BasicSetting.Resolution_Bpp + ";" + Helpers.NewLine() +
                "terrainGrid=" + BasicSetting.TerrainGrid.ToString(CultureInfo.InvariantCulture) + ";" + Helpers.NewLine() +
                "viewDistance=" + BasicSetting.ViewDistance + ";" + Helpers.NewLine() +
                "Windowed="+ BasicSetting.Windowed + ";";

            return basicConfig;
        }
        private string GetServerConfigString(ServerSettings settings)
        {
            var configString = "";
             
            configString +=
                "hostName = \"" + settings.hostName+ "\";" + Helpers.NewLine() +
                "password = \"" + settings.password + "\";" + Helpers.NewLine() +
                "passwordAdmin = \"" + settings.passwordAdmin + "\";" + Helpers.NewLine() +
                "serverCommandPassword = \"" + settings.serverCommandPassword + "\";" + Helpers.NewLine() +
                "logFile = \"" + settings.logFile + ".log\";" + Helpers.NewLine(2) +
                "motd[] = {" + Helpers.NewLine() + GetMotd(settings.motd) + "};" + Helpers.NewLine() +
                "motdInterval = " + settings.motdInterval + ";" + Helpers.NewLine(2) +
                "maxPlayers = " + settings.maxPlayers + ";" + Helpers.NewLine() +
                "kickduplicate = " + Convert.ToInt32(settings.kickDuplicate) + ";" + Helpers.NewLine() +
                "verifySignatures = " + settings.verifySignatures + ";" + Helpers.NewLine() +
                "allowedFilePatching = " + settings.allowedFilePatching + ";" + Helpers.NewLine() +
                "requiredSecureId = " + settings.requiredSecureId + ";" + Helpers.NewLine();

            if (settings.upnp)
            {
                configString += "upnp = 1;" + Helpers.NewLine();
            }

            if (settings.loopback)
            {
                configString += "loopback = true;" + Helpers.NewLine();
            }

            if (settings.requiredBuild > 0)
            {
                configString += "requiredBuild = " + settings.requiredBuild + ";" + Helpers.NewLine();
            }

            if (settings.HeadlessEnabled)
            {
                configString += "headlessClients[]={" + string.Join(",", settings.headlessIps) + "};" + Helpers.NewLine();
                configString += "localClient[]={" + string.Join(",", settings.localIps) + "};" + Helpers.NewLine(2);
            }

            if (!settings.VotingEnabled)
            {
                configString += Helpers.NewLine() + "allowedVoteCmds[] = {};";
            }

            double voteThreshold = settings.VotingEnabled ? settings.voteThreshold : 1.5;

            configString += Helpers.NewLine() +
                            "voteMissionPlayers = " + settings.voteMissionPlayers + ";" + Helpers.NewLine() +
                            "voteThreshold = " + voteThreshold.ToString(CultureInfo.InvariantCulture) + ";" + Helpers.NewLine(2) +
                            "disableVoN = " + Convert.ToInt32(settings.disableVoN) + ";" + Helpers.NewLine() +
                            "vonCodecQuality = " + settings.vonCodecQuality + ";" + Helpers.NewLine() +
                            "persistent = " + Convert.ToInt32(settings.persistent) + ";" + Helpers.NewLine() +
                            "timeStampFormat = \"" + settings.timeStampFormat + "\";" + Helpers.NewLine() +
                            "BattlEye = " + Convert.ToInt32(settings.BattlEye) + ";" + Helpers.NewLine();
           
            if (settings.HeadlessEnabled)
            {
                configString += "battleyeLicense = 1;" + Helpers.NewLine();
            }

            if (settings.maxPingEnabled)
            {
                configString += "maxPing = " + settings.maxPing + ";" + Helpers.NewLine();
            }

            if (settings.maxDesyncEnabled)
            {
                configString += "maxDesync = " + settings.maxDesync + ";" + Helpers.NewLine();
            }

            if (settings.maxPacketLossEnabled)
            {
                configString += "maxPacketloss = " + settings.maxPacketLoss + ";" + Helpers.NewLine();
            }

            if (settings.disconnectTimeoutEnabled)
            {
                configString += "disconnectTimeout = " + settings.disconnectTimeout + ";" + Helpers.NewLine();
            }

            if (settings.KickClientsOnSlowNetworkEnabled)
            {
                configString += "kickClientsOnSlowNetwork = " + settings.kickClientsOnSlowNetwork + ";" + Helpers.NewLine();
            }
             
            configString += Helpers.NewLine() + "doubleIdDetected = \"" + settings.doubleIdDetected + "\";" + Helpers.NewLine() +
                            "onUserConnected = \"" + settings.onUserConnected + "\";" + Helpers.NewLine() +
                            "onUserDisconnected = \"" + settings.onUserDisconnected + "\";" + Helpers.NewLine() +
                            "onHackedData = \"" + settings.onHackedData + "\";" + Helpers.NewLine() +
                            "onDifferentData = \"" + settings.onDifferentData + "\";" + Helpers.NewLine() +
                            "onUnsignedData = \"" + settings.onUnsignedData + "\";" + Helpers.NewLine() +
                            "regularCheck = \"" + settings.regularCheck + "\";" + Helpers.NewLine(2) +
                            "class Missions" + Helpers.NewLine() + "{" + Helpers.NewLine() + GetMissions(settings.Missions) + Helpers.NewLine() + "};";


            return configString;
        }

        // Write Json Config As String
        public void WriteBasicConfigFile(string file, ServerBasicSettings basicSetting) => File.WriteAllText(file, GetBasicConfigString(basicSetting));
        public void WriteServerConfigFile(string file, ServerSettings serverSettings) => File.WriteAllText(file, GetServerConfigString(serverSettings));
        
        // Creates .cfg from .json values
        public bool CreateA3ConfigFile(Settings settings, int config = 0)
        {
            if (config != 0 && config != 1) return false;
            var path = Path.Combine(settings.serverSettings.ServerDirectory, "A3Config");
            var file = Path.Combine(path, (config == 0) ? "FragSqaudBasic.cfg" : "FragSqaudServer.cfg");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if(config == 0) WriteBasicConfigFile(file, settings.BasicSetting); else WriteServerConfigFile(file, settings.serverSettings);
            Thread.Sleep(2000);
            return File.Exists(file);
        }
    }
}
 