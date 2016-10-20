using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1066_新的邮件通知
    /// </summary>
    public class Action1066 : BaseAction
    {
        /// <summary>
        /// 新的邮件id
        /// </summary>
        private string newid;
        private MailData receipt;

        public Action1066(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1066, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1066;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("NewMailId", ref newid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = ContextUser.findMail(newid);
            if (receipt == null)
                return false;
            
            return true;
        }
    }
}