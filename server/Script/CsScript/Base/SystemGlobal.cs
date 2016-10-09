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
            LoadGlobalData();
            LoadUser();

            GameUser.Callback = new AsyncDataChangeCallback(UserHelper.TriggerUserCallback);

            // new GameActiveCenter(null);
            // new GuildGameActiveCenter(null);
            //每天执行用于整点刷新
            //TimeListener.Append(PlanConfig.EveryDayPlan(UserHelper.DoZeroRefreshDataTask, "DoZeroRefreshDataTask", "00:00"));
            TimeListener.Append(PlanConfig.EveryMinutePlan(UserHelper.DoZeroRefreshDataTask, "DoZeroRefreshDataTask", "08:00", "22:00", 600));
            //每天5点执行用于整点刷新
            TimeListener.Append(PlanConfig.EveryDayPlan(UserHelper.DoEveryDayRefreshDataTask, "EveryDayRefreshDataTask", "05:00"));
            // 每周二，周五竞技场奖励
            //TimeListener.Append(PlanConfig.EveryWeekPlan(UserHelper.DoCombatAwardTask, "TuesdayCombatAwardTask", DayOfWeek.Tuesday, "04:00"));
            //TimeListener.Append(PlanConfig.EveryWeekPlan(UserHelper.DoCombatAwardTask, "FridayCombatAwardTask", DayOfWeek.Friday, "04:00"));
            TimeListener.Append(PlanConfig.EveryMinutePlan(UserHelper.DoCombatAwardTask, "CombatAwardTask", "08:00", "22:00", 600));

            InitRanking();
            stopwatch.Stop();
            new BaseLog().SaveLog("系统全局运行环境加载所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");

            lock (thisLock)
            {
                _isRunning = true;
            }
        }

        private static void InitRanking()
        {
            int timeOut = ConfigUtils.GetSetting("Ranking.timeout", "3600").ToInt();
            RankingFactory.Add(new CombatRanking());
            RankingFactory.Add(new LevelRanking());
            RankingFactory.Add(new FightValueRanking());
            RankingFactory.Start(timeOut);
        }
        
        private static void LoadUser()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var userList = GetLoadUser(LoadDay, maxCount);
            new BaseLog().SaveLog("系统加载当天用户数:" + userList.Count + "/最大:" + maxCount);
            foreach (string userId in userList)
            {
                UserCacheGlobal.LoadOffline(userId);
            }
            stopwatch.Stop();
            new BaseLog().SaveLog("系统加载当天用户所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");
        }

        public static List<string> GetLoadUser(int days, int maxCount)
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);

            var command = dbProvider.CreateCommandStruct("GameUser", CommandMode.Inquiry, "UserID");
            command.OrderBy = "LoginDate desc";
            command.Filter = dbProvider.CreateCommandFilter();
            command.Filter.Condition = command.Filter.FormatExpression("LoginDate", ">");
            command.Filter.AddParam("LoginDate", DateTime.Now.AddDays(-days));
            command.Parser();

            List<string> userList = new List<string>();
            using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
            {
                while (reader.Read())
                {
                    userList.Add(reader["UserID"].ToString());
                }
            }
            return userList;
        }

        public static void LoadGlobalData()
        {
            new BaseLog().SaveLog("系统加载单服配置开始...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int capacity = int.MaxValue;
            //todo Load
            var dbFilter = new DbDataFilter(capacity);
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


            var classcache = new ShareCacheStruct<ClassDataCache>();
            classcache.AutoLoad(dbFilter);
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            jobcache.AutoLoad(dbFilter);
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            occupycache.AutoLoad(dbFilter);

            DataHelper.InitData();
            stopwatch.Stop();
            new BaseLog().SaveLog("系统加载单服配置所需时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");

        }

        
        public static void Stop()
        {
            //CountryCombat.Stop();
            //GameActiveCenter.Stop();
            //GuildGameActiveCenter.Stop();
        }
    }
}