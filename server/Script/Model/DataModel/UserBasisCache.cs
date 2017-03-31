
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

        private int _Exp;
        [ProtoMember(12)]
        [EntityField("Exp")]
        public int Exp
        {
            get
            {
                return _Exp;
            }
            set
            {
                SetChange("Exp", value);
            }
        }

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
        /// 当天是否抽奖
        /// </summary>
        private bool _IsTodayLottery;
        [ProtoMember(32)]
        [EntityField("IsTodayLottery")]
        public bool IsTodayLottery
        {
            get
            {
                return _IsTodayLottery;
            }
            set
            {
                SetChange("IsTodayLottery", value);
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
        /// 每周切磋获得钻石数量
        /// </summary>
        private int _InviteFightDiamondNum;
        [ProtoMember(34)]
        [EntityField("InviteFightDiamondNum")]
        public int InviteFightDiamondNum
        {
            get
            {
                return _InviteFightDiamondNum;
            }
            set
            {
                SetChange("InviteFightDiamondNum", value);
            }
        }
        /// <summary>
        /// 充值切磋钻石数量日期
        /// </summary>
        private DateTime _ResetInviteFightDiamondDate;
        [ProtoMember(35)]
        [EntityField("ResetInviteFightDiamondDate")]
        public DateTime ResetInviteFightDiamondDate
        {
            get
            {
                return _ResetInviteFightDiamondDate;
            }
            set
            {
                SetChange("ResetInviteFightDiamondDate", value);
            }
        }

        /// <summary>
        /// 随机的抽奖id
        /// </summary>
        private int _LastLotteryId;
        [ProtoMember(36)]
        [EntityField("LastLotteryId")]
        public int LastLotteryId
        {
            get
            {
                return _LastLotteryId;
            }
            set
            {
                SetChange("LastLotteryId", value);
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
        /// 随机的抽奖id
        /// </summary>
        [ProtoMember(104)]
        public int RandomLotteryId
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
                    case "Exp": return Exp;
                    case "VipLv": return VipLv;
                    case "UserStatus": return UserStatus;
                    case "CreateDate": return CreateDate;
                    case "LoginDate": return LoginDate;
                    case "OfflineDate": return OfflineDate;
                    case "SessionID": return SessionID;
                    case "RestoreDate": return RestoreDate;
                    case "FightValueRankID": return FightValueRankID;
                    case "LevelRankID": return LevelRankID;
                    case "CombatRankID": return CombatRankID;
                    case "IsTodayLottery": return IsTodayLottery;
                    case "VipGiftProgress": return VipGiftProgress;
                    case "InviteFightDiamondNum": return InviteFightDiamondNum;
                    case "ResetInviteFightDiamondDate": return ResetInviteFightDiamondDate;
                    case "LastLotteryId": return LastLotteryId;
                    case "IsReceivedRedPacket": return IsReceivedRedPacket;
                    case "OfflineEarnings": return OfflineEarnings;
                    case "OfflineTimeSec": return OfflineTimeSec;
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
                    case "Exp":
                        _Exp = value.ToInt();
                        break;
                    case "VipLv":
                        _VipLv = value.ToInt();
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
                    case "IsTodayLottery":
                        _IsTodayLottery = value.ToBool();
                        break;
                    case "VipGiftProgress":
                        _VipGiftProgress = value.ToInt();
                        break;
                    case "InviteFightDiamondNum":
                        _InviteFightDiamondNum = value.ToInt();
                        break;
                    case "ResetInviteFightDiamondDate":
                        _ResetInviteFightDiamondDate = value.ToDateTime();
                        break;
                    case "LastLotteryId":
                        _LastLotteryId = value.ToInt();
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