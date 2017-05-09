using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;

namespace GameServer.Script.Model.DataModel
{
    public static class DataHelper
    {
        /// <summary>
        /// 开服日期
        /// </summary>
        static public DateTime OpenServiceDate;
        static public string OpenServiceDataCacheKey = "OpenServicesDate";

        static public int SignStartID;
        static public string SignStartIDCacheKey = "SignStartID";
        /// <summary>
        /// 用户初始体力
        /// </summary>
        static public int InitVit;
        /// <summary>
        /// 竞技场日志最大数量
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


        static DataHelper()
        {

        }



        static public void InitData()
        {
            InitVit = ConfigEnvSet.GetInt("User.InitVit");
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
            }
            OpenServiceDate = openServiceCache.Value.ToDateTime();

            GameCache signStartIDCache = gameCache.FindKey(SignStartIDCacheKey);
            if (signStartIDCache == null)
            {
                signStartIDCache = new GameCache();
                signStartIDCache.Key = SignStartIDCacheKey;
                signStartIDCache.Value = "1";
                gameCache.Add(signStartIDCache);
                gameCache.Update();
            }
            SignStartID = signStartIDCache.Value.ToInt();
        }
        

    }
}