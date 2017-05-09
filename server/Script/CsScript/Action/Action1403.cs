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
using GameServer.Script.Model.Enum.Enum;
using System;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1403_竞技场挑战结果
    /// </summary>
    public class Action1403 : BaseAction
    {
        private CombatFightEndData receipt;
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
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            

            rankinfo = UserHelper.FindRankUser(Current.UserId, RankType.Combat);
            if (rankinfo == null)
            {
                new BaseLog("Action1403").SaveLog(string.Format("Not found user combat rank. UserId={0}", Current.UserId));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }
            UserBasisCache rival = UserHelper.FindUserBasis(rankinfo.FightDestUid);
            if (rival == null)
            {
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            rivalrankinfo = UserHelper.FindRankUser(rankinfo.FightDestUid, RankType.Combat);
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


            
            int fromRankId = GetBasis.CombatRankID;
            int toRankId = rival.CombatRankID;
            //TraceLog.WriteLine(string.Format("#BEGIN srcId:[{0}] destId:[{1}]", fromid, toid));
            GetBasis.UserStatus = UserStatus.MainUi;
            if (result == EventStatus.Good)
            {
                var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
                ranking.TryMove(fromRankId, toRankId);
                GetBasis.CombatRankID = toRankId;
                rival.CombatRankID = fromRankId;

                if (GetBasis.CombatRankID <= 10)
                {
                    string context = string.Format("恭喜 {0} 挑战 {1} 成功，成为竞技场第{1}名！", GetBasis.NickName, rival.NickName, rankinfo.RankId);
                    GlobalRemoteService.SendNotice(NoticeMode.World, context);
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

            int rankrise = result == EventStatus.Good ? MathUtils.Subtraction(fromRankId, toRankId, 0) : 0;

            //TraceLog.WriteLine(string.Format("###END srcId:[{0}] destId:[{1}]", GetBasis.CombatData.RankID, rival.CombatData.RankID));

            // 日志
            CombatLogData log = new CombatLogData();
            log.RivalUid = rankinfo.FightDestUid;
            log.RivalName = rival.NickName;
            log.LogTime = DateTime.Now;
            log.Type = EventType.Challenge;
            log.Status = result;
            log.RankIdDiff = rankrise;
            log.RankId = GetBasis.CombatRankID;
            GetCombat.PushCombatLog(log);

            string content = UserHelper.FormatCombatLog(log);
            GlobalRemoteService.SendSystemChat(Current.UserId, content);


            CombatLogData rivallog = new CombatLogData();
            rivallog.RivalUid = Current.UserId;
            rivallog.RivalName = GetBasis.NickName;
            rivallog.LogTime = DateTime.Now;
            rivallog.Type = EventType.PassiveChallenge;
            rivallog.Status = result;
            rivallog.RankIdDiff = rankrise;
            rivallog.RankId = rival.CombatRankID;
            UserHelper.FindUserCombat(rival.UserID).PushCombatLog(rivallog);

            content = UserHelper.FormatCombatLog(rivallog);
            GlobalRemoteService.SendSystemChat(rival.UserID, content);

            rankinfo.IsFighting = false;
            rankinfo.FightDestUid = 0;
            rivalrankinfo.IsFighting = false;

            receipt = new CombatFightEndData();
            receipt.Result = result;
            receipt.CurrRankId = GetBasis.CombatRankID;
            receipt.RankRise = rankrise;
            receipt.LastFailedTime = Util.ConvertDateTimeStamp(GetCombat.LastFailedDate);
            if (result == EventStatus.Good)
            {
                receipt.AwardDiamond = ConfigEnvSet.GetInt("User.CombatWinAward");
                UserHelper.RewardsDiamond(Current.UserId, receipt.AwardDiamond);
            }
            receipt.CurrDiamond = GetBasis.DiamondNum;


            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.Combat, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.CombatRandID);

            PushMessageHelper.NewCombatLogNotification(Current);
            PushMessageHelper.NewCombatLogNotification(GameSession.Get(rival.UserID));
            return true;
        }
        

    }
}