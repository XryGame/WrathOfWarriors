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

    public class RobRivalData
    {
        public RobRivalData()
        {

        }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public string AvatarUrl { get; set; }

        public int LevelRankID { get; set; }

        public int UserLv { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSkillCache Skill { get; set; }

        public int ElfID { get; set; }

        public bool IsAutoFight { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class Action1815 : BaseAction
    {
        /// <summary>
        ///
        /// </summary>
        private int destuid;
        private RobRivalData receipt;

        public Action1815(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1815, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref destuid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserBasisCache dest = UserHelper.FindUserBasis(destuid);

            receipt = new RobRivalData();
            receipt.UserId = dest.UserID;
            receipt.NickName = dest.NickName;
            receipt.Profession = dest.Profession;
            receipt.AvatarUrl = dest.AvatarUrl;
            receipt.UserLv = dest.UserLv;
            //receipt.VipLv = dest.VipLv;
            receipt.Equips = UserHelper.FindUserEquips(destuid);
            receipt.Attribute = UserHelper.FindUserAttribute(destuid);
            receipt.Skill = UserHelper.FindUserSkill(destuid);
            receipt.LevelRankID = dest.LevelRankID;
            receipt.ElfID = UserHelper.FindUserElf(destuid).SelectID;
            var pay = UserHelper.FindUserPay(destuid);
            receipt.IsAutoFight = pay.MonthCardDays >= 0 || pay.QuarterCardDays >= 0;


            return true;
        }
    }
}