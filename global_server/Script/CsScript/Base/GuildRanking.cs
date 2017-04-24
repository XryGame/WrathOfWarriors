using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Common.Serialization;
using System;

namespace GameServer.CsScript.Base
{
    public class GuildRankingData
    {
        public GuildRankingData()
        {
            RankList = new List<GuildRank>();
        }
        public DateTime RankTime { get; set;  }

        public List<GuildRank> RankList { get; set; }
    }
    /// <summary>
    /// 公会排行
    /// </summary>
    public class GuildRanking : Ranking<GuildRank>
    {
        public const string RankingKey = "GuildRanking";
        public List<GuildRank> rankList = new List<GuildRank>();

        public GuildRankingData rankingData = new GuildRankingData();

        public GuildRanking()
            : base(RankingKey, int.MaxValue, 0, true)
        {
            
        }

        protected override int ComparerTo(GuildRank x, GuildRank y)
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

            result = y.Liveness.CompareTo(x.Liveness);
            if (result == 0)
            {
                result = x.CreateDate.CompareTo(y.CreateDate);
            }

            return result;
        }

        
        protected override IList<GuildRank> GetCacheList()
        {
            rankList.Clear();
            if (rankingData.RankList.Count == 0)
            {
                var gameCache = new ShareCacheStruct<GameCache>();
                var guildrank = gameCache.FindKey("GuildRanking");
                if (guildrank == null)
                {
                    guildrank = new GameCache();
                    guildrank.Key = "GuildRanking";
                    guildrank.Value = "";
                    gameCache.Add(guildrank);
                    gameCache.Update();
                }

                GuildRankingData data = null;
                data = JsonUtils.Deserialize<GuildRankingData>(guildrank.Value);
                if (data == null)
                    data = new GuildRankingData();

            }

            foreach (var v in rankingData.RankList)
            {
                GuildRank rank = new GuildRank()
                {
                    GuildID = v.GuildID,
                    GuildName = v.GuildName,
                    Liveness = v.Liveness,
                    RankId = v.RankId,
                    Lv = v.Lv,
                    CreateDate = v.CreateDate
                };
                rankList.Add(rank);
            }

            return rankList;
        }
        protected override void ChangeRankNo(GuildRank item)
        {

        }

    }
}