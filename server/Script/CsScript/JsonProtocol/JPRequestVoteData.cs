using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestVoteData
    {
        public RequestVoteResult Result { get; set; }

        public int DestUid { get; set; }

        public int CampaignsUserVoteNum { get; set; }

        public int CurrVoteNum { get; set; }

        public int CurrCanBuyVoteNum { get; set; }

        public int CurrDiamond { get; set; }
    }
}
