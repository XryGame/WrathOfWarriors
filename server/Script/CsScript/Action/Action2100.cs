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
    /// 请求体力数据
    /// </summary>
    public class Action2100 : BaseAction
    {
        private JPVitData receipt;

        public Action2100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2100, actionGetter)
        {

        }


        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
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
            receipt = new JPVitData();

            receipt.RemainTimeSec = GetBasis.RestoreVitRemainTimeSec();
            receipt.Vit = GetBasis.Vit;
            return true;
        }


    }
}
