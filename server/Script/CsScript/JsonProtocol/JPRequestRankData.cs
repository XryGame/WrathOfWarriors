using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestRankData
    {
        public JPRequestRankData()
        {
            List = new List<JPRankUserData>();
        }
        public RankType Type { get; set; }

        public int SelfRank { get; set; }

        public List<JPRankUserData> List;
        

    }
}
