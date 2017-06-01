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
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 通天塔排名
    /// </summary>
    public class Action2000 : BaseAction
    {
        public List<JPRankUserData> receipt;
        private Random random = new Random();

        public Action2000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2000, actionGetter)
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
            receipt = new List<JPRankUserData>();
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);


            int pagecout;
            var list = ranking.GetRange(0, 50, out pagecout);
            foreach (var data in list)
            {
                JPRankUserData jpdata = new JPRankUserData()
                {
                    UserID = data.UserID,
                    NickName = data.NickName,
                    Profession = data.Profession,
                    AvatarUrl = data.AvatarUrl,
                    RankId = data.RankId,
                    UserLv = data.UserLv,
                    FightValue = data.FightValue,
                    VipLv = data.VipLv
                };

                receipt.Add(jpdata);
            }
            return true;
        }
        
    }
}