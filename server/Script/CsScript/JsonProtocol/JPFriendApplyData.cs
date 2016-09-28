using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPFriendApplyData
    {

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int LooksId { get; set; }

        public int UserLv { get; set; }

        public int FightValue { get; set; }

        public int VipLv { get; set; }

        public long ApplyTime { get; set; }

        public bool IsOnline { get; set; }

    }
}
