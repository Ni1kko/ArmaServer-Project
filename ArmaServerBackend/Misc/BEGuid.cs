using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace ArmaServerBackend
{
    class BEGuid
    {
        private static Random rnd = new Random();

        internal static string CheckSteamID(BEPort _BEPort, string SteamID)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.ReceiveTimeout = 1000;
            sock.SendTimeout = 1000;
            //All domains lead to the same server, so it should work for Arma3/DayZ as well
            //Arma 2: arma2oa1.battleye.com Port:2324
            //Arma 3: arma31.battleye.com Port:2344
            //DayZ SA: dayz1.battleye.com Port:2354
            string domain = null;
            switch (_BEPort)
            {
                case BEPort.Arma2OA:
                    domain = "arma2oa1.battleye.com";
                    break;
                case BEPort.Arma3:
                    domain = "arma31.battleye.com";
                    break;
                case BEPort.DayZ:
                    domain = "dayz1.battleye.com";
                    break;
                default:
                    return "Unknown config";
            }
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(domain)[0], (int)_BEPort);
            byte[] send_buffer = Encoding.ASCII.GetBytes(CreateRequestString(SteamID));
            try
            {
                sock.SendTo(send_buffer, endPoint);
                byte[] receive_buffer = new byte[1024];
                EndPoint endpnt = (EndPoint)endPoint;
                int recv = sock.ReceiveFrom(receive_buffer, ref endpnt);
                string response = Encoding.ASCII.GetString(receive_buffer, 0, recv).Remove(0, 4);
                return string.IsNullOrEmpty(response) ? "Clean" : response;
            }
            catch (Exception) { }
            return "Unknown Error";
        }

        private static string CreateRequestString(string SteamIDIn)
        {
            if (DLL.HelperFunctions.IsValidSteamID(SteamIDIn))
            {
                byte[] bArray = new byte[5];
                rnd.NextBytes(bArray);
                bArray[4] = 1;
                long steamID;
                bool status = long.TryParse(SteamIDIn, out steamID);
                if (status && DLL.HelperFunctions.IsValidSteamID(steamID)) return Encoding.ASCII.GetString(bArray) + CreateBEGuid(steamID);

                //Old way to check if key is banned
                return ".gr." + GetMD5Hash("BE" + GetMD5Hash(SteamIDIn));
            }

            return "";
        }

        /// <summary>
        /// Creates BEGUID from steamID
        /// </summary>
        /// <param name="SteamId"></param>
        /// <returns></returns>
        public static string CreateBEGuid(long SteamId)
        {
            byte[] parts = { 0x42, 0x45, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte counter = 2;

            do
            {
                parts[counter++] = (byte)(SteamId & 0xFF);
            } while ((SteamId >>= 8) > 0);

            return GetMD5Hash(parts);
        }

        /// <summary>
        /// Computes MD5 HASH from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        private static string GetMD5Hash(string value) => GetMD5Hash(Encoding.UTF8.GetBytes(value));
        
        /// <summary>
        /// Computes MD5 HASH from bytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        private static string GetMD5Hash(byte[] value)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(value);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
