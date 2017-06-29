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
using GameServer.CsScript.Remote;
using System.Numerics;
using GameServer.Script.Model.Enum.Enum;

namespace GameServer.Script.Model.DataModel
{
    public static class UserHelper
    {
        static private Random random = new Random();

        //static int scount = 0;
        static UserHelper()
        {

        }

        public static UserBasisCache FindUserBasis(int userid)
        {
            UserBasisCache basis = new PersonalCacheStruct<UserBasisCache>().FindKey(userid.ToString());
            //if (basis != null && (!basis.IsOnline))
            //{
            //    basis.RefreshFightValue();
            //    basis.IsOnline = true;
            //}
            return basis;
        }

        public static UserBasisCache FindUserBasis(string userName)
        {
            var user = new ShareCacheStruct<UserCenterUser>().Find(t => (t.NickName == userName));
            if (user != null)
            {
                return new PersonalCacheStruct<UserBasisCache>().FindKey(user.UserID.ToString());
            }
            return null;
        }


        public static UserBasisCache FindUserBasis(string PassportID, int EnterServerId)
        {
            var gameUserCache = new PersonalCacheStruct<UserBasisCache>();
            gameUserCache.LoadFrom(t => (t.Pid == PassportID && t.ServerID == EnterServerId));
            var list = new PersonalCacheStruct<UserBasisCache>().FindGlobal(t => (
                                t.Pid == PassportID && t.ServerID == EnterServerId)
            );
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static UserBasisCache FindUserBasisOfRetail(string retailId, string openId, int EnterServerId)
        {
            var userCenterUserCache = new ShareCacheStruct<UserCenterUser>();
            var list = userCenterUserCache.FindAll(t => (
                    t.OpenID == openId && t.RetailID == retailId && t.ServerID == EnterServerId)
                    );
            if (list.Count > 0)
            {
                return FindUserBasis(list[0].UserID);
            }
            return null;
        }

        public static UserAttributeCache FindUserAttribute(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserAttributeCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserAttributeCache();
                ret.UserID = userid;
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserEquipsCache FindUserEquips(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserEquipsCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserEquipsCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserPackageCache FindUserPackage(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserPackageCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserPackageCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserSoulCache FindUserSoul(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserSoulCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserSoulCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserSkillCache FindUserSkill(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserSkillCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserSkillCache();
                ret.UserID = userid;
                ret.ResetCache(FindUserBasis(userid).Profession);
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserFriendsCache FindUserFriends(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserFriendsCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserFriendsCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserAchievementCache FindUserAchievement(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserAchievementCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserAchievementCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserMailBoxCache FindUserMailBox(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserMailBoxCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserMailBoxCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }


        public static UserTaskCache FindUserTask(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserTaskCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserTaskCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserPayCache FindUserPay(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserPayCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserPayCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserCombatCache FindUserCombat(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserCombatCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserCombatCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserEventAwardCache FindUserEventAward(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserEventAwardCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserEventAwardCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserGuildCache FindUserGuild(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserGuildCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserGuildCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }


        public static UserElfCache FindUserElf(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserElfCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserElfCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static UserTransferItemCache FindUserTransfer(int userid)
        {
            var cacheset = new PersonalCacheStruct<UserTransferItemCache>();
            var ret = cacheset.FindKey(userid.ToString());
            if (ret == null)
            {
                ret = new UserTransferItemCache();
                ret.UserID = userid;
                ret.ResetCache();
                cacheset.Add(ret);
                cacheset.Update();
            }
            return ret;
        }

        public static List<GameSession> GetOnlinesList()
        {
            var sessionlist = GameSession.GetAll();
            
            List<GameSession> onlinelist = new List<GameSession>();
            foreach (var on in sessionlist)
            {
                if (on.Connected && !on.IsRemote)
                    onlinelist.Add(on);
            }
            return onlinelist;
        }

        public static void RestoreUserData(int uid, int restoreCount = 1)
        {
            UserBasisCache basis = FindUserBasis(uid);
            UserFriendsCache friends = FindUserFriends(uid);
            UserTaskCache task = FindUserTask(uid);
            UserCombatCache combat = FindUserCombat(uid);
            UserEventAwardCache eventaward = FindUserEventAward(uid);
            UserGuildCache guild = FindUserGuild(uid);
            UserTransferItemCache transfer = FindUserTransfer(uid);
            if (basis == null)
            {
                return;
            }

            // 通天塔挑战次数
            combat.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            combat.BuyTimes = 0;
            combat.MatchTimes = ConfigEnvSet.GetInt("Combat.MatchTimes");
            combat.BuyMatchTimes = 0;

            // 好友
            friends.GiveAwayCount = 0;
            foreach (var fl in friends.FriendsList)
            {
                fl.IsGiveAway = false;
                fl.IsByGiveAway = false;
                fl.IsReceiveGiveAway = false;
            }


            // 每日任务
            task.ResetCache();

            // 签到，首周，在线
            //if (basis.RestoreDate != DateTime.MinValue && basis.RestoreDate.DayOfWeek == DayOfWeek.Monday)
            if (eventaward.SignStartID != DataHelper.SignStartID)
            {
                eventaward.SignStartID = DataHelper.SignStartID;
                eventaward.SignCount = 0;
            }

            eventaward.IsTodaySign = false;
            //eventaward.IsTodayReceiveFirstWeek = false;
            //eventaward.IsStartedOnlineTime = true;
            //eventaward.TodayOnlineTime = 0;
            eventaward.OnlineAwardId = 1;
            eventaward.OnlineStartTime = DateTime.Now;


            // 公会
            guild.IsSignIn = false;


            // 月卡季卡处理
            UserPayCache paycache = FindUserPay(uid);
            paycache.BuyGoldTimes = 0;
            if (paycache != null)
            {
                UserMailBoxCache mailbox = FindUserMailBox(uid);
                if (paycache.QuarterCardDays > 0)
                {
                    int realDays = paycache.QuarterCardDays;
                    if (restoreCount > 0)
                    {
                        int count = Math.Min(realDays, restoreCount);
                        while (count > 0)
                        {
                            count--;
                            paycache.QuarterCardDays--;
                            paycache.QuarterCardAwardDate = DateTime.Now;

                            AddQuarterCardMail(uid);
                        }
                        if (restoreCount > realDays)
                        {
                            paycache.QuarterCardDays = -1;
                        }
                    }
                }
                else if (paycache.QuarterCardDays == 0)
                {
                    paycache.QuarterCardDays = -1;
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

                            AddMouthCardMail(uid);
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

            
            basis.LotteryTimes = ConfigEnvSet.GetInt("User.LotteryTimes");
            //basis.RandomLotteryId = 0;
            //var lottery = RandomLottery(basis.UserID, basis.UserLv);
            //if (lottery != null)
            //{
            //    basis.RandomLotteryId = lottery.ID;
            //}
            

            //// 切磋钻石处理
            //System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            //int lastWeekOfYear = gc.GetWeekOfYear(basis.ResetInviteFightDiamondDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            //int nowWeekOfYear = gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            //if (lastWeekOfYear != nowWeekOfYear)
            //{
            //    basis.InviteFightDiamondNum = 0;
            //    basis.ResetInviteFightDiamondDate = DateTime.Now;
            //}

            // 红包重置
            basis.IsReceivedRedPacket = false;

            basis.ShareCount = 0;

            transfer.SendCount = 0;
            transfer.ReceiveCount = 0;

            // 设置新的恢复时间
            basis.RestoreDate = DateTime.Now;
        }


        public static void UserOnline(int uid)
        {
            if (uid == 0)
                return;
            UserBasisCache basis = FindUserBasis(uid);
            UserPackageCache package = FindUserPackage(uid);
            UserMailBoxCache mailbox = FindUserMailBox(uid);
            UserEventAwardCache eventaward = FindUserEventAward(uid);
            if (basis == null)
            {
                return;
            }


            basis.LoginDate = DateTime.Now;
            basis.IsRefreshing = true;
            basis.UserStatus = UserStatus.MainUi;
            basis.InviteFightDestUid = 0;
            basis.IsReceiveOfflineEarnings = false;
            //basis.RandomLotteryId = 0;
            package.NewItemCache.Clear();

            // 计算上线时间，刷新数据
            int restoreCount = 1;
            var nowTime = DateTime.Now;
            bool isRefresh = false;
            if (basis.RestoreDate != DateTime.MinValue)
            {
                //TimeSpan timeSpan = nowTime.Date - basis.OfflineDate.Date;
                TimeSpan timeSpans = DateTime.Now.Subtract(basis.RestoreDate);
                int day = (int)Math.Floor(timeSpans.TotalDays);
                if (day > 0)
                {
                    restoreCount = day;
                    isRefresh = true;
                }
                else if (day == 0 && nowTime.Hour >= 5)
                {
                    if ((nowTime.Day == basis.RestoreDate.Day && basis.RestoreDate.Hour < 5)
                        || (nowTime.Day != basis.RestoreDate.Day))
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

            // 通天塔处理
            CombatProcess(uid);

            //if (!eventaward.IsStartedOnlineTime)
            //{
            //    eventaward.IsStartedOnlineTime = true;
            //    eventaward.OnlineStartTime = basis.LoginDate;
            //}

            // 每日
            EveryDayTaskProcess(basis.UserID, TaskType.Login, 1, false);

            // 离线收益时间
            TimeSpan ts = DateTime.Now.Subtract(basis.OfflineDate);
            basis.OfflineTimeSec += (long)Math.Floor(ts.TotalSeconds);


        }
        /// <summary>
        /// 用户下线处理
        /// </summary>
        /// <param name="uid"></param>
        public static void UserOffline(int uid)
        {
            if (uid == 0)
            {
                return;
            }
            UserBasisCache basis = FindUserBasis(uid);
            UserFriendsCache friend = FindUserFriends(uid);
            UserGuildCache guild = FindUserGuild(uid);
            if (basis == null)
            {
                return;
            }
            //basis.IsOnline = false;
            basis.OfflineDate = DateTime.Now;
            basis.UserStatus = UserStatus.MainUi;

            // 通天塔处理
            CombatProcess(uid);


            // 通知好友下线
            foreach (FriendData fd in friend.FriendsList)
            {
                PushMessageHelper.FriendOffineNotification(GameSession.Get(fd.UserId), basis.UserID);
            }

            // 通知公会成员下线
            if (!guild.GuildID.IsEmpty())
            {
                var guildData = new ShareCacheStruct<GuildsCache>().FindKey(guild.GuildID);
                foreach (var v in guildData.MemberList)
                {
                    if (v.UserID != uid)
                        PushMessageHelper.GuildMemberOffineNotification(GameSession.Get(v.UserID), uid);
                }
            }


        }

        public static void BulidJPGuildData(string guildId, JPGuildData outData)
        {
            if (guildId.IsEmpty())
            {
                return;
            }
            var guildData = new ShareCacheStruct<GuildsCache>().FindKey(guildId);
            if (guildData != null)
            {
                outData.GuildID = guildData.GuildID;
                outData.GuildName = guildData.GuildName;
                outData.Liveness = guildData.Liveness;
                outData.Lv = guildData.Lv;
                outData.Notice = guildData.Notice;
                outData.RankID = guildData.RankID;
                outData.CreateDate = guildData.CreateDate;

                foreach (var v in guildData.MemberList)
                {
                    var basis = FindUserBasis(v.UserID);
                    JPGuildMemberData member = new JPGuildMemberData()
                    {
                        UserID = v.UserID,
                        NickName = basis.NickName,
                        Profession = basis.Profession,
                        AvatarUrl = basis.AvatarUrl,
                        UserLv = basis.UserLv,
                        CombatRankID = basis.CombatRankID,
                        JobTitle = v.JobTitle,
                        Liveness = v.Liveness,
                    };
                    var gameSession = GameSession.Get(v.UserID);
                    member.IsOnline = gameSession != null && gameSession.Connected;
                    outData.MemberList.Add(member);
                }
                foreach (var v in guildData.ApplyList)
                {
                    var basis = FindUserBasis(v.UserID);
                    JPGuildApplyData apply = new JPGuildApplyData()
                    {
                        UserID = v.UserID,
                        NickName = basis.NickName,
                        Profession = basis.Profession,
                        AvatarUrl = basis.AvatarUrl,
                        UserLv = basis.UserLv,
                        CombatRankID = basis.CombatRankID,
                        ApplyTime = v.Date
                    };
                    var gameSession = GameSession.Get(v.UserID);
                    apply.IsOnline = gameSession != null && gameSession.Connected;
                    outData.ApplyList.Add(apply);
                }
                foreach (var v in guildData.LogList)
                {
                    JPGuildLogData log = new JPGuildLogData()
                    {
                        LogTime = v.LogTime,
                        UserId = v.UserId,
                        UserName = v.UserName,
                        Content = v.Content,
                    };
                    outData.LogList.Add(log);
                }

            }
        }
   
        public static void CombatProcess(int uid)
        {
            // 通天塔处理
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);

            UserRank rankinfo = FindRankUser(uid, RankType.Combat);
            UserRank rivalrankinfo = null;

            if (rankinfo != null && rankinfo.FightDestUid != 0)
            {
                rankinfo.IsFighting = false;

                rivalrankinfo = FindRankUser(rankinfo.FightDestUid, RankType.Combat);
                if (rivalrankinfo != null && rivalrankinfo.IsFighting)
                {
                    rivalrankinfo.IsFighting = false;
                }
                rankinfo.FightDestUid = 0;
            }
        }

        /// <summary>
        /// 格式化输出通天塔日志
        /// </summary>
        /// <param name="logdata"></param>
        /// <returns></returns>
        public static string FormatCombatLog(CombatLogData logdata)
        {

            string ret = "";
            //string date = Util.FormatDate(logdata.LogTime);
            //ret = date + "，";
            if (logdata.Type == EventType.Challenge)
            {
                ret += "你挑战";
                ret += " ";
                ret += logdata.RivalName;
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
                ret += logdata.RivalName;
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

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                if (new ShareCacheStruct<Config_Signin>().FindKey(DataHelper.SignStartID + 13) != null)
                {
                    DataHelper.SignStartID += 7;
                }
                else
                {
                    DataHelper.SignStartID = 1;
                }
                GameCache signStartIDCache = new ShareCacheStruct<GameCache>().FindKey(DataHelper.SignStartIDCacheKey);
                signStartIDCache.Value = DataHelper.SignStartID.ToNotNullString();

            }

            var sessionlist = GameSession.GetAll();
            foreach (var session in sessionlist)
            {
                if (session.Connected)
                    RestoreUserData(session.UserId);
            }
            PushMessageHelper.RestoreUserNotification();

            ProgressCombatAward();

        }
        /// <summary>
        /// 每周二刷新任务
        /// </summary>
        /// <param name="planconfig"></param>
        public static void DoTuesdayRefreshTask(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something
            Ranking<GuildRank> guildRanking = RankingFactory.Get<GuildRank>(GuildRanking.RankingKey);
            guildRanking.ForceRefresh();
            //ProgressCombatAward();
        }
        ///// <summary>
        ///// 每周五刷新任务
        ///// </summary>
        ///// <param name="planconfig"></param>
        //public static void DoFridayRefreshTask(PlanConfig planconfig)
        //{
        //    if (ScriptEngines.IsCompiling)
        //    {
        //        return;
        //    }
        //    //do something
        //    ProgressCombatAward();
        //}

        /// <summary>
        /// 进行发放通天塔奖励
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
                Config_CelebrityRanking cr = crlist.Find(t => (t.Ranking >= ur.RankId));
                if (cr != null)
                {
                    MailData mail = new MailData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Title = "通天塔奖励",
                        Sender = "系统",
                        Date = DateTime.Now,
                        Context = string.Format("截止当前时间，您获得通天塔第{0}名，奖励如下，请查收！", ur.RankId),
                    };
                    mail.AppendItem.Add(new ItemData() { ID = cr.AwardItemID, Num = cr.AwardNum });
                    AddNewMail(ur.UserID, mail);
                }
            }
        }

        public static UserRank FindRankUser(int userid, RankType type)
        {
            Ranking<UserRank> ranking = null;
            switch (type)
            {
                case RankType.Combat:
                    ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
                    break;
                case RankType.Level:
                    ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                    break;
                case RankType.FightValue:
                    ranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
                    break;

            }
            UserRank rankInfo = null;
            int rankID = 0;
            if (ranking.TryGetRankNo(m => (m.UserID == userid), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == userid));
            }

            return rankInfo;
        }
        

        public static void AchievementProcess(int uid, AchievementType type, string addcount = "0", int addId = 0, bool isNotification = true)
        {
            UserAchievementCache userachieve = FindUserAchievement(uid);

            var achdata = userachieve.AchievementList.Find(t => (t.Type == type));
            if (achdata != null && achdata.ID != 0 && achdata.Status == TaskStatus.No)
            {
                
                var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                switch (type)
                {
                    case AchievementType.LevelCount:
                        {
                            UserBasisCache basis = FindUserBasis(uid);
                            achdata.Count = basis.UserLv.ToString();
                            if (achdata.Count.ToInt() > achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.CombatMatch:
                    case AchievementType.Diamond:
                        {
                            int count = achdata.Count.ToInt() + addcount.ToInt();
                            achdata.Count = count == 0 ? "0" : count.ToString();
                            if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.Gold:
                        {
                            BigInteger count, old, add;
                            old = BigInteger.Parse(achdata.Count);
                            add = BigInteger.Parse(addcount);
                            count = old + add;
                            achdata.Count = count == 0 ? "0" : count.ToString();
                            if (count >= BigInteger.Parse(achconfig.ObjectiveNum))
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.UpgradeElf:
                        {
                            achdata.Count = "0";
                            UserElfCache elf = FindUserElf(uid);
                            var findlist = elf.ElfList.FindAll(t => (t.Lv >= achconfig.ObjectiveGrade));

                            achdata.Count = findlist.Count.ToString();
                            if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.UpgradeSkill:
                        {
                            achdata.Count = "0";
                            UserSkillCache userSkill = FindUserSkill(uid);
                            var findlist = userSkill.SkillList.FindAll(t => (t.Lv >= achconfig.ObjectiveGrade));

                            achdata.Count = findlist.Count.ToString();
                            if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.UpgradeEquip:
                        {
                            achdata.Count = "0";
                            int finishcount = 0;
                            UserEquipsCache userEquip = FindUserEquips(uid);
                            for (EquipID id = EquipID.Weapon; id <= EquipID.Accessory; ++id)
                            {
                                var equip = userEquip.FindEquipData(id);
                                if (equip.Lv >= achconfig.ObjectiveGrade)
                                {
                                    finishcount++;
                                }
                            }
                            achdata.Count = finishcount.ToString();
                            
                            if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    case AchievementType.Gem:
                        {
                            //UserPackageCache userPackage = FindUserPackage(uid);
                            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(addId);
                            if (itemcfg != null && itemcfg.ItemType == ItemType.Gem)
                            {
                                if (itemcfg.ItemGrade == achconfig.ObjectiveGrade)
                                {
                                    int count = achdata.Count.ToInt() + addcount.ToInt();
                                    achdata.Count = count.ToString();
                                }
                            }
                            if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                    //case AchievementType.OpenSoul:
                    //    {
                    //        UserSoulCache userSoul = FindUserSoul(uid);
                    //        int soullv = addId != 0 ? addId % 10000 : 0;
                    //        if (soullv == achconfig.ObjectiveGrade)
                    //        {
                    //            achdata.Count = userSoul.OpenList.Count.ToString();
                    //        }
                    //        else if (soullv > achconfig.ObjectiveGrade)
                    //        {
                    //            achdata.Count = achconfig.ObjectiveNum;
                    //        }
                    //        if (achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                    //        {
                    //            achdata.Status = TaskStatus.Finished;
                    //        }
                    //    }
                    //    break;
                    case AchievementType.CombatRandID:
                        {
                            int count = achdata.Count.ToInt() + addcount.ToInt();
                            achdata.Count = count == 0 ? "0" : count.ToString();

                            UserBasisCache basis = FindUserBasis(uid);

                            if (basis.CombatRankID <= achconfig.ObjectiveGrade.ToInt()
                                && achdata.Count.ToInt() >= achconfig.ObjectiveNum.ToInt())
                            {
                                achdata.Status = TaskStatus.Finished;
                            }
                        }
                        break;
                }
                
                if (isNotification)
                {
                    GameSession session = GameSession.Get(uid);
                    if (session != null && session.Connected)
                        PushMessageHelper.AchievementUpdateNotification(session, achdata.Type);
                }

            }
        }


        public static void RewardsDiamond(int uid, int count, UpdateCoinOperate updateType = UpdateCoinOperate.NormalReward)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.RewardsDiamond = MathUtils.Addition(basis.RewardsDiamond, count, int.MaxValue);
            
            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Diamond, updateType);
            // 成就
            AchievementProcess(uid, AchievementType.Diamond, count.ToString());
        }

        public static void ConsumeDiamond(int uid, int count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.UsedDiamond = MathUtils.Addition(basis.UsedDiamond, count, int.MaxValue);
            
            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Diamond, UpdateCoinOperate.Consume);
        }

        public static void RewardsCombatCoin(int uid, int count)
        {
            UserCombatCache combat = FindUserCombat(uid);

            combat.CombatCoin = MathUtils.Addition(combat.CombatCoin, count, int.MaxValue);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.CombatCoin, UpdateCoinOperate.NormalReward);

        }

        public static void ConsumeCombatCoin(int uid, int count)
        {
            UserCombatCache combat = FindUserCombat(uid);
            combat.CombatCoin = MathUtils.Subtraction(combat.CombatCoin, count, 0);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.CombatCoin, UpdateCoinOperate.Consume);
        }

        public static void RewardsGuildCoin(int uid, int count)
        {
            UserGuildCache guild = FindUserGuild(uid);

            guild.GuildCoin = MathUtils.Addition(guild.GuildCoin, count, int.MaxValue);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.GuildCoin, UpdateCoinOperate.NormalReward);

        }

        public static void ConsumeGuildCoin(int uid, int count)
        {
            UserGuildCache guild = FindUserGuild(uid);
            guild.GuildCoin = MathUtils.Subtraction(guild.GuildCoin, count, 0);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.GuildCoin, UpdateCoinOperate.Consume);
        }


        public static bool PayDiamond(int uid, int count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return false;
            basis.BuyDiamond = MathUtils.Addition(basis.BuyDiamond, count, int.MaxValue / 2);
            
            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Diamond, UpdateCoinOperate.PayReward);

            // 成就
            AchievementProcess(uid, AchievementType.Diamond, count.ToString());

            return true;
        }

        public static bool OnWebPay(int uid, int payId)
        {
            var basis = UserHelper.FindUserBasis(uid);
            var paycfg = new ShareCacheStruct<Config_Pay>().FindKey(payId);
            if (paycfg == null)
            {
                return false;
            }

            int deliverNum = paycfg.AcquisitionDiamond + paycfg.PresentedDiamond;
            int oldVipLv = basis.VipLv;

            UserPayCache userpay = UserHelper.FindUserPay(uid);

            userpay.PayMoney += paycfg.PaySum;
            basis.VipLv = userpay.ConvertPayVipLevel();

            // 这里刷新排行榜数据
            var combat = FindRankUser(uid, RankType.Combat);
            combat.VipLv = basis.VipLv;
            var level = FindRankUser(uid, RankType.Level);
            level.VipLv = basis.VipLv;
            //var fightvaluer = FindRankUser(uid, RankType.FightValue);
            //fightvaluer.VipLv = basis.VipLv;


            if (!PayDiamond(uid, deliverNum))
            {
                return false;
            }

            if (paycfg.id == 102)
            {// 是否季卡
                PayQuarterCard(uid);
            }
            else if (paycfg.id == 101)
            {// 是否月卡
                PayMonthCard(uid);
            }

            if (oldVipLv != basis.VipLv)
            {
                VipLvChangeNotification(uid);
            }

            PushMessageHelper.UserPaySucceedNotification(GameSession.Get(uid));

            return true;
        }

        /// <summary>
        /// 添加季卡
        /// </summary>
        /// <param name="userId"></param>
        public static bool PayQuarterCard(int userId)
        {
            UserPayCache userpay = UserHelper.FindUserPay(userId);


            bool isAward = false;
            if (userpay.QuarterCardDays < 0)
            {
                isAward = true;
                userpay.QuarterCardDays = 89;
            }
            else
            {
                userpay.QuarterCardDays += 90;
            }
            userpay.QuarterCardAwardDate = DateTime.Now;
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "恭喜您成功充值季卡",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("季卡有效期间，每天都会发送给您 {0} 钻石奖励哦！ ",
                            ConfigEnvSet.GetInt("System.QuarterCardDiamond")),
            };

            AddNewMail(userId, mail);

            if (isAward)
            {
                mail = new MailData()
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = "季卡奖励",
                    Sender = "系统",
                    Date = DateTime.Now,
                    Context = string.Format("这是今天您的季卡奖励，您的季卡剩余时间还有 {0} 天！", userpay.QuarterCardDays),
                    ApppendCoinType = CoinType.Diamond,
                    ApppendCoinNum = ConfigEnvSet.GetInt("System.QuarterCardDiamond").ToNotNullString("0")
                };
                AddNewMail(userId, mail);
            }

            return true;

        }

        /// <summary>
        /// 添加月卡
        /// </summary>
        /// <param name="userId"></param>
        public static bool PayMonthCard(int userId)
        {
            UserPayCache userpay = UserHelper.FindUserPay(userId);

            bool isAward = false;
            if (userpay.MonthCardDays < 0)
            {
                isAward = true;
                userpay.MonthCardDays = 29;
            }
            else
            {
                userpay.MonthCardDays += 30;
            }

            userpay.MonthCardAwardDate = DateTime.Now;
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "恭喜您成功充值月卡",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("月卡有效期间，每天都会发送给您 {0} 钻石奖励哦！ ",
                            ConfigEnvSet.GetInt("System.MonthCardDiamond")),
            };

            AddNewMail(userId, mail);

            if (isAward)
            {
                mail = new MailData()
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = "月卡奖励",
                    Sender = "系统",
                    Date = DateTime.Now,
                    Context = string.Format("这是今天您的月卡奖励，您的月卡剩余时间还有 {0} 天！", userpay.MonthCardDays),
                    ApppendCoinType = CoinType.Diamond,
                    ApppendCoinNum = ConfigEnvSet.GetInt("System.MonthCardDiamond").ToNotNullString("0")
                };

                AddNewMail(userId, mail);
            }
            return true;
        }

        public static void RewardsGold(int uid, BigInteger count, UpdateCoinOperate updateType = UpdateCoinOperate.NormalReward, bool isElfSkillAdd = false)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            UserElfCache elf = FindUserElf(uid);

            if (isElfSkillAdd && updateType != UpdateCoinOperate.OffineReward && elf.SelectElfType == ElfSkillType.OnlineGold)
            {
                basis.AddGold(count + count / 1000 * elf.SelectElfValue);
            }
            else
            {
                basis.AddGold(count);
            }
            
            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Gold, updateType);

            // 成就
            AchievementProcess(uid, AchievementType.Gold, count.ToString());
        }

        public static void RewardsGold(int uid, string strCount, UpdateCoinOperate updateType = UpdateCoinOperate.NormalReward, bool isElfSkillAdd = false)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            BigInteger count = BigInteger.Parse(strCount);

            UserElfCache elf = FindUserElf(uid);

            if (isElfSkillAdd && updateType != UpdateCoinOperate.OffineReward && elf.SelectElfType == ElfSkillType.OnlineGold)
            {
                basis.AddGold(count + count / 1000 * elf.SelectElfValue);
            }
            else
            {
                basis.AddGold(count);
            }
            
            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Gold, updateType);

            // 成就
            AchievementProcess(uid, AchievementType.Gold, count.ToString());
        }

        public static void ConsumeGold(int uid, BigInteger count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.SubGold(count);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Gold, UpdateCoinOperate.Consume);
        }

        public static void ConsumeGold(int uid, string unitsValue)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            BigInteger bi = BigInteger.Parse(unitsValue);
            basis.SubGold(bi);

            PushMessageHelper.UserCoinChangedNotification(GameSession.Get(uid), CoinType.Gold, UpdateCoinOperate.Consume);
        }

        public static void RefreshUserFightValue(int uid, bool isNotification = true)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            UserAttributeCache attribute = FindUserAttribute(uid);
            UserEquipsCache equips = FindUserEquips(uid);


            attribute.ResetAtt();
            attribute.AppandBaseAttribute(basis.UserLv);
            attribute.AppandEquipAttribute(basis.UserLv, equips.Weapon);
            attribute.AppandEquipAttribute(basis.UserLv, equips.Coat);
            attribute.AppandEquipAttribute(basis.UserLv, equips.Ring);
            attribute.AppandEquipAttribute(basis.UserLv, equips.Shoe);
            attribute.AppandEquipAttribute(basis.UserLv, equips.Accessory);

            attribute.AppandSoulAttribute(FindUserSoul(uid));
            attribute.AppandElfAttribute(FindUserElf(uid));

            attribute.ConvertFightValue();

            if (isNotification)
            {
                PushMessageHelper.UserAttributeChangedNotification(GameSession.Get(uid));
            }

            // 这里刷新排行榜数据
            var combat = FindRankUser(uid, RankType.Combat);
            if (combat != null) combat.FightValue = attribute.FightValue;
            var level = FindRankUser(uid, RankType.Level);
            if (level != null) level.FightValue = attribute.FightValue;
            //var fightvaluer = FindRankUser(uid, RankType.FightValue);
            //if (fightvaluer != null) fightvaluer.FightValue = attribute.FightValue;


        }

        /// <summary>  
        /// 用户升级处理
        /// </summary>  
        /// <returns></returns>  
        public static void UserLevelUp(int uid)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;

            
            RefreshUserFightValue(uid);

            //UserAchievementCache achieve = FindUserAchievement(uid);


            //PushMessageHelper.UserLevelUpNotification(GameSession.Get(uid));


            // 这里刷新排行榜数据
            var combat = FindRankUser(uid, RankType.Combat);
            combat.UserLv = basis.UserLv;
            var level = FindRankUser(uid, RankType.Level);
            level.UserLv = basis.UserLv;
            //var fightvaluer = FindRankUser(uid, RankType.FightValue);
            //fightvaluer.UserLv = basis.UserLv;
        }

