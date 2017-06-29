
using System;
using System.Data;
using ProtoBuf;
using ZyGames.Framework.Data;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;
using System.Numerics;

namespace GameServer.Script.Model.DataModel
{



    //public delegate void AsyncDataChangeCallback(string property, int userId, object oldValue, object value);

    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserBasisCache : BaseUser
    {

       // public static AsyncDataChangeCallback Callback { get; set; }

        public UserBasisCache()
            : base(AccessLevel.ReadWrite)
        {
            IsRefreshing = true;
            ShareDate = 0;
            ReceiveInviteList = new CacheList<int>();
            LastDropGoldTime = DateTime.Now;
        }
        public UserBasisCache(int userid)
        : this()
        {
            UserID = userid;
        }

        #region auto-generated Property
        private int _UserID;
        [ProtoMember(1)]
        [EntityField("UserID", IsKey = true)]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }

        private int _ServerId;

        [ProtoMember(2)]
        [EntityField("ServerID")]
        public int ServerID
        {
            get
            {
                return _ServerId;
            }
            set
            {
                SetChange("ServerID", value);
            }
        }


        private string _RetailID;

        [ProtoMember(3)]
        [EntityField("RetailID")]
        public string RetailID
        {
            get
            {
                return _RetailID;
            }
            set
            {
                SetChange("RetailID", value);
            }
        }
        private string _Pid;
        [ProtoMember(4)]
        [EntityField("Pid")]
        public string Pid
        {
            get
            {
                return _Pid;
            }
            set
            {
                SetChange("Pid", value);
            }
        }

