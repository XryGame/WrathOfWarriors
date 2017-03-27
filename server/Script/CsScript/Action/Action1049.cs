using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1049_金币数量改变通知接口
    /// </summary>
    public class Action1049 : BaseAction
    {
        private string receipt;
        public Action1049(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1049, actionGetter)
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
            receipt = GetBasis.Gold;
            return true;
        }
    }
}