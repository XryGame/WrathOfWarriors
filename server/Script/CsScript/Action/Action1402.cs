using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1402_名人榜请求挑战
    /// </summary>
    public class Action1402 : BaseAction
    {
        private JPCombatRivalData receipt;
        private int rivaluid;
        private int mapid;

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
            if (httpGet.GetInt("RivalUid", ref rivaluid)
                && httpGet.GetInt("MapId", ref mapid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            if (ranking.TryGetRankNo(m => m.UserID == GetBasis.UserID, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == GetBasis.UserID);
            }
            if (ranking.TryGetRankNo(m => m.UserID == rivaluid, out rankID))
            {
                rivalrankinfo = ranking.Find(s => s.UserID == rivaluid);
            }
            if (rankinfo == null || rivalrankinfo == null)
            {
                int erroruid = rankinfo == null ? GetBasis.UserID : rivaluid;
                new BaseLog("Action1402").SaveLog(string.Format("Not found user combat rank. UserId={0}", erroruid));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            receipt = new JPCombatRivalData();
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
            //Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(rivalrankinfo.UserLv);
            //if (rolegrade == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
            //    return true;
            //}

            GetBasis.UserStatus = UserStatus.Fighting;
            GetCombat.CombatTimes = MathUtils.Subtraction(GetCombat.CombatTimes, 1, 0);
            rankinfo.IsFighting = true;
            rankinfo.FightDestUid = rivaluid;
            rivalrankinfo.IsFighting = true;
            /////rivalrankinfo.FightDestUid = GetBasis.UserID;

            receipt.UserId = rivaluid;
            receipt.NickName = rivalrankinfo.NickName;
            receipt.Profession = rivalrankinfo.Profession;
            receipt.RankId = rivalrankinfo.RankId;
            receipt.UserLv = rivalrankinfo.UserLv;
            //receipt.FightingValue = rivalrankinfo.FightingValue;
            //receipt.Attack = rival.Attack;
            //receipt.Defense = rival.Defense;
            //receipt.HP = rival.Hp;

            return true;
        }

    }
}