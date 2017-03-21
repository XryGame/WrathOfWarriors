
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
    public class Config_Equip : ShareEntity
    {

        public Config_Equip()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private int _ID;
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            private set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 装备id
        /// </summary>
        private EquipID _EquipID;
        [EntityField("EquipID")]
        public EquipID EquipID
        {
            get
            {
                return _EquipID;
            }
            private set
            {
                SetChange("EquipID", value);
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        private int _Grade;
        [EntityField("Grade")]
        public int Grade
        {
            get
            {
                return _Grade;
            }
            private set
            {
                SetChange("Grade", value);
            }
        }

        /// <summary>
        /// 品质
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
        /// 升级消耗金币
        /// </summary>
        private string _GradeConsumeGold;
        [EntityField("GradeConsumeGold")]
        public string GradeConsumeGold
        {
            get
            {
                return _GradeConsumeGold;
            }
            private set
            {
                SetChange("GradeConsumeGold", value);
            }
        }

        /// <summary>
        /// 升级消耗钻石
        /// </summary>
        private int _GradeConsumediamond;
        [EntityField("GradeConsumediamond")]
        public int GradeConsumediamond
        {
            get
            {
                return _GradeConsumediamond;
            }
            private set
            {
                SetChange("GradeConsumediamond", value);
            }
        }

        /// <summary>
        /// 加成生命
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
        /// 加成攻击
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
        /// 加成防御
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
        /// 加成闪避
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
        /// 加成暴击
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
        /// 加成命中
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
        /// 加成韧性
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
                    case "ID": return ID;
                    case "EquipID": return EquipID;
                    case "Grade": return Grade;
                    case "Quality": return Quality;
                    case "GradeConsumeGold": return GradeConsumeGold;
                    case "GradeConsumediamond": return GradeConsumediamond;
                    case "hp": return hp;
                    case "attack": return attack;
                    case "defense": return defense;
                    case "dodge": return dodge;
                    case "crit": return crit;
                    case "hit": return hit;
                    case "tenacity": return tenacity;
                    default: throw new ArgumentException(string.Format("Config_Equip index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ID":
                        _ID = value.ToInt(); 
                        break;
                    case "EquipID":
                        _EquipID = value.ToEnum<EquipID>();
                        break;
                    case "Grade":
                        _Grade = value.ToInt();
                        break;
                    case "Quality":
                        _Quality = value.ToEnum<ItemQuality>();
                        break;
                    case "GradeConsumeGold":
                        _GradeConsumeGold = value.ToNotNullString();
                        break;
                    case "GradeConsumediamond":
                        _GradeConsumediamond = value.ToInt();
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
                    default: throw new ArgumentException(string.Format("Config_Equip index[{0}] isn't exist.", index));
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