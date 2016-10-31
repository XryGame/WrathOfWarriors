using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatLogData
    {

        public int UserId { get; set; }

        public int RivalCurrRankId { get; set; }

        public EventType Type { get; set; }

        public EventStatus FightResult { get; set; }

        public string Log { get; set; }
    }
}
