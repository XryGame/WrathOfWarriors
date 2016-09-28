using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1051_钻石数量改变通知接口
    /// </summary>
    public class Action1051 : BaseAction
    {
        private int receipt;
        public Action1051(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1051, actionGetter)
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
            receipt = ContextUser.DiamondNum;
            return true;
        }
    }
}