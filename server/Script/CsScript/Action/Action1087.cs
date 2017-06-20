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
    /// 新增邀请
    /// </summary>
    public class Action1087 : BaseAction
    {
        /// <summary>
        /// 刷新邀请数据 
        /// </summary>
        private int receipt;

        public Action1087(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1087, actionGetter)
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
            receipt = GetBasis.InviteCount;
            return true;
        }
    }
}