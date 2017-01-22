using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Com.Rank;
using GameServer.Script.Model;
using ZyGames.Framework.Net;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.CsScript.Com;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Script;
using ZyGames.Framework.Game.Config;
using System.Web;
using System.Text;
using ZyGames.Framework.Game.Sns._91sdk;
using GameServer.Script.Model.Enum;
using System.Configuration;
using System.Threading;
using GameServer.Script.Model.Config;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 系统全局运行环境
    /// </summary>
    public static class SystemGlobal
    {
        private static readonly object thisLock = new object();
        private static int maxCount = ConfigUtils.GetSetting("MaxLoadCount", "100").ToInt();
        private const int LoadDay = 1;

        private static int cacheOverdueTime = ConfigUtils.GetSetting("CacheOverdueTime", "4").ToInt();
        private static bool _isRunning;

        public static int userPassprotCount = 0;
        public static int userRoleCount = 0;


        public static bool IsRunning
        {
            get { return _isRunning; }
        }

        public static void CloseRunState()
        {
            lock (thisLock)
            {
                _isRunning = false;
            }
        }

        static SystemGlobal()
        {
            _isRunning = false;
        }

        public static ProtocolSection GetSection()
        {
            return ConfigManager.Configger.GetFirstOrAddConfig<ProtocolSection>();
        }

        public static void Run()
        {
            //AppstoreClientManager.Current.InitConfig();
            //var dispatch = TaskDispatch.StartTask();
            //dispatch.Add(new StudyTask());

            lock (thisLock)
            {
                _isRunning = false;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var httpHost = GetSection().HttpHost;
            var httpPort = GetSection().HttpPort;
            var httpName = GetSection().HttpName;
            if (!string.IsNullOrEmpty(httpHost))
            {
                var names = httpName.Split(',');
                new NewHttpListener(httpHost, httpPort, new HashSet<string>(names));
            }

            
            LoadGlobalData();
            //LoadUser();

            GameUser.Callback = new AsyncDataChangeCallback(UserHelper.TriggerUserCallback);

            TimeListener.Append(PlanConfig.EveryMinutePlan(submitServerStatus, "submitServerStatus", "00:00", "23:59", ConfigurationManager.AppSettings["ServerStatusSendInterval"].ToInt()));
            TimeListener.Append(PlanConfig.EveryMinutePlan(BotsAction, "BotsAction", "00:00", "23:59", 3));
            // new GameActiveCenter(null);
            // new GuildGameActiveCenter(null);
            //每天执行用于整点刷新
            TimeListener.Append(PlanConfig.EveryDayPlan(UserHelper.DoZeroRefreshDataTask, "DoZeroRefreshDataTask", "00:00"));
            //TimeListener.Append(PlanConfig.EveryMinutePlan(UserHelper.DoZeroRefreshDataTask, "DoZeroRefreshDataTask", "08:00", "22:00", 60));
            //每天5点执行用于整点刷新
            TimeListener.Append(PlanConfig.EveryDayPlan(UserHelper.DoEveryDayRefreshDataTask, "EveryDayRefreshDataTask", "05:00"));
            // 每周二，周五名人榜奖励
            TimeListener.Append(PlanConfig.EveryWeekPlan(UserHelper.DoCombatAwardTask, "TuesdayCombatAwardTask", DayOfWeek.Tuesday, "04:00"));
            TimeListener.Append(PlanConfig.EveryWeekPlan(UserHelper.DoCombatAwardTask, "FridayCombatAwardTask", DayOfWeek.Friday, "04:00"));
            //TimeListener.Append(PlanConfig.EveryMinutePlan(UserHelper.DoCombatAwardTask, "CombatAwardTask", "08:00", "22:00", 600));

            DataHelper.InitData();
            
            InitRanking();

            Bots.InitBots();

            stopwatch.Stop();
            new BaseLog().SaveLog("系统全局运行环境加载所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");

            SendServerStatus(ServerStatus.Unhindered, 0);

            lock (thisLock)
            {
                _isRunning = true;
            }

            

            
        }

        private static void InitRanking()
        {
            int timeOut = ConfigUtils.GetSetting("Ranking.timeout", "60").ToInt();

            RankingFactory.Add(new CombatRanking());
            RankingFactory.Add(new LevelRanking());
            RankingFactory.Start(timeOut);

            // 设置竞技场不刷新
            Ranking<UserRank> combatRanking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            combatRanking.SetIntervalTimes(int.MaxValue);
        }
        
        //private static void LoadUser()
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    var userList = GetLoadUser(LoadDay, maxCount);
        //    new BaseLog().SaveLog("系统加载当天用户数:" + userList.Count + "/最大:" + maxCount);
        //    foreach (string userId in userList)
        //    {
        //        UserCacheGlobal.LoadOffline(userId);
        //    }
        //    stopwatch.Stop();
        //    new BaseLog().SaveLog("系统加载当天用户所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");
        //}

        //public static List<string> GetLoadUser(int days, int maxCount)
        //{
        //    var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);

        //    var command = dbProvider.CreateCommandStruct("GameUser", CommandMode.Inquiry, "UserID");
        //    command.OrderBy = "LoginDate desc";
        //    command.Filter = dbProvider.CreateCommandFilter();
        //    command.Filter.Condition = command.Filter.FormatExpression("LoginDate", ">");
        //    command.Filter.AddParam("LoginDate", DateTime.Now.AddDays(-days));
        //    command.Parser();

        //    List<string> userList = new List<string>();
        //    using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
        //    {
        //        while (reader.Read())
        //        {
        //            userList.Add(reader["UserID"].ToString());
        //        }
        //    }
        //    return userList;
        //}

        public static void LoadGlobalData()
        {
            new BaseLog().SaveLog("系统加载单服配置开始...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int capacity = int.MaxValue;
            //todo Load
            var dbFilter = new DbDataFilter(capacity);

            //RestoreRedisFromDB(dbFilter);

            var userCenterPassport = new ShareCacheStruct<UserCenterPassport>();
            userCenterPassport.AutoLoad(dbFilter);
            userPassprotCount = userCenterPassport.Count;
            var userCenterUser = new ShareCacheStruct<UserCenterUser>();
            userCenterUser.AutoLoad(dbFilter);
            userRoleCount = userCenterUser.Count;


            new ShareCacheStruct<Config_RoleGrade>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Role>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_SceneMap>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Scene>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_SubjectExp>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Item>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_ItemGrade>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Skill>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_SkillGrade>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Task>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Achievement>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Signin>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_FirstWeek>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_OnlineReward>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Lottery>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Purchase>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Vip>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_Pay>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_CelebrityRanking>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_AccumulatePay>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_CdKey>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_ChatKeyWord>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_BotsName>().AutoLoad(dbFilter);
            new ShareCacheStruct<Config_BotsChat>().AutoLoad(dbFilter);

            new ShareCacheStruct<ClassDataCache>().AutoLoad(dbFilter);
            new ShareCacheStruct<JobTitleDataCache>().AutoLoad(dbFilter);
            new ShareCacheStruct<OccupyDataCache>().AutoLoad(dbFilter);
            
            stopwatch.Stop();
            new BaseLog().SaveLog("系统加载单服配置所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");

        }

        
        public static void Stop()
        {
            SendServerStatus(ServerStatus.Close, 0);

            var onlines = GameSession.GetAll();
            foreach (var sess in onlines)
            {
                if (sess.Connected)
                {
                    GameUser user = UserHelper.FindUser(sess.UserId);
                    if (user == null)
                        continue;
                    user.OfflineDate = DateTime.Now;
                }

            }

            Thread.Sleep(50000);
        }


        public static void RestoreRedisFromDB(DbDataFilter ddf)
        {
            new ShareCacheStruct<CDKeyCache>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<ClassDataCache>().TryRecoverFromDb(ddf);
            new PersonalCacheStruct<GameUser>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<JobTitleDataCache>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<NickNameCache>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<OccupyDataCache>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<OrderInfoCache>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<UserCenterPassport>().TryRecoverFromDb(ddf);
            new ShareCacheStruct<UserCenterUser>().TryRecoverFromDb(ddf);
            new PersonalCacheStruct<UserPayCache>().TryRecoverFromDb(ddf);
        }

        public static void submitServerStatus(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something

            var sessions = GameSession.GetAll();
            int count = 0;
            foreach (var s in sessions)
            {
                if (s.Connected)
                    count++;
            }
            if (count < 20)
                SendServerStatus(ServerStatus.Unhindered, count);
            else if (count < 100)
                SendServerStatus(ServerStatus.Crowd, count);
            else
                SendServerStatus(ServerStatus.Full, count);
        }

        public static void SendServerStatus(ServerStatus status, int activeNum)
        {

            string Sign = "3f261d4f2f8941ea90552cf7507f021b";
            string addr = ConfigurationManager.AppSettings["ServerStatusAddr"];
            string url = addr + "/Service.aspx?d=";
            string urlData = string.Format("MsgId={0}&Sid={1}&Uid={2}&St={3}&ActionId={4}&GameID={5}&ServerID={6}&Status={7}&ActiveNum={8}&ServerUrl={9}&Sign={10}",
                0,
                0,
                0,
                0,
                1002,
                1,
                ConfigManager.Configger.GetFirstOrAddConfig<AppServerSection>().ProductServerId,
                status.ToInt(),
                activeNum,
                ConfigUtils.GetSetting("Game.IpAddress") + ":" + ConfigUtils.GetSetting("Game.Port"),
                Sign
            );
            string getUrlData = url + HttpUtility.UrlEncode(urlData, Encoding.UTF8);
            
            try
            {
                string result = HttpPostManager.GetStringData(getUrlData);
                if (string.IsNullOrEmpty(result))
                {
                    TraceLog.ReleaseWrite("Submit server status fail result:{0}, request url:{1}", result, getUrlData);
                    return;
                }
                else
                {
                    TraceLog.WriteLine("Submit server status successful:{0},{1},{2}", addr, status, activeNum);
                }
            }
            catch (Exception ex)
            {
                new BaseLog().SaveLog(ex);
                return;
            }
        }

        public static void BotsAction(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something
            /// 这里处理Bot的聊天
            Bots.Chat();
            /// 处理Bot切磋响应
            Bots.FightResponse();
            /// 处理Bot上线公告
            Bots.OnlineNotification();
            /// 处理Bot充值公告
            Bots.PayVipNotification();
            /// 处理Bot竞技道具公告
            Bots.CombatItemNotification();
            /// 处理Bot占领
            Bots.Occupy();
            //// 机器人自动参加竞选(测试)
            //Bots.Campaign();
        }

    }
}