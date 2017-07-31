
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Pay : ShareEntity
    {

        
        public Config_Pay()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private int _id;
        [EntityField("id", IsKey = true)]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                SetChange("id", value);
            }
        }

        /// <summary>
        /// 标识
        /// </summary>
        private string _Identifying;
        [EntityField("Identifying")]
        public string Identifying
        {
            get
            {
                return _Identifying;
            }
            set
            {
                SetChange("Identifying", value);
            }
        }

        /// <summary>
        /// 充值的金额数（元）
        /// </summary>
        private int _PaySum;
        [EntityField("PaySum")]
        public int PaySum
        {
            get
            {
                return _PaySum;
            }
            set
            {
                SetChange("PaySum", value);
            }
        }

        /// <summary>
        /// 1：10兑换得到的钻石数量
        /// </summary>
        private int _AcquisitionDiamond;
        [EntityField("AcquisitionDiamond")]
        public int AcquisitionDiamond
        {
            get
            {
                return _AcquisitionDiamond;
            }
            set
            {
                SetChange("AcquisitionDiamond", value);
            }
        }

        /// <summary>
        /// 额外赠送的钻石数量
        /// </summary>
        private int _PresentedDiamond;
        [EntityField("PresentedDiamond")]
        public int PresentedDiamond
        {
            get
            {
                return _PresentedDiamond;
            }
            set
            {
                SetChange("PresentedDiamond", value);
            }
        }

        ///// <summary>
        ///// 是否限购
        ///// </summary>
        //private bool _IfQuota;
        //[EntityField("IfQuota")]
        //public bool IfQuota
        //{
        //    get
        //    {
        //        return _IfQuota;
        //    }
        //    set
        //    {
        //        SetChange("IfQuota", value);
        //    }
        //}

        /// <summary>
        /// 每天返还的钻石
        /// </summary>
        private int _EverydayReturn;
        [EntityField("EverydayReturn")]
        public int EverydayReturn
        {
            get
            {
                return _EverydayReturn;
            }
            set
            {
                SetChange("EverydayReturn", value);
            }
        }

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "id": return id;
                    case "Identifying": return Identifying;
                    case "PaySum": return PaySum;
                    case "AcquisitionDiamond": return AcquisitionDiamond;
                    case "PresentedDiamond": return PresentedDiamond;
                    //case "IfQuota": return IfQuota;
                    case "EverydayReturn": return EverydayReturn;
                    default: throw new ArgumentException(string.Format("Config_Pay index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToInt(); 
                        break;
                    case "Identifying":
                        _Identifying = value.ToNotNullString();
                        break;
                    case "PaySum":
                        _PaySum = value.ToInt(); 
                        break;
                    case "AcquisitionDiamond":
                        _AcquisitionDiamond = value.ToInt();
                        break;
                    case "PresentedDiamond":
                        _PresentedDiamond = value.ToInt();
                        break;
                    //case "IfQuota":
                    //    _IfQuota = value.ToBool();
                    //    break;
                    case "EverydayReturn":
                        _EverydayReturn = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Pay index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
	}
}