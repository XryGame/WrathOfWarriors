using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
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
            body = receipt;
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


                if (ContextUser.ChallengeRoleList.Find(t => (t == monsterId)) == 0)
                {
                    ContextUser.ChallengeRoleList.Add(monsterId);
                }

                //ContextUser.AdditionFightExpValue(role.Exp);

                int addvalue = ContextUser.AdditionFightExpValue(role.Exp);
                if (addvalue > 0)
                {
                    ContextUser.RefreshFightValue();
                }
                    


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
                receipt.ChallengeRoleList = ContextUser.ChallengeRoleList;

                // 每日
                UserHelper.EveryDayTaskProcess(ContextUser.UserID, TaskType.FightTeacher, 1);

                // 成就
                UserHelper.AchievementProcess(ContextUser.UserID, 1, AchievementType.ChallengeCount);
            }

 
            return true;
        }
    }
}