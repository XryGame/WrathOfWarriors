using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 
    /// </summary>
    public class Action1010 : BaseAction
    {

        public Action1010(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1010, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = null;
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