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
    /// 1080_一个公会成员移除通知
    /// </summary>
    public class Action1080 : BaseAction
    {

        private int receipt;
        private int _MemberUid;
        public Action1080(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1080, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MemberUid", ref _MemberUid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = _MemberUid;
            return true;
        }
    }
}