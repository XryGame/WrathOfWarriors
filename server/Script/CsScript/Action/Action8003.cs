using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 8003_切磋完毕
    /// </summary>
    public class Action8003 : BaseAction
    {
        private EventStatus Result;
       
        public Action8003(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action8003, actionGetter)
        {
            IsNotRespond = true;
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Result", ref Result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            ContextUser.UserStatus = UserStatus.MainUi;
            ContextUser.InviteFightDestUid = 0;


            if (Result == EventStatus.Good && ContextUser.InviteFightDiamondNum < DataHelper.InviteFightDiamondWeekMax)
            {
                int diamond = MathUtils.Subtraction(DataHelper.InviteFightDiamondWeekMax, ContextUser.InviteFightDiamondNum);
                diamond = Math.Min(diamond, DataHelper.InviteFightAwardDiamond);
                ContextUser.InviteFightDiamondNum += diamond;
                UserHelper.GiveAwayDiamond(ContextUser.UserID, diamond);
            }

            // 成就
            UserHelper.AchievementProcess(ContextUser.UserID, 1, AchievementType.InviteFightCount);
            return true;
        }
    }
}