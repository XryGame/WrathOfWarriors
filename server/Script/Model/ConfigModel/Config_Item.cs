
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Item : ShareEntity
    {

        public Config_Item()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 物品id
        /// </summary>
        private int _ItemID;
        [EntityField("ItemID", IsKey = true)]
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            private set
            {
                SetChange("ItemID", value);
            }
        }

        /// <summary>
        /// 物品名称
        /// </summary>
        private string _ItemName;
        [EntityField("ItemName")]
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            private set
            {
                SetChange("ItemName", value);
            }
        }

        /// <summary>
        /// 物品类型
        /// </summary>
        private ItemType _ItemType;
        [EntityField("ItemType")]
        public ItemType ItemType
        {
            get
            {
                return _ItemType;
            }
            private set
            {
                SetChange("ItemType", value);
            }
        }

        /// <summary>
        /// 物品子类型
        /// </summary>
        private int _Species;
        [EntityField("Species")]
        public int Species
        {
            get
            {
                return _Species;
            }
            private set
            {
                SetChange("Species", value);
            }
        }

        /// <summary>
        /// 物品初始品质
        /// </summary>
        private ItemQuality _Quality;
        [EntityField("Quality")]
        public ItemQuality Quality
        {
            get
            {
                return _Quality;
            }
            private set
            {
                SetChange("Quality", value);
            }
        }

        /// <summary>
        /// 使用获得资源类型
        /// </summary>
        private ResourceType _ResourceType;
        [EntityField("ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return _ResourceType;
            }
            private set
            {
                SetChange("ResourceType", value);
            }
        }

        /// <summary>
        /// 获得资源数量
        /// </summary>
        private string _ResourceNum;
        [EntityField("ResourceNum")]
        public string ResourceNum
        {
            get
            {
                return _ResourceNum;
            }
            private set
            {
                SetChange("ResourceNum", value);
            }
        }

        /// <summary>
        /// 是否可出售
        /// </summary>
        private bool _IfSell;
        [EntityField("IfSell")]
        public bool IfSell
        {
            get
            {
                return _IfSell;
            }
            private set
            {
                SetChange("IfSell", value);
            }
        }

        /// <summary>
        /// 出售获得金钱
        /// </summary>
        private string _SellGold;
        [EntityField("SellGold")]
        public string SellGold
        {
            get
            {
                return _SellGold;
            }
            private set
            {
                SetChange("SellGold", value);
            }
        }

        /// <summary>
        /// 初始生命
        /// </summary>
        private int _hp;
        [EntityField("hp")]
        public int hp
        {
            get
            {
                return _hp;
            }
            private set
            {
                SetChange("hp", value);
            }
        }

        /// <summary>
        /// 初始攻击
        /// </summary>
        private int _attack;
        [EntityField("attack")]
        public int attack
        {
            get
            {
                return _attack;
            }
            private set
            {
                SetChange("attack", value);
            }
        }

        /// <summary>
        /// 初始防御
        /// </summary>
        private int _defense;
        [EntityField("defense")]
        public int defense
        {
            get
            {
                return _defense;
            }
            private set
            {
                SetChange("defense", value);
            }
        }

        /// <summary>
        /// 初始闪避
        /// </summary>
        private int _dodge;
        [EntityField("dodge")]
        public int dodge
        {
            get
            {
                return _dodge;
            }
            private set
            {
                SetChange("dodge", value);
            }
        }

        /// <summary>
        /// 初始暴击
        /// </summary>
        private int _crit;
        [EntityField("crit")]
        public int crit
        {
            get
            {
                return _crit;
            }
            private set
            {
                SetChange("crit", value);
            }
        }

        /// <summary>
        /// 初始命中
        /// </summary>
        private int _hit;
        [EntityField("hit")]
        public int hit
        {
            get
            {
                return _hit;
            }
            private set
            {
                SetChange("hit", value);
            }
        }

        /// <summary>
        /// 初始韧性
        /// </summary>
        private int _tenacity;
        [EntityField("tenacity")]
        public int tenacity
        {
            get
            {
                return _tenacity;
            }
            private set
            {
                SetChange("tenacity", value);
            }
        }
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ItemID": return ItemID;
                    case "ItemName": return ItemName;
                    case "ItemType": return ItemType;
                    case "Species": return Species;
                    case "Quality": return Quality;
                    case "ResourceType": return ResourceType;
                    case "ResourceNum": return ResourceNum;
                    case "IfSell": return IfSell;
                    case "SellGold": return SellGold;
                    case "hp": return hp;
                    case "attack": return attack;
                    case "defense": return defense;
                    case "dodge": return dodge;
                    case "crit": return crit;
                    case "hit": return hit;
                    case "tenacity": return tenacity;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ItemID":
                        _ItemID = value.ToInt(); 
                        break; 
                    case "ItemName":
                        _ItemName = value.ToNotNullString(); 
                        break;
                    case "ItemType":
                        _ItemType = value.ToEnum<ItemType>();
                        break;
                    case "Species":
                        _Species = value.ToInt(); 
                        break; 
                    case "Quality":
                        _Quality = value.ToEnum<ItemQuality>();
                        break; 
                    case "ResourceType":
                        _ResourceType = value.ToEnum<ResourceType>();
                        break;
                    case "ResourceNum":
                        _ResourceNum = value.ToNotNullString();
                        break;
                    case "IfSell":
                        _IfSell = value.ToBool();
                        break;
                    case "SellGold":
                        _SellGold = value.ToNotNullString();
                        break;
                    case "hp":
                        _hp = value.ToInt();
                        break;
                    case "attack":
                        _attack = value.ToInt();
                        break;
                    case "defense":
                        _defense = value.ToInt();
                        break;
                    case "dodge":
                        _dodge = value.ToInt();
                        break;
                    case "crit":
                        _crit = value.ToInt();
                        break;
                    case "hit":
                        _hit = value.ToInt();
                        break;
                    case "tenacity":
                        _tenacity = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
        //protected override int GetIdentityId()
        //{
        //    //allow modify return value
        //    return DefIdentityId;
        //}
	}
}