using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatFightEndData
    {
        public EventStatus Result { get; set; }

        public int CurrRankId { get; set; }

        public int RankRise { get; set; }

        public long LastFailedTime { get; set; }

        public int AwardDiamond { get; set; }

        public int CurrDiamond { get; set; }
    }
}
