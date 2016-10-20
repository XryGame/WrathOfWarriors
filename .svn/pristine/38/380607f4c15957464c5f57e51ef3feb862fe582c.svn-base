
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
    public class Config_Vip : ShareEntity
    {

        
        public Config_Vip()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// vip等级ID
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
        /// 充值金额
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
        /// 购买体力次数
        /// </summary>
        private int _BuyStamina;
        [EntityField("BuyStamina")]
        public int BuyStamina
        {
            get
            {
                return _BuyStamina;
            }
            set
            {
                SetChange("BuyStamina", value);
            }
        }

        /// <summary>
        /// 购买名人榜挑战次数
        /// </summary>
        private int _BuyAthletics;
        [EntityField("BuyAthletics")]
        public int BuyAthletics
        {
            get
            {
                return _BuyAthletics;
            }
            set
            {
                SetChange("BuyAthletics", value);
            }
        }

        /// <summary>
        /// vip累冲奖励类型
        /// </summary>
        private VipObtainType _ObtainType;
        [EntityField("ObtainType")]
        public VipObtainType ObtainType
        {
            get
            {
                return _ObtainType;
            }
            set
            {
                SetChange("ObtainType", value);
            }
        }

        /// <summary>
        /// vip累冲奖励ID或者数量
        /// </summary>
        private int _ObtainNum;
        [EntityField("ObtainNum")]
        public int ObtainNum
        {
            get
            {
                return _ObtainNum;
            }
            set
            {
                SetChange("ObtainNum", value);
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
                    case "PaySum": return PaySum;
                    case "BuyStamina": return BuyStamina;
                    case "BuyAthletics": return BuyAthletics;
                    case "ObtainType": return ObtainType;
                    case "ObtainNum": return ObtainNum;
                    default: throw new ArgumentException(string.Format("Config_Vip index[{0}] isn't exist.", index));
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
                    case "PaySum":
                        _PaySum = value.ToInt(); 
                        break;
                    case "BuyStamina":
                        _BuyStamina = value.ToInt();
                        break;
                    case "BuyAthletics":
                        _BuyAthletics = value.ToInt();
                        break;
                    case "ObtainType":
                        _ObtainType = value.ToEnum<VipObtainType>();
                        break;
                    case "ObtainNum":
                        _ObtainNum = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Vip index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        }
	}
}