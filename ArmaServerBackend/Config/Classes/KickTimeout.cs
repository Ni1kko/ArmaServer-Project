using System;

namespace ArmaServerBackend
{
    /// <summary>
    /// Kick Timeout Settings https://community.bistudio.com/wiki/server.cfg
    /// </summary>
    public class KickTimeout : Helpers
    {
        /// <summary>
        /// 
        /// </summary>
        public bool enabled { get; set; }

        /// <summary>
        /// kickID (type to determine from where the kick originated e.g. admin or votekick etc.)
        ///0 - manual kick(vote kick, admin kick, bruteforce detection etc.)
        ///1 - connectivity kick(ping, timeout, packetloss, desync)
        ///2 - BattlEye kick
        ///3 - harmless kick(wrong addons, steam timeout or checks, signatures, content etc.)
        /// </summary>
        public KickID type { get; set; }

        /// <summary>
        /// timeout = in seconds how long until kicked player can return 
        /// >0 seconds
        /// -1 until missionEnd
        /// -2 until serverRestart
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_time"></param>
        public KickTimeout(bool _enabled, KickID _type, int _time)
        {
            enabled = _enabled;
            type = _type;
            time = _time;
        }

        /// <summary>
        /// Converts KickTimeout to user friendly string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => enabled ? "{" + Convert.ToInt32(Enum.ToObject(typeof(KickID), type)).ToString() + "," + NewSpace() + time.ToString() + "}" : "";
    }
}
