using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class InviterData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int FightValue { get; set; }

        public int VipLv { get; set; }
    }

    /// <summary>
    /// 1062_切磋请求通知
    /// </summary>
    public class Action1062 : BaseAction
    {
        private InviterData receipt;

        /// <summary>
        /// 邀请人UID
        /// </summary>
        private int inviteruid;

        public Action1062(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1062, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1062;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("InviterUid", ref inviteruid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserBasisCache inviter = UserHelper.FindUserBasis(inviteruid);
            if (inviter == null)
                return false;

            receipt = new InviterData()
            {
                UserId = inviter.UserID,
                NickName = inviter.NickName,
                //FightValue = inviter.FightingValue,
                VipLv = inviter.VipLv
            };
            //receipt = id;
            return true;
        }
    }
}