        private string _NickName;
        [ProtoMember(5)]
        [EntityField("NickName")]
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }

        private int _UserLv;
        [ProtoMember(6)]
        [EntityField("UserLv")]
        public int UserLv
        {
            get
            {
                return _UserLv;
            }
            set
            {
                SetChange("UserLv", value);
            }
        }

        /// <summary>
        /// 职业
        /// </summary>
        private int _Profession;
        [ProtoMember(7)]
        [EntityField("Profession")]
        public int Profession
        {
            get
            {
                return _Profession;
            }
            set
            {
                SetChange("Profession", value);
            }
        }

        private int _RewardsDiamond;
        [ProtoMember(8)]
        [EntityField("RewardsDiamond")]
        public int RewardsDiamond
        {
            get
            {
                return _RewardsDiamond;
            }
            set
            {
                //if (Callback != null && !IsRefreshing)
                //{
                //    Callback.BeginInvoke("DiamondChange", UserID, _RewardsDiamond, value, null, this);
                //}
                SetChange("RewardsDiamond", value);
            }
        }
        private int _BuyDiamond;
        [ProtoMember(9)]
        [EntityField("BuyDiamond")]
        public int BuyDiamond
        {
            get
            {
                return _BuyDiamond;
            }
            set
            {
                //if (Callback != null && !IsRefreshing)
                //{
                //    Callback.BeginInvoke("DiamondChange", UserID, _BuyDiamond, value, null, this);
                //}
                SetChange("BuyDiamond", value);
            }
        }

        private int _UsedDiamond;
        [ProtoMember(10)]
        [EntityField("UsedDiamond")]
        public int UsedDiamond
        {
            get
            {
                return _UsedDiamond;
            }
            set
            {
                //if (Callback != null && !IsRefreshing)
                //{
                //    Callback.BeginInvoke("DiamondChange", UserID, _UsedDiamond, value, null, this);
                //}
                SetChange("UsedDiamond", value);
            }
        }

        private string _Gold;
        [ProtoMember(11)]
        [EntityField("Gold")]
        public string Gold
        {
            get
            {
                return _Gold;
            }
            set
            {
                SetChange("Gold", value);
            }
        }

        //private int _Exp;
        //[ProtoMember(12)]
        //[EntityField("Exp")]
        //public int Exp
        //{
        //    get
        //    {
        //        return _Exp;
        //    }
        //    set
        //    {
        //        SetChange("Exp", value);
        //    }
        //}

        private int _VipLv;
        [ProtoMember(13)]
        [EntityField("VipLv")]
        public int VipLv
        {
            get
            {
                return _VipLv;
            }
            set
            {
                SetChange("VipLv", value);
            }
        }

        private string _AvatarUrl;
        [ProtoMember(14)]
        [EntityField("AvatarUrl")]
        public string AvatarUrl
        {
            get
            {
                return _AvatarUrl;
            }
            set
            {
                SetChange("AvatarUrl", value);
            }
        }



        private UserStatus _UserStatus;
        [ProtoMember(20)]
        [EntityField("UserStatus")]
        public UserStatus UserStatus
        {
            get
            {
                return _UserStatus;
            }
            set
            {
                SetChange("UserStatus", value);
            }
        }
        
        private DateTime _CreateDate;
        [ProtoMember(21)]
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        private DateTime _LoginDate;
        [ProtoMember(22)]
        [EntityField("LoginDate")]
        public DateTime LoginDate
        {
            get
            {
                return _LoginDate;
            }
            set
            {
                SetChange("LoginDate", value);
            }
        }

        private DateTime _OfflineDate;
        [ProtoMember(23)]
        [EntityField("OfflineDate")]
        public DateTime OfflineDate
        {
            get
            {
                return _OfflineDate;
            }
            set
            {
                SetChange("OfflineDate", value);
            }
        }

       

        private DateTime _RestoreDate;
        [ProtoMember(25)]
        [EntityField("RestoreDate")]
        public DateTime RestoreDate
        {
            get
            {
                return _RestoreDate;
            }
            set
            {
                SetChange("RestoreDate", value);
            }
        }

        private int _FightValueRankID;
        [ProtoMember(29)]
        [EntityField("FightValueRankID")]
        public int FightValueRankID
        {
            get
            {
                return _FightValueRankID;
            }
            set
            {
                SetChange("FightValueRankID", value);
            }
        }

        private int _LevelRankID;
        [ProtoMember(30)]
        [EntityField("LevelRankID")]
        public int LevelRankID
        {
            get
            {
                return _LevelRankID;
            }
            set
            {
                SetChange("LevelRankID", value);
            }
        }


        private int _CombatRankID;
        [ProtoMember(31)]
        [EntityField("CombatRankID")]
        public int CombatRankID
        {
            get
            {
                return _CombatRankID;
            }
            set
            {
                SetChange("CombatRankID", value);
            }
        }
        

        /// <summary>
        /// 当天抽奖剩余次数
        /// </summary>
        private int _LotteryTimes;
        [ProtoMember(32)]
        [EntityField("LotteryTimes")]
        public int LotteryTimes
        {
            get
            {
                return _LotteryTimes;
            }
            set
            {
                SetChange("LotteryTimes", value);
            }
        }
        
        

        /// <summary>
        /// vip礼包领取进度
        /// </summary>
        private int _VipGiftProgress;
        [ProtoMember(33)]
        [EntityField("VipGiftProgress")]
        public int VipGiftProgress
        {
            get
            {
                return _VipGiftProgress;
            }
            set
            {
                SetChange("VipGiftProgress", value);
            }
        }

        /// <summary>
        /// 分享计数
        /// </summary>
        private int _ShareCount;
        [ProtoMember(34)]
        [EntityField("ShareCount")]
        public int ShareCount
        {
            get
            {
                return _ShareCount;
            }
            set
            {
                SetChange("ShareCount", value);
            }
        }

        /// <summary>
        /// 分享时间
        /// </summary>
        private long _ShareDate;
        [ProtoMember(35)]
        [EntityField("ShareDate")]
        public long ShareDate
        {
            get
            {
                return _ShareDate;
            }
            set
            {
                SetChange("ShareDate", value);
            }
        }


        /// <summary>
        /// 春节红包
        /// </summary>
        private bool _IsReceivedRedPacket;
        [ProtoMember(37)]
        [EntityField("IsReceivedRedPacket")]
        public bool IsReceivedRedPacket
        {
            get
            {
                return _IsReceivedRedPacket;
            }
            set
            {
                SetChange("IsReceivedRedPacket", value);
            }
        }

        /// <summary>
        /// 离线收益
        /// </summary>
        private string _OfflineEarnings;
        [ProtoMember(38)]
        [EntityField("OfflineEarnings")]
        public string OfflineEarnings
        {
            get
            {
                return _OfflineEarnings;
            }
            set
            {
                SetChange("OfflineEarnings", value);
            }
        }

        /// <summary>
        /// 离线收益时间计数
        /// </summary>
        private long _OfflineTimeSec;
        [ProtoMember(39)]
        [EntityField("OfflineTimeSec")]
        public long OfflineTimeSec
        {
            get
            {
                return _OfflineTimeSec;
            }
            set
            {
                SetChange("OfflineTimeSec", value);
            }
        }

        /// <summary>
        /// 邀请数量
        /// </summary>
        private int _InviteCount;
        [ProtoMember(40)]
        [EntityField("InviteCount")]
        public int InviteCount
        {
            get
            {
                return _InviteCount;
            }
            set
            {
                SetChange("InviteCount", value);
            }
        }

        /// <summary>
        /// 邀请领取记录
        /// </summary>
        private CacheList<int> _ReceiveInviteList;
        [ProtoMember(41)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> ReceiveInviteList
        {
            get
            {
                return _ReceiveInviteList;
            }
            set
            {
                SetChange("ReceiveInviteList", value);
            }
        }

        #endregion

        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        [ProtoMember(100)]
        public bool IsRefreshing { get; set; }

        private string _sessionID;
        public string SessionID
        {
            get
            {
                return _sessionID;
            }
            set
            {
                _sessionID = value;
            }
        }

        [ProtoMember(101)]
        public int GameId { get; set; }


        [ProtoMember(103)]
        public bool IsOnline
        {
            get;
            set;
        }
        
        /// <summary>
        /// 切磋目标
        /// </summary>
        [ProtoMember(105)]
        public int InviteFightDestUid
        {
            get;
            set;
        }


        /// <summary>
        /// 是否领取离线收益
        /// </summary>
        [ProtoMember(106)]
        public bool IsReceiveOfflineEarnings
        {
            get;
            set;
        }

        [ProtoMember(107)]
        public DateTime LastDropGoldTime
        {
            get;
            set;
        }

        [ProtoMember(108)]
        public int DropGoldIntervalCount
        {
            get;
            set;
        }

        public override string GetNickName()
        {
            return NickName;
        }

        public override string GetPassportId()
        {
            return Pid;
        }

        public override string GetRetailId()
        {
            return RetailID;
        }

        public override int GetUserId()
        {
            return UserID.ToInt();
        }

        public override bool IsLock
        {
            get { return UserStatus == UserStatus.Lock; }
        }


        public static bool IsNickName(string name)
        {
            bool bl = false;
            bl = new PersonalCacheStruct<UserBasisCache>().IsExist(u => u.NickName.ToLower() == name.ToLower().Trim());
            if (!bl)
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);

                var command = dbProvider.CreateCommandStruct("UserBasisCache", CommandMode.Inquiry, "NickName");
                command.Filter = dbProvider.CreateCommandFilter();
                command.Filter.Condition = command.Filter.FormatExpression("NickName");
                command.Filter.AddParam("NickName", name);
                command.Parser();
                using (var reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
                {
                    while (reader.Read())
                    {
                        bl = true;
                    }
                }
            }

            return bl;
        }


        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "ServerID": return ServerID;
                    case "RetailID": return RetailID;
                    case "Pid": return Pid;
                    case "NickName": return NickName;
                    case "UserLv": return UserLv;
                    case "Profession": return Profession;
                    case "RewardsDiamond": return RewardsDiamond;
                    case "BuyDiamond": return BuyDiamond;
                    case "UsedDiamond": return UsedDiamond;
                    case "Gold": return Gold;
                    //case "Exp": return Exp;
                    case "VipLv": return VipLv;
                    case "AvatarUrl": return AvatarUrl;
                    case "UserStatus": return UserStatus;
                    case "CreateDate": return CreateDate;
                    case "LoginDate": return LoginDate;
                    case "OfflineDate": return OfflineDate;
                    case "SessionID": return SessionID;
                    case "RestoreDate": return RestoreDate;
                    case "FightValueRankID": return FightValueRankID;
                    case "LevelRankID": return LevelRankID;
                    case "CombatRankID": return CombatRankID;
                    case "LotteryTimes": return LotteryTimes;
                    case "VipGiftProgress": return VipGiftProgress;
                    case "ShareCount": return ShareCount;
                    case "ShareDate": return ShareDate;
                    case "IsReceivedRedPacket": return IsReceivedRedPacket;
                    case "OfflineEarnings": return OfflineEarnings;
                    case "OfflineTimeSec": return OfflineTimeSec;
                    case "InviteCount": return InviteCount;
                    case "ReceiveInviteList": return ReceiveInviteList;
                    default: throw new ArgumentException(string.Format("UserBasisCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserID":
                        _UserID = value.ToInt();
                        break;
                    case "ServerID":
                        _ServerId = value.ToInt();
                        break;
                    case "RetailID":
                        _RetailID = value.ToNotNullString();
                        break;
                    case "Pid":
                        _Pid = value.ToNotNullString();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "UserLv":
                        _UserLv = value.ToInt();
                        break;
                    case "Profession":
                        _Profession = value.ToInt();
                        break;
                    case "RewardsDiamond":
                        _RewardsDiamond = value.ToInt();
                        break;
                    case "BuyDiamond":
                        _BuyDiamond = value.ToInt();
                        break;
                    case "UsedDiamond":
                        _UsedDiamond = value.ToInt();
                        break;
                    case "Gold":
                        _Gold = value.ToNotNullString();
                        break;
                    //case "Exp":
                    //    _Exp = value.ToInt();
                    //    break;
                    case "VipLv":
                        _VipLv = value.ToInt();
                        break;
                    case "AvatarUrl":
                        _AvatarUrl = value.ToNotNullString();
                        break;
                    case "UserStatus":
                        _UserStatus = value.ToEnum<UserStatus>();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    case "LoginDate":
                        _LoginDate = value.ToDateTime();
                        break;
                    case "OfflineDate":
                        _OfflineDate = value.ToDateTime();
                        break;
                    case "RestoreDate":
                        _RestoreDate = value.ToDateTime();
                        break;
                    case "FightValueRankID":
                        _FightValueRankID = value.ToInt();
                        break;
                    case "LevelRankID":
                        _LevelRankID = value.ToInt();
                        break;
                    case "CombatRankID":
                        _CombatRankID = value.ToInt();
                        break;
                    case "LotteryTimes":
                        _LotteryTimes = value.ToInt();
                        break;
                    case "VipGiftProgress":
                        _VipGiftProgress = value.ToInt();
                        break;
                    case "ShareCount":
                        _ShareCount = value.ToInt();
                        break;
                    case "ShareDate":
                        _ShareDate = value.ToLong();
                        break;
                    case "IsReceivedRedPacket":
                        _IsReceivedRedPacket = value.ToBool();
                        break;
                    case "OfflineEarnings":
                        _OfflineEarnings = value.ToNotNullString();
                        break;
                    case "OfflineTimeSec":
                        _OfflineTimeSec = value.ToLong();
                        break;
                    case "InviteCount":
                        _InviteCount = value.ToInt();
                        break;
                    case "ReceiveInviteList":
                        _ReceiveInviteList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserBasisCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }



        public int DiamondNum
        {
            get
            {
                var val = MathUtils.Addition(RewardsDiamond, BuyDiamond, int.MaxValue);
                val = MathUtils.Subtraction(val, UsedDiamond, 0);
                return val;
            }
        }

        public BigInteger GoldNum
        {
            get
            {
                return BigInteger.Parse(Gold);
            }
        }


        public void AddGold(BigInteger num)
        {
            Gold = (GoldNum + num).ToNotNullString();
        }

        public void SubGold(BigInteger num)
        {
            if (GoldNum > num)
            {
                Gold = (GoldNum - num).ToNotNullString();
            }
        }


    }
}