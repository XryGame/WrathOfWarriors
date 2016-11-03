using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum.Enum;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 请求排行榜数据
    /// </summary>
    public class Action3004 : BaseAction
    {
        private JPRequestRankData receipt;
        private RankType _ranktype;

        public Action3004(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action3004, actionGetter)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("RankType", ref _ranktype))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            receipt = new JPRequestRankData();
            receipt.Type = _ranktype;
            Ranking<UserRank> ranking = null;
            
            switch (_ranktype)
            {
                case RankType.Combat:
                    ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
                    break;
                case RankType.Level:
                    ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                    break;
                case RankType.FightValue:
                    ranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
                    break;
            }
            if (ranking == null)
                return true;

            int rankID = 0;
            UserRank rankInfo = null;
            if (ranking.TryGetRankNo(m => (m.UserID == ContextUser.UserID), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == ContextUser.UserID));
            }

            if (rankInfo != null)
            {
                receipt.SelfRank = rankInfo.RankId;
            }



            int pagecout;
            var list = ranking.GetRange(0, 50, out pagecout);
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
                receipt.List.Add(jpdata);
            }
            return true;
        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action3004;
            }
            return base.BuildJsonPack();
        }

    }
}
