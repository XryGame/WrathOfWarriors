using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1058_竞选结果通知接口
    /// </summary>
    public class Action1058 : BaseAction
    {
        private object receipt;
        private int Uid;
        public Action1058(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1058, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1058;
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