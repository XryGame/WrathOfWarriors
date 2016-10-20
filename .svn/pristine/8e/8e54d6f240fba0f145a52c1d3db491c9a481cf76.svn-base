using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPReceiveAchievementData
    {
        public JPReceiveAchievementData()
        {
            AwardItemList = new List<int>();
        }
        public EventStatus Result { get; set; }
        
        public int AwardDiamondNum { get; set; }

        public int CurrDiamond { get; set; }

        public List<int> AwardItemList { get; set; }
        
        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

        public AchievementData NewAchievement { get; set; }

    }
}
