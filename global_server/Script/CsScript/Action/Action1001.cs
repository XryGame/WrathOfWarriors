using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class LevelRankingTop50Data
    {
        public LevelRankingTop50Data()
        {
            List = new List<UserRank>();
        }
        public int SelfRank { get; set; }

        public List<UserRank> List { get; set; }
    }
    /// <summary>
    /// 全服等级排行榜
    /// </summary>
    public class Action1001 : BaseAction
    {

        LevelRankingTop50Data receipt = null;
        public Action1001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1001, actionGetter)
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
            receipt = new LevelRankingTop50Data();

            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            UserRank rankInfo = null;
            int rankID = 0;
            if (ranking.TryGetRankNo(m => (m.UserID == Current.UserId), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == Current.UserId));
            }

            if (rankInfo != null)
            {
                receipt.SelfRank = rankInfo.RankId;
            }

            int pagecout;
            var list = ranking.GetRange(0, 50, out pagecout);
            foreach (var data in list)
            {
                UserRank rank = new UserRank(data);
                receipt.List.Add(rank);
            }
            return true;
        }
    }
}