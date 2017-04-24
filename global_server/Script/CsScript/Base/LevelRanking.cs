
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Common.Serialization;

namespace GameServer.CsScript.Base
{
    public class LevelRankingData
    {
        public LevelRankingData()
        {
            RankList = new List<UserRank>();
        }
        public DateTime RankTime { get; set; }

        public List<UserRank> RankList { get; set; }
    }
    /// <summary>
    /// 等级排行
    /// </summary>
    public class LevelRanking : Ranking<UserRank>
    {
        public const string RankingKey = "LevelRanking";
        public List<UserRank> rankList = new List<UserRank>();

        public LevelRankingData rankingData = new LevelRankingData();

        public LevelRanking()
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

            result = y.UserLv.CompareTo(x.UserLv);
            if (result == 0)
            {
                result = y.FightValue.CompareTo(x.FightValue);
                if (result == 0)
                {
                    result = x.UserID.CompareTo(y.UserID);
                }
            }

            return result;
        }
        
        protected override IList<UserRank> GetCacheList()
        {
            rankList.Clear();
            if (rankingData.RankList.Count == 0)
            {
                var gameCache = new ShareCacheStruct<GameCache>();
                var levelrank = gameCache.FindKey("LevelRanking");
                if (levelrank == null)
                {
                    levelrank = new GameCache();
                    levelrank.Key = "LevelRanking";
                    levelrank.Value = "";
                    gameCache.Add(levelrank);
                    gameCache.Update();
                }

                LevelRankingData data = null;
                data = JsonUtils.Deserialize<LevelRankingData>(levelrank.Value);
                if (data == null)
                    data = new LevelRankingData();

            }

            foreach (var v in rankingData.RankList)
            {
                UserRank rank = new UserRank(v);
                rankList.Add(rank);
            }

            return rankList;
        }
        protected override void ChangeRankNo(UserRank item)
        {

        }
    }
}