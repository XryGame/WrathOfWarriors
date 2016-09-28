using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRankUserData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int LooksId { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public bool IsOnline { get; set; }

        public int Exp { get; set; }

        public int FightingValue { get; set; }
    }
}
