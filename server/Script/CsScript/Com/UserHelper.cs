﻿using System;
using GameServer.Script.Model.Enum;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Com.Rank;
using GameServer.Script.Model.Config;
using GameServer.CsScript.Com;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.ConfigModel;
using GameServer.CsScript.Base;
using ZyGames.Framework.Script;
using ZyGames.Framework.Common.Timing;
using System.Collections.Generic;
using GameServer.CsScript.Action;
using ZyGames.Framework.RPC.Sockets;
using ZyGames.Framework.Game.Contract;
using GameServer.Script.CsScript.Com;
using ZyGames.Framework.Common;

namespace GameServer.Script.Model.DataModel
{
    public static class UserHelper
    {
        static private Random random = new Random();

        static int scount = 0;
        static UserHelper()
        {

        }

        public static GameUser FindUser(int userid)
        {
            return new PersonalCacheStruct<GameUser>().FindKey(userid.ToString());
        }

        public static void RestoreUserData(int uid, bool islogin = true)
        {
            GameUser gameUser = FindUser(uid);
            if (gameUser == null)
            {
                return;
            }
            // 竞技场挑战次数
            gameUser.CombatData.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            // 挑战班长次数
            gameUser.ChallengeMonitorTimes = 0;
            // 赠送好友次数
            gameUser.FriendsData.GiveAwayCount = 0;
            // 体力
            if (gameUser.Vit < ConfigEnvSet.GetInt("User.RestoreVit"))
                gameUser.Vit = ConfigEnvSet.GetInt("User.RestoreVit");
            // 选票数量
            gameUser.CampaignTicketNum = ConfigEnvSet.GetInt("User.RestoreCampaignTicketNum");
            // 购买的选票数量
            gameUser.BuyCampaignTicketNum = 0;

            // 每日任务
            gameUser.DailyQuestData.ID = TaskType.No;
            gameUser.DailyQuestData.IsFinish = false;
            gameUser.DailyQuestData.RefreshCount = 0;
            gameUser.DailyQuestData.FinishCount = 0;
            gameUser.DailyQuestData.Count = 0;
            List<Config_Task> tasklist = new ShareCacheStruct<Config_Task>().FindAll();
            if (tasklist.Count > 0)
            {
                int randv = random.Next(tasklist.Count);
                var randtask = tasklist[randv];
                gameUser.DailyQuestData.ID = randtask.id;
            }

            // 占领重置
            gameUser.OccupySceneList.Clear();

            // 签到，首周，在线
            if (gameUser.OfflineDate != DateTime.MinValue && gameUser.OfflineDate.Month != DateTime.Now.Month)
            {// 下个月签到次数要清零
                gameUser.EventAwardData.SignCount = 0;
            }
            gameUser.EventAwardData.IsTodaySign = false;
            gameUser.EventAwardData.IsTodayReceiveFirstWeek = false;
            gameUser.EventAwardData.TodayOnlineTime = 0;
            gameUser.EventAwardData.OnlineAwardId = 1;
            if (!islogin)
            {
                gameUser.EventAwardData.OnlineStartTime = DateTime.Now;
            }


        }
        public static void UserOnline(int uid)
        {
            GameUser gameUser = FindUser(uid);
            if (gameUser == null)
            {
                return;
            }
            if (gameUser.OfflineDate > gameUser.LoginDate)
            {
                gameUser.EventAwardData.OnlineStartTime = gameUser.LoginDate;
            }
            else
            {
                gameUser.OfflineDate = DateTime.Now;
                gameUser.EventAwardData.OnlineStartTime = gameUser.LoginDate;
            }

            if (gameUser.EventAwardData.OnlineStartTime < gameUser.EventAwardData.LastOnlineAwayReceiveTime)
            {
                if (gameUser.OfflineDate > gameUser.EventAwardData.LastOnlineAwayReceiveTime)
                {
                    gameUser.EventAwardData.OnlineStartTime = gameUser.EventAwardData.LastOnlineAwayReceiveTime;
                }
                else
                {
                    gameUser.EventAwardData.OnlineStartTime = gameUser.OfflineDate;
                }
              
            }


            gameUser.LoginDate = DateTime.Now;
            gameUser.IsOnline = true;
            gameUser.ChatVesion = 0;
            gameUser.BroadcastVesion = 0;
            gameUser.IsRefreshing = true;
            gameUser.UserStatus = UserStatus.MainUi;
            gameUser.InviteFightDestUid = 0;

            DateTime startDate = gameUser.EventAwardData.OnlineStartTime;

            TimeSpan timeSpan = gameUser.OfflineDate.Subtract(startDate);
            int sec = (int)Math.Floor(timeSpan.TotalSeconds);

            gameUser.EventAwardData.TodayOnlineTime += sec;
            

            // 计算上线时间，刷新数据
            var nowTime = DateTime.Now;
            bool isRefresh = false;
            if (gameUser.OfflineDate != DateTime.MinValue)
            {
                //TimeSpan timeSpan = nowTime.Date - gameUser.OfflineDate.Date;
                timeSpan = DateTime.Now.Subtract(gameUser.OfflineDate);
                int day = (int)Math.Floor(timeSpan.TotalDays);
                if (day > 0 || (day == 0 && nowTime.Hour >= 5 && gameUser.OfflineDate.Hour < 5))
                {
                    isRefresh = true;
                }
            }

            if (isRefresh)
            {// 刷新
                RestoreUserData(uid);
            }

            
        }
        /// <summary>
        /// 用户下线处理
        /// </summary>
        /// <param name="uid"></param>
        public static void UserOffline(int uid)
        {
            GameUser gameUser = FindUser(uid);
            if (gameUser == null)
            {
                return;
            }
            gameUser.IsOnline = false;
            gameUser.OfflineDate = DateTime.Now;
            gameUser.UserStatus = UserStatus.MainUi;

            // 竞技场处理
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            if (ranking.TryGetRankNo(m => m.UserID == uid, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == uid);
            }
            if (rankinfo != null && rankinfo.FightDestUid > 0)
            {
                rankinfo.IsFighting = false;

                if (ranking.TryGetRankNo(m => m.UserID == rankinfo.FightDestUid, out rankID))
                {
                    rivalrankinfo = ranking.Find(s => s.UserID == rankinfo.FightDestUid);
                }
                if (rivalrankinfo != null && rivalrankinfo.IsFighting)
                {
                    rivalrankinfo.IsFighting = false;
                }
                rankinfo.FightDestUid = 0;
            }

            // 班级处理
            if (gameUser.ClassData.ClassID != 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == gameUser.ClassData.ClassID));
                if (classdata != null && classdata.IsChallenging && classdata.ChallengeUserId == gameUser.UserID)
                {
                    classdata.IsChallenging = false;
                    classdata.ChallengeUserId = 0;
                }
            }