        public static bool UserLevelUpCheck(int uid, int levelId)
        {
            var basis = FindUserBasis(uid);
            var skill = FindUserSkill(uid);
            var roleInitialSet = new ShareCacheStruct<Config_RoleInitial>();
            if (roleInitialSet.FindKey(levelId) == null)
                return false;

            if (levelId == basis.UserLv)
            {
                if (roleInitialSet.FindKey(levelId + 1) != null)
                {
                    basis.UserLv = levelId + 1;
                    UserLevelUp(uid);

                    // 技能
                    if (basis.UserLv % 10 == 0 && (basis.UserLv / 10) % 2 == 0)
                    {
                        var skillcfg = new ShareCacheStruct<Config_Skill>().Find(t => (
                            t.SkillGroup == basis.Profession && t.SkillID % 10000 == basis.UserLv / 10)
                        );
                        if (skillcfg != null)
                        {
                            if (skill.AddSkill(skillcfg.SkillID))
                            {
                                PushMessageHelper.NewSkillNotification(GameSession.Get(uid), skillcfg.SkillID);
                            }
                        }

                    }

                    // 邀请处理
                    if (basis.UserLv == 6)
                    {
                        var selflist = Util.FindUserCenterUser(basis.Pid, basis.RetailID, basis.ServerID);
                        if (selflist.Count > 0 && !string.IsNullOrEmpty(selflist[0].Unid))
                        {
                            var inviterlist = Util.FindUserCenterUser(selflist[0].Unid, basis.RetailID, basis.ServerID);
                            if (inviterlist.Count > 0 && inviterlist[0].UserID != 0)
                            {
                                UserBasisCache inviter = FindUserBasis(inviterlist[0].UserID);
                                inviter.InviteCount++;
                                PushMessageHelper.NewInviteSucceedNotification(GameSession.Get(inviterlist[0].UserID));
                            }

                        }
                    }

                        
                }

                // 每日
                if (levelId % 5 == 0)
                {
                    EveryDayTaskProcess(uid, TaskType.PassStageBoss, 1);
                }
                else
                {
                    EveryDayTaskProcess(uid, TaskType.PassStage, 1);
                }

                // 成就
                AchievementProcess(uid, AchievementType.LevelCount);
            }
            return true;
        }


