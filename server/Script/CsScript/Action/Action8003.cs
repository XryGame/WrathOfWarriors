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
            GetBasis.UserStatus = UserStatus.MainUi;
            GetBasis.InviteFightDestUid = 0;


            if (Result == EventStatus.Good)
            {
                int gold = DataHelper.InviteFightAwardGold;
                UserHelper.RewardsGold(Current.UserId, gold);
            }

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.FriendCompare, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.FriendCompare, "1");
            return true;
        }
    }
}