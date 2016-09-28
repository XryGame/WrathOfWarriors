
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
    public class Config_ItemGrade : ShareEntity
    {

        public Config_ItemGrade()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private int _ID;
        /// <summary>
        /// 道具id
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
        private int _ItemID;
        /// <summary>
        /// 道具id
        /// </summary>
        [EntityField("ItemID")]
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
        private int _ItemLv;
        /// <summary>
        /// 等级
        /// </summary>
        [EntityField("ItemLv")]
        public int ItemLv
        {
            get
            {
                return _ItemLv;
            }
            private set
            {
                SetChange("ItemLv", value);
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
        private int _Hurt;
        /// <summary>
        /// 伤害
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
        /// 需要同id道具数量
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

        private string _Des;
        /// <summary>
        /// 描述
        /// </summary>
        [EntityField("Des")]
        public string Des
        {
            get
            {
                return _Des;
            }
            private set
            {
                SetChange("Des", value);
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
                    case "ItemID": return ItemID;
                    case "ItemLv": return ItemLv;
                    case "Attack": return Attack;
                    case "Defense": return Defense;
                    case "HP": return HP;
                    case "Hurt": return Hurt;
                    case "Condition": return Condition;
                    case "Des": return Des;
                    default: throw new ArgumentException(string.Format("Config_ItemGrade index[{0}] isn't exist.", index));
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
                    case "ItemID":
                        _ItemID = value.ToInt();
                        break;
                    case "ItemLv":
                        _ItemLv = value.ToInt();
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
                    case "Des":
                        _Des = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_ItemGrade index[{0}] isn't exist.", index));
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