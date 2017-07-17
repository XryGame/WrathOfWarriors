using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum.Enum;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 请求连击排行榜数据
    /// </summary>
    public class Action2004 : BaseAction
    {
        private JPRequestRankData receipt;

        public Action2004(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2004, actionGetter)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            return true;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            receipt = new JPRequestRankData();


            UserRank rankInfo = UserHelper.FindRankUser(Current.UserId, RankType.Combo);
            if (rankInfo != null)
            {
                receipt.SelfRank = rankInfo.RankId;
            }

            int pagecout;
            var ranking = RankingFactory.Get<UserRank>(ComboRanking.RankingKey);
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
                    VipLv = data.VipLv,
                    ComboNum = data.ComboNum
                };
                receipt.List.Add(jpdata);
            }
            return true;
        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

    }
}
