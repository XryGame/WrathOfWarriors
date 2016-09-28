
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Role : ShareEntity
    {

        public Config_Role()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private int _ID;
        /// <summary>
        /// 怪物id
        /// </summary>
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
        private string _Name;
        /// <summary>
        /// 怪物名称
        /// </summary>
        [EntityField("Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                SetChange("Name", value);
            }
        }
        private int _RoleType;
        /// <summary>
        /// 怪物类型
        /// </summary>
        [EntityField("RoleType")]
        public int RoleType
        {
            get
            {
                return _RoleType;
            }
            private set
            {
                SetChange("RoleType", value);
            }
        }
        private int _RoleLV;
        /// <summary>
        /// 怪物等级
        /// </summary>
        [EntityField("RoleLV")]
        public int RoleLV
        {
            get
            {
                return _RoleLV;
            }
            private set
            {
                SetChange("RoleLV", value);
            }
        }
        private string _RoleResource;
        /// <summary>
        /// 怪物资源
        /// </summary>
        [EntityField("RoleResource")]
        public string RoleResource
        {
            get
            {
                return _RoleResource;
            }
            private set
            {
                SetChange("RoleResource", value);
            }
        }
        private int _Skill;
        /// <summary>
        /// 技能id
        /// </summary>
        [EntityField("Skill")]
        public int Skill
        {
            get
            {
                return _Skill;
            }
            private set
            {
                SetChange("Skill", value);
            }
        }
        private int _SkillGrade;
        /// <summary>
        /// 技能等级
        /// </summary>
        [EntityField("SkillGrade")]
        public int SkillGrade
        {
            get
            {
                return _SkillGrade;
            }
            private set
            {
                SetChange("SkillGrade", value);
            }
        }
        private int _Attack;
        /// <summary>
        /// 攻击
        /// </summary>
        [EntityField("Attack")]
        public int Attack
        {
            get
            {
                return _Attack;
            }
            private set
            {
                SetChange("Attack", value);
            }
        }
        private int _Defense;
        /// <summary>
        /// 防御
        /// </summary>
        [EntityField("Defense")]
        public int Defense
        {
            get
            {
                return _Defense;
            }
            private set
            {
                SetChange("Defense", value);
            }
        }
        private int _HP;
        /// <summary>
        /// 生命
        /// </summary>
        [EntityField("HP")]
        public int HP
        {
            get
            {
                return _HP;
            }
            private set
            {
                SetChange("HP", value);
            }
        }
        private int _Time;
        /// <summary>
        /// 消耗时间
        /// </summary>
        [EntityField("Time")]
        public int Time
        {
            get
            {
                return _Time;
            }
            private set
            {
                SetChange("Time", value);
            }
        }
        private int _Exp;
        /// <summary>
        /// 获得经验
        /// </summary>
        [EntityField("Exp")]
        public int Exp
        {
            get
            {
                return _Exp;
            }
            private set
            {
                SetChange("Exp", value);
            }
        }
        private string _Slogan;
        /// <summary>
        /// 口号
        /// </summary>
        [EntityField("Slogan")]
        public string Slogan
        {
            get
            {
                return _Slogan;
            }
            private set
            {
                SetChange("Slogan", value);
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
                    case "Name": return Name;
                    case "RoleType": return RoleType;
                    case "RoleLV": return RoleLV;
                    case "RoleResource": return RoleResource;
                    case "Skill": return Skill;
                    case "SkillGrade": return SkillGrade;
                    case "Attack": return Attack;
                    case "Defense": return Defense;
                    case "HP": return HP;
                    case "Time": return Time;
                    case "Exp": return Exp;
                    case "Slogan": return Slogan;
                    default: throw new ArgumentException(string.Format("Config_Role index[{0}] isn't exist.", index));
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
                    case "Name":
                        _Name = value.ToNotNullString(); 
                        break;
                    case "RoleType":
                        _RoleType = value.ToInt();
                        break;
                    case "RoleLV":
                        _RoleLV = value.ToInt(); 
                        break; 
                    case "RoleResource":
                        _RoleResource = value.ToNotNullString();
                        break;
                    case "Skill":
                        _Skill = value.ToInt();
                        break;
                    case "SkillGrade":
                        _SkillGrade = value.ToInt();
                        break;
                    case "Attack":
                        _Attack = value.ToInt();
                        break;
                    case "Defense":
                        _Defense = value.ToInt();
                        break;
                    case "HP":
                        _HP = value.ToInt();
                        break;
                    case "Time":
                        _Time = value.ToInt();
                        break;
                    case "Exp":
                        _Exp = value.ToInt();
                        break;
                    case "Slogan":
                        _Slogan = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Rolo index[{0}] isn't exist.", index));
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