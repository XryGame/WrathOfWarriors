using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRankUserData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        ///public bool IsOnline { get; set; }

        public int FightValue { get; set; }

        public int VipLv { get; set; }
    }
}
