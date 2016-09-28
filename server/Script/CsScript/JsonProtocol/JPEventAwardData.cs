using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPEventAwardData
    {

        public int SignCount { get; set; }


        public bool IsTodaySign { get; set; }


        public int FirstWeekCount { get; set; }

        public bool IsTodayReceiveFirstWeek { get; set; }


        public int TodayOnlineTime { get; set; }

        public int OnlineAwardId { get; set; }
    }
}
