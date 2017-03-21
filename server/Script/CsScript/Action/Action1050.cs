using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1050_属性改变通知接口
    /// </summary>
    public class Action1050 : BaseAction
    {
        public Action1050(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1050, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = GetAttribute;
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