
using System;
using ProtoBuf;
using ZyGames.Framework.Game.Com.Model;

namespace GameServer.Script.Model.Config
{

    [Serializable, ProtoContract]
    public class UserRankAward : RankingItem
    {
        public UserRankAward()
        {
        }

        public UserRankAward(UserRank ur)
        {
            UserID = ur.UserID;
            NickName = ur.NickName;
            Profession = ur.Profession;
            RankId = ur.RankId;
            UserLv = ur.UserLv;
            AvatarUrl = ur.AvatarUrl;
            ComboNum = ur.ComboNum;
        }

        [ProtoMember(1)]
        public int UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家昵称
        /// </summary>
        [ProtoMember(2)]
        public string NickName
        {
            get;
            set;
        }

        /// <summary>
        /// 职业ID
        /// </summary>
        [ProtoMember(3)]
        public int Profession
        {
            get;
            set;
        }

        private int tmp;
        /// <summary>
        /// 竞技排行
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
        public int UserLv
        {
            get;
            set;
        }

        /// <summary>
        /// 战斗力
        /// </summary>
        [ProtoMember(6)]
        public long FightValue
        {
            get;
            set;
        }
        /// <summary>
        /// 头像链接
        /// </summary>
        [ProtoMember(7)]
        public string AvatarUrl
        {
            get;
            set;
        }
        
        /// <summary>
        /// 连击数
        /// </summary>
        [ProtoMember(8)]
        public int ComboNum { get; set; }

        /// <summary>
        /// 是否已领取
        /// </summary>
        [ProtoMember(9)]
        public bool IsReceived { get; set; }

    }
}