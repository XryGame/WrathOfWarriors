using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1403_名人榜挑战结果
    /// </summary>
    public class Action1403 : BaseAction
    {
        private JPCombatFightEndData receipt;
        private EventStatus result;

        public Action1403(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1403, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1403;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            if (ranking.TryGetRankNo(m => m.UserID == ContextUser.UserID, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == ContextUser.UserID);
            }
            if (rankinfo == null)
            {
                new BaseLog("Action1403").SaveLog(string.Format("Not found user combat rank. UserId={0}", ContextUser.UserID));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }
            if (ranking.TryGetRankNo(m => m.UserID == rankinfo.FightDestUid, out rankID))
            {
                rivalrankinfo = ranking.Find(s => s.UserID == rankinfo.FightDestUid);
            }
            if (rivalrankinfo == null)
            {
                new BaseLog("Action1403").SaveLog(string.Format("Not found user combat rank. UserId={0}", rankinfo.FightDestUid));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            if (rankinfo.RankId <= rivalrankinfo.RankId)
            {
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }
            if (!rankinfo.IsFighting || !rivalrankinfo.IsFighting)
            {
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            int fromid = rankinfo.RankId;
            int toid = rivalrankinfo.RankId;

            ContextUser.UserStatus = UserStatus.MainUi;
            if (result == EventStatus.Good)
            {
                if (ranking.TryMove(fromid, toid))
                {
                    rankinfo = ranking.Find(s => s.UserID == ContextUser.UserID);
                }
                //rivalrankinfo = ranking.Find(s => s.UserID == rankinfo.FightDestUid);
                if (ranking.TryMove(rivalrankinfo.RankId, fromid))
                {
                    rivalrankinfo = ranking.Find(s => s.UserID == rankinfo.FightDestUid);
                }
            }
            else
            {
                ContextUser.CombatData.LastFailedDate = DateTime.Now;
            }

            int rankrise = result == EventStatus.Good ? MathUtils.Subtraction(fromid, toid, 0) : 0;



            // 日志
            CombatLogData log = new CombatLogData();
            log.UserId = rankinfo.FightDestUid;
            log.LogTime = DateTime.Now;
            log.Type = EventType.Challenge;
            log.Status = result;
            log.RankIdDiff = rankrise;
            log.RankId = rankinfo.RankId;
            ContextUser.PushCombatLog(ref log);

            GameUser rival = UserHelper.FindUser(rankinfo.FightDestUid);
            if (rival != null)
            {
                log = new CombatLogData();
                log.UserId = ContextUser.UserID;
                log.LogTime = DateTime.Now;
                log.Type = EventType.PassiveChallenge;
                log.Status = result;
                log.RankIdDiff = rankrise;
                log.RankId = rivalrankinfo.RankId;
                rival.PushCombatLog(ref log);
            }
            
            rankinfo.IsFighting = false;
            rankinfo.FightDestUid = 0;
            rivalrankinfo.IsFighting = false;

            receipt = new JPCombatFightEndData();
            receipt.Result = result;
            receipt.CurrRankId = rankinfo.RankId;
            receipt.RankRise = rankrise;
            receipt.LastFailedTime = Util.ConvertDateTimeStamp(ContextUser.CombatData.LastFailedDate);
            if (result == EventStatus.Good)
            {
                receipt.AwardDiamond = ConfigEnvSet.GetInt("User.CombatWinAward");
                UserHelper.GiveAwayDiamond(ContextUser.UserID, receipt.AwardDiamond);
            }
            receipt.CurrDiamond = ContextUser.DiamondNum;


            // 每日
            UserHelper.EveryDayTaskProcess(ContextUser.UserID, TaskType.CombatFight, 1);


            return true;
        }

    }
}