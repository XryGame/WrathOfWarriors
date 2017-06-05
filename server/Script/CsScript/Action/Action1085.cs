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
    /// 新的赠送物品通知
    /// </summary>
    public class Action1085 : BaseAction
    {
        /// <summary>
        /// 新的邮件id
        /// </summary>
        private string newid;
        private ReceiveTransferItemData receipt;

        public Action1085(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1085, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1085;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("ID", ref newid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = GetTransfer.FindReceive(newid);
            if (receipt == null)
                return false;
            
            return true;
        }
    }
}