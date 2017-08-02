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
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Model;

namespace GameServer.CsScript.Com
{
    /// <summary>
    /// 合区
    /// </summary>
    public static class MERGE_SERVICE
    {


        public static void Run()
        {
            DoCDKeyCache();
            DoOrderInfoCache();
            DoUserAchievementCache();
        }

        /// <summary>
        /// CDKeyCache
        /// </summary>
        public static void DoCDKeyCache()
        {
            var cdkSet = new ShareCacheStruct<CDKeyCache>();

            TraceLog.WriteInfo("Do CDKeyCache table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT CDKey,UsedTime FROM CDKeyCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        CDKeyCache cdk = new CDKeyCache();
                        cdk.CDKey = reader["CDKey"].ToString();
                        cdk.UsedTime = reader["UsedTime"].ToDateTime();
                        if (cdkSet.FindKey(cdk.CDKey) == null)
                        {
                            //cdkSet.Add(cdk);
                            count++;
                        }
                    }
                    //cdkSet.Update();
                }
            }
            catch(Exception ex)
            {
                TraceLog.WriteError("Do CDKeyCache table error Exception: {0} .", ex);
                return;
            }


            TraceLog.WriteInfo("Do CDKeyCache table successful : {0} .", count);
        }

        /// <summary>
        /// OrderInfoCache
        /// </summary>
        public static void DoOrderInfoCache()
        {
            var orderInfoSet = new ShareCacheStruct<OrderInfoCache>();

            TraceLog.WriteInfo("Do OrderInfoCache table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT OrderId,UserId,NickName,MerchandiseName,PayId,Amount,PassportID,PassportID," +
                    "GameCoins,CreateDate,RetailID,RcId FROM OrderInfoCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        OrderInfoCache newOrderInfo = new OrderInfoCache()
                        {
                            OrderId = reader["OrderId"].ToString(),
                            UserId = reader["UserId"].ToInt(),
                            NickName = reader["NickName"].ToString(),
                            MerchandiseName = reader["MerchandiseName"].ToString(),
                            PayId = reader["PayId"].ToInt(),
                            Amount = reader["Amount"].ToInt(),
                            PassportID = reader["PassportID"].ToString(),
                            ServerID = reader["ServerID"].ToInt(),
                            GameCoins = reader["GameCoins"].ToInt(),
                            CreateDate = reader["CreateDate"].ToDateTime(),
                            RetailID = reader["RetailID"].ToString(),
                            RcId = reader["RcId"].ToInt(),
                        };
                        if (orderInfoSet.FindKey(newOrderInfo.OrderId) == null)
                        {
                            //orderInfoSet.Add(newOrderInfo);
                            count++;
                        }
                    }
                    //orderInfoSet.Update();
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Do OrderInfoCache table error Exception: {0} .", ex);
                return;
            }


            TraceLog.WriteInfo("Do OrderInfoCache table successful : {0} .", count);
        }


        /// <summary>
        /// UserAchievementCache
        /// </summary>
        public static void DoUserAchievementCache()
        {
            var achieveSet = new PersonalCacheStruct<UserAchievementCache>();

            TraceLog.WriteInfo("Do UserAchievementCache table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT UserID,AchievementList FROM UserAchievementCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        UserAchievementCache achievecache = new UserAchievementCache()
                        {
                            UserID = reader["UserID"].ToInt()
                        };

                        var bytes = reader.GetValue(1) as byte[];
                        achievecache.AchievementList = (CacheList<AchievementData>)ProtoBufUtils.Deserialize(bytes, typeof(CacheList<AchievementData>));
                        
                        if (achieveSet.FindKey(achievecache.UserID.ToString()) == null)
                        {
                            //achieveSet.Add(achievecache);
                            count++;
                        }
                    }
                    //achieveSet.Update();
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Do UserAchievementCache table error Exception: {0} .", ex);
                return;
            }


            TraceLog.WriteInfo("Do UserAchievementCache table successful : {0} .", count);
        }

        /// <summary>
        /// UserAttributeCache
        /// </summary>
        public static void DoUserAttributeCache()
        {
            var attributeSet = new PersonalCacheStruct<UserAttributeCache>();

            TraceLog.WriteInfo("Do UserAttributeCache table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT UserID,FightValue,Hp,Atk,Def,Crit,Hit,Dodge,Tenacity FROM UserAttributeCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        UserAttributeCache attcache = new UserAttributeCache()
                        {
                            UserID = reader["UserID"].ToInt(),
                            FightValue = reader["FightValue"].ToLong(),
                            Hp = reader["Hp"].ToLong(),
                            Atk = reader["Atk"].ToLong(),
                            Def = reader["Def"].ToLong(),
                            Crit = reader["Crit"].ToLong(),
                            Hit = reader["Hit"].ToLong(),
                            Dodge = reader["Dodge"].ToLong(),
                            Tenacity = reader["Tenacity"].ToLong(),
                        };

                        if (attributeSet.FindKey(attcache.UserID.ToString()) == null)
                        {
                            //attributeSet.Add(attributeSet);
                            count++;
                        }
                    }
                    //attributeSet.Update();
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Do UserAttributeCache table error Exception: {0} .", ex);
                return;
            }
            
            TraceLog.WriteInfo("Do UserAttributeCache table successful : {0} .", count);
        }

        /// <summary>
        /// UserBasisCache
        /// </summary>
        public static void DoUserBasisCache()
        {
            var basisSet = new PersonalCacheStruct<UserBasisCache>();

            TraceLog.WriteInfo("Do UserBasisCache table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT UserID,ServerID,RetailID,Pid,NickName,UserLv,Profession," +
                    "RewardsDiamond,BuyDiamond,UsedDiamond,Gold,VipLv,AvatarUrl,Vit,UserStatus," +
                    "CreateDate,LoginDate,OfflineDate,RestoreDate,FightValueRankID,LevelRankID," +
                    "CombatRankID,ComboRankID,VipGiftProgress,ShareCount,ShareDate,StartRestoreVitDate," +
                    "IsReceivedRedPacket,OfflineEarnings,OfflineTimeSec," +
                    "InviteCount,ComboNum,BackLevelNum,ReceiveInviteList, ReceiveLevelAwardList," +
                    "ReceiveRankingAwardList FROM UserBasisCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        UserBasisCache basis = new UserBasisCache()
                        {
                            UserID = reader["UserID"].ToInt(),
                            ServerID = reader["ServerID"].ToInt(),
                            RetailID = reader["RetailID"].ToString(),
                            Pid = reader["Pid"].ToString(),
                            NickName = reader["NickName"].ToString(),
                            UserLv = reader["UserLv"].ToInt(),
                            Profession = reader["Profession"].ToInt(),
                            RewardsDiamond = reader["RewardsDiamond"].ToInt(),
                            BuyDiamond = reader["BuyDiamond"].ToInt(),
                            UsedDiamond = reader["UsedDiamond"].ToInt(),
                            Gold = reader["Gold"].ToString(),
                            VipLv = reader["VipLv"].ToInt(),
                            AvatarUrl = reader["AvatarUrl"].ToString(),
                            Vit = reader["Vit"].ToInt(),
                            UserStatus = reader["UserStatus"].ToEnum<UserStatus>(),
                            CreateDate = reader["CreateDate"].ToDateTime(),
                            LoginDate = reader["LoginDate"].ToDateTime(),
                            OfflineDate = reader["OfflineDate"].ToDateTime(),
                            RestoreDate = reader["RestoreDate"].ToDateTime(),
                            FightValueRankID = reader["FightValueRankID"].ToInt(),
                            LevelRankID = reader["LevelRankID"].ToInt(),
                            CombatRankID = reader["CombatRankID"].ToInt(),
                            ComboRankID = reader["ComboRankID"].ToInt(),
                            VipGiftProgress = reader["VipGiftProgress"].ToInt(),
                            ShareCount = reader["ShareCount"].ToInt(),
                            ShareDate = reader["ShareDate"].ToLong(),
                            StartRestoreVitDate = reader["StartRestoreVitDate"].ToDateTime(),
                            IsReceivedRedPacket = reader["IsReceivedRedPacket"].ToBool(),
                            OfflineEarnings = reader["OfflineEarnings"].ToString(),
                            OfflineTimeSec = reader["OfflineTimeSec"].ToLong(),
                            InviteCount = reader["InviteCount"].ToInt(),
                            //ReceiveInviteList = reader["ReceiveInviteList"].ToInt(),
                            ComboNum = reader["ComboNum"].ToInt(),
                            BackLevelNum = reader["BackLevelNum"].ToInt(),
                            //ReceiveLevelAwardList = reader["ReceiveLevelAwardList"].ToInt(),
                            //ReceiveRankingAwardList = reader["ReceiveRankingAwardList"].ToInt(),
                        };
                        var receiveInviteListBytes = reader.GetValue(33) as byte[];
                        basis.ReceiveInviteList = (CacheList<int>)ProtoBufUtils.Deserialize(receiveInviteListBytes, typeof(CacheList<int>));
                        var receiveLevelAwardListBytes = reader.GetValue(34) as byte[];
                        basis.ReceiveLevelAwardList = (CacheList<int>)ProtoBufUtils.Deserialize(receiveInviteListBytes, typeof(CacheList<int>));
                        var receiveRankingAwardListBytes = reader.GetValue(35) as byte[];
                        basis.ReceiveRankingAwardList = (CacheList<int>)ProtoBufUtils.Deserialize(receiveRankingAwardListBytes, typeof(CacheList<int>));

                        if (basisSet.FindKey(basis.UserID.ToString()) == null)
                        {
                            //basisSet.Add(basis);
                            count++;
                        }
                    }
                    //basisSet.Update();
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Do UserBasisCache table error Exception: {0} .", ex);
                return;
            }

            TraceLog.WriteInfo("Do UserBasisCache table successful : {0} .", count);
        }

        /// <summary>
        /// UserCenterUser
        /// </summary>
        public static void DoUserCenterUser()
        {
            var centerUserSet = new ShareCacheStruct<UserCenterUser>();

            TraceLog.WriteInfo("Do UserCenterUser table start...");
            int count = 0;

            try
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.MERGE);
                string sql = "SELECT UserID,NickName,OpenID,ServerID,AccessTime,LoginNum,RetailID,Unid FROM UserCenterUser";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        UserCenterUser center = new UserCenterUser()
                        {
                            UserID = reader["UserID"].ToInt(),
                            NickName = reader["NickName"].ToString(),
                            OpenID = reader["OpenID"].ToString(),
                            ServerID = reader["ServerID"].ToInt(),
                            AccessTime = reader["AccessTime"].ToDateTime(),
                            LoginNum = reader["LoginNum"].ToInt(),
                            RetailID = reader["RetailID"].ToString(),
                            Unid = reader["Unid"].ToString(),
                        };

                        if (centerUserSet.FindKey(center.UserID.ToString()) == null)
                        {
                            //centerUserSet.Add(basis);
                            count++;
                        }
                    }
                    //centerUserSet.Update();
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Do UserCenterUser table error Exception: {0} .", ex);
                return;
            }

            TraceLog.WriteInfo("Do UserCenterUser table successful : {0} .", count);
        }
    }
}