﻿
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using System;
using System.Data;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Data;

namespace GameServer.Script.Model.DataModel
{
    public static class DataHelper
    {
        /// <summary>
        /// 开服日期
        /// </summary>
        static public bool IsFirstOpenService = false;

        static public DateTime OpenServiceDate;
        static public string OpenServiceDataCacheKey = "OpenServicesDate";

        static public int SignStartID;
        static public string SignStartIDCacheKey = "SignStartID";

        static public string LevelRankingAwardCacheKey = "LevelRankingAwardCache";
        static public string FightValueRankingAwardCacheKey = "FightValueRankingAwardCache";
        static public string ComboRankingAwardCacheKey = "ComboRankingAwardCache";
        /// <summary>
        /// 用户初始体力
        /// </summary>
        static public int VitInit;
        static public int VitMax;
        static public int VitRestore;
        static public int VitRestoreTimesSec = 3600;
        /// <summary>
        /// 通天塔日志最大数量
        /// </summary>
        static public int CombatLogCountMax;

        /// <summary>
        /// 好友最大人数
        /// </summary>
        static public int FriendCountMax;
        /// <summary>
        /// 好友申请最大数量
        /// </summary>
        static public int FriendApplyCountMax;
        /// <summary>
        /// 好友赠送最大次数
        /// </summary>
        static public int FriendGiveAwayCountMax;
        /// <summary>
        /// 赠送好友体力/时间值
        /// </summary>
        static public int FriendGiveAwayVitValue;
        /// <summary>
        /// 补签所需钻石数量
        /// </summary>
        static public int RepairSignNeedDiamond;
        /// <summary>
        /// 最大邮件数量
        /// </summary>
        static public int MaxMailNum;

        /// <summary>
        /// 开启每日任务系统用户等级
        /// </summary>
        static public int OpenTaskSystemUserLevel;
        /// <summary>
        /// 开启排行榜系统用户等级
        /// </summary>
        static public int OpenRankSystemUserLevel;


        /// <summary>
        /// 邀请切磋成功获得金币数量
        /// </summary>
        static public int InviteFightAwardGold;
        /// <summary>
        /// 每周切磋获得钻石最大数量
        /// </summary>
        static public int InviteFightDiamondWeekMax;


        static public CacheList<UserRankAward> LevelRankingAwardCacheList = new CacheList<UserRankAward>();
        static public CacheList<UserRankAward> FightValueRankingAwardCacheList = new CacheList<UserRankAward>();
        static public CacheList<UserRankAward> ComboRankingAwardCacheList = new CacheList<UserRankAward>();

        static DataHelper()
        {

        }



