﻿
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Data;
using System.Data;
using ZyGames.Framework.Common;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model;
using Newtonsoft.Json;

namespace GameServer.CsScript.Com
{
    /// <summary>
    /// 通天塔排行
    /// </summary>
    public class CombatRanking : Ranking<UserRank>
    {
        public const string RankingKey = "CombatRanking";
        public List<UserRank> rankList = new List<UserRank>();

        public CombatRanking()
            : base(RankingKey, int.MaxValue, 0, false)
        {
        }

        protected override int ComparerTo(UserRank x, UserRank y)
        {
            return -1;
        }


        /// <summary>
        /// 获取前5名玩家
        /// </summary>
        /// <returns></returns>
        public List<UserRank> GetRanking(UserBasisCache user)
        {
            List<UserRank> userRankList = new List<UserRank>();
            int currRankId;
            if (TryGetRankNo(m => m.UserID == user.UserID, out currRankId))
            {
                //user.RankID = currRankId;
                int rankIncrice;
                int length = 5;
                if (currRankId < 51) rankIncrice = 1;
                else if (currRankId < 101) rankIncrice = 2;
                else if (currRankId < 501) rankIncrice = 5;
                else rankIncrice = 10;
                int pagesize;
                int pageIndex;
                if (currRankId > 5)
                {
                    pagesize = currRankId;
                    pageIndex = currRankId - (length + 1) * rankIncrice;
                }
                else
                {
                    pagesize = 6;
                    pageIndex = 0;
                }
                int pagecount;
                IList<UserRank> list = this.GetRange(1, pagesize, out pagecount);
                while (pageIndex < pagesize && pageIndex < list.Count)
                {
                    if (list.Count <= pageIndex)
                    {
                        break;
                    }
                    if (list[pageIndex].UserID == user.UserID)
                    {
                        pageIndex = MathUtils.Addition(pageIndex, rankIncrice);
                        continue;
                    }
                    userRankList.Add(list[pageIndex]);
                    pageIndex = MathUtils.Addition(pageIndex, rankIncrice);
                }
            }
            return userRankList;
        }
        
        protected override IList<UserRank> GetCacheList()
        {

            if (rankList.Count == 0)
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
                string sql = "SELECT UserID,NickName,Profession,UserLv,VipLv,AvatarUrl,CombatRankID FROM UserBasisCache";
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
                        rankInfo.RankId = reader["CombatRankID"].ToInt();
                        rankList.Add(rankInfo);
                    }
                }

                sql = "SELECT UserID, FightValue FROM UserAttributeCache";
                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
                {
                    while (reader.Read())
                    {
                        int userId = reader["UserID"].ToInt();
                        var rank = rankList.Find(t => t.UserID == userId);
                        if (rank != null)
                        {
                            rank.FightValue = reader["FightValue"].ToInt();
                        }
                    }
                }
            }

            SortOfRankId();
            return rankList;
        }
        protected override void ChangeRankNo(UserRank item)
        {

            var basis = UserHelper.FindUserBasis(item.UserID);
            basis.CombatRankID = item.RankId;
            
        }

        private void SortOfRankId()
        {

            int count = 1;
            List<UserRank> tmp = new List<UserRank>();
            while (count <= rankList.Count)
            {
                var findlist = rankList.FindAll(t => (t.RankId == count));
                foreach (var v in findlist)
                {
                    tmp.Add(v);
                }
                count++;
            }
            var list = rankList.FindAll(t => (t.RankId == 0));
            tmp.AddRange(list);

            rankList = tmp;
        }
    }
}