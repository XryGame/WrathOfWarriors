namespace GameServer.CsScript.JsonProtocol
{
    public class JPFriendData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }

        public int VipLv { get; set; }
        
        public int FightValue { get; set; }

        public bool IsOnline { get; set; }

        public bool IsGiveAway { get; set; }

        public bool IsByGiveAway { get; set; }

        public bool IsReceiveGiveAway { get; set; }

    }
}
