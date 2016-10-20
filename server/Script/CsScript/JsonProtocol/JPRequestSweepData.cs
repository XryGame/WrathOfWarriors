using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestSweepData
    {
        public EventStatus Result { get; set; }

        public int CurrVit { get; set; }

        public int SweepingRoleId { get; set; }

        public int SweepTimes { get; set; }

        public long StartSweepTime { get; set; }

    }
}
