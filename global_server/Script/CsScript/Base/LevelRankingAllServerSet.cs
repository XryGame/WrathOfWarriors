using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Sns._91sdk;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 全服排行
    /// </summary>
    public static class LevelRankingAllServerSet
    {
        public static void LoadServerRanking()
        {

            try
            {
                new BaseLog().SaveLog("全服排名开始...");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                LevelRanking levelranking = ranking as LevelRanking;
                levelranking.rankingData.RankTime = DateTime.Now;
                levelranking.rankingData.RankList.Clear();


                int count = 0;
                for (int i = 0; i < ServerSet.Set.Count; ++i)
                {
                    if (ServerSet.Set[i].ServerUrl.Contains("168.254")
                        || ServerSet.Set[i].ServerUrl.Contains("192.168"))
                    {
                        continue;
                    }
                    string serverName = ServerSet.Set[i].ServerName;
                    string connectKey = "ServerDB_" + (count++ + 1).ToString();
                    var dbProvider = DbConnectionProvider.CreateDbProvider(connectKey);
                    string sql = "SELECT UserID,NickName,ServerID,Profession,UserLv,AvatarUrl,VipLv,LevelRankID FROM UserBasisCache";
                    using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                    {
                        while (reader.Read())
                        {
                            UserRank rankInfo = new UserRank();
                            rankInfo.UserID = reader["UserID"].ToInt();
                            rankInfo.NickName = reader["NickName"].ToString();
                            rankInfo.Profession = reader["Profession"].ToInt();
                            rankInfo.UserLv = Convert.ToInt16(reader["UserLv"]);
                            rankInfo.VipLv = reader["VipLv"].ToInt();
                            rankInfo.AvatarUrl = reader["AvatarUrl"].ToString();
                            rankInfo.RankId = reader["LevelRankID"].ToInt();
                            rankInfo.ServerID = reader["ServerID"].ToInt();
                            rankInfo.ServerName = serverName;
                            levelranking.rankingData.RankList.Add(rankInfo);
                        }
                    }

                    sql = "SELECT UserID, FightValue FROM UserAttributeCache";
                    using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                    {
                        while (reader.Read())
                        {
                            int userId = reader["UserID"].ToInt();
                            var rank = levelranking.rankingData.RankList.Find(t => t.UserID == userId);
                            if (rank != null)
                            {
                                rank.FightValue = reader["FightValue"].ToInt();
                            }
                        }
                    }
                }
                

                Ranking<UserRank> levelRanking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                levelRanking.ForceRefresh();

                stopwatch.Stop();
                new BaseLog().SaveLog("全服排名消耗时间:" + stopwatch.Elapsed.TotalMilliseconds + "ms");
            }
            catch (Exception ex)
            {
                new BaseLog().SaveLog(ex);
                return;
            }

        }
    }
}