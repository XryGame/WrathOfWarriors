using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1401_名人榜前20名
    /// </summary>
    public class Action1406 : BaseAction
    {
        public List<JPRankUserData> receipt;
        private Random random = new Random();

        public Action1406(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1406, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1406;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new List<JPRankUserData>();
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            if (ranking == null)
                return true;
            
            int pagecout;
            var list = ranking.GetRange(0, 20, out pagecout);
            foreach (var data in list)
            {
                JPRankUserData jpdata = new JPRankUserData()
                {
                    UserId = data.UserID,
                    NickName = data.NickName,
                    LooksId = data.LooksId,
                    RankId = data.RankId,
                    UserLv = data.UserLv,
                    IsOnline = data.IsOnline,
                    Exp = data.Exp,
                    FightingValue = data.FightingValue
                };
                receipt.Add(jpdata);
            }
            return true;
        }
        
    }
}