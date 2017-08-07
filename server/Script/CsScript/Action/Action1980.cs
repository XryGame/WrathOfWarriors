using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 领取排行榜奖励 
    /// </summary>
    public class Action1980 : BaseAction
    {
        private bool receipt;
        private RankType type;
        public Action1980(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1980, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("RankType", ref type))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = false;
            UserRankAward rankAward = null;
            switch (type)
            {
                case RankType.Level:
                    rankAward = DataHelper.LevelRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
                    break;
                case RankType.FightValue:
                    rankAward = DataHelper.FightValueRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
                    break;
                case RankType.Combo:
                    rankAward = DataHelper.ComboRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
                    break;
            }
            
            if (rankAward == null)
            {
                return false;
            }

            if (rankAward.IsReceived)
            {
                return true;
            }

            DateTime openServiceDate = DataHelper.OpenServiceDate;
            DateTime temp = new DateTime(openServiceDate.Year, openServiceDate.Month, openServiceDate.Day);
            TimeSpan ts = DateTime.Now.Subtract(temp);
            int days = Math.Floor(ts.TotalDays).ToInt() + 1;


            var rankingCfg = new ShareCacheStruct<Config_Ranking>().Find(t => (t.Type == type && t.Days == days));
            if (rankingCfg == null)
                return false;

            rankAward.IsReceived = true;
            DataHelper.UpdateRankingAwardCache();

            List<ItemData> itemlist = new List<ItemData>();
            itemlist.Add(new ItemData() { ID = rankingCfg.AAwardID, Num = rankingCfg.AAwardN });
            itemlist.Add(new ItemData() { ID = rankingCfg.BAwardID, Num = rankingCfg.BAwardN });
            itemlist.Add(new ItemData() { ID = rankingCfg.CAwardID, Num = rankingCfg.CAwardN });
            itemlist.Add(new ItemData() { ID = rankingCfg.DAwardID, Num = rankingCfg.DAwardN });

            UserHelper.RewardsItems(Current.UserId, itemlist);

            GetBasis.ReceiveRankingAwardList.Add(rankingCfg.ID);


            receipt = true;
            return true;
        }
    }
}