            // 占领处理
            if (gameUser.OccupySceneType != SceneType.No)
            {
                var occupycache = new ShareCacheStruct<OccupyDataCache>();
                var findocc = occupycache.FindKey(gameUser.OccupySceneType);

                if (findocc.ChallengerId == gameUser.UserID)
                {
                    findocc.ChallengerId = 0;
                    findocc.ChallengerNickName = "";
                }
                gameUser.OccupySceneType = SceneType.No;
            }



        }
   

        public static void buildBaseExpData(GameUser gameUser, out object outdata)
        {
            outdata = null;

            SubjectStage stage = gameUser.getSubjectStage();
            switch (stage)
            {
                case SubjectStage.PrimarySchool:
                    outdata = new JPExpPrimarySchoolData()
                    {
                        id1 = gameUser.ExpData.id1,
                        id2 = gameUser.ExpData.id2,
                        id3 = gameUser.ExpData.id3,
                        id4 = gameUser.ExpData.id4,
                        id5 = gameUser.ExpData.id5,
                        id6 = gameUser.ExpData.id6,
                    };
                    break;
                case SubjectStage.MiddleSchool:
                    outdata = new JPExpMiddleSchoolData()
                    {
                        id7 = gameUser.ExpData.id7,
                        id8 = gameUser.ExpData.id8,
                        id9 = gameUser.ExpData.id9,
                        id10 = gameUser.ExpData.id10,
                        id11 = gameUser.ExpData.id11,
                        id12 = gameUser.ExpData.id12,
                        id13 = gameUser.ExpData.id13,
                        id14 = gameUser.ExpData.id14,
                        id15 = gameUser.ExpData.id15,
                        id16 = gameUser.ExpData.id16,
                    };
                    break;
                case SubjectStage.SeniorHighSchool:
                    outdata = new JPExpSeniorHighSchoolData()
                    {
                        id17 = gameUser.ExpData.id17,
                        id18 = gameUser.ExpData.id18,
                        id19 = gameUser.ExpData.id19,
                        id20 = gameUser.ExpData.id20,
                        id21 = gameUser.ExpData.id21,
                        id22 = gameUser.ExpData.id22,
                        id23 = gameUser.ExpData.id23,
                        id24 = gameUser.ExpData.id24,
                        id25 = gameUser.ExpData.id25,
                        id26 = gameUser.ExpData.id26,
                    };
                    break;
                case SubjectStage.University:
                    outdata = new JPExpUniversityData()
                    {
                        id27 = gameUser.ExpData.id27,
                        id28 = gameUser.ExpData.id28,
                        id29 = gameUser.ExpData.id29,
                        id30 = gameUser.ExpData.id30,
                        id31 = gameUser.ExpData.id31,
                        id32 = gameUser.ExpData.id32,
                        id33 = gameUser.ExpData.id33,
                        id34 = gameUser.ExpData.id34,
                        id35 = gameUser.ExpData.id35,
                        id36 = gameUser.ExpData.id36,
                    };
                    break;
                default: throw new ArgumentException(string.Format("buildBaseExpData stage[{0}] isn't exist.", stage));
            }

        }

        /// <summary>
        /// 格式化输出竞技场日志
        /// </summary>
        /// <param name="logdata"></param>
        /// <returns></returns>
        public static string FormatCombatLog(CombatLogData logdata)
        {
            GameUser gameUser = FindUser(logdata.UserId);
            if (gameUser == null)
            {
                return "";
            }
            string ret = "";
            string date = Util.FormatDate(logdata.LogTime);
            ret = date + "，";
            if (logdata.Type == EventType.Challenge)
            {
                ret += "你挑战";
                ret += " ";
                ret += gameUser.NickName;
                ret += " ";
                string tmp = "";
                if (logdata.Status == EventStatus.Good)
                {
                    tmp = string.Format("成功，排名上升 {0} 位。", logdata.RankIdDiff);
                }
                else
                {
                    tmp = string.Format("失败。", logdata.RankIdDiff);
                }
                ret += tmp;
            }
            else
            {
                ret += " ";
                ret += gameUser.NickName;
                ret += " ";
                ret += "挑战你";
                string tmp = "";
                if (logdata.Status == EventStatus.Good)
                {
                    tmp = string.Format("成功，排名下降 {0} 位。", logdata.RankIdDiff);
                }
                else
                {
                    tmp = string.Format("失败。", logdata.RankIdDiff);
                }
                ret += tmp;
            }

            return ret;
        }

        /// <summary>
        /// 每天零点刷新
        /// </summary>
        /// <param name="planconfig"></param>
        public static void DoZeroRefreshDataTask(PlanConfig planconfig)
        {
            
            if (ScriptEngines.IsCompiling)
            {
                return;
            }

            //do something
            JobTitleDataCache electionfd = null;
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
            {
                var fd = jobcache.FindKey(i);
                if (fd.Status == CampaignStatus.Runing)
                {
                    fd.Status = CampaignStatus.Over;
                    CampaignUserData votemax = null;
                    foreach (var cuserdata in fd.CampaignUserList)
                    {
                        if (votemax == null)
                        {
                            votemax = cuserdata;
                        }
                        else if (votemax.VoteCount < cuserdata.VoteCount)
                        {
                            votemax = cuserdata;
                        }
                    }
                    if (votemax != null)
                    {
                        fd.UserId = votemax.UserId;
                        fd.NickName = votemax.NickName;
                        fd.ClassId = votemax.ClassId;
                        fd.LooksId = votemax.LooksId;

                        electionfd = fd;
                    }
                }
            }
            if (electionfd != null)
            {
                var fdlist = jobcache.FindAll(t => (t.UserId == electionfd.UserId));
                foreach (var d in fdlist)
                {
                    if (d.TypeId != electionfd.TypeId)
                    {
                        d.UserId = 0;
                        d.NickName = "";
                        d.LooksId = 0;
                    }
                }

                // 成就
                GameUser user = FindUser(electionfd.UserId);
                if (user != null)
                {
                    var achdata = user.AchievementList.Find(t => (t.Type == AchievementType.CompaignsCount));
                    if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
                    {
                        achdata.Count++;
                        var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                        if ((electionfd.TypeId == JobTitleType.Class && achconfig.ObjectiveNum == 1)
                            || (electionfd.TypeId == JobTitleType.Sports && achconfig.ObjectiveNum == 2))
                        {
                            achdata.IsFinish = true;
                            GameSession session = GameSession.Get(electionfd.UserId);
                            PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
                        }
                    }
                }


            }
            


            if (scount == 7)
                scount = 0;
            var fdnow = jobcache.FindKey((JobTitleType)scount);
            scount++;
            ///var fdnow = jobcache.FindKey((JobTitleType)DateTime.Now.DayOfWeek);
            if (fdnow != null)
            {
                // 取消加成
                if (fdnow.ClassId != 0)
                {
                    var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(fdnow.ClassId);
                    if (classdata != null)
                    {
                        foreach (int id in classdata.MemberList)
                        {
                            GameUser mem = FindUser(id);
                            if (mem == null)
                                continue;
                            mem.AditionJobTitle = JobTitleType.No;
                            mem.IsHaveJobTitle = false;
                        }
                    }
                }

                fdnow.Status = CampaignStatus.Runing;
                fdnow.UserId = 0;
                fdnow.NickName = "";
                fdnow.ClassId = 0;
                fdnow.LooksId = 0;
                fdnow.CampaignUserList.Clear();
            }
        }
        /// <summary>
        /// 每天整点刷新（5）
        /// </summary>
        /// <param name="planconfig"></param>
        public static void DoEveryDayRefreshDataTask(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something
            var onlinelist = GameSession.GetOnlineAll();
            foreach (var session in onlinelist)
            {
                RestoreUserData(session.UserId, false);
            }
            PushMessageHelper.RestoreUserNotification();
        }
        /// <summary>
        /// 每周二周五竞技场奖励任务
        /// </summary>
        /// <param name="planconfig"></param>
        public static void DoCombatAwardTask(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something
            ProgressCombatAward();
        }


        /// <summary>
        /// 进行发放竞技场奖励
        /// </summary>
        private static void ProgressCombatAward()
        {
            int pageCount;
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            IList<UserRank> list = ranking.GetRange(0, ranking.Count, out pageCount);

            foreach (UserRank ur in list)
            {
                if (ur.RankId == 1)
                {

                }
                else if (ur.RankId == 2)
                {

                }
                else if (ur.RankId == 3)
                {

                }
                else if (ur.RankId <= 10)
                {

                }
                else
                {

                }
            }
        }

        public static UserRank FindCombatRankUser(int userid)
        {
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            UserRank rankInfo = null;
            int rankID = 0;
            if (ranking.TryGetRankNo(m => (m.UserID == userid), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == userid));
            }
            return rankInfo;
        }

        public static UserRank FindLevelRankUser(int userid)
        {
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            UserRank rankInfo = null;
            int rankID = 0;
            if (ranking.TryGetRankNo(m => (m.UserID == userid), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == userid));
            }
            return rankInfo;
        }

        public static UserRank FindFightValueRankUser(int userid)
        {
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
            UserRank rankInfo = null;
            int rankID = 0;
            if (ranking.TryGetRankNo(m => (m.UserID == userid), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == userid));
            }
            return rankInfo;
        }

        public static void AchievementProcess(int uid, int addcount, AchievementType type)
        {
            GameUser user = FindUser(uid);
            if (user == null)
                return;

            var achdata = user.AchievementList.Find(t => (t.Type == type));
            if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
            {
                achdata.Count += addcount;
                var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                if (achdata.Count >= achconfig.ObjectiveNum)
                {
                    achdata.IsFinish = true;
                    GameSession session = GameSession.Get(uid);
                    if (session != null)
                        PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
                }
            }
        }


        public static void GiveAwayDiamond(int uid, int count)
        {
            GameUser user = FindUser(uid);
            if (user == null)
                return;
            user.GiveAwayDiamond = MathUtils.Addition(user.GiveAwayDiamond, count, int.MaxValue / 2);

            // 成就
            AchievementProcess(uid, count, AchievementType.AwardDiamondCount);
        }



        /// <summary>
        /// 处理玩家变更数据
        /// </summary>
        /// <param name="property"></param>
        /// <param name="userID"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public static void TriggerUserCallback(string property, int userId, object oldValue, object value)
        {
            //int useNum = MathUtils.Subtraction(value.ToInt(), oldValue.ToInt(), 0);
            //int consumeNum = MathUtils.Subtraction(oldValue.ToInt(), value.ToInt(), 0);
            if (property == "DiamondChange")
            {
                GameSession session = GameSession.Get(userId);
                if (session != null)
                    PushMessageHelper.UserDiamondChangedNotification(session);
            }
            else if (property == "FightValueChange")
            {
                GameSession session = GameSession.Get(userId);
                if (session != null)
                    PushMessageHelper.UserFightValueChangedNotification(session);
            }
            else if (property == "SkillLevelAchievement")
            {
                GameSession session = GameSession.Get(userId);
                if (session != null)
                    PushMessageHelper.AchievementFinishNotification(session, value.ToInt());
            }
            else if (property == "LevelUp")
            {
                GameUser user = FindUser(userId);
                // 成就
                AchievementData achdata = user.AchievementList.Find(t => (t.Type == AchievementType.LevelCount));
                if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
                {
                    achdata.Count = user.UserLv;
                    var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                    if (achdata.Count >= achconfig.ObjectiveNum)
                    {
                        achdata.IsFinish = true;
                        GameSession session = GameSession.Get(userId);
                        if (session != null)
                            PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
                    }
                }

                // 将用户从原有班级中剔除
                bool ischangeclass = false;
                if (user.UserLv % 2 == 0 && user.ClassData.ClassID != 0)
                {
                    ischangeclass = true;
                    user.ClassData.ClassID = 0;
                    ClassDataCache oldclass = new ShareCacheStruct<ClassDataCache>().FindKey(user.ClassData.ClassID);
                    if (oldclass != null)
                    {
                        if (oldclass.MemberList.Find(t => (t ==userId)) != 0)
                        {
                            oldclass.MemberList.Remove(userId);
                            if (oldclass.Monitor == userId)
                            {
                                oldclass.Monitor = oldclass.MemberList.Count > 0 ? oldclass.MemberList[0] : 0;
                                PushMessageHelper.ClassMonitorChangeNotification(user.ClassData.ClassID);
                            }
                        }
                    }
                }
                GameSession usession = GameSession.Get(userId);
                if (usession != null)
                    PushMessageHelper.UserLevelUpNotification(usession, ischangeclass);
            }
            else if (property == "NewMail")
            {
                GameSession session = GameSession.Get(userId);
                if (session != null)
                    PushMessageHelper.NewMailNotification(session, value.ToString());
            }
        }

    }
}