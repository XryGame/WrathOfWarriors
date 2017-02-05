using System;
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
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Model;

namespace GameServer.Script.Model.DataModel
{
    public static class UserHelper
    {
        static private Random random = new Random();

        //static int scount = 0;
        static UserHelper()
        {

        }

        public static GameUser FindUser(int userid)
        {
            return new PersonalCacheStruct<GameUser>().FindKey(userid.ToString());
        }


        public static GameUser FindUser(string PassportID, int EnterServerId)
        {
            var list = new PersonalCacheStruct<GameUser>().FindGlobal(t => (
            t.Pid == PassportID && t.EnterServerId == EnterServerId)
            );
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static GameUser FindUserOfRetail(string retailId, string openId, int EnterServerId)
        {
            var userpassport = new ShareCacheStruct<UserCenterPassport>().Find(t => (t.OpenId == openId && t.RetailId == retailId));
            if (userpassport == null)
                return null;
            var list = new PersonalCacheStruct<GameUser>().FindGlobal(t => (
            t.Pid == userpassport.PassportID && t.EnterServerId == EnterServerId)
            );
            if (list.Count > 0)
                return list[0];
            return null;
        }


        public static UserPayCache FindUserPay(int userid)
        {
            return new PersonalCacheStruct<UserPayCache>().FindKey(userid.ToString());
        }

        public static List<GameSession> GetOnlinesList()
        {
            var sessionlist = GameSession.GetAll();
            
            List<GameSession> onlinelist = new List<GameSession>();
            foreach (var on in sessionlist)
            {
                if (on.Connected)
                    onlinelist.Add(on);
            }
            return onlinelist;
        }

        public static void RestoreUserData(int uid, int restoreCount = 1)
        {
            GameUser gameUser = FindUser(uid);
            if (gameUser == null)
            {
                return;
            }
            
            // 名人榜挑战次数
            gameUser.CombatData.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            gameUser.CombatData.ButTimes = 0;
            // 挑战班长次数
            gameUser.ChallengeMonitorTimes = 0;
            // 好友
            gameUser.FriendsData.GiveAwayCount = 0;
            foreach (var fl in gameUser.FriendsData.FriendsList)
            {
                fl.IsGiveAway = false;
                fl.IsByGiveAway = false;
                fl.IsReceiveGiveAway = false;
            }

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
            if (gameUser.RestoreDate != DateTime.MinValue && gameUser.RestoreDate.Month != DateTime.Now.Month)
            {// 下个月签到次数要清零
                gameUser.EventAwardData.SignCount = 0;
            }

            gameUser.EventAwardData.IsTodaySign = false;
            gameUser.EventAwardData.IsTodayReceiveFirstWeek = false;
            gameUser.EventAwardData.IsStartedOnlineTime = false;
            //gameUser.EventAwardData.TodayOnlineTime = 0;
            gameUser.EventAwardData.OnlineAwardId = 1;
            gameUser.EventAwardData.OnlineStartTime = DateTime.Now;

            gameUser.ReceiveVitStatus = ReceiveVitStatus.No;

            // 周卡月卡处理
            UserPayCache paycache = FindUserPay(uid);
            if (paycache != null)
            {
                if (paycache.WeekCardDays > 0)
                {
                    int realDays = paycache.WeekCardDays;
                    if (restoreCount > 0)
                    {
                        int count = Math.Min(realDays, restoreCount);
                        while (count > 0)
                        {
                            count--;
                            paycache.WeekCardDays--;
                            paycache.WeekCardAwardDate = DateTime.Now;

                            AddWeekCardMail(gameUser, paycache);
                        }
                        if (restoreCount > realDays)
                        {
                            paycache.WeekCardDays = -1;
                        }
                    }
                }
                else if (paycache.WeekCardDays == 0)
                {
                    paycache.WeekCardDays = -1;
                }

                if (paycache.MonthCardDays > 0)
                {
                    int realDays = paycache.MonthCardDays;
                    if (restoreCount > 0)
                    {
                        int count = Math.Min(realDays, restoreCount);
                        while (count > 0)
                        {
                            count--;
                            paycache.MonthCardDays--;
                            paycache.MonthCardAwardDate = DateTime.Now;

                            AddMouthCardMail(gameUser, paycache);
                        }
                        if (restoreCount > realDays)
                        {
                            paycache.MonthCardDays = -1;
                        }
                    }
                }
                else if (paycache.MonthCardDays == 0)
                {
                    paycache.MonthCardDays = -1;
                }
            }

            
            gameUser.IsTodayLottery = false;
            //gameUser.RandomLotteryId = 0;
            //var lottery = RandomLottery(gameUser.UserID, gameUser.UserLv);
            //if (lottery != null)
            //{
            //    gameUser.RandomLotteryId = lottery.ID;
            //}

            gameUser.BuyVitCount = 0;


            // 切磋钻石处理
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            int lastWeekOfYear = gc.GetWeekOfYear(gameUser.ResetInviteFightDiamondDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int nowWeekOfYear = gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (lastWeekOfYear != nowWeekOfYear)
            {
                gameUser.InviteFightDiamondNum = 0;
                gameUser.ResetInviteFightDiamondDate = DateTime.Now;
            }

            // 红包重置
            gameUser.IsReceivedRedPacket = false;

            // 设置新的恢复时间
            gameUser.RestoreDate = DateTime.Now;
        }

        public static void AddWeekCardMail(GameUser user, UserPayCache pay)
        {
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "周卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的周卡奖励，您的周卡 {0} 天后到期！", pay.WeekCardDays + 1),
                ApppendDiamond = ConfigEnvSet.GetInt("System.WeekCardDiamond")
            };

            user.AddNewMail(ref mail);
        }
        public static void AddMouthCardMail(GameUser user, UserPayCache pay)
        {
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "月卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的月卡奖励，您的月卡 {0} 天后到期！", pay.MonthCardDays + 1),
                ApppendDiamond = ConfigEnvSet.GetInt("System.MonthCardDiamond")
            };

