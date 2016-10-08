using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPReceiveDailyQuestData
    {
        public JPReceiveDailyQuestData()
        {
            New = new JPDailyQuestData();
        }
        public EventStatus Result { get; set; }

        public JPDailyQuestData New { get; set; }

        public TaskAwardType AwardType { get; set; }
        
        public int AwardValue { get; set; }

        public int CurrDiamond { get; set; }

        public int CurrExtendExp { get; set; }

        public List<int> RandGoods { get; set; }

        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

        public int CurrFightValue { get; set; }
    }
}
