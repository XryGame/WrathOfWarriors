﻿using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestAccumulatePay
    {
        public JPRequestAccumulatePay()
        {
            AwardItemList = new List<int>();
        }

        public int ReceiveId { get; set; }

        public ReceiveAccumulatePayResult Result { get; set; }
        
        public List<int> AwardItemList { get; set; }

        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }
        
    }
}
