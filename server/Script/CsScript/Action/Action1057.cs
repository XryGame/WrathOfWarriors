using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1057_赠送通知接口
    /// </summary>
    public class Action1057 : BaseAction
    {
        private object receipt;
        private int Uid;
        public Action1057(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1057, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1057;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Uid", ref Uid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            
            receipt = Uid;

            return true;
        }
    }
}