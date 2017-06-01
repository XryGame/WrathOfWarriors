using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{



    /// <summary>
    /// 1402_通天塔请求挑战
    /// </summary>
    public class Action1402 : BaseAction
    {
        private CombatRivalData receipt;
        private int rivaluid;

        public Action1402(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1402, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1402;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("RivalUid", ref rivaluid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);

            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;

            rankinfo = UserHelper.FindRankUser(Current.UserId, RankType.Combat);
            rivalrankinfo = UserHelper.FindRankUser(rivaluid, RankType.Combat);

            if (rankinfo == null || rivalrankinfo == null)
            {
                int erroruid = rankinfo == null ? Current.UserId : rivaluid;
                new BaseLog("Action1402").SaveLog(string.Format("Not found user combat rank. UserId={0}", erroruid));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            receipt = new CombatRivalData();
            receipt.Result = CombatReqRivalResult.OK;
            if (rankinfo.RankId <= rivalrankinfo.RankId)
            {
                receipt.Result = CombatReqRivalResult.RankOverdue;
                return true;
            }
            if (rivalrankinfo.IsFighting)
            {
                receipt.Result = CombatReqRivalResult.RivalIsFinging;
                return true;
            }
            if (rankinfo.IsFighting)
            {
                receipt.Result = CombatReqRivalResult.SelfIsFinging;
                return true;
            }
            if (GetCombat.CombatTimes <= 0)
            {
                receipt.Result = CombatReqRivalResult.NoTimes;
                return true;
            }
            
            UserBasisCache rival = UserHelper.FindUserBasis(rivaluid);
            if (rival == null)
            {
                ErrorInfo = Language.Instance.NoFoundUser;
                return true;
            }


            GetBasis.UserStatus = UserStatus.Fighting;
            GetCombat.CombatTimes = MathUtils.Subtraction(GetCombat.CombatTimes, 1, 0);
            rankinfo.IsFighting = true;
            rankinfo.FightDestUid = rivaluid;
            rivalrankinfo.IsFighting = true;
            /////rivalrankinfo.FightDestUid = Current.UserId;

            receipt.UserId = rivaluid;
            receipt.NickName = rivalrankinfo.NickName;
            receipt.Profession = rivalrankinfo.Profession;
            receipt.AvatarUrl = rivalrankinfo.AvatarUrl;
            receipt.RankId = rivalrankinfo.RankId;
            receipt.UserLv = rivalrankinfo.UserLv;
            receipt.Equips = UserHelper.FindUserEquips(rivaluid);
            receipt.Attribute = UserHelper.FindUserAttribute(rivaluid);
            receipt.Skill = UserHelper.FindUserSkill(rivaluid);
            return true;
        }

    }
}