        public static void InitData()
        {
            VitInit = ConfigEnvSet.GetInt("User.VitInit");
            VitMax = ConfigEnvSet.GetInt("User.VitMax");
            VitRestore = ConfigEnvSet.GetInt("User.VitRestore");
            CombatLogCountMax = ConfigEnvSet.GetInt("User.CombatLogCountMax");
            FriendCountMax = ConfigEnvSet.GetInt("User.FriendCountMax");
            FriendApplyCountMax = ConfigEnvSet.GetInt("User.FriendApplyCountMax");
            FriendGiveAwayCountMax = ConfigEnvSet.GetInt("User.FriendGiveAwayCountMax");
            FriendGiveAwayVitValue = ConfigEnvSet.GetInt("User.FriendGiveAwayVitValue");
            RepairSignNeedDiamond = ConfigEnvSet.GetInt("User.RepairSignNeedDiamond");
            MaxMailNum = ConfigEnvSet.GetInt("User.MaxMailNum");
            OpenTaskSystemUserLevel = ConfigEnvSet.GetInt("System.OpenTaskSystemUserLevel");
            OpenRankSystemUserLevel = ConfigEnvSet.GetInt("System.OpenRankSystemLevel");
            InviteFightAwardGold = ConfigEnvSet.GetInt("User.InviteFightAwardGold");
            InviteFightDiamondWeekMax = ConfigEnvSet.GetInt("User.InviteFightDiamondWeekMax");





            var guildlist = new ShareCacheStruct<GuildsCache>().FindAll();
            foreach (var v in guildlist)
            {
                v.Lv = v.ConvertLevel();
            }
            var gameCache = new ShareCacheStruct<GameCache>();
            
            GameCache openServiceCache = gameCache.FindKey(OpenServiceDataCacheKey);
            if (openServiceCache == null)
            {
                openServiceCache = new GameCache();
                openServiceCache.Key = OpenServiceDataCacheKey;
                openServiceCache.Value = DateTime.Now.ToString();
                gameCache.Add(openServiceCache);
                gameCache.Update();

                IsFirstOpenService = true;
            }
            OpenServiceDate = openServiceCache.Value.ToDateTime();
            OpenServiceDate = new DateTime(OpenServiceDate.Year, OpenServiceDate.Month, 31, 9, 0, 0);

            //GameCache signStartIDCache = gameCache.FindKey(SignStartIDCacheKey);
            //if (signStartIDCache == null)
            //{
            //    signStartIDCache = new GameCache();
            //    signStartIDCache.Key = SignStartIDCacheKey;
            //    signStartIDCache.Value = "1";
            //    gameCache.Add(signStartIDCache);
            //    gameCache.Update();
            //}
            //SignStartID = signStartIDCache.Value.ToInt();
            SignStartID = GetSignStartID();


            // 加载老用户记录
            var cacheSet = new MemoryCacheStruct<OldUserCache>();

            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Log);
            string sql = "SELECT OpenID,NickName,AvatarUrl,CreateDate FROM OldUserLog";
            using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    OldUserCache olduser = new OldUserCache();
                    olduser.OpenID = reader["OpenID"].ToString();
                    olduser.NickName = reader["NickName"].ToString();
                    olduser.AvatarUrl = reader["AvatarUrl"].ToString();
                    olduser.CreateDate = reader["CreateDate"].ToDateTime();
                    cacheSet.TryAdd(olduser.OpenID, olduser);
                }
            }


            // 排行榜奖励数据
            GameCache levelCache = gameCache.FindKey(LevelRankingAwardCacheKey);
            if (levelCache == null)
            {
                levelCache = new GameCache();
                levelCache.Key = LevelRankingAwardCacheKey;
                levelCache.Value = MathUtils.ToJson(LevelRankingAwardCacheList);
                gameCache.Add(levelCache);
                gameCache.Update();
            }
            LevelRankingAwardCacheList = MathUtils.ParseJson<CacheList<UserRankAward>>(levelCache.Value);

            GameCache fightValueCache = gameCache.FindKey(FightValueRankingAwardCacheKey);
            if (fightValueCache == null)
            {
                fightValueCache = new GameCache();
                fightValueCache.Key = FightValueRankingAwardCacheKey;
                fightValueCache.Value = MathUtils.ToJson(FightValueRankingAwardCacheList);
                gameCache.Add(fightValueCache);
                gameCache.Update();
            }
            FightValueRankingAwardCacheList = MathUtils.ParseJson<CacheList<UserRankAward>>(fightValueCache.Value);

            GameCache comboCache = gameCache.FindKey(ComboRankingAwardCacheKey);
            if (comboCache == null)
            {
                comboCache = new GameCache();
                comboCache.Key = ComboRankingAwardCacheKey;
                comboCache.Value = MathUtils.ToJson(ComboRankingAwardCacheList);
                gameCache.Add(comboCache);
                gameCache.Update();
            }
            ComboRankingAwardCacheList = MathUtils.ParseJson<CacheList<UserRankAward>>(comboCache.Value);
        }

        public static int GetSignStartID()
        {
            int ret = 0;
            DateTime temp = new DateTime(
                        OpenServiceDate.Year,
                        OpenServiceDate.Month,
                        OpenServiceDate.Day,
                        5,
                        0,
                        0
                    );

            TimeSpan timespan = DateTime.Now.Subtract(temp);

            var alllist = new ShareCacheStruct<Config_Signin>().FindAll();
            int remainder = timespan.Days % alllist.Count;
            ret = remainder / 7 * 7 + 1;

            return ret;
        }

        public static void UpdateRankingAwardCache()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache levelCache = gameCache.FindKey(LevelRankingAwardCacheKey);
            levelCache.Value = MathUtils.ToJson(LevelRankingAwardCacheList);

            GameCache fightValueCache = gameCache.FindKey(FightValueRankingAwardCacheKey);
            fightValueCache.Value = MathUtils.ToJson(FightValueRankingAwardCacheList);

            GameCache comboCache = gameCache.FindKey(ComboRankingAwardCacheKey);
            comboCache.Value = MathUtils.ToJson(ComboRankingAwardCacheList);
        }
    }



}