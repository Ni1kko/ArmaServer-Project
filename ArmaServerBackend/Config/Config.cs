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
                motd += Helpers.NewTab() + "\"" + Motd[i] + "\"";
                motd += i < Motd.Count - 1 ? "," + Helpers.NewLine() : Helpers.NewLine();
            }
            return motd;
        }
        private string GetMissions(List<PBOFile> pboFiles)
        {
            var missionString = "";
            var index = 0;

            foreach (PBOFile pbo in pboFiles)
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
        internal string GetMods(List<PBOFile> pboFiles, PboModType pboModType)
        {
            var modString = "";
            foreach (PBOFile pbo in pboFiles)
            {
                if (pbo.ModType != pboModType) continue;//Not server mod
                if (!pbo.IsEnabled) continue;
                modString += pbo.ServerPath + ";";
            }
            if (modString.EndsWith(";")) modString = modString.TrimEnd(';');
            return modString;
        }
        private string GetAdvancedOptions(AdvancedOptions advancedOptions)
        {
            string options = "";
            options += Helpers.NewTab() + "LogObjectNotFound = " + advancedOptions.LogObjectNotFound.ToString().ToLower() + ";" + Helpers.NewLine();
            options += Helpers.NewTab() + "SkipDescriptionParsing = " + advancedOptions.SkipDescriptionParsing.ToString().ToLower() + ";" + Helpers.NewLine();
            options += Helpers.NewTab() + "ignoreMissionLoadErrors = " + advancedOptions.ignoreMissionLoadErrors.ToString().ToLower() + ";";
            return options;
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
        private string GetServerConfigString(Settings settings)
        {
            var configString = "";
             
            configString +=
                "hostName = \"" + settings.serverSettings.hostName+ "\";" + Helpers.NewLine() +
                "password = \"" + settings.serverSettings.password + "\";" + Helpers.NewLine() +
                "passwordAdmin = \"" + settings.serverSettings.passwordAdmin + "\";" + Helpers.NewLine() +
                "serverCommandPassword = \"" + settings.serverSettings.serverCommandPassword + "\";" + Helpers.NewLine() +
                "logFile = \"" + settings.serverSettings.logFile + ".log\";" + Helpers.NewLine(2) +
                "motd[] = {" + Helpers.NewLine() + GetMotd(settings.serverSettings.motd) + "};" + Helpers.NewLine() +
                "motdInterval = " + settings.serverSettings.motdInterval + ";" + Helpers.NewLine(2) +
                "maxPlayers = " + settings.serverSettings.maxPlayers + ";" + Helpers.NewLine() +
                "kickduplicate = " + Convert.ToInt32(settings.serverSettings.kickDuplicate) + ";" + Helpers.NewLine() +
                "verifySignatures = " + settings.serverSettings.verifySignatures + ";" + Helpers.NewLine() +
                "allowedFilePatching = " + settings.serverSettings.allowedFilePatching + ";" + Helpers.NewLine() +
                "requiredSecureId = " + settings.serverSettings.requiredSecureId + ";" + Helpers.NewLine();

            if (settings.serverSettings.upnp)
            {
                configString += "upnp = 1;" + Helpers.NewLine();
            }

            if (settings.serverSettings.loopback)
            {
                configString += "loopback = true;" + Helpers.NewLine();
            }

            if (settings.serverSettings.requiredBuild > 0)
            {
                configString += "requiredBuild = " + settings.serverSettings.requiredBuild + ";" + Helpers.NewLine();
            }

            if (settings.serverSettings.HeadlessEnabled)
            {
                configString += "headlessClients[]={" + string.Join(",", settings.serverSettings.headlessIps) + "};" + Helpers.NewLine();
                configString += "localClient[]={" + string.Join(",", settings.serverSettings.localIps) + "};" + Helpers.NewLine(2);
            }

            if (settings.serverSettings.VotingEnabled)
            {
                //configString += Helpers.NewLine() + "allowedVoteCmds[] = { "+ string.Join(",", settings.allowedVoteCmds) +"};";
            }
            else
            {
                configString += Helpers.NewLine() + "allowedVoteCmds[] = {};";
            }

            double voteThreshold = settings.serverSettings.VotingEnabled ? settings.serverSettings.voteThreshold : 1.5;

            configString += Helpers.NewLine() +
                            "voteMissionPlayers = " + settings.serverSettings.voteMissionPlayers + ";" + Helpers.NewLine() +
                            "voteThreshold = " + voteThreshold.ToString(CultureInfo.InvariantCulture) + ";" + Helpers.NewLine(2) +
                            "disableVoN = " + Convert.ToInt32(settings.serverSettings.disableVoN) + ";" + Helpers.NewLine() +
                            "vonCodecQuality = " + settings.serverSettings.vonCodecQuality + ";" + Helpers.NewLine() +
                            "persistent = " + Convert.ToInt32(settings.serverSettings.persistent) + ";" + Helpers.NewLine() +
                            "timeStampFormat = \"" + settings.serverSettings.timeStampFormat + "\";" + Helpers.NewLine() +
                            "BattlEye = " + Convert.ToInt32(settings.serverSettings.BattlEye) + ";" + Helpers.NewLine();
           
            if (settings.serverSettings.HeadlessEnabled)
            {
                configString += "battleyeLicense = 1;" + Helpers.NewLine();
            }

            if (settings.serverSettings.maxPingEnabled)
            {
                configString += "maxPing = " + settings.serverSettings.maxPing + ";" + Helpers.NewLine();
            }

            if (settings.serverSettings.maxDesyncEnabled)
            {
                configString += "maxDesync = " + settings.serverSettings.maxDesync + ";" + Helpers.NewLine();
            }

            if (settings.serverSettings.maxPacketLossEnabled)
            {
                configString += "maxPacketloss = " + settings.serverSettings.maxPacketLoss + ";" + Helpers.NewLine();
            }

            if (settings.serverSettings.disconnectTimeoutEnabled)
            {
                configString += "disconnectTimeout = " + settings.serverSettings.disconnectTimeout + ";" + Helpers.NewLine();
            }

            if (settings.serverSettings.kickClientsOnSlowNetwork == 1)
            {
                configString += "kickClientsOnSlowNetwork = " + settings.serverSettings.kickClientsOnSlowNetwork + ";" + Helpers.NewLine();
            }
             
            configString += Helpers.NewLine() + "doubleIdDetected = \"" + settings.serverSettings.doubleIdDetected + "\";" + Helpers.NewLine() +
                            "onUserConnected = \"" + settings.serverSettings.onUserConnected + "\";" + Helpers.NewLine() +
                            "onUserDisconnected = \"" + settings.serverSettings.onUserDisconnected + "\";" + Helpers.NewLine() +
                            "onHackedData = \"" + settings.serverSettings.onHackedData + "\";" + Helpers.NewLine() +
                            "onDifferentData = \"" + settings.serverSettings.onDifferentData + "\";" + Helpers.NewLine() +
                            "onUnsignedData = \"" + settings.serverSettings.onUnsignedData + "\";" + Helpers.NewLine() +
                            "regularCheck = \"" + settings.serverSettings.regularCheck + "\";" + Helpers.NewLine(2) +
                            "class AdvancedOptions" + Helpers.NewLine() + "{" + Helpers.NewLine() + GetAdvancedOptions(settings.serverSettings.advancedOptions) + Helpers.NewLine() + "};" + Helpers.NewLine(2) +
                            "class Missions" + Helpers.NewLine() + "{" + Helpers.NewLine() + GetMissions(settings.Pbos) + Helpers.NewLine() + "};";


            return configString;
        }

        // Write Json Config As String
        public void WriteBasicConfigFile(string file, ServerBasicSettings basicSetting) => File.WriteAllText(file, GetBasicConfigString(basicSetting));
        public void WriteServerConfigFile(string file, Settings settings) => File.WriteAllText(file, GetServerConfigString(settings));

        //Get Config Path
        public string GetA3Config(Settings settings, int config = 0)
        { 
            if (config != 0 && config != 1) return "";
            var path = Path.Combine(settings.serverSettings.ServerDirectory, "A3Config");
            var file = Path.Combine(path, (config == 0) ? "ArmaBasic.cfg" : "ArmaServer.cfg");
            return File.Exists(file) ? file : "";
        }

        // Creates .cfg from .json values
        public bool CreateA3ConfigFile(Settings settings, int config = 0)
        {
            if (config != 0 && config != 1) return false;
            var path = Path.Combine(settings.serverSettings.ServerDirectory, "A3Config");
            var file = Path.Combine(path, (config == 0) ? "ArmaBasic.cfg" : "ArmaServer.cfg");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if(config == 0) WriteBasicConfigFile(file, settings.BasicSetting); else WriteServerConfigFile(file, settings);
            Thread.Sleep(2000);
            return File.Exists(file);
        }
    }
}
 