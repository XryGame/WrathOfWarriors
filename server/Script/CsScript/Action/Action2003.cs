using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 请求公会排行榜数据
    /// </summary>
    public class Action2003 : BaseAction
    {
        private List<GuildRank> receipt;

        public Action2003(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2003, actionGetter)
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
            receipt = new List<GuildRank>();
            
            
            int pagecout;
            var ranking = RankingFactory.Get<GuildRank>(GuildRanking.RankingKey);
            var list = ranking.GetRange(0, 30, out pagecout);
            foreach (var data in list)
            {
                receipt.Add(data);
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
