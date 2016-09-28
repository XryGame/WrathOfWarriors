using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
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
        private JPInviteFightRivalData receipt;

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
                ErrorCode = ActionIDDefine.Cst_Action1057;
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
            GameUser dest = UserHelper.FindUser(destuid);
            if (dest == null)
                return false;
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(dest.UserLv);
            if (rolegrade == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
                return true;
            }

            receipt = new JPInviteFightRivalData();
            receipt.AppointResult = fightresult;
            receipt.RivalData.UserId = dest.UserID;
            receipt.RivalData.NickName = dest.NickName;
            receipt.RivalData.LooksId = dest.LooksId;
            receipt.RivalData.UserLv = dest.UserLv;
            receipt.RivalData.VipLv = dest.VipLv;
            receipt.RivalData.Attack = rolegrade.Attack;
            receipt.RivalData.Defense = rolegrade.Defense;
            receipt.RivalData.HP = rolegrade.HP;
            if (dest.ClassData.ClassID > 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(dest.ClassData.ClassID);
                if (classdata != null)
                {
                    receipt.RivalData.ClassName = classdata.Name;
                }
            }
            receipt.RivalData.UserStage = dest.UserStage;
            receipt.RivalData.FightValue = dest.FightingValue;
            receipt.RivalData.IsOnline = dest.IsOnline;
            receipt.RivalData.ItemList = dest.ItemDataList;
            receipt.RivalData.SkillList = dest.SkillDataList;
            receipt.RivalData.SkillCarryList = dest.SkillCarryList;

            return true;
        }
    }
}