        public static void RewardsItems(int uid, List<ItemData> list)
        {
            UserPackageCache package = FindUserPackage(uid);
            
            foreach (var v in list)
            {
                if (package.AddItem(v.ID, v.Num, true))
                {
                    var item = new ShareCacheStruct<Config_Item>().FindKey(v.ID);
                    if (item.ItemType == ItemType.Gem)
                    {
                        // 成就
                        AchievementProcess(uid, AchievementType.Gem, v.Num.ToString(), v.ID);
                    }
                }
            }
            
            PushMessageHelper.UserNewItemNotification(GameSession.Get(uid), package.NewItemCache.ToList());
            package.NewItemCache.Clear();
        }
        public static void RewardsItem(int uid, int itemId, int itemNum)
        {
            UserPackageCache package = FindUserPackage(uid);
            if (package.AddItem(itemId, itemNum, true))
            {
                var item = new ShareCacheStruct<Config_Item>().FindKey(itemId);
                if (item.ItemType == ItemType.Gem)
                {
                    // 成就
                    AchievementProcess(uid, AchievementType.Gem, itemNum.ToNotNullString("0"), itemId);
                }

                PushMessageHelper.UserNewItemNotification(GameSession.Get(uid), package.NewItemCache.ToList());
                package.NewItemCache.Clear();
            }
           
        }

