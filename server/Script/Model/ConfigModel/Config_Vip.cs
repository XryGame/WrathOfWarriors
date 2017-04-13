
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
        /// 购买金币次数
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
        /// 购买竞技场挑战次数
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
        /// 是否离线收益加倍
        /// </summary>
        private int _Multiple;
        [EntityField("Multiple")]
        public int Multiple
        {
            get
            {
                return _Multiple;
            }
            set
            {
                SetChange("Multiple", value);
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
                    case "Multiple": return Multiple;
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
                    case "Multiple":
                        _Multiple = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Vip index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
               
	}
}