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
using System.Configuration;

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

            public string chatContext;

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

        public static bool IsInitEnd = false;

        private static List<UserBasisCache> BotsList = new List<UserBasisCache>();

        private static List<ChatBot> ChatList = new List<ChatBot>();

        private static int ChatIntervalCount = 0;

        private static List<FightBot> FightList = new List<FightBot>();

        //private static int OnlineIntervalCount = 0;

        private static int PayVipIntervalCount = 0;


        public static List<UserBasisCache> getBots
        {
            get
            {
                return BotsList;
            }  
        }

        public static void InitBots()
        {
            new PersonalCacheStruct<UserBasisCache>().LoadFrom(t => (t.ServerID == 0));

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
                    int minlv = ConfigurationManager.AppSettings["BotLevelMin"].ToInt();
                    int maxlv = ConfigurationManager.AppSettings["BotLevelMax"].ToInt();
                    int minviplv = ConfigurationManager.AppSettings["BotVipLvMin"].ToInt();
                    int maxviplv = ConfigurationManager.AppSettings["BotVipLvMax"].ToInt();
                    if (minlv != 0 && maxlv != 0)
                    {
                        v.UserLv = (random.Next(maxlv - minlv + 1) + minlv).ToShort();
                    }

                    if (minviplv != 0 && maxviplv != 0)
                    {
                        v.VipLv = random.Next(maxviplv - minviplv + 1) + minviplv;
                    }


                }
                var roleGradeCache = new ShareCacheStruct<Config_RoleInitial>();

                var grade = roleGradeCache.FindKey(v.UserLv - 1);
                if (grade != null)
                {
                    //v.BaseExp = grade.BaseExp;
                    //v.FightExp = grade.FightExp;
                }
                //v.RefreshFightValue();

      
            }

            IsInitEnd = true;
        } 

        public static List<UserBasisCache> FindBots()
        {
            return new PersonalCacheStruct<UserBasisCache>().FindGlobal(t => (t.ServerID == 0));
        }
        private static void CreateBots(int count)
        {
            //Random random = new Random();
            //for (int i = 0; i < count; ++i)
            //{
            //    // 机器人的区服ID为0
            //    int serverId = 0;
            //    var cache = new ShareCacheStruct<UserCenterUser>();
            //    var ucu = cache.Find(t => (t.PassportID == passport && t.ServerID == serverId));
            //    if (ucu == null)
            //    {
            //        //not user create it.
            //        ucu = Util.CreateUserCenterUser(passport, serverId);
            //    }
            //    ucu.AccessTime = DateTime.Now;
            //    ucu.LoginNum++;

            //    UserBasisCache basis = UserHelper.FindUserBasis(ucu.UserId);
            //    if (basis == null)
            //    {
            //        var botsName = new ShareCacheStruct<Config_BotsName>().FindAll();
            //        string nickName, lastName, firstName;
            //        int randValue = random.Next(botsName.Count);
            //        lastName = botsName[randValue].String;
            //        randValue = random.Next(botsName.Count);
            //        firstName = botsName[randValue].Value;
            //        nickName = lastName + firstName;

            //        var roleFunc = new RoleFunc();
            //        string msg;

            //        if (roleFunc.VerifyRange(nickName, out msg) ||
            //            roleFunc.VerifyKeyword(nickName, out msg) ||
            //            roleFunc.IsExistNickName(nickName, out msg))
            //        {
            //            continue;
            //        }


            //        Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            //        Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            //        var combat = combatranking as CombatRanking;
            //        var level = levelranking as LevelRanking;

            //        //Ranking<UserRank> fightvalueranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
            //        if (combat == null || level == null)
            //        {
            //            continue;
            //        }



            //        basis = new UserBasisCache(ucu.UserId);
            //        basis.IsRefreshing = true;
            //        basis.SessionID = "";
            //        basis.EnterServerId = serverId;
            //        basis.Pid = passport;
            //        basis.RetailID = "0000";
            //        basis.NickName = nickName;
            //        basis.UserLv = 1;
            //        basis.UserStage = SubjectStage.PreschoolSchool;
            //        basis.RewardsDiamond = ConfigEnvSet.GetInt("User.InitDiamond");
            //        basis.Vit = DataHelper.InitVit;
            //        basis.VipLv = ConfigEnvSet.GetInt("User.VipLv");
            //        basis.Profession = random.Next(4);
            //        basis.UserStatus = UserStatus.MainUi;
            //        basis.LoginDate = DateTime.Now;
            //        basis.CreateDate = DateTime.Now;
            //        basis.OfflineDate = DateTime.Now;
            //        //user.ClassData = new UserClassData();
            //        //user.StudyTaskData = new UserStudyTaskData();
            //        //user.ExerciseTaskData = new UserExerciseTaskData();
            //        //user.ExpData = new UserExpData();
            //        //user.CombatData = new UserCombatData();
            //        basis.CombatData.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            //        basis.CampaignTicketNum = ConfigEnvSet.GetInt("User.RestoreCampaignTicketNum");
            //        //user.EventAwardData.OnlineStartTime = DateTime.Now;
            //        basis.PlotId = 0;
            //        basis.IsOnline = true;
            //        basis.InviteFightDiamondNum = 0;
            //        basis.ResetInviteFightDiamondDate = DateTime.Now;
            //        //user.FriendsData = new UserFriendsData();
            //        basis.RefreshFightValue();
                    

            //        var cacheSet = new PersonalCacheStruct<UserBasisCache>();
            //        cacheSet.Add(basis);
            //        cacheSet.Update();

            //        UserHelper.RestoreUserData(ucu.UserId);


            //        // 加入排行榜
            //        UserRank rankInfo = new UserRank()
            //        {
            //            UserID = basis.UserID,
            //            NickName = basis.NickName,
            //            Profession = basis.Profession,
            //            UserLv = basis.UserLv,
            //            VipLv = basis.VipLv,
            //            IsOnline = true,
            //            RankId = int.MaxValue,
            //            Exp = basis.TotalExp,
            //            FightingValue = basis.FightingValue,
            //            RankDate = DateTime.Now,
            //        };
            //        combat.TryAppend(rankInfo);
            //        combat.rankList.Add(rankInfo);
            //        UserRank lvUserRank = new UserRank(rankInfo);
            //        level.TryAppend(lvUserRank);
            //        level.rankList.Add(lvUserRank);
            //        //fightvalueranking.TryAppend(rankInfo);


            //        // 充值数据
            //        UserPayCache paycache = new UserPayCache()
            //        {
            //            UserID = basis.UserID,
            //            PayMoney = 0,
            //            IsReceiveFirstPay = false,
            //            WeekCardDays = -1,
            //            MonthCardDays = 2,
            //            WeekCardAwardDate = DateTime.Now,
            //            MonthCardAwardDate = DateTime.Now,
            //        };
            //        var payCacheSet = new PersonalCacheStruct<UserPayCache>();
            //        payCacheSet.Add(paycache);
            //        payCacheSet.Update();

            //        basis.RandItem(random.Next(5) + 15);
            //        basis.RandSkillBook(random.Next(8) + 10);

                    
            //        if (basis == null)
            //            continue;
            //        roleFunc.OnCreateAfter(basis);
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
   
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
           
            if (ChatList.Count <= 0)
            {
                var list = new ShareCacheStruct<Config_BotsChat>().FindAll();
                var chat = list[random.Next(list.Count)];

                UserBasisCache bot = BotsList[random.Next(BotsList.Count)];
                ChatBot cb = new ChatBot()
                {
                    UserId = bot.UserID,
                    chatCount = 0,
                    maxChatCount = 1,//random.Next(3) + 2
                    chatContext = chat.Word
                };
                ChatList.Add(cb);

                if (!chat.Reply.IsEmpty())
                {
                    UserBasisCache rbot = BotsList[random.Next(BotsList.Count)];
                    ChatBot rcb = new ChatBot()
                    {
                        UserId = rbot.UserID,
                        chatCount = 0,
                        maxChatCount = 1,//random.Next(3) + 2
                        chatContext = chat.Reply
                    };
                    ChatList.Add(rcb);
                }

            }


            if (ChatList.Count <= 0)
                return;

            ChatBot chatbot = ChatList[0];
            UserBasisCache gbot = BotsList.Find(t => (t.UserID == chatbot.UserId));
            if (gbot == null)
                return;

            
            
            //var chatService = new TryXChatService(gbot);
            //chatService.Send(ChatType.World, chatbot.chatContext);
            //PushMessageHelper.SendWorldChatToOnlineUser();

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
            //if (BotsList.Count <= 0)
            //    return;

            //var list = FightList.FindAll(t => (t.IsCanFight()));
            //foreach (var v in list)
            //{
            //    UserBasisCache bot = BotsList.Find(t => (t.UserID == v.UserId));
            //    UserBasisCache player = UserHelper.FindUserBasis(v.PlayerUserId);
            //    if (bot == null || player == null)
            //        continue;
            //    player.UserStatus = UserStatus.Fighting;
            //    //bot.UserStatus = UserStatus.Fighting;

            //    EventStatus retresult = EventStatus.Good;
            //    //float diff = (float)GetBasis.GetCombatFightValue() / dest.GetCombatFightValue();
            //    float diff = (float)player.FightingValue / bot.FightingValue;
            //    if (diff > 1.1f)
            //    {
            //        retresult = EventStatus.Good;
            //    }
            //    else if (diff < 0.9f)
            //    {
            //        retresult = EventStatus.Bad;
            //    }
            //    else
            //    {
            //        int skilllv = 0, skilllv2 = 0;
            //        if (player.SkillCarryList.Count > 0)
            //        {
            //            var skdata = player.findSkill(player.SkillCarryList[0]);
            //            if (skdata != null)
            //                skilllv = skdata.Lv;
            //        }
            //        if (bot.SkillCarryList.Count > 0)
            //        {
            //            var skdata = bot.findSkill(bot.SkillCarryList[0]);
            //            if (skdata != null)
            //                skilllv2 = skdata.Lv;
            //        }

            //        if (diff >= 1.0f)
            //        {
            //            if (skilllv - skilllv2 > -2)
            //                retresult = EventStatus.Good;
            //            else
            //                retresult = EventStatus.Bad;
            //        }
            //        else
            //        {
            //            if (skilllv - skilllv2 > 2)
            //                retresult = EventStatus.Good;
            //            else
            //                retresult = EventStatus.Bad;
            //        }
            //    }


            //    PushMessageHelper.StartInviteFightNotification(GameSession.Get(player.UserID), bot.UserID, retresult);
            //}

            //lock (FightList)
            //{
            //    foreach (var v in list)
            //    {
            //        FightList.Remove(v);
            //    }
            //}
        }

        /// <summary>
        /// 上线通知
        /// </summary>
        public static void OnlineNotification()
        {
            //if (BotsList.Count <= 0)
            //    return;

            //OnlineIntervalCount++;
            //if (OnlineIntervalCount < 200)
            //    return;

            //OnlineIntervalCount = 0;

            //Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);

            //int pageCount;
            //var combatlist = combatranking.GetRange(0, 10, out pageCount);
            //var levellist = levelranking.GetRange(0, 10, out pageCount);

            //List<int> combattemp = new List<int>();
            //List<int> leveltemp = new List<int>();
            //foreach (var v in combatlist)
            //{
            //    var bot = BotsList.Find(t => (t.UserID == v.UserID));
            //    if (bot == null)
            //        continue;
            //    combattemp.Add(v.UserID);
            //}
            //foreach (var v in levellist)
            //{
            //    var bot = BotsList.Find(t => (t.UserID == v.UserID));
            //    if (bot == null)
            //        continue;
            //    leveltemp.Add(v.UserID);
            //}

            //int uid = 0;
            //Random random = new Random();

            //if (leveltemp.Count > 0)
            //{
            //    uid = leveltemp[random.Next(leveltemp.Count)];

            //    UserRank levelrank = UserHelper.FindLevelRankUser(uid);
            //    var finalbot = BotsList.Find(t => (t.UserID == uid));
            //    string context = string.Format("排行榜排名第{0}名的 {1} 上线了！", levelrank.RankId, finalbot.NickName);

            //    PushMessageHelper.SendNoticeToOnlineUser(NoticeMode.Game, context);

            //    var chatService = new TryXChatService();
            //    chatService.SystemSend(context);
            //    PushMessageHelper.SendSystemChatToOnlineUser();
            //}

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


        

    }
}