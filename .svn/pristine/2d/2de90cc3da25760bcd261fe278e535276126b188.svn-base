
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
    public class Config_RoleGrade : ShareEntity
    {

        public Config_RoleGrade()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private int _ID;
        /// <summary>
        /// ID
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
        /// 等级name
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
        private int _Stage;
        /// <summary>
        /// 阶段
        /// </summary>
        [EntityField("Stage")]
        public int Stage
        {
            get
            {
                return _Stage;
            }
            private set
            {
                SetChange("Stage", value);
            }
        }
        private int _BaseExp;
        /// <summary>
        /// 学习和劳动经验
        /// </summary>
        [EntityField("BaseExp")]
        public int BaseExp
        {
            get
            {
                return _BaseExp;
            }
            private set
            {
                SetChange("BaseExp", value);
            }
        }
        private int _FightExp;
        /// <summary>
        /// 挑战经验
        /// </summary>
        [EntityField("FightExp")]
        public int FightExp
        {
            get
            {
                return _FightExp;
            }
            private set
            {
                SetChange("FightExp", value);
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
        /// 防御力
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
        /// 生命值
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

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ID": return ID;
                    case "Name": return Name;
                    case "Stage": return Stage;
                    case "BaseExp": return BaseExp;
                    case "FightExp": return FightExp;
                    case "Attack": return Attack;
                    case "Defense": return Defense;
                    case "HP": return HP;
                    default: throw new ArgumentException(string.Format("Config_RoloGrade index[{0}] isn't exist.", index));
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
                    case "Stage":
                        _Stage = value.ToInt();
                        break;
                    case "BaseExp":
                        _BaseExp = value.ToInt(); 
                        break; 
                    case "FightExp":
                        _FightExp = value.ToInt(); 
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
                    default: throw new ArgumentException(string.Format("Config_RoleGrade index[{0}] isn't exist.", index));
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