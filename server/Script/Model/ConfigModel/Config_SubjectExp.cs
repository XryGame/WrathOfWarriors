
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 科目经验
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_SubjectExp : ShareEntity
    {

        public Config_SubjectExp()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private SubjectID _id;
        /// <summary>
        /// ID
        /// </summary>
        [EntityField("id", IsKey = true)]
        public SubjectID id
        {
            get
            {
                return _id;
            }
            private set
            {
                SetChange("id", value);
            }
        }
        private string _Subject;
        /// <summary>
        /// 项目
        /// </summary>
        [EntityField("Subject")]
        public string Subject
        {
            get
            {
                return _Subject;
            }
            private set
            {
                SetChange("Subject", value);
            }
        }
        private SubjectType _Type;
        /// <summary>
        /// 类型
        /// </summary>
        [EntityField("Type")]
        public SubjectType Type
        {
            get
            {
                return _Type;
            }
            private set
            {
                SetChange("Type", value);
            }
        }

        private SubjectChildType _SubType;
        /// <summary>
        /// 子类型
        /// </summary>
        [EntityField("SubType")]
        public SubjectChildType SubType
        {
            get
            {
                return _SubType;
            }
            private set
            {
                SetChange("SubType", value);
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
        private int _UnitTime;
        /// <summary>
        /// 单位时间
        /// </summary>
        [EntityField("UnitTime")]
        public int UnitTime
        {
            get
            {
                return _UnitTime;
            }
            private set
            {
                SetChange("UnitTime", value);
            }
        }
        private int _UnitExp;
        /// <summary>
        /// 单位经验
        /// </summary>
        [EntityField("UnitExp")]
        public int UnitExp
        {
            get
            {
                return _UnitExp;
            }
            private set
            {
                SetChange("UnitExp", value);
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
                    case "Subject": return Subject;
                    case "Type": return Type;
                    case "SubType": return SubType;
                    case "Stage": return Stage;
                    case "UnitTime": return UnitTime;
                    case "UnitExp": return UnitExp;
                    default: throw new ArgumentException(string.Format("Config_SubjectExp index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToEnum<SubjectID>(); 
                        break; 
                    case "Subject":
                        _Subject = value.ToNotNullString(); 
                        break; 
                    case "Type":
                        _Type = value.ToEnum<SubjectType>(); 
                        break;
                    case "SubType":
                        _SubType = value.ToEnum< SubjectChildType>();
                        break;
                    case "Stage":
                        _Stage = value.ToInt(); 
                        break;
                    case "UnitTime":
                        _UnitTime = value.ToInt();
                        break;
                    case "UnitExp":
                        _UnitExp = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_SubjectExp index[{0}] isn't exist.", index));
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