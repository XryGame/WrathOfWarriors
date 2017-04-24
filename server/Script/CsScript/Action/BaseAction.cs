using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.Script.CsScript.Action
{
    public abstract class BaseAction : JsonAuthorizeAction
    {
        private ResultData _resultData;

        protected BaseAction(int aActionId, ActionGetter actionGetter)
            : base(aActionId, actionGetter)
        {
            _resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorInfo = "",
            };
        }

        /// <summary>
        /// 上下文玩家
        /// </summary>
        public UserBasisCache GetBasis
        {
            get
            {
                return UserHelper.FindUserBasis(Current.UserId);
            }
        }

        public UserAttributeCache GetAttribute
        {
            get
            {
                return UserHelper.FindUserAttribute(Current.UserId);
            }
        }

        public UserEquipsCache GetEquips
        {
            get
            {
                return UserHelper.FindUserEquips(Current.UserId);
            }
        }

        public UserPackageCache GetPackage
        {
            get
            {
                return UserHelper.FindUserPackage(Current.UserId);
            }
        }

        public UserSoulCache GetSoul
        {
            get
            {
                return UserHelper.FindUserSoul(Current.UserId);
            }
        }
        public UserSkillCache GetSkill
        {
            get
            {
                return UserHelper.FindUserSkill(Current.UserId);
            }
        }
        public UserFriendsCache GetFriends
        {
            get
            {
                return UserHelper.FindUserFriends(Current.UserId);
            }
        }

        public UserAchievementCache GetAchievement
        {
            get
            {
                return UserHelper.FindUserAchievement(Current.UserId);
            }
        }

        public UserMailBoxCache GetMailBox
        {
            get
            {
                return UserHelper.FindUserMailBox(Current.UserId);
            }
        }

        public UserTaskCache GetTask
        {
            get
            {
                return UserHelper.FindUserTask(Current.UserId);
            }
        }

        public UserPayCache GetPay
        {
            get
            {
                return UserHelper.FindUserPay(Current.UserId);
            }
        }

        public UserCombatCache GetCombat
        {
            get
            {
                return UserHelper.FindUserCombat(Current.UserId);
            }
        }

        public UserEventAwardCache GetEventAward
        {
            get
            {
                return UserHelper.FindUserEventAward(Current.UserId);
            }
        }

        public UserGuildCache GetGuild
        {
            get
            {
                return UserHelper.FindUserGuild(Current.UserId);
            }
        }

        public UserElfCache GetElf
        {
            get
            {
                return UserHelper.FindUserElf(Current.UserId);
            }
        }

        public void setErrorCode(int errorcode)
        {
            _resultData.ErrorCode = errorcode;
        }

        public void setErrorInfo(string errorinfo)
        {
            _resultData.ErrorInfo = errorinfo;
        }
        public object body
        {
            set { _resultData.Data = value; }
        }



        protected override string BuildJsonPack()
        {
            _resultData.intend(ErrorCode, ErrorInfo);
            string retString = MathUtils.ToJson(_resultData);
            return retString;
        }
    }
}
