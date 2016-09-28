using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1063_切磋取消通知
    /// </summary>
    public class Action1063 : BaseAction
    {
        /// <summary>
        /// 邀请人昵称
        /// </summary>
        private string nickname;

        public Action1063(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1063, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = nickname;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("NickName", ref nickname))
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