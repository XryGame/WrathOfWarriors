
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
    public class Config_Elves : ShareEntity
    {

        
        public Config_Elves()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// ID
        /// </summary>
        private int _ID;
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 精灵id
        /// </summary>
        private int _ElvesID;
        [EntityField("ElvesID")]
        public int ElvesID
        {
            get
            {
                return _ElvesID;
            }
            set
            {
                SetChange("ElvesID", value);
            }
        }

        /// <summary>
        /// 精灵名称
        /// </summary>
        private string _ElvesName;
        [EntityField("ElvesName")]
        public string ElvesName
        {
            get
            {
                return _ElvesName;
            }
            set
            {
                SetChange("ElvesName", value);
            }
        }

        /// <summary>
        /// 技能类型
        /// </summary>
        private ElfSkillType _ElvesType;
        [EntityField("ElvesType")]
        public ElfSkillType ElvesType
        {
            get
            {
                return _ElvesType;
            }
            set
            {
                SetChange("ElvesType", value);
            }
        }

        /// <summary>
        /// 技能加成值
        /// </summary>
        private int _ElvesNum;
        [EntityField("ElvesNum")]
        public int ElvesNum
        {
            get
            {
                return _ElvesNum;
            }
            set
            {
                SetChange("ElvesNum", value);
            }
        }

        /// <summary>
        /// 技能等级
        /// </summary>
        private int _ElvesGrade;
        [EntityField("ElvesGrade")]
        public int ElvesGrade
        {
            get
            {
                return _ElvesGrade;
            }
            set
            {
                SetChange("ElvesGrade", value);
            }
        }

        /// <summary>
        /// 升级花费
        /// </summary>
        private string _GradeConsume;
        [EntityField("GradeConsume")]
        public string GradeConsume
        {
            get
            {
                return _GradeConsume;
            }
            set
            {
                SetChange("GradeConsume", value);
            }
        }

        /// <summary>
        /// 生命加成
        /// </summary>
        private long _hp;
        [EntityField("hp")]
        public long hp
        {
            get
            {
                return _hp;
            }
            set
            {
                SetChange("hp", value);
            }
        }

        /// <summary>
        /// 攻击加成
        /// </summary>
        private long _attack;
        [EntityField("attack")]
        public long attack
        {
            get
            {
                return _attack;
            }
            set
            {
                SetChange("attack", value);
            }
        }
        /// <summary>
        /// 防御加成
        /// </summary>
        private long _defense;
        [EntityField("defense")]
        public long defense
        {
            get
            {
                return _defense;
            }
            set
            {
                SetChange("defense", value);
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
                    case "ElvesID": return ElvesID;
                    case "ElvesName": return ElvesName;
                    case "ElvesType": return ElvesType;
                    case "ElvesNum": return ElvesNum;
                    case "ElvesGrade": return ElvesGrade;
                    case "GradeConsume": return GradeConsume;
                    case "hp": return hp;
                    case "attack": return attack;
                    case "defense": return defense;
                    default: throw new ArgumentException(string.Format("Config_Elves index[{0}] isn't exist.", index));
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
                    case "ElvesID":
                        _ElvesID = value.ToInt(); 
                        break;
                    case "ElvesName":
                        _ElvesName = value.ToNotNullString();
                        break;
                    case "ElvesType":
                        _ElvesType = value.ToEnum<ElfSkillType>();
                        break;
                    case "ElvesNum":
                        _ElvesNum = value.ToInt(); 
                        break;
                    case "ElvesGrade":
                        _ElvesGrade = value.ToInt(); 
                        break;
                    case "GradeConsume":
                        _GradeConsume = value.ToNotNullString("0");
                        break;
                    case "hp":
                        _hp = value.ToLong();
                        break;
                    case "attack":
                        _attack = value.ToLong();
                        break;
                    case "defense":
                        _defense = value.ToLong();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Elves index[{0}] isn't exist.", index));
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