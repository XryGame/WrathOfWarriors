using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
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
using ZyGames.Framework.Game.Model;
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
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            if (ranking.TryGetRankNo(m => m.UserID == GetBasis.UserID, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == GetBasis.UserID);
            }
            if (rankinfo == null)
            {
                new BaseLog("Action1403").SaveLog(string.Format("Not found user combat rank. UserId={0}", GetBasis.UserID));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }
            UserBasisCache rival = UserHelper.FindUserBasis(rankinfo.FightDestUid);
            if (rival == null)
            {
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

            if (GetBasis.CombatRankID <= rival.CombatRankID)
            {
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }
            if (!rankinfo.IsFighting || !rivalrankinfo.IsFighting)
            {
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }


            int fromid = GetBasis.CombatRankID;
            int toid = rival.CombatRankID;
            //TraceLog.WriteLine(string.Format("#BEGIN srcId:[{0}] destId:[{1}]", fromid, toid));
            GetBasis.UserStatus = UserStatus.MainUi;
            if (result == EventStatus.Good)
            {
                ranking.TryMove(fromid, toid);
                GetBasis.CombatRankID = toid;
                rival.CombatRankID = fromid;

                if (GetBasis.CombatRankID <= 10)
                {
                    string context = string.Format("恭喜 {0} 成为名人榜排名第{1}名，引来众人羡煞的目光！", GetBasis.NickName, rankinfo.RankId);
                    ChatRemoteService.SendNotice(NoticeMode.World, context);
                    //PushMessageHelper.SendNoticeToOnlineUser(NoticeMode.Game, context);

                    //var chatService = new TryXChatService();
                    //chatService.SystemSend(context);
                    //PushMessageHelper.SendSystemChatToOnlineUser();
                }

        }
            else
            {
                GetCombat.LastFailedDate = DateTime.Now;
            }

            int rankrise = result == EventStatus.Good ? MathUtils.Subtraction(fromid, toid, 0) : 0;

            //TraceLog.WriteLine(string.Format("###END srcId:[{0}] destId:[{1}]", GetBasis.CombatData.RankID, rival.CombatData.RankID));

            // 日志
            CombatLogData log = new CombatLogData();
            log.UserId = rankinfo.FightDestUid;
            log.LogTime = DateTime.Now;
            log.Type = EventType.Challenge;
            log.Status = result;
            log.RankIdDiff = rankrise;
            log.RankId = GetBasis.CombatRankID;
            GetCombat.PushCombatLog(log);


            CombatLogData rivallog = new CombatLogData();
            rivallog.UserId = GetBasis.UserID;
            rivallog.LogTime = DateTime.Now;
            rivallog.Type = EventType.PassiveChallenge;
            rivallog.Status = result;
            rivallog.RankIdDiff = rankrise;
            rivallog.RankId = rival.CombatRankID;
            UserHelper.FindUserCombat(rival.UserID).PushCombatLog(rivallog);


            rankinfo.IsFighting = false;
            rankinfo.FightDestUid = 0;
            rivalrankinfo.IsFighting = false;

            receipt = new JPCombatFightEndData();
            receipt.Result = result;
            receipt.CurrRankId = GetBasis.CombatRankID;
            receipt.RankRise = rankrise;
            receipt.LastFailedTime = Util.ConvertDateTimeStamp(GetCombat.LastFailedDate);
            if (result == EventStatus.Good)
            {
                receipt.AwardDiamond = ConfigEnvSet.GetInt("User.CombatWinAward");
                UserHelper.RewardsDiamond(GetBasis.UserID, receipt.AwardDiamond);
            }
            receipt.CurrDiamond = GetBasis.DiamondNum;


            // 每日
            UserHelper.EveryDayTaskProcess(GetBasis.UserID, TaskType.Combat, 1);

            // 成就
            UserHelper.AchievementProcess(GetBasis.UserID, AchievementType.CombatRandID);

            return true;
        }

    }
}