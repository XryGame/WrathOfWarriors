
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
    public class Config_Monster : ShareEntity
    {

        
        public Config_Monster()
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
        /// 怪物类型
        /// </summary>
        private MonsterType _Type;
        [EntityField("Type")]
        public MonsterType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                SetChange("Type", value);
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        private string _Name;
        [EntityField("Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetChange("Name", value);
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
            set
            {
                SetChange("Grade", value);
            }
        }

        /// <summary>
        /// 掉落金币
        /// </summary>
        private string _DropoutGold;
        [EntityField("DropoutGold")]
        public string DropoutGold
        {
            get
            {
                return _DropoutGold;
            }
            set
            {
                SetChange("DropoutGold", value);
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
                    case "Type": return Type;
                    case "Name": return Name;
                    case "Grade": return Grade;
                    case "DropoutGold": return DropoutGold;
                    default: throw new ArgumentException(string.Format("Config_Monster index[{0}] isn't exist.", index));
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
                    case "Type":
                        _Type = value.ToEnum<MonsterType>();
                        break;
                    case "Name":
                        _Name = value.ToNotNullString(); 
                        break;
                    case "Grade":
                        _Grade = value.ToInt(); 
                        break;
                    case "DropoutGold":
                        _DropoutGold = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Monster index[{0}] isn't exist.", index));
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