using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 8001_回应切磋请求
    /// </summary>
    public class Action8001 : BaseAction
    {
        private int destuid;
        private EventStatus result;
        private Random random = new Random();

        public Action8001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action8001, actionGetter)
        {
            IsNotRespond = true;
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref destuid)
                && httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            GetBasis.UserStatus = UserStatus.MainUi;
            GameSession destSession = GameSession.Get(destuid);
            UserBasisCache destBasis = UserHelper.FindUserBasis(destuid);
            UserAttributeCache destAttribute = UserHelper.FindUserAttribute(destuid);
            if (destSession == null || !destSession.Connected || destBasis == null
                || destBasis.UserStatus != UserStatus.Inviteing || destBasis.InviteFightDestUid != GetBasis.UserID)
            {
                return true;
            }
            if (result == EventStatus.Bad)
            {
                destBasis.UserStatus = UserStatus.MainUi;
                destBasis.InviteFightDestUid = 0;
                PushMessageHelper.RefuseInviteFightNotification(destSession, GetBasis.NickName);
            }
            else
            {// 切磋开始通知
                GetBasis.UserStatus = UserStatus.Fighting;
                destBasis.UserStatus = UserStatus.Fighting;

                EventStatus retresult = EventStatus.Good;
                float diff = (float)GetAttribute.FightValue / destAttribute.FightValue;
                if (diff > 1.1f)
                {
                    retresult = EventStatus.Good;
                }
                else if (diff < 0.9f)
                {
                    retresult = EventStatus.Bad;
                }
                else
                {
                    if (diff >= 1.0f)
                    {
                        retresult = random.Next(1000) < 8000 ? EventStatus.Good : EventStatus.Bad;
                    }
                    else
                    {
                        retresult = random.Next(1000) < 8000 ? EventStatus.Bad : EventStatus.Good;
                    }
                }

                if (retresult == EventStatus.Bad)
                {
                    PushMessageHelper.StartInviteFightNotification(Current, destuid, EventStatus.Bad);
                    PushMessageHelper.StartInviteFightNotification(destSession, GetBasis.UserID, EventStatus.Good);
                }
                else
                {
                    PushMessageHelper.StartInviteFightNotification(Current, destuid, EventStatus.Good);
                    PushMessageHelper.StartInviteFightNotification(destSession, GetBasis.UserID, EventStatus.Bad);
                }

            }
            return true;
        }
    }
}