using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPPaySucceedData
    {
        public int VipLv { get; set; }

        public int Diamond { get; set; }
        
        public int BuyDiamond { get; set; }

        public int PayMoney { get; set; }

        public int QuarterCardDays { get; set; }

        public int MonthCardDays { get; set; }
    }
}
