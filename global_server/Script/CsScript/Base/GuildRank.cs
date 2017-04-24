
using System;
using ProtoBuf;
using ZyGames.Framework.Game.Com.Model;

namespace GameServer.CsScript.Base
{

    [Serializable, ProtoContract]
    public class GuildRank : RankingItem
    {
        public GuildRank()
        {
        }


        [ProtoMember(1)]
        public string GuildID
        {
            get;
            set;
        }

        /// <summary>
        /// 公会名
        /// </summary>
        [ProtoMember(2)]
        public string GuildName
        {
            get;
            set;
        }

        /// <summary>
        /// 活跃度
        /// </summary>
        [ProtoMember(3)]
        public int Liveness
        {
            get;
            set;
        }

        private int tmp;
        /// <summary>
        /// 排行
        /// </summary>
        [ProtoMember(4)]
        public override int RankId
        {
            get
            {
                return tmp;
            }
            set
            {
                tmp = value;
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        [ProtoMember(5)]
        public int Lv
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(6)]
        public DateTime CreateDate
        {
            get;
            set;
        }
        
    }
}