        public static void RewardsElf(int uid, int elfId, bool isExperience = false, long experienceTimeMin = 0)
        {
            UserElfCache elf = FindUserElf(uid);

            if (elf.AddElf(elfId, isExperience, experienceTimeMin))
            {
                RefreshUserFightValue(uid);

                PushMessageHelper.NewElfNotification(GameSession.Get(uid), elfId);
            }
            
        }

        public static void AddNewMail(int uid, MailData mail, bool isNotification = true)
        {
            if (mail == null)
                return;
            UserMailBoxCache mailbox = FindUserMailBox(uid);

            mailbox.MailList.Add(mail);
            if (mailbox.MailList.Count > DataHelper.MaxMailNum)
            {
                MailData removemail = mailbox.MailList[0];
                mailbox.MailList.Remove(removemail);
            }
            if (isNotification)
            {
                PushMessageHelper.NewMailNotification(GameSession.Get(uid), mail.ID);
            }
            
        }

        public static void AddQuarterCardMail(int uid)
        {
            UserPayCache pay = FindUserPay(uid);
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "季卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的季卡奖励，您的季卡 {0} 天后到期！", pay.QuarterCardDays + 1),
                ApppendCoinType = CoinType.Diamond,
                ApppendCoinNum = ConfigEnvSet.GetInt("System.QuarterCardDiamond").ToNotNullString("0")
            };

