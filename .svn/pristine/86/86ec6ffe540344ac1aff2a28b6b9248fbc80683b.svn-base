
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
    /// 名人榜排行
    /// </summary>
    public class CombatRanking : Ranking<UserRank>
    {
        public const string RankingKey = "CombatRanking";
        public List<UserRank> rankList = new List<UserRank>();

        public CombatRanking()
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
            result = x.RankId.CompareTo(y.RankId);
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

        /// <summary>
        /// 获取前5名玩家
        /// </summary>
        /// <returns></returns>
        public List<UserRank> GetRanking(GameUser user)
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
            /// 修改刷新
            //rankList.Clear();

            if (rankList.Count > 0)
            {
                return rankList;
            }
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = "SELECT UserID,NickName,LooksId,UserLv,VipLv,FightingValue,CombatData FROM GameUser";
            using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    UserCombatData combat = (UserCombatData)JsonConvert.DeserializeObject(reader["CombatData"].ToString(), typeof(UserCombatData));
                    if (combat.RankID > 0)
                    {
                        UserRank rankInfo = new UserRank();
                        rankInfo.UserID = reader["UserID"].ToInt();
                        rankInfo.NickName = reader["NickName"].ToString();
                        rankInfo.LooksId = reader["LooksId"].ToInt();
                        rankInfo.UserLv = Convert.ToInt16(reader["UserLv"]);
                        rankInfo.VipLv = reader["VipLv"].ToInt();
                        rankInfo.FightingValue = reader["FightingValue"].ToInt();
                        rankInfo.RankId = combat.RankID;
                        rankList.Add(rankInfo);
                    }
                    
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
            gameUser.CombatData.RankID = item.RankId;
        }
    }
}