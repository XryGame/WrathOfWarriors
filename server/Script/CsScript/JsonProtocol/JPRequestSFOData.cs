using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestSFOData
    {
        public JPRequestSFOData()
        {
            AwardItemList = new List<int>();
        }
        public EventStatus Result { get; set; }

        public int ReturnId { get; set; }

        public int AwardDiamondNum { get; set; }

        public int CurrDiamond { get; set; }

        public List<int> AwardItemList { get; set; }
        
        
    }
}
