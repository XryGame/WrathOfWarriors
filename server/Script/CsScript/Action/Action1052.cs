using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 等级改变通知接口
    /// </summary>
    public class Action1052 : BaseAction
    {
        public Action1052(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1052, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = GetBasis.UserLv;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}