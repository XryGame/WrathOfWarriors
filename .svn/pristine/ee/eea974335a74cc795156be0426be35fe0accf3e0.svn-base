using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1111_挑战结果
    /// </summary>
    public class Action1111 : BaseAction
    {
        private JPReceiveTaskAwardData receipt;
        private int monsterId;
        private EventStatus result;

        public Action1111(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1111, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1111;
            }

            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MonsterId", ref monsterId)
                && httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            Config_Role role = new ShareCacheStruct<Config_Role>().FindKey(monsterId);
            if (role == null)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            ContextUser.UserStatus = UserStatus.MainUi;
            if (result == EventStatus.Good)
            {
                ContextUser.AdditionFightExpValue(role.Exp);

                int addvalue = ContextUser.AdditionFightExpValue(role.Exp);

                receipt = new JPReceiveTaskAwardData()
                {
                    AwardExp = addvalue,
                    CurrFightExp = ContextUser.FightExp,
                    CurrLv = ContextUser.UserLv,
                    CurrFightValue = ContextUser.FightingValue
                };
                object outexpdata;
                UserHelper.buildBaseExpData(ContextUser, out outexpdata);
                receipt.CurrBaseExp = outexpdata;

                // 每日
                if (ContextUser.DailyQuestData.ID == TaskType.FightTeacher)
                {
                    ContextUser.DailyQuestData.IsFinish = true;
                    PushMessageHelper.DailyQuestFinishNotification(Current);
                }

                // 成就
                UserHelper.AchievementProcess(ContextUser.UserID, 1, AchievementType.ChallengeCount);
            }

 
            return true;
        }
    }
}