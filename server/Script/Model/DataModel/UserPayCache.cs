﻿
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户充值信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserPayCache : BaseEntity
    {

        public UserPayCache()
            : base(AccessLevel.ReadWrite)
        {
            AccumulatePayList = new CacheList<int>();
            AccumulateConsumeList = new CacheList<int>();
            PayList = new CacheList<int>();
            Fund50 = new FundData();
            Fund98 = new FundData();
            Fund298 = new FundData();
            //ResetCache();
        }
        
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

        private int _PayMoney;
        [ProtoMember(2)]
        [EntityField("PayMoney")]
        public int PayMoney
        {
            get
            {
                return _PayMoney;
            }
            set
            {
                SetChange("PayMoney", value);
            }
        }

        /// <summary>
        /// 是否领取首充
        /// </summary>
        private bool _IsReceiveFirstPay;
        [ProtoMember(3)]
        [EntityField("IsReceiveFirstPay")]
        public bool IsReceiveFirstPay
        {
            get
            {
                return _IsReceiveFirstPay;
            }
            set
            {
                SetChange("IsReceiveFirstPay", value);
            }
        }

        /// <summary>
        /// 季卡剩余天数
        /// </summary>
        private int _QuarterCardDays;
        [ProtoMember(4)]
        [EntityField("QuarterCardDays")]
        public int QuarterCardDays
        {
            get
            {
                return _QuarterCardDays;
            }
            set
            {
                SetChange("QuarterCardDays", value);
            }
        }

        /// <summary>
        /// 月卡剩余天数
        /// </summary>
        private int _MonthCardDays;
        [ProtoMember(5)]
        [EntityField("MonthCardDays")]
        public int MonthCardDays
        {
            get
            {
                return _MonthCardDays;
            }
            set
            {
                SetChange("MonthCardDays", value);
            }
        }

        /// <summary>
        /// 季卡奖励日期
        /// </summary>
        private DateTime _QuarterCardAwardDate;
        [ProtoMember(6)]
        [EntityField("QuarterCardAwardDate")]
        public DateTime QuarterCardAwardDate
        {
            get
            {
                return _QuarterCardAwardDate;
            }
            set
            {
                SetChange("QuarterCardAwardDate", value);
            }
        }

        /// <summary>
        /// 月卡奖励日期
        /// </summary>
        private DateTime _MonthCardAwardDate;
        [ProtoMember(7)]
        [EntityField("MonthCardAwardDate")]
        public DateTime MonthCardAwardDate
        {
            get
            {
                return _MonthCardAwardDate;
            }
            set
            {
                SetChange("MonthCardAwardDate", value);
            }
        }

        /// <summary>
        /// 领取累充记录
        /// </summary>
        private CacheList<int> _AccumulatePayList;
        [ProtoMember(8)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> AccumulatePayList
        {
            get
            {
                return _AccumulatePayList;
            }
            set
            {
                SetChange("AccumulatePayList", value);
            }
        }

        /// <summary>
        /// 购买金币次数
        /// </summary>
        private int _BuyGoldTimes;
        [ProtoMember(9)]
        [EntityField("BuyGoldTimes")]
        public int BuyGoldTimes
        {
            get
            {
                return _BuyGoldTimes;
            }
            set
            {
                SetChange("BuyGoldTimes", value);
            }
        }

        /// <summary>
        /// 购买体力次数
        /// </summary>
        private int _BuyVitTimes;
        [ProtoMember(10)]
        [EntityField("BuyVitTimes")]
        public int BuyVitTimes
        {
            get
            {
                return _BuyVitTimes;
            }
            set
            {
                SetChange("BuyVitTimes", value);
            }
        }

        /// <summary>
        /// 领取累耗记录
        /// </summary>
        private CacheList<int> _AccumulateConsumeList;
        [ProtoMember(11)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> AccumulateConsumeList
        {
            get
            {
                return _AccumulateConsumeList;
            }
            set
            {
                SetChange("AccumulateConsumeList", value);
            }
        }

        /// <summary>
        /// 充值记录
        /// </summary>
        private CacheList<int> _PayList;
        [ProtoMember(12)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> PayList
        {
            get
            {
                return _PayList;
            }
            set
            {
                SetChange("PayList", value);
            }
        }

        /// <summary>
        /// 50元返利领取记录
        /// </summary>
        private FundData _Fund50;
        [ProtoMember(13)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public FundData Fund50
        {
            get
            {
                return _Fund50;
            }
            set
            {
                SetChange("Fund50", value);
            }
        }

        /// <summary>
        /// 98元返利领取记录
        /// </summary>
        private FundData _Fund98;
        [ProtoMember(14)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public FundData Fund98
        {
            get
            {
                return _Fund98;
            }
            set
            {
                SetChange("Fund98", value);
            }
        }

        /// <summary>
        /// 298元返利领取记录
        /// </summary>
        private FundData _Fund298;
        [ProtoMember(15)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public FundData Fund298
        {
            get
            {
                return _Fund298;
            }
            set
            {
                SetChange("Fund298", value);
            }
        }

        /// <summary>
        /// 领取重复充值计数
        /// </summary>
        private int _ReceivedRepeatPayCount;
        [ProtoMember(16)]
        [EntityField("ReceivedRepeatPayCount")]
        public int ReceivedRepeatPayCount
        {
            get
            {
                return _ReceivedRepeatPayCount;
            }
            set
            {
                SetChange("ReceivedRepeatPayCount", value);
            }
        }

        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "PayMoney": return PayMoney;
                    case "IsReceiveFirstPay": return IsReceiveFirstPay;
                    case "QuarterCardDays": return QuarterCardDays;
                    case "MonthCardDays": return MonthCardDays;
                    case "QuarterCardAwardDate": return QuarterCardAwardDate;
                    case "MonthCardAwardDate": return MonthCardAwardDate;
                    case "AccumulatePayList": return AccumulatePayList;
                    case "BuyGoldTimes": return BuyGoldTimes;
                    case "BuyVitTimes": return BuyVitTimes;
                    case "AccumulateConsumeList": return AccumulateConsumeList;
                    case "PayList": return PayList;
                    case "Fund50": return Fund50;
                    case "Fund98": return Fund98;
                    case "Fund298": return Fund298;
                    case "ReceivedRepeatPayCount": return ReceivedRepeatPayCount;
                    default: throw new ArgumentException(string.Format("UserPayCache index[{0}] isn't exist.", index));
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
                    case "PayMoney":
                        _PayMoney = value.ToInt();
                        break;
                    case "IsReceiveFirstPay":
                        _IsReceiveFirstPay = value.ToBool();
                        break;
                    case "QuarterCardDays":
                        _QuarterCardDays = value.ToInt();
                        break;
                    case "MonthCardDays":
                        _MonthCardDays = value.ToInt();
                        break;
                    case "QuarterCardAwardDate":
                        _QuarterCardAwardDate = value.ToDateTime();
                        break;
                    case "MonthCardAwardDate":
                        _MonthCardAwardDate = value.ToDateTime();
                        break;
                    case "AccumulatePayList":
                        _AccumulatePayList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "BuyGoldTimes":
                        _BuyGoldTimes = value.ToInt();
                        break;
                    case "BuyVitTimes":
                        _BuyVitTimes = value.ToInt();
                        break;
                    case "AccumulateConsumeList":
                        _AccumulateConsumeList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "PayList":
                        _PayList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "Fund50":
                        _Fund50 = ConvertCustomField<FundData>(value, index);
                        break;
                    case "Fund98":
                        _Fund98 = ConvertCustomField<FundData>(value, index);
                        break;
                    case "Fund298":
                        _Fund298 = ConvertCustomField<FundData>(value, index);
                        break;
                    case "ReceivedRepeatPayCount":
                        _ReceivedRepeatPayCount = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserPayCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        /// <summary>  
        /// 根据充值数量获得用户Vip等级
        /// </summary>  
        /// <returns></returns>  
        public short ConvertPayVipLevel()
        {
            var list = new ShareCacheStruct<Config_Vip>().FindAll(t => (t.PaySum <= PayMoney));
            if (list.Count > 0)
            { 
                return (short)list[list.Count - 1].id;
            }
            else
            {
                return 0;
            }
        }

        public void ResetCache()
        {
            PayMoney = 0;
            IsReceiveFirstPay = false;
            QuarterCardDays = -1;
            MonthCardDays = 1;
            BuyGoldTimes = 0;
            BuyVitTimes = 0;
            QuarterCardAwardDate = DateTime.Now;
            MonthCardAwardDate = DateTime.Now;
            AccumulatePayList.Clear();
            AccumulateConsumeList.Clear();
        }
    }
}