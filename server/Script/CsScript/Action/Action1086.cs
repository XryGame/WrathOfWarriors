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
    /// 已领取赠送物品通知
    /// </summary>
    public class Action1086 : BaseAction
    {
        /// <summary>
        /// 新的邮件id
        /// </summary>
        private string receipt;

        public Action1086(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1086, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("ID", ref receipt))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            return true;
        }
    }
}