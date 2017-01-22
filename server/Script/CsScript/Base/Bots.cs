using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Com.Rank;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.CsScript.Com;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using GameServer.Script.CsScript.Com;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 机器人
    /// </summary>
    public static class Bots
    {
        private class ChatBot
        {
            public int UserId;
            public int chatCount;
            public int maxChatCount;

        }

        public class FightBot
        {
            public int UserId;
            /// <summary>
            /// 邀请时间
            /// </summary>
            public DateTime InviteTime;

            /// <summary>
            /// 邀请机器人战斗的玩家UserId
            /// </summary>
            public int PlayerUserId;

            public bool IsCanFight()
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(InviteTime);
                int inviteSecond = timeSpan.TotalSeconds.ToInt();
                return inviteSecond >= 3;
            }
        }

        private static List<GameUser> BotsList = new List<GameUser>();

        private static List<ChatBot> ChatList = new List<ChatBot>();

        private static int ChatIntervalCount = 0;

        private static List<FightBot> FightList = new List<FightBot>();

        private static int OnlineIntervalCount = 0;

        private static int PayVipIntervalCount = 0;

        private static int CombatItemIntervalCount = 0;

        private static int OccupyIntervalCount = 0;

        //private static int TempCount = 0;

        public static void InitBots()
        {
            new PersonalCacheStruct<GameUser>().LoadFrom(t => (t.EnterServerId == 0));

            int maxBotNum = 20;
            int currBotNum = 0;
            var list = FindBots();
            if (list == null)
                currBotNum = 0;
            else
                currBotNum = list.Count;
            CreateBots(maxBotNum - currBotNum);
            BotsList = FindBots();

            Random random = new Random();
            foreach (var v in BotsList)
            {
                if (v.UserLv == 1)
                {
                    v.UserLv = (random.Next(7) + 8).ToShort();
                }
                var roleGradeCache = new ShareCacheStruct<Config_RoleGrade>();

                var grade = roleGradeCache.FindKey(v.UserLv - 1);
                if (grade != null)
                {
                    v.BaseExp = grade.BaseExp;
                    v.FightExp = grade.FightExp;
                }
                v.RefreshFightValue();

                if (v.ClassData.ClassID == 0)
                {
                    int classlv = v.UserLv / 2 + 1;
                    List<ClassDataCache> classeslist = new ShareCacheStruct<ClassDataCache>().FindAll(t => (t.Lv == classlv));
                    List<ClassDataCache> findclasseslist = new ShareCacheStruct<ClassDataCache>().FindAll(
                                        t => (t.Lv == classlv) && t.MemberList.Count < ConfigEnvSet.GetInt("Class.OpenNum")
                                    );

                    ClassDataCache classdata = null;
                    int extClassID = DataHelper.ExtendClass(classlv);
                    if (extClassID == 0)
                    {
                        classdata = findclasseslist[0];
                    }
                    else
                    {
                        classdata = new ShareCacheStruct<ClassDataCache>().FindKey(extClassID);
                    }
                    
                    v.ClassData.ClassID = classdata.ClassID;
                    v.ChallengeMonitorTimes = 0;
                    classdata.MemberList.Add(v.UserID);
                    if (classdata.MemberList.Count == 1 || classdata.Monitor == 0)
                    {// 设置班长
                        classdata.Monitor = v.UserID;
                    }
                }
            }

        } 

        public static List<GameUser> FindBots()
        {
            return new PersonalCacheStruct<GameUser>().FindGlobal(t => (t.EnterServerId == 0));
        }
        private static void CreateBots(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; ++i)
            {
                string passport = string.Empty;
                string password = string.Empty;
                Util.CrateAccount(out passport, out password);

                // 机器人的区服ID为0
                int serverId = 0;
                var cache = new ShareCacheStruct<UserCenterUser>();
                var ucu = cache.Find(t => (t.PassportID == passport && t.ServerId == serverId));
                if (ucu == null)
                {
                    //not user create it.
                    ucu = Util.CreateUserCenterUser(passport, serverId);
                }
                ucu.AccessTime = DateTime.Now;
                ucu.LoginNum++;

                GameUser gameUser = UserHelper.FindUser(ucu.UserId);
                if (gameUser == null)
                {
                    var botsName = new ShareCacheStruct<Config_BotsName>().FindAll();
                    string nickName, lastName, firstName;
                    int randValue = random.Next(botsName.Count);
                    lastName = botsName[randValue].String;
                    randValue = random.Next(botsName.Count);
                    firstName = botsName[randValue].Value;
                    nickName = lastName + firstName;

                    var roleFunc = new RoleFunc();
                    string msg;

                    if (roleFunc.VerifyRange(nickName, out msg) ||
                        roleFunc.VerifyKeyword(nickName, out msg) ||
                        roleFunc.IsExistNickName(nickName, out msg))
                    {
                        continue;
                    }


                    Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
                    Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                    var combat = combatranking as CombatRanking;
                    var level = levelranking as LevelRanking;

                    //Ranking<UserRank> fightvalueranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
                    if (combat == null || level == null)
                    {
                        continue;
                    }



                    gameUser = new GameUser(ucu.UserId);
                    gameUser.IsRefreshing = true;
                    gameUser.SessionID = "";
                    gameUser.EnterServerId = serverId;
                    gameUser.Pid = passport;
                    gameUser.RetailID = "0000";
                    gameUser.NickName = nickName;
                    gameUser.UserLv = 1;
                    gameUser.UserStage = SubjectStage.PreschoolSchool;
                    gameUser.GiveAwayDiamond = ConfigEnvSet.GetInt("User.InitDiamond");
                    gameUser.Vit = DataHelper.InitVit;
                    gameUser.VipLv = random.Next(4) + 3;
                    gameUser.LooksId = random.Next(4);
                    gameUser.UserStatus = UserStatus.MainUi;
                    gameUser.LoginDate = DateTime.Now;
                    gameUser.CreateDate = DateTime.Now;
                    gameUser.OfflineDate = DateTime.Now;
                    //user.ClassData = new UserClassData();
                    //user.StudyTaskData = new UserStudyTaskData();
                    //user.ExerciseTaskData = new UserExerciseTaskData();
                    //user.ExpData = new UserExpData();
                    //user.CombatData = new UserCombatData();
                    gameUser.CombatData.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
                    gameUser.CampaignTicketNum = ConfigEnvSet.GetInt("User.RestoreCampaignTicketNum");
                    //user.EventAwardData.OnlineStartTime = DateTime.Now;
                    gameUser.PlotId = 0;
                    gameUser.IsOnline = true;
                    gameUser.InviteFightDiamondNum = 0;
                    gameUser.ResetInviteFightDiamondDate = DateTime.Now;
                    //user.FriendsData = new UserFriendsData();
                    gameUser.RefreshFightValue();
                    

                    var cacheSet = new PersonalCacheStruct<GameUser>();
                    cacheSet.Add(gameUser);
                    cacheSet.Update();

                    UserHelper.RestoreUserData(ucu.UserId);


                    // 加入排行榜
                    UserRank rankInfo = new UserRank()
                    {
                        UserID = gameUser.UserID,
                        NickName = gameUser.NickName,
                        LooksId = gameUser.LooksId,
                        UserLv = gameUser.UserLv,
                        VipLv = gameUser.VipLv,
                        IsOnline = true,
                        RankId = int.MaxValue,
                        Exp = gameUser.TotalExp,
                        FightingValue = gameUser.FightingValue,
                        RankDate = DateTime.Now,
                    };
                    combat.TryAppend(rankInfo);
                    combat.rankList.Add(rankInfo);
                    UserRank lvUserRank = new UserRank(rankInfo);
                    level.TryAppend(lvUserRank);
                    level.rankList.Add(lvUserRank);
                    //fightvalueranking.TryAppend(rankInfo);


                    // 充值数据
                    UserPayCache paycache = new UserPayCache()
                    {
                        UserID = gameUser.UserID,
                        PayMoney = 0,
                        IsReceiveFirstPay = false,
                        WeekCardDays = -1,
                        MonthCardDays = 2,
                        WeekCardAwardDate = DateTime.Now,
                        MonthCardAwardDate = DateTime.Now,
                    };
                    var payCacheSet = new PersonalCacheStruct<UserPayCache>();
                    payCacheSet.Add(paycache);
                    payCacheSet.Update();

                    gameUser.RandItem(random.Next(5) + 15);
                    gameUser.RandSkillBook(random.Next(8) + 10);

                    
                    if (gameUser == null)
                        continue;
                    roleFunc.OnCreateAfter(gameUser);
                }
                else
                {
                    continue;
                }
            }
   
        }


        public static void Chat()
        {
            if (BotsList.Count <= 0)
                return;

            ChatIntervalCount++;
            if (ChatIntervalCount < 5)
                return;

            ChatIntervalCount = 0;

            Random random = new Random();
            if (random.Next(1000) < 200)
            {
                return;
            }
            
            if (ChatList.Count < 3)
            {
                if (random.Next(1000) < 800)
                {
                    GameUser bot = BotsList[random.Next(BotsList.Count)];
                    ChatBot cb = new ChatBot()
                    {
                        UserId = bot.UserID,
                        chatCount = 0,
                        maxChatCount = 1//random.Next(3) + 2
                    };
                    ChatList.Add(cb);
                }
            }


            if (ChatList.Count <= 0)
                return;

            ChatBot chatbot = ChatList[random.Next(ChatList.Count)];
            GameUser gbot = BotsList.Find(t => (t.UserID == chatbot.UserId));
            if (gbot == null)
                return;

            var list = new ShareCacheStruct<Config_BotsChat>().FindAll();
            
            var chatService = new TryXChatService(gbot);
            chatService.Send(ChatType.World, list[random.Next(list.Count)].Word);
            PushMessageHelper.SendWorldChatToOnlineUser();

            chatbot.chatCount++;
            if (chatbot.chatCount >= chatbot.maxChatCount)
            {
                ChatList.Remove(chatbot);
            }
        }


        public static void AddFightBot(FightBot fbot)
        {
            lock (FightList)
            {
                FightList.Add(fbot);
            }
            
        }

        public static void FightResponse()
        {
            if (BotsList.Count <= 0)
                return;

            var list = FightList.FindAll(t => (t.IsCanFight()));
            foreach (var v in list)
            {
                GameUser bot = BotsList.Find(t => (t.UserID == v.UserId));
                GameUser player = UserHelper.FindUser(v.PlayerUserId);
                if (bot == null || player == null)
                    continue;
                player.UserStatus = UserStatus.Fighting;
                //bot.UserStatus = UserStatus.Fighting;

                EventStatus retresult = EventStatus.Good;
                //float diff = (float)ContextUser.GetCombatFightValue() / dest.GetCombatFightValue();
                float diff = (float)player.FightingValue / bot.FightingValue;
                if (diff > 1.1f)
                {
                    retresult = EventStatus.Good;
                }
                else if (diff < 0.9f)
                {
                    retresult = EventStatus.Bad;
                }
                else
                {
                    int skilllv = 0, skilllv2 = 0;
                    if (player.SkillCarryList.Count > 0)
                    {
                        var skdata = player.findSkill(player.SkillCarryList[0]);
                        if (skdata != null)
                            skilllv = skdata.Lv;
                    }
                    if (bot.SkillCarryList.Count > 0)
                    {
                        var skdata = bot.findSkill(bot.SkillCarryList[0]);
                        if (skdata != null)
                            skilllv2 = skdata.Lv;
                    }

                    if (diff >= 1.0f)
                    {
                        if (skilllv - skilllv2 > -2)
                            retresult = EventStatus.Good;
                        else
                            retresult = EventStatus.Bad;
                    }
                    else
                    {
                        if (skilllv - skilllv2 > 2)
                            retresult = EventStatus.Good;
                        else
                            retresult = EventStatus.Bad;
                    }
                }


                PushMessageHelper.StartInviteFightNotification(GameSession.Get(player.UserID), bot.UserID, retresult);
            }

            lock (FightList)
            {
                foreach (var v in list)
                {
                    FightList.Remove(v);
                }
            }
        }

        /// <summary>
        /// 上线通知
        /// </summary>
        public static void OnlineNotification()
        {
            if (BotsList.Count <= 0)
                return;

            OnlineIntervalCount++;
            if (OnlineIntervalCount < 200)
                return;

            OnlineIntervalCount = 0;

            Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);

            int pageCount;
            var combatlist = combatranking.GetRange(0, 10, out pageCount);
            var levellist = levelranking.GetRange(0, 10, out pageCount);

            List<int> combattemp = new List<int>();
            List<int> leveltemp = new List<int>();
            foreach (var v in combatlist)
            {
                var bot = BotsList.Find(t => (t.UserID == v.UserID));
                if (bot == null)
                    continue;
                combattemp.Add(v.UserID);
            }
            foreach (var v in levellist)
            {
                var bot = BotsList.Find(t => (t.UserID == v.UserID));
                if (bot == null)
                    continue;
                leveltemp.Add(v.UserID);
            }

            int uid = 0;
            Random random = new Random();

            if (leveltemp.Count > 0)
            {
                uid = leveltemp[random.Next(leveltemp.Count)];

                UserRank levelrank = UserHelper.FindLevelRankUser(uid);
                var finalbot = BotsList.Find(t => (t.UserID == uid));
                string context = string.Format("排行榜排名第{0}名的 {1} 上线了！", levelrank.RankId, finalbot.NickName);

                PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                var chatService = new TryXChatService();
                chatService.SystemSend(context);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }

        }

        /// <summary>
        /// vip等级变更
        /// </summary>
        public static void PayVipNotification()
        {
            if (BotsList.Count <= 0)
                return;

            PayVipIntervalCount++;
            if (PayVipIntervalCount < 1000)
                return;

            PayVipIntervalCount = 0;
            Random random = new Random();
            var bot = BotsList[random.Next(BotsList.Count)];

            UserHelper.VipLvChangeNotification(bot.UserID);
        }

        /// <summary>
        /// 获得竞技道具
        /// </summary>
        public static void CombatItemNotification()
        {
            if (BotsList.Count <= 0)
                return;

            CombatItemIntervalCount++;
            if (CombatItemIntervalCount < 300)
                return;

            CombatItemIntervalCount = 0;
            Random random = new Random();
            var bot = BotsList[random.Next(BotsList.Count)];
            int randv = random.Next(6) + 1;
            UserHelper.CombatItemNotification(bot.UserID, 10000 + randv);
        }

        /// <summary>
        /// 占领
        /// </summary>
        public static void Occupy()
        {
            if (BotsList.Count <= 0)
                return;

            OccupyIntervalCount++;
            if (OccupyIntervalCount < 1200)
                return;

            OccupyIntervalCount = 0;

            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            var findnull = occupycache.Find(t => (t.UserId == 0 && t.ChallengerId == 0));
            if (findnull == null)
                return;


            CombatItemIntervalCount = 0;
            Random random = new Random();
            var bot = BotsList[random.Next(BotsList.Count)];

            var findlist = occupycache.FindAll(t => (t.UserId == bot.UserID));
            foreach (var fv in findlist)
            {
                fv.ResetOccupy();
            }

            findnull.UserId = bot.UserID;
            findnull.NickName = bot.NickName;

            bot.OccupySceneList.Add(findnull.SceneId);

            ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(bot.ClassData.ClassID);
            if (classdata != null)
            {
                PushMessageHelper.ClassOccupyAddChangeNotification(bot.ClassData.ClassID);
            }

            UserHelper.OccupySucceedNotification(findnull.SceneId);

        }

        /// <summary>
        /// 参加竞选
        /// </summary>
        public static void Campaign()
        {
            //if (TempCount > 0)
            //    return;
            //TempCount++;
            if (BotsList.Count <= 0)
                return;

            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            var fdnow = jobcache.FindKey((JobTitleType)DateTime.Now.DayOfWeek);
            if (fdnow == null)
                return;
            if (fdnow.Status == CampaignStatus.NotStarted)
            {
                return;
            }
            if (fdnow.Status == CampaignStatus.Over)
            {
                return;
            }

            for (int i = 0; i < 5; ++i)
            {
                Random random = new Random();
                var bot = BotsList[random.Next(BotsList.Count)];
                
                if (fdnow.CampaignUserList.Find(t => (t.UserId == bot.UserID)) != null)
                {
                    continue;
                }

                // 到这里就表示成功了
                CampaignUserData campaignuserdata = new CampaignUserData()
                {
                    UserId = bot.UserID,
                    NickName = bot.NickName,
                    ClassId = bot.ClassData.ClassID,
                    VoteCount = random.Next(30) + 10,
                    LooksId = bot.LooksId
                };
                fdnow.CampaignUserList.Add(campaignuserdata);


            }
  
        }

    }
}