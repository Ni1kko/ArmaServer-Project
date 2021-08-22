using System.Globalization;
using System.IO;

namespace ArmaServerBackend
{
    /// <summary>
    /// ServerBasic Settings
    /// </summary>
    public class ServerBasicSettings : Helpers
    {
        /// <summary>
        /// Sets server language
        /// </summary>
        public Language language { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public int adapter { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public double Performance_3D { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public int Resolution_W { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public int Resolution_H { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public int Resolution_Bpp { get; set; }

        /// <summary>
        /// Bandwidth the server is guaranteed to have (in bps).
        /// This value helps server to estimate bandwidth available.
        /// Increasing it to too optimistic values can increase lag and CPU load, as too many messages will be sent but discarded.
        /// Default: 131072
        /// </summary>
        public int MinBandwidth { get; set; }

        /// <summary>
        /// Bandwidth the server is guaranteed to never have (in bps).
        /// This value helps the server to estimate bandwidth available.
        /// </summary>
        public int MaxBandwidth { get; set; }

        /// <summary>
        /// Maximum number of packets (aggregate messages) that can be sent in one simulation cycle ("frame").
        /// Increasing this value can decrease lag on high upload bandwidth servers.
        /// Default: 128
        /// </summary>
        public double MaxMsgSend { get; set; }

        /// <summary>
        /// Maximum size (payload) of guaranteed packet in bytes (without headers).
        /// Small messages are packed to larger packets(aggregate messages).
        /// Guaranteed packets(aggregate messages) are used for non-repetitive events like shooting.
        /// Default: 512
        /// </summary>
        public int MaxSizeGuaranteed { get; set; }

        /// <summary>
        /// Maximum size (payload) of non-guaranteed packet in bytes (without headers).
        /// Small messages are packed to larger packets(aggregate messages).
        /// Non-guaranteed packets(aggregate messages) are used  for repetitive updates like soldier or vehicle position.
        /// Increasing this value may improve bandwidth requirement, but it may increase lag.
        /// Default: 256
        /// </summary>
        public int MaxSizeNonguaranteed { get; set; }

        /// <summary>
        /// Maximal size of packet sent over network. 
        /// This can be set for both client-to-server AND server-to-client(s) independently! 
        /// Default: 1400
        /// </summary>
        public int MaxPacketSize { get; set; }

        /// <summary>
        /// Minimal error to send updates across network.
        /// Using a smaller value can make units observed by binoculars or sniper rifle to move smoother at the trade off of
        /// increased network traffic.
        /// Default: 0.001 (was 0.01 until ARMA 2:OA version 1.60, ARMA 2 version 1.11 uses 0.01)
        /// </summary>
        public double MinErrorToSend { get; set; }

        /// <summary>
        /// Minimal error to send updates across network for near units.
        /// Using larger value can reduce traffic sent for near units.Used to control client to server traffic as well.
        /// Introduced in Arma 2 1.60, Default: 0.01
        /// </summary>
        public double MinErrorToSendNear { get; set; }

        /// <summary>
        /// Users with custom face or custom sound larger than this size are kicked when trying to connect.
        /// </summary>
        public int MaxCustomFileSize { get; set; }

        /// <summary>
        /// Sets default TerrainGrid values
        /// Low = 50 (NoGrass)
        /// Standard = 25
        /// High = 12.5
        /// Very High = 6.25
        /// Ultra = 3.125 
        /// </summary>
        public int TerrainGrid { get; set; }

        /// <summary>
        /// server's view distance
        /// </summary>
        public int ViewDistance { get; set; }

        /// <summary>
        /// Unknowing Usage
        /// </summary>
        public int Windowed { get; set; }

        /// <summary>
        /// Convert Config to user friendly string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            //Start cfg string
            var basicConfig = "";

            basicConfig +=
                "language=\"" + DLL.HelperFunctions.Capitalize(System.Enum.GetName(typeof(Language), language)) + "\";" + Helpers.NewLine() +
                "MaxMsgSend = " + MaxMsgSend + ";" + NewLine() +
                "MaxSizeGuaranteed = " + MaxSizeGuaranteed + ";" + NewLine() +
                "MaxSizeNonguaranteed = " + MaxSizeNonguaranteed + ";" + NewLine() +
                "MinBandwidth = " + MinBandwidth + ";" + NewLine() +
                "MaxBandwidth = " + MaxBandwidth + ";" + NewLine() +
                "MinErrorToSend = " + MinErrorToSend.ToString(CultureInfo.InvariantCulture) + ";" +
                NewLine() +
                "MinErrorToSendNear = " + MinErrorToSendNear.ToString(CultureInfo.InvariantCulture) + ";" +
                NewLine() +
                "MaxCustomFileSize = " + MaxCustomFileSize + ";" + NewLine() +
                "class sockets{maxPacketSize = " + MaxPacketSize + ";};" + NewLine() +
                "adapter=" + adapter + ";" + NewLine() +
                "3D_Performance=" + Performance_3D + ";" + NewLine() +
                "Resolution_W= " + Resolution_W + ";" + NewLine() +
                "Resolution_H=" + Resolution_H + ";" + NewLine() +
                "Resolution_Bpp=" + Resolution_Bpp + ";" + NewLine() +
                "terrainGrid=" + TerrainGrid.ToString(CultureInfo.InvariantCulture) + ";" + NewLine() +
                "viewDistance=" + ViewDistance + ";" + NewLine() +
                "Windowed=" + Windowed + ";";

            return basicConfig;
        }

        /// <summary>
        /// Write Saved Basic Config
        /// </summary>
        /// <param name="file"></param> 
        internal override void WriteFile(string file, string _ = null) => DLL.HelperFunctions.WriteFile(file, DLL.ConfigValues.BasicSetting.ToString());

    }

    /// <summary>
    /// ServerBasicSetting Defualt Settings
    /// </summary>
    public class ServerBasicSettingDefault
    {
        /// <summary>
        /// Creates default config
        /// </summary>
        /// <returns>ServerBasicSettings</returns>
        public ServerBasicSettings Values() => new ServerBasicSettings()
        {
            language = 0,
            adapter = -1,
            Performance_3D = 1.000000,
            Resolution_W = 800,
            Resolution_H = 600,
            Resolution_Bpp = 32,
            MinBandwidth = 180000000,
            MaxBandwidth = 250000000,
            MaxMsgSend = 384,
            MaxSizeGuaranteed = 512,		
            MaxSizeNonguaranteed = 232,
            MaxPacketSize = 600,
            MinErrorToSend = 0.0015,	
            MinErrorToSendNear = 0.0015,
            MaxCustomFileSize = 0,
            TerrainGrid = 50,
            ViewDistance = 1000,
            Windowed = 0
        };
    }
}
