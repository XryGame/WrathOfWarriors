using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestLotteryData
    {
        public bool IsTodayLottery { get; set; }

        public LotteryAwardType LotteryAwardType { get; set; }

        public int LotteryId { get; set; }
    }
}
