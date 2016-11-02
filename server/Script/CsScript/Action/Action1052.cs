using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1052_等级改变通知接口
    /// </summary>
    public class Action1052 : BaseAction
    {
        private JPLevelUpData receipt;
        private bool _isChangeClass;
        public Action1052(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1052, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }

            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetBool("IsChangeClass", ref _isChangeClass))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPLevelUpData()
            {
                CurrLevel = ContextUser.UserLv,
                IsChangeClass = _isChangeClass
            };
            return true;
        }
    }
}