            user.AddNewMail(ref mail);
        }
        public static void UserOnline(int uid)
        {
            GameUser gameUser = FindUser(uid);
            if (gameUser == null)
            {
                return;
            }

            gameUser.LoginDate = DateTime.Now;
            gameUser.IsOnline = true;
            gameUser.ChatVesion = 0;
            gameUser.BroadcastVesion = 0;
            gameUser.IsRefreshing = true;
            gameUser.UserStatus = UserStatus.MainUi;
            gameUser.InviteFightDestUid = 0;
            //gameUser.RandomLotteryId = 0;


            // 计算上线时间，刷新数据
            int restoreCount = 1;
            var nowTime = DateTime.Now;
            bool isRefresh = false;
            if (gameUser.RestoreDate != DateTime.MinValue)
            {
                //TimeSpan timeSpan = nowTime.Date - gameUser.OfflineDate.Date;
                TimeSpan timeSpans = DateTime.Now.Subtract(gameUser.RestoreDate);
                int day = (int)Math.Floor(timeSpans.TotalDays);
                if (day > 0)
                {
                    restoreCount = day;
                    isRefresh = true;
                }
                else if (day == 0 && nowTime.Hour >= 5)
                {
                    if ((nowTime.Day == gameUser.RestoreDate.Day && gameUser.RestoreDate.Hour < 5)
                        || (nowTime.Day != gameUser.RestoreDate.Day))
                    {
                        restoreCount = 1;
                        isRefresh = true;
                    }
                }

            }

            if (isRefresh)
            {// 刷新
                RestoreUserData(uid, restoreCount);
            }

            // 名人榜处理
            CombatProcess(uid);

            // 在线时间处理
            //if (gameUser.OfflineDate > gameUser.LoginDate)
            //{
            //    gameUser.EventAwardData.OnlineStartTime = gameUser.LoginDate;
            //}
            //else
            //{// 离线时间比登录时间小，说明当前用户未下线
            //    gameUser.OfflineDate = DateTime.Now;
            //    //gameUser.EventAwardData.OnlineStartTime = gameUser.LoginDate;
            //}

            if (!gameUser.EventAwardData.IsStartedOnlineTime)
            {
                gameUser.EventAwardData.IsStartedOnlineTime = true;
                gameUser.EventAwardData.OnlineStartTime = gameUser.LoginDate;
                //gameUser.EventAwardData.TodayOnlineTime = 0;
            }

            //if (gameUser.EventAwardData.OnlineStartTime < gameUser.EventAwardData.LastOnlineAwayReceiveTime)
            //{
            //    if (gameUser.OfflineDate > gameUser.EventAwardData.LastOnlineAwayReceiveTime)
            //    {
            //        gameUser.EventAwardData.OnlineStartTime = gameUser.EventAwardData.LastOnlineAwayReceiveTime;
            //    }
            //    else
            //    {
            //        gameUser.EventAwardData.OnlineStartTime = gameUser.OfflineDate;
            //    }

            //}
            

            //TimeSpan timeSpan = DateTime.Now.Subtract(gameUser.EventAwardData.OnlineStartTime);
            //int sec = (int)Math.Floor(timeSpan.TotalSeconds);

            //gameUser.EventAwardData.TodayOnlineTime = sec;

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

            // 名人榜处理
            CombatProcess(uid);

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

            // 通知好友下线
            //foreach (FriendData fd in gameUser.FriendsData.FriendsList)
            //{
            //    GameSession session = GameSession.Get(fd.UserId);
            //    if (session != null)
            //    {
            //        PushMessageHelper.FriendOffineNotification(session, gameUser.UserID);
            //    }
            //}

        }
   
        public static void CombatProcess(int uid)
        {
            // 名人榜处理
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            if (ranking.TryGetRankNo(m => m.UserID == uid, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == uid);
            }
            if (rankinfo != null && rankinfo.FightDestUid != 0)
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
        /// 格式化输出名人榜日志
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
                    tmp = string.Format("成功，排名上升至第{0}位。", logdata.RankId);
                }
                else
                {
                    tmp = string.Format("失败。");
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
                    tmp = string.Format("成功，排名下降至第{0}位。", logdata.RankId);
                }
                else
                {
                    tmp = string.Format("失败。");
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

                        GameUser winuser = FindUser(votemax.UserId);
                        if (winuser != null)
                        {
                            winuser.AditionJobTitle = fd.TypeId;
                            winuser.IsHaveJobTitle = true;
                        }
                        PushMessageHelper.UserJobTitleAddChangedNotification(GameSession.Get(winuser.UserID));
                        var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(votemax.ClassId);
                        if (classdata != null)
                        {
                            foreach (int id in classdata.MemberList)
                            {
                                GameUser mem = FindUser(id);
                                if (mem == null)
                                    continue;
                                if (mem.AditionJobTitle == JobTitleType.No)
                                    mem.AditionJobTitle = fd.TypeId;
                            }
                        }
                        PushMessageHelper.ClassJobTitleAddChangeNotification(votemax.ClassId);


                        CampaignSucceedNotification(fd.TypeId);
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
                        if (achconfig.ObjectiveNum == electionfd.TypeId.ToInt())
                        {
                            achdata.IsFinish = true;
                            GameSession session = GameSession.Get(electionfd.UserId);
                            PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
                        }
                    }
                }


            }



            //if (scount == 7)
            //    scount = 0;
            //var fdnow = jobcache.FindKey((JobTitleType)scount);
            //scount++;

            var fdnow = jobcache.FindKey((JobTitleType)DateTime.Now.DayOfWeek);
            if (fdnow != null)
            {
                // 取消加成
                if (fdnow.UserId != 0)
                    PushMessageHelper.UserJobTitleAddChangedNotification(GameSession.Get(fdnow.UserId));
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
                            if (mem.AditionJobTitle != JobTitleType.No)
                                mem.AditionJobTitle = JobTitleType.No;
                            if (mem.IsHaveJobTitle != false)
                                mem.IsHaveJobTitle = false;
                        }
                    }
                    GameUser user = FindUser(fdnow.UserId);
                    if (user != null)
                    {
                        if (user.AditionJobTitle != JobTitleType.No)
                            user.AditionJobTitle = JobTitleType.No;
                        if (user.IsHaveJobTitle != false)
                            user.IsHaveJobTitle = false;
                    }
                    PushMessageHelper.ClassJobTitleAddChangeNotification(fdnow.ClassId);
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
            var sessionlist = GameSession.GetAll();
            foreach (var session in sessionlist)
            {
                if (session.Connected)
                    RestoreUserData(session.UserId);
            }
            PushMessageHelper.RestoreUserNotification();

            // 机器人自动参加竞选
            Bots.Campaign();
        }
        /// <summary>
        /// 每周二周五名人榜奖励任务
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
        /// 进行发放名人榜奖励
        /// </summary>
        private static void ProgressCombatAward()
        {
            new BaseLog().SaveLog(string.Format("Progress Combat Award..."));

            int pageCount;
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            IList<UserRank> list = ranking.GetRange(0, ranking.Count, out pageCount);

            var crlist = new ShareCacheStruct<Config_CelebrityRanking>().FindAll();
            foreach (UserRank ur in list)
            {
                GameUser user = FindUser(ur.UserID);
                if (user == null)
                    continue;
                
                Config_CelebrityRanking cr = crlist.Find(t => (t.Ranking >= ur.RankId));
                if (cr != null)
                {
                    MailData mail = new MailData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Title = "名人榜奖励",
                        Sender = "系统",
                        Date = DateTime.Now,
                        Context = string.Format("截止当前时间，您获得名人榜第{0}名，奖励如下，请查收！", ur.RankId),
                        ApppendDiamond = cr.AwardNum
                    };
                    user.AddNewMail(ref mail);
                }
            }
        }

        public static UserRank FindCombatRankUser(int userid)
        {
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
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
                    if (session != null && session.Connected)
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

        public static bool PayDiamond(int uid, int count)
        {
            GameUser user = FindUser(uid);
            if (user == null)
                return false;
            user.BuyDiamond = MathUtils.Addition(user.BuyDiamond, count, int.MaxValue / 2);

            // 成就
            AchievementProcess(uid, count, AchievementType.AwardDiamondCount);

            return true;
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
                if (session != null && session.Connected)
                    PushMessageHelper.UserDiamondChangedNotification(session);
            }
            else if (property == "FightValueChange")
            {
                GameUser user = FindUser(userId);
                // 这里刷新排行榜数据
                var combatuser = FindCombatRankUser(userId);
                if (combatuser != null)
                {
                    combatuser.UserLv = user.UserLv;
                    combatuser.Exp = user.TotalExp;
                    combatuser.FightingValue = user.FightingValue;
                }
                var leveluser = FindLevelRankUser(userId);
                if (leveluser != null)
                {
                    leveluser.UserLv = user.UserLv;
                    leveluser.Exp = user.TotalExp;
                    leveluser.FightingValue = user.FightingValue;
                }

                GameSession session = GameSession.Get(userId);
                if (session != null && session.Connected)
                    PushMessageHelper.UserFightValueChangedNotification(session);
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
                        if (session != null && session.Connected)
                            PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
                    }
                }


                bool ischangeclass = false;
                if (user.UserLv % 2 == 0)
                {
                    int inclass = oldValue.ToInt();
                    if (inclass != 0)
                    {
                        ischangeclass = true;
                        ClassDataCache oldclass = new ShareCacheStruct<ClassDataCache>().FindKey(inclass);
                        if (oldclass != null)
                        {
                            if (value.ToBool())
                            {
                                PushMessageHelper.ClassMonitorChangeNotification(inclass);
                            }

                            var occupylist = new ShareCacheStruct<OccupyDataCache>().FindAll();
                            foreach (var v in occupylist)
                            {
                                if (v.UserId == user.UserID)
                                {
                                    PushMessageHelper.ClassOccupyAddChangeNotification(inclass);
                                }
                            }
                        }
                    }
                }
                GameSession usession = GameSession.Get(userId);
                if (usession != null && usession.Connected)
                    PushMessageHelper.UserLevelUpNotification(usession, ischangeclass);
            }
            else if (property == "NewMail")
            {
                GameSession session = GameSession.Get(userId);
                if (session != null && session.Connected)
                    PushMessageHelper.NewMailNotification(session, value.ToString());
            }
            else if (property == "GetCombatItem")
            {
                CombatItemNotification(userId, value.ToInt());
            }

        }


        public static void EveryDayTaskProcess(int UserId, TaskType type, int count)
        {
            GameUser user = FindUser(UserId);
            if (user == null || user.DailyQuestData.ID != type)
                return;
            if (user.UserLv < DataHelper.OpenTaskSystemUserLevel || user.DailyQuestData.IsFinish != false)
                return;
            
            var task = new ShareCacheStruct<Config_Task>().FindKey(type);
            user.DailyQuestData.Count += count;
            if (user.DailyQuestData.Count >= task.ObjectiveNum)
            {
                user.DailyQuestData.IsFinish = true;
            }
            
            GameSession session = GameSession.Get(UserId);
            if (session != null && session.Connected)
                PushMessageHelper.DailyQuestFinishNotification(session);

        }
        public static Config_Lottery RandomLottery(int userId, short userlv)
        {
            GameUser user = FindUser(userId);
            var list = new ShareCacheStruct<Config_Lottery>().FindAll(t => (t.Level <= userlv));
            List<int> removelist = new List<int>();
            foreach (var v in list)
            {
                if (v.Type == LotteryAwardType.Item)
                {
                    Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(v.Content);
                    if (item != null)
                    {
                        if (item.Type == ItemType.Item)
                        {
                            ItemData itemdata = user.findItem(v.Content);
                            if (itemdata != null && itemdata.Num >= user.GetItemLvMax(itemdata.ID))
                            {
                                removelist.Add(v.ID);
                            }
                        }
                        else if (item.Type == ItemType.Skill)
                        {
                            Config_SkillGrade sg = new ShareCacheStruct<Config_SkillGrade>().Find(t => (t.Condition == item.ID));
                            if (sg != null)
                            {
                                SkillData skill = user.findSkill(sg.SkillID);
                                if (skill != null && skill.Lv >= user.GetSkillLvMax(skill.ID))
                                    removelist.Add(v.ID);
                            }
                            else
                            {
                                removelist.Add(v.ID);
                            }
                        }
                    }
                }
                
            }
            for (int i = 0; i < removelist.Count; ++i)
            {
                list.RemoveAll(t => (t.ID == removelist[i]));
            }

            if (list.Count > 0)
            {
                int weight = 0;
                foreach (var cl in list)
                {
                    weight += cl.Weight;
                }
                Config_Lottery lott = null;
                int randv = random.Next(weight);
                int tmpw = 0;
                for (int i = 0; i < list.Count; ++i)
                {
                    tmpw += list[i].Weight;
                    if (randv <= tmpw)
                    {
                        lott = list[i];
                        break;
                    }
                }
                return lott;
            }
            var diam = new ShareCacheStruct<Config_Lottery>().Find(t => (t.Type == LotteryAwardType.Diamond));
            return diam;
        }


        /// <summary>
        /// 竞选成功广播在线玩家
        /// </summary>
        /// <param name="JobTitleType"></param>
        public static void CampaignSucceedNotification(JobTitleType jtt)
        {
            JobTitleDataCache jtdc = new ShareCacheStruct<JobTitleDataCache>().FindKey(jtt);
            if (jtdc != null)
            {
                ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(jtdc.ClassId);
                if (classdata == null)
                    return;
                GameUser monitor = FindUser(classdata.Monitor);
                if (monitor == null)
                    return;
                string context = string.Format(
                    "恭喜{0} {1} 当选为新的【{2}】，该班级全体成员7天内在所有场景进行学习与劳动均可获得经验加成！          ",
                    classdata.Name,
                    monitor.NickName,
                    DataHelper.JobTitles[jtt.ToInt()]
                    );

                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemRedundantSend(context, monitor.UserID, ChatChildType.CampaignSucceed);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }

        /// <summary>
        /// 占领成功广播在线玩家
        /// </summary>
        /// <param name="SceneType"></param>
        public static void OccupySucceedNotification(SceneType st)
        {
            OccupyDataCache occupydata = new ShareCacheStruct<OccupyDataCache>().FindKey(st);
            if (occupydata != null)
            {
                GameUser user = FindUser(occupydata.UserId);
                if (user == null)
                    return;
                ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(user.ClassData.ClassID);
                if (classdata == null)
                    return;
                string context = string.Format(
                    "恭喜{0} {1} 占领【{2}】,在占领期间班级所有成员学习和劳动均可获得150%经验加成！",
                    classdata.Name,
                    user.NickName,
                    new ShareCacheStruct<Config_Scene>().FindKey(st).Name
                    );

                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemRedundantSend(context, user.UserID, ChatChildType.OccupySucceed);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }

        /// <summary>
        /// 获得竞技道具广播在线玩家
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        public static void CombatItemNotification(int userId, int itemId)
        {
            GameUser user = FindUser(userId);
            var item = new ShareCacheStruct<Config_Item>().FindKey(itemId);
            if (user != null && item != null)
            {
                string context = string.Format("恭喜 {0} 获得竞技对战道具【{1}】！", user.NickName, item.Name);
                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemSend(context);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }

        /// <summary>
        /// 挑战班长成功广播在线玩家
        /// </summary>
        /// <param name="classId"></param>
        public static void ChallengeMonitorSucceedNotification(int classId)
        {
            ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(classId);
            if (classdata != null)
            {
                GameUser monitor = FindUser(classdata.Monitor);
                if (monitor == null)
                    return;
                string context = string.Format("恭喜 {0} 挑战班长成功，成为{1}新任班长！", monitor.NickName, classdata.Name);

                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemSend(context);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }

        /// <summary>
        /// 充值成功vip等级改变通知
        /// </summary>
        /// <param name="classId"></param>
        public static void VipLvChangeNotification(int userId)
        {
            GameUser user = FindUser(userId);
            if (user != null)
            {
                string context = string.Format("恭喜 {0} 成为VIP{1}，引来众人羡煞的目光！", user.NickName, user.VipLv);
                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemSend(context);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }
    }
}