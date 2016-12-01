
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
    /// 战斗力排行
    /// </summary>
    public class FightValueRanking : Ranking<UserRank>
    {
        public const string RankingKey = "FightValueRanking";
        public List<UserRank> rankList = new List<UserRank>();

        public FightValueRanking()
            : base(RankingKey, int.MaxValue)
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

            result = y.FightingValue.CompareTo(x.FightingValue);
            if (result == 0)
            {
                result = x.UserLv.CompareTo(y.UserLv);
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
            string sql = "SELECT UserID,NickName,LooksId,UserLv,VipLv,FightingValue,FightValueRankId FROM GameUser";
            using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    UserRank rankInfo = new UserRank();
                    rankInfo.UserID = reader["UserID"].ToInt();
                    rankInfo.NickName = reader["NickName"].ToString();
                    rankInfo.LooksId = reader["LooksId"].ToInt();
                    rankInfo.UserLv = Convert.ToInt16(reader["UserLv"]);
                    rankInfo.FightingValue = reader["FightingValue"].ToInt();
                    rankInfo.RankId = reader["FightValueRankId"].ToInt();
                    rankList.Add(rankInfo);
                }
            }
            return rankList;
        }
        protected override void ChangeRankNo(UserRank item)
        {
            var gameUser = UserHelper.FindUser(item.UserID);
            if (gameUser == null)
            {
                return;
            }
            gameUser.LevelRankId = item.RankId;
        }
    }
}