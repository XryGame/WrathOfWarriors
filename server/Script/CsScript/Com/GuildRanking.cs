
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
    /// 公会排行
    /// </summary>
    public class GuildRanking : Ranking<GuildRank>
    {
        public const string RankingKey = "GuildRanking";
        public List<GuildRank> rankList = new List<GuildRank>();

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
            var list = new ShareCacheStruct<GuildsCache>().FindAll();
            foreach (var v in list)
            {
                GuildRank rank = new GuildRank()
                {
                    GuildID = v.GuildID,
                    GuildName = v.GuildName,
                    Liveness = v.Liveness,
                    RankId = v.RankID,
                    Lv = v.Lv,
                    CreateDate = v.CreateDate
                };
                rankList.Add(rank);
            }
            return rankList;
        }
        protected override void ChangeRankNo(GuildRank item)
        {
            var guilddata = new ShareCacheStruct<GuildsCache>().FindKey(item.GuildID);
            guilddata.RankID = item.RankId;

        }

    }
}