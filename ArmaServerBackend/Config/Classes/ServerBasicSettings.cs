namespace ArmaServerBackend
{
    public class ServerBasicSettings
    {
        public string language { get; set; }
        public int adapter { get; set; }
        public double Performance_3D { get; set; }
        public int Resolution_W { get; set; }
        public int Resolution_H { get; set; }
        public int Resolution_Bpp { get; set; }
        public int MinBandwidth { get; set; }
        public int MaxBandwidth { get; set; }
        public double MaxMsgSend { get; set; }
        public int MaxSizeGuaranteed { get; set; }
        public int MaxSizeNonguaranteed { get; set; }
        public int MaxPacketSize { get; set; }
        public double MinErrorToSend { get; set; }
        public double MinErrorToSendNear { get; set; }
        public int MaxCustomFileSize { get; set; }
        public int TerrainGrid { get; set; }
        public int ViewDistance { get; set; }
        public int Windowed { get; set; }
    }
    
    public class ServerBasicSettingDefault
    {
        public ServerBasicSettings Values() => new ServerBasicSettings()
        {
            language = "English",
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
