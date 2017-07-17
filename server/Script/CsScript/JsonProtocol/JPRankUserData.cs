using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRankUserData
    {
        public int UserID { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public string AvatarUrl { get; set; }

        public long FightValue { get; set; }

        public int VipLv { get; set; }

        public int ComboNum { get; set; }
    }
}
