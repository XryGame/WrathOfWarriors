
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
    /// 连击排行
    /// </summary>
    public class ComboRanking : Ranking<UserRank>
    {
        public const string RankingKey = "ComboRanking";
        public List<UserRank> rankList = new List<UserRank>();

        public ComboRanking()
            : base(RankingKey, int.MaxValue, 0, true)
        {
        }

        protected override int ComparerTo(UserRank x, UserRank y)
        {
            int result = 0;
            if (x == null && y == null)
            {
                return 0;
            }
            if (x != null && y == null)
            {
                return 1;
            }
            if (x == null)
            {
                return -1;
            }

            result = y.ComboNum.CompareTo(x.ComboNum);
            if (result == 0)
            {
                result = y.UserLv.CompareTo(x.UserLv);
                if (result == 0)
                {
                    result = x.UserID.CompareTo(y.UserID);
                }
            }

            return result;
        }
        
        protected override IList<UserRank> GetCacheList()
        {
            /// 修改刷新
            //rankList.Clear();

            if (rankList.Count > 0)
            {
                return rankList;
            }
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = "SELECT UserID,NickName,Profession,UserLv,VipLv,AvatarUrl,ComboRankID,ComboNum FROM UserBasisCache";
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
                    rankInfo.RankId = reader["ComboRankID"].ToInt();
                    rankInfo.ComboNum = reader["ComboNum"].ToInt();
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
                        rank.FightValue = reader["FightValue"].ToLong();
                    }
                }
            }

            return rankList;
        }
        protected override void ChangeRankNo(UserRank item)
        {
            var basis = UserHelper.FindUserBasis(item.UserID);
            basis.ComboRankID = item.RankId;
        }
    }
}