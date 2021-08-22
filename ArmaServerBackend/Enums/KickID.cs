namespace ArmaServerBackend
{ 
    public enum KickID
    {
        /// <summary>
        /// 0 - manual kick(vote kick, admin kick, bruteforce detection etc.)
        /// </summary>
        manual,

        /// <summary>
        /// 1 - connectivity kick(ping, timeout, packetloss, desync)
        /// </summary>
        connectivity,

        /// <summary>
        /// 2 - BattlEye kick
        /// </summary>
        BattlEye,

        /// <summary>
        /// /3 - harmless kick(wrong addons, steam timeout or checks, signatures, content etc.)
        /// </summary>
        harmless
    }
}
