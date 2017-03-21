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
                    t.OpenID == openId && t.RetailID == openId && t.ServerID == EnterServerId)
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
                if (on.Connected)
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
            if (basis == null)
            {
                return;
            }

            // 名人榜挑战次数
            combat.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            combat.ButTimes = 0;
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
            if (basis.RestoreDate != DateTime.MinValue && basis.RestoreDate.Month != DateTime.Now.Month)
            {// 下个月签到次数要清零
                eventaward.SignCount = 0;
            }

            eventaward.IsTodaySign = false;
            eventaward.IsTodayReceiveFirstWeek = false;
            eventaward.IsStartedOnlineTime = false;
            //eventaward.TodayOnlineTime = 0;
            eventaward.OnlineAwardId = 1;
            eventaward.OnlineStartTime = DateTime.Now;
            

            // 周卡月卡处理
            UserPayCache paycache = FindUserPay(uid);
            if (paycache != null)
            {
                UserMailBoxCache mailbox = FindUserMailBox(uid);
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

                            AddWeekCardMail(uid);
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

            
            basis.IsTodayLottery = false;
            //basis.RandomLotteryId = 0;
            //var lottery = RandomLottery(basis.UserID, basis.UserLv);
            //if (lottery != null)
            //{
            //    basis.RandomLotteryId = lottery.ID;
            //}
            

            // 切磋钻石处理
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            int lastWeekOfYear = gc.GetWeekOfYear(basis.ResetInviteFightDiamondDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int nowWeekOfYear = gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (lastWeekOfYear != nowWeekOfYear)
            {
                basis.InviteFightDiamondNum = 0;
                basis.ResetInviteFightDiamondDate = DateTime.Now;
            }

            // 红包重置
            basis.IsReceivedRedPacket = false;

            // 设置新的恢复时间
            basis.RestoreDate = DateTime.Now;
        }


        public static void UserOnline(int uid)
        {
            UserBasisCache basis = FindUserBasis(uid);
            UserMailBoxCache mailbox = FindUserMailBox(uid);
            UserEventAwardCache eventaward = FindUserEventAward(uid);
            if (basis == null)
            {
                return;
            }


            basis.LoginDate = DateTime.Now;
            basis.IsOnline = true;
            basis.IsRefreshing = true;
            basis.UserStatus = UserStatus.MainUi;
            basis.InviteFightDestUid = 0;
            basis.IsReceiveOfflineEarnings = false;
            //basis.RandomLotteryId = 0;


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

            // 名人榜处理
            CombatProcess(uid);

            // 在线时间处理
            //if (basis.OfflineDate > basis.LoginDate)
            //{
            //    basis.EventAwardData.OnlineStartTime = basis.LoginDate;
            //}
            //else
            //{// 离线时间比登录时间小，说明当前用户未下线
            //    basis.OfflineDate = DateTime.Now;
            //    //basis.EventAwardData.OnlineStartTime = basis.LoginDate;
            //}

            if (!eventaward.IsStartedOnlineTime)
            {
                eventaward.IsStartedOnlineTime = true;
                eventaward.OnlineStartTime = basis.LoginDate;
                //basis.EventAwardData.TodayOnlineTime = 0;
            }

            // 每日
            UserHelper.EveryDayTaskProcess(basis.UserID, TaskType.Login, 1);

            //if (basis.EventAwardData.OnlineStartTime < basis.EventAwardData.LastOnlineAwayReceiveTime)
            //{
            //    if (basis.OfflineDate > basis.EventAwardData.LastOnlineAwayReceiveTime)
            //    {
            //        basis.EventAwardData.OnlineStartTime = basis.EventAwardData.LastOnlineAwayReceiveTime;
            //    }
            //    else
            //    {
            //        basis.EventAwardData.OnlineStartTime = basis.OfflineDate;
            //    }

            //}


            //TimeSpan timeSpan = DateTime.Now.Subtract(basis.EventAwardData.OnlineStartTime);
            //int sec = (int)Math.Floor(timeSpan.TotalSeconds);

            //basis.EventAwardData.TodayOnlineTime = sec;

        }
        /// <summary>
        /// 用户下线处理
        /// </summary>
        /// <param name="uid"></param>
        public static void UserOffline(int uid)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
            {
                return;
            }
            //basis.IsOnline = false;
            basis.OfflineDate = DateTime.Now;
            basis.UserStatus = UserStatus.MainUi;

            // 名人榜处理
            CombatProcess(uid);


            // 通知好友下线
            //foreach (FriendData fd in basis.FriendsData.FriendsList)
            //{
            //    GameSession session = GameSession.Get(fd.UserId);
            //    if (session != null)
            //    {
            //        PushMessageHelper.FriendOffineNotification(session, basis.UserID);
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
        
        /// <summary>
        /// 格式化输出名人榜日志
        /// </summary>
        /// <param name="logdata"></param>
        /// <returns></returns>
        public static string FormatCombatLog(CombatLogData logdata)
        {
            UserBasisCache basis = FindUserBasis(logdata.UserId);
            if (basis == null)
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
                ret += basis.NickName;
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
                ret += basis.NickName;
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
            var sessionlist = GameSession.GetAll();
            foreach (var session in sessionlist)
            {
                if (session.Connected)
                    RestoreUserData(session.UserId);
            }
            PushMessageHelper.RestoreUserNotification();

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
                    AddNewMail(ur.UserID, mail);
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


        public static void AchievementProcess(int uid, AchievementType type, int addcount = 0, int addId = 0, bool isNotification = true)
        {
            UserAchievementCache userachieve = FindUserAchievement(uid);

            var achdata = userachieve.AchievementList.Find(t => (t.Type == type));
            if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
            {
                
                var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                switch (type)
                {
                    case AchievementType.LevelCount:
                        {
                            UserBasisCache basis = FindUserBasis(uid);
                            achdata.Count = basis.UserLv;
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.FriendCompare:
                    case AchievementType.Gold:
                    case AchievementType.Diamond:
                        {
                            achdata.Count += addcount;
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.UpgradeSkill:
                        {
                            UserSkillCache userSkill = FindUserSkill(uid);
                            var findlist = userSkill.SkillList.FindAll(t => (t.Lv >= achconfig.ObjectiveGrade));
                            achdata.Count = findlist.Count;
                            if (findlist.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.UpgradeEquip:
                        {
                            UserEquipsCache userEquip = FindUserEquips(uid);
                            for (EquipID id = EquipID.Coat; id <= EquipID.Accessory; ++id)
                            {
                                var equip = userEquip.FindEquipData(id);
                                if (equip.Lv >= achconfig.ObjectiveGrade)
                                {
                                    achdata.Count++;
                                }
                            }
                            
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.InlayGem:
                        {
                            UserPackageCache userPackage = FindUserPackage(uid);
                            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(addId);
                            if (itemcfg != null)
                            {
                                if ((int)itemcfg.Quality >= achconfig.ObjectiveGrade)
                                {
                                    achdata.Count += addcount;
                                }
                            }
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.OpenSoul:
                        {
                            UserSoulCache userSoul = FindUserSoul(uid);
                            int soullv = addId != 0 ? addId % 10000 : 0;
                            if (soullv == achconfig.ObjectiveGrade)
                            {
                                achdata.Count = userSoul.OpenList.Count;
                            }
                            else if (soullv > achconfig.ObjectiveGrade)
                            {
                                achdata.Count = achconfig.ObjectiveNum;
                            }
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
                            }
                        }
                        break;
                    case AchievementType.CombatRandID:
                        {
                            UserBasisCache basis = FindUserBasis(uid);
                            achdata.Count = basis.CombatRankID;
                            if (achdata.Count >= achconfig.ObjectiveNum)
                            {
                                achdata.IsFinish = true;
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


        public static void RewardsDiamond(int uid, int count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.RewardsDiamond = MathUtils.Addition(basis.RewardsDiamond, count, int.MaxValue);

            PushMessageHelper.UserDiamondChangedNotification(GameSession.Get(uid));
            // 成就
            AchievementProcess(uid, AchievementType.Diamond, count);
        }

        public static void ConsumeDiamond(int uid, int count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.UsedDiamond = MathUtils.Addition(basis.UsedDiamond, count, int.MaxValue);

            PushMessageHelper.UserDiamondChangedNotification(GameSession.Get(uid));
        }


        public static bool PayDiamond(int uid, int count)
        {
            UserBasisCache user = FindUserBasis(uid);
            if (user == null)
                return false;
            user.BuyDiamond = MathUtils.Addition(user.BuyDiamond, count, int.MaxValue / 2);

            PushMessageHelper.UserDiamondChangedNotification(GameSession.Get(uid));

            // 成就
            AchievementProcess(uid, AchievementType.Diamond, count);

            return true;
        }

        public static void RewardsGold(int uid, BigInteger count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.AddGold(count);
            //basis.Gold = MathUtils.Addition(basis.Gold, count, int.MaxValue);

            PushMessageHelper.UserGoldChangedNotification(GameSession.Get(uid));
        }

        public static void RewardsGold(int uid, string unitsValue)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            BigInteger bi = Util.ConvertGameCoin(unitsValue);
            basis.AddGold(bi);
            //basis.Gold = MathUtils.Addition(basis.Gold, count, int.MaxValue);

            PushMessageHelper.UserGoldChangedNotification(GameSession.Get(uid));
        }

        public static void ConsumeGold(int uid, BigInteger count)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            basis.SubGold(count);

            PushMessageHelper.UserGoldChangedNotification(GameSession.Get(uid));
        }

        public static void ConsumeGold(int uid, string unitsValue)
        {
            UserBasisCache basis = FindUserBasis(uid);
            if (basis == null)
                return;
            BigInteger bi = Util.ConvertGameCoin(unitsValue);
            basis.SubGold(bi);

            PushMessageHelper.UserGoldChangedNotification(GameSession.Get(uid));
        }

        public static void RefreshUserFightValue(int uid, bool isNotification = true)
        {
            UserBasisCache basis = FindUserBasis(uid);
            UserAttributeCache attribute = FindUserAttribute(uid);
            UserEquipsCache equips = FindUserEquips(uid);
            if (basis == null)
                return;

            attribute.ResetAtt();
            attribute.AppandBaseAttribute(basis.UserLv);
            attribute.AppandEquipAttribute(equips.Weapon);
            attribute.AppandEquipAttribute(equips.Coat);
            attribute.AppandEquipAttribute(equips.Ring);
            attribute.AppandEquipAttribute(equips.Shoe);
            attribute.AppandEquipAttribute(equips.Accessory);

            attribute.ConvertFightValue();

            if (isNotification)
            {
                PushMessageHelper.UserAttributeChangedNotification(GameSession.Get(uid));
            }
            
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
            RefreshUserFightValue(uid, false);
            
            UserAchievementCache achieve = FindUserAchievement(uid);


            PushMessageHelper.UserLevelUpNotification(GameSession.Get(uid));

        }


        public static void RewardsItems(int uid, List<ItemData> list)
        {
            UserPackageCache package = FindUserPackage(uid);
            if (package == null)
                return;

            foreach (var v in list)
            {
                package.AddItem(v.ID, v.Num);
            }

            PushMessageHelper.UserNewItemNotification(GameSession.Get(uid));
            
        }
        public static void RewardsItem(int uid, int itemId, int itemNum)
        {
            UserPackageCache package = FindUserPackage(uid);
            if (package == null)
                return;

            if (package.AddItem(itemId, itemNum))
            {
                PushMessageHelper.UserNewItemNotification(GameSession.Get(uid));
            }
           
        }


        public static void AddNewMail(int uid, MailData mail)
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
            PushMessageHelper.NewMailNotification(GameSession.Get(uid), mail.ID);
        }

        public static void AddWeekCardMail(int uid)
        {
            UserPayCache pay = FindUserPay(uid);
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "周卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的周卡奖励，您的周卡 {0} 天后到期！", pay.WeekCardDays + 1),
                ApppendDiamond = ConfigEnvSet.GetInt("System.WeekCardDiamond")
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
                ApppendDiamond = ConfigEnvSet.GetInt("System.MonthCardDiamond")
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


        public static void EveryDayTaskProcess(int UserId, TaskType id, int count)
        {
            UserBasisCache basis = FindUserBasis(UserId);
            UserTaskCache task = FindUserTask(UserId);
            UserDailyQuestData dailyQuest = task.FindTask(id);
            if (dailyQuest == null)
                return;
            if (basis.UserLv < DataHelper.OpenTaskSystemUserLevel || dailyQuest.IsFinished)
                return;

            var taskconfig = new ShareCacheStruct<Config_Task>().FindKey(id);
            dailyQuest.Count += count;
            if (dailyQuest.Count >= taskconfig.ObjectiveNum)
            {
                dailyQuest.IsFinished = true;
            }

            GameSession session = GameSession.Get(UserId);
            if (session != null && session.Connected)
                PushMessageHelper.DailyQuestUpdateNotification(session, id);

        }
        public static Config_Lottery RandomLottery(int userId, int userlv)
        {
            UserPackageCache package = FindUserPackage(userId);
            var list = new ShareCacheStruct<Config_Lottery>().FindAll(t => (t.Level <= userlv));
            List<int> removelist = new List<int>();
            foreach (var v in list)
            {
                if (v.Type == LotteryAwardType.Item)
                {
                    Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(v.Content);
                    if (item != null)
                    {
                        if (item.ItemType == ItemType.Gem)
                        {
                            ItemData itemdata = package.FindItem(v.Content);
                            if (itemdata != null)
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
        /// 充值成功vip等级改变通知
        /// </summary>
        /// <param name="classId"></param>
        public static void VipLvChangeNotification(int userId)
        {
            UserBasisCache user = FindUserBasis(userId);
            if (user != null)
            {
                string context = string.Format("恭喜 {0} 成为VIP{1}，引来众人羡煞的目光！", user.NickName, user.VipLv);
                ChatRemoteService.SendNotice(NoticeMode.World, context);

                //var chatService = new TryXChatService();
                //chatService.SystemSend(context);
                //PushMessageHelper.SendSystemChatToOnlineUser();
            }
        }
    }
}