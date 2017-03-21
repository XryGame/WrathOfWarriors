﻿
using System;
using ProtoBuf;
using ZyGames.Framework.Game.Com.Model;

namespace GameServer.Script.Model.Config
{

    [Serializable, ProtoContract]
    public class UserRank : RankingItem
    {
        public UserRank()
        {
        }

        public UserRank(UserRank ur)
        {
            UserID = ur.UserID;
            NickName = ur.NickName;
            Profession = ur.Profession;
            RankId = ur.RankId;
            UserLv = ur.UserLv;
            RankDate = ur.RankDate;
            HaveRankNum = ur.HaveRankNum;
            IsFighting = ur.IsFighting;
            FightDestUid = ur.FightDestUid;
            VipLv = ur.VipLv;
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
        /// 形象ID
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
        /// 排名时间
        /// </summary>
        [ProtoMember(9)]
        public DateTime RankDate { get; set; }

        /// <summary>
        /// 连续上榜天数
        /// </summary>
        [ProtoMember(10)]
        public int HaveRankNum { get; set; }

        /// <summary>
        /// 是否在战斗中
        /// </summary>
        [ProtoMember(11)]
        public bool IsFighting { get; set; }

        /// <summary>
        /// 战斗目标
        /// </summary>
        [ProtoMember(12)]
        public int FightDestUid { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        [ProtoMember(13)]
        public int VipLv { get; set; }

    }
}