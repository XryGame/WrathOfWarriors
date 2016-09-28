
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_SkillGrade : ShareEntity
    {

        public Config_SkillGrade()
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
        private int _SkillID;
        /// <summary>
        /// 技能id
        /// </summary>
        [EntityField("SkillID")]
        public int SkillID
        {
            get
            {
                return _SkillID;
            }
            private set
            {
                SetChange("SkillID", value);
            }
        }
        private int _SkillLv;
        /// <summary>
        /// 技能等级
        /// </summary>
        [EntityField("SkillLv")]
        public int SkillLv
        {
            get
            {
                return _SkillLv;
            }
            private set
            {
                SetChange("SkillLv", value);
            }
        }
        private int _Attack;
        /// <summary>
        /// 攻击加成
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
        /// 防御加成
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
        /// 生命加成
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
        private int _Hurt;
        /// <summary>
        /// 伤害加成
        /// </summary>
        [EntityField("Hurt")]
        public int Hurt
        {
            get
            {
                return _Hurt;
            }
            private set
            {
                SetChange("Hurt", value);
            }
        }
        private int _Condition;
        /// <summary>
        /// 升级需要道具
        /// </summary>
        [EntityField("Condition")]
        public int Condition
        {
            get
            {
                return _Condition;
            }
            private set
            {
                SetChange("Condition", value);
            }
        }
        private int _Number;
        /// <summary>
        /// 升级需要道具数量
        /// </summary>
        [EntityField("Number")]
        public int Number
        {
            get
            {
                return _Number;
            }
            private set
            {
                SetChange("Number", value);
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
                    case "SkillID": return SkillID;
                    case "SkillLv": return SkillLv;
                    case "Attack": return Attack;
                    case "Defense": return Defense;
                    case "HP": return HP;
                    case "Hurt": return Hurt;
                    case "Condition": return Condition;
                    case "Number": return Number;
                    default: throw new ArgumentException(string.Format("Config_SkillGrade index[{0}] isn't exist.", index));
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
                    case "SkillID":
                        _SkillID = value.ToInt(); 
                        break; 
                    case "SkillLv":
                        _SkillLv = value.ToInt();
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
                    case "Hurt":
                        _Hurt = value.ToInt();
                        break;
                    case "Condition":
                        _Condition = value.ToInt();
                        break;
                    case "Number":
                        _Number = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_SkillGrade index[{0}] isn't exist.", index));
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