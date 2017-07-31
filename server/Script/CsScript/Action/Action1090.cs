using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class Hour12
    {
        public Hour12()
        {
            RankAwardData = new JPRankAwardData();
        }
        public JPRankAwardData RankAwardData { get; set; }

    }

    /// <summary>
    /// 1053_整点刷新通知接口
    /// </summary>
    public class Action1090 : BaseAction
    {
        private Hour12 receipt;
        public Action1090(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1090, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {


            receipt = new Hour12();

            UserRankAward rankAward = DataHelper.LevelRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
            if (rankAward != null)
            {
                receipt.RankAwardData.LevelRankID = rankAward.RankId;
                receipt.RankAwardData.IsReceivedLevel = rankAward.IsReceived;
            }
            rankAward = DataHelper.FightValueRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
            if (rankAward != null)
            {
                receipt.RankAwardData.FightValueRankID = rankAward.RankId;
                receipt.RankAwardData.IsReceivedFightValue = rankAward.IsReceived;
            }
            rankAward = DataHelper.ComboRankingAwardCacheList.Find(t => t.UserID == Current.UserId);
            if (rankAward != null)
            {
                receipt.RankAwardData.ComboRankID = rankAward.RankId;
                receipt.RankAwardData.IsReceivedCombo = rankAward.IsReceived;
            }

            return true;
        }
    }
}