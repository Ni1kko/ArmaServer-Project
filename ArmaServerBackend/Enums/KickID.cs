namespace ArmaServerBackend
{ 
    public enum KickID
    {
        manual,         //0 - manual kick(vote kick, admin kick, bruteforce detection etc.)
        connectivity,   //1 - connectivity kick(ping, timeout, packetloss, desync)
        BattlEye,       //2 - BattlEye kick
        harmless        //3 - harmless kick(wrong addons, steam timeout or checks, signatures, content etc.)
    }
}
