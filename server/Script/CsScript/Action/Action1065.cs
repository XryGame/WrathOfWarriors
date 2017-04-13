using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class InviteFightRivalData
    {
        public InviteFightRivalData()
        {

        }

        public EventStatus FightResult { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSkillCache Skill { get; set; }


    }


    /// <summary>
    /// 1065_开始切磋通知
    /// </summary>
    public class Action1065 : BaseAction
    {
        /// <summary>
        /// 切磋对象Uid
        /// </summary>
        private int destuid;
        private EventStatus fightresult;
        private InviteFightRivalData receipt;

        public Action1065(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1065, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1065;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref destuid)
                && httpGet.GetEnum("FightResult", ref fightresult))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserBasisCache dest = UserHelper.FindUserBasis(destuid);

            //Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(dest.UserLv);
            //if (rolegrade == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
            //    return true;
            //}

            receipt = new InviteFightRivalData();
            receipt.FightResult = fightresult;
            receipt.UserId = dest.UserID;
            receipt.NickName = dest.NickName;
            receipt.Profession = dest.Profession;
            receipt.UserLv = dest.UserLv;
            //receipt.VipLv = dest.VipLv;
            receipt.Equips = UserHelper.FindUserEquips(destuid);
            receipt.Attribute = UserHelper.FindUserAttribute(destuid);
            receipt.Skill = UserHelper.FindUserSkill(destuid);
            //receipt.RivalData.RankId = dest.CombatRankID;

            //receipt.RivalData.FightValue = dest.FightingValue;
            //GameSession fsession = GameSession.Get(dest.UserID);
            //if (fsession != null && fsession.Connected)
            //    receipt.RivalData.IsOnline = true;

            return true;
        }
    }
}