            AddNewMail(uid, mail);
        }
        public static void AddMouthCardMail(int uid)
        {
            UserPayCache pay = FindUserPay(uid);
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "月卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的月卡奖励，您的月卡 {0} 天后到期！", pay.MonthCardDays + 1),
                ApppendCoinType = CoinType.Diamond,
                ApppendCoinNum = ConfigEnvSet.GetInt("System.MonthCardDiamond").ToNotNullString("0")
            };

            AddNewMail(uid, mail);
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
            //if (property == "DiamondChange")
            //{
            //    GameSession session = GameSession.Get(userId);
            //    if (session != null && session.Connected)
            //        PushMessageHelper.UserDiamondChangedNotification(session);
            //}
            //else if (property == "FightValueChange")
            //{
            //    UserBasisCache user = FindUserBasis(userId);
            //    // 这里刷新排行榜数据

            //    GameSession session = GameSession.Get(userId);
            //    if (session != null && session.Connected)
            //        PushMessageHelper.UserFightValueChangedNotification(session);
            //}
            //else if (property == "LevelUp")
            //{
            //    UserBasisCache userbasis = FindUserBasis(userId);
            //    UserAchievementCache userachieve = FindUserAchievement(userId);
                
            //    // 成就
            //    AchievementData achdata = userachieve.AchievementList.Find(t => (t.Type == AchievementType.LevelCount));
            //    if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
            //    {
            //        achdata.Count = userbasis.UserLv;
            //        var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
            //        if (achdata.Count >= achconfig.ObjectiveNum)
            //        {
            //            achdata.IsFinish = true;
            //            GameSession session = GameSession.Get(userId);
            //            if (session != null && session.Connected)
            //                PushMessageHelper.AchievementFinishNotification(session, achdata.ID);
            //        }
            //    }
                
            //    GameSession usession = GameSession.Get(userId);
            //    if (usession != null && usession.Connected)
            //        PushMessageHelper.UserLevelUpNotification(usession);
            //}
            //else if (property == "NewMail")
            //{
            //    GameSession session = GameSession.Get(userId);
            //    if (session != null && session.Connected)
            //        PushMessageHelper.NewMailNotification(session, value.ToString());
            //}


        }


        public static void EveryDayTaskProcess(int UserId, TaskType id, int count, bool isNotification = true)
        {
            UserBasisCache basis = FindUserBasis(UserId);
            UserTaskCache task = FindUserTask(UserId);
            UserDailyQuestData dailyQuest = task.FindTask(id);
            if (dailyQuest == null)
                return;
            if (dailyQuest.Status != TaskStatus.No)
                return;

            var taskconfig = new ShareCacheStruct<Config_Task>().FindKey(id);
            dailyQuest.Count += count;
            if (dailyQuest.Count >= taskconfig.ObjectiveNum)
            {
                dailyQuest.Status = TaskStatus.Finished;
            }

            if (isNotification)
            {
                GameSession session = GameSession.Get(UserId);
                if (session != null && session.Connected)
                    PushMessageHelper.DailyQuestUpdateNotification(session, id);
            }


        }
        public static Config_Lottery RandomLottery(int userlv)
        {
            var list = new ShareCacheStruct<Config_Lottery>().FindAll(t => (t.Level <= userlv));

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

        public static Config_LotteryGem RandomLotteryGem()
        {
            var list = new ShareCacheStruct<Config_LotteryGem>().FindAll();

            if (list.Count > 0)
            {
                int weight = 0;
                foreach (var cl in list)
                {
                    weight += cl.Weight;
                }
                Config_LotteryGem lott = null;
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
            return null;
        }


        /// <summary>
        /// 充值成功vip等级改变通知
        /// </summary>
        /// <param name="classId"></param>
        public static void VipLvChangeNotification(int userId)
        {
            //UserBasisCache user = FindUserBasis(userId);
            //if (user != null)
            //{
            //    string context = string.Format("恭喜 {0} 成为VIP{1}，引来众人羡煞的目光！", user.NickName, user.VipLv);
            //    GlobalRemoteService.SendNotice(NoticeMode.World, context);

            //    //var chatService = new TryXChatService();
            //    //chatService.SystemSend(context);
            //    //PushMessageHelper.SendSystemChatToOnlineUser();
            //}
        }


        public static void TransferExpireCheck(int userId)
        {
            if (userId == 0)
                return;
            var transfer = FindUserTransfer(userId);
            if (transfer != null)
            {
                List<ReceiveTransferItemData> removeList = new List<ReceiveTransferItemData>();
                if (transfer.ReceiveList.RemoveAll(t => (
                            DateTime.Now.Subtract(t.Date).TotalDays >= 1.0
                        ), out removeList))
                {
                    foreach (var v in removeList)
                    {
                        var senderTransfer = FindUserTransfer(v.Sender);
                        var sendTransfer = senderTransfer.FindSend(v.ID);
                        senderTransfer.SendList.Remove(sendTransfer);
                    }
                }
            }
        }

        public static void ElfExperienceExpireCheck(int userId)
        {
            if (userId == 0)
                return;
            var elf = FindUserElf(userId);
            if (elf != null)
            {
                elf.ElfList.RemoveAll(t => (
                            t.IsExperience && DateTime.Now.Subtract(t.Date).TotalMinutes >= t.ExperienceTimeMin
                        ));
                if (elf.FindElf(elf.SelectID) == null)
                {
                    elf.SelectElfType = ElfSkillType.None;
                    elf.SelectElfValue = 0;
                }
            }
        }
    }
}