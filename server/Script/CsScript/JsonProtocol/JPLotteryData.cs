using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPLotteryData
    {
        public RequestLotteryResult Result { get; set; }

        public LotteryAwardType Type { get; set; }

        public int AwardItemId { get; set; }

        public int AwardNum { get; set; }

        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

    }
}
