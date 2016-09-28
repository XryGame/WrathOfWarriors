
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 场景
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Scene : ShareEntity
    {

        public Config_Scene()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private SceneType _ID;
        /// <summary>
        /// 场景id
        /// </summary>
        [EntityField("ID", IsKey = true)]
        public SceneType ID
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
        /// 场景名字
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
        private int _Behavior;
        /// <summary>
        /// 行为
        /// </summary>
        [EntityField("Behavior")]
        public int Behavior
        {
            get
            {
                return _Behavior;
            }
            private set
            {
                SetChange("Behavior", value);
            }
        }
        private int _ClearGrade;
        /// <summary>
        /// 解锁等级
        /// </summary>
        [EntityField("ClearGrade")]
        public int ClearGrade
        {
            get
            {
                return _ClearGrade;
            }
            private set
            {
                SetChange("ClearGrade", value);
            }
        }
        private int _Resource;
        /// <summary>
        /// 资源
        /// </summary>
        [EntityField("Resource")]
        public int Resource
        {
            get
            {
                return _Resource;
            }
            private set
            {
                SetChange("Resource", value);
            }
        }
        private int _OccupyAdd;
        /// <summary>
        /// 占领加成
        /// </summary>
        [EntityField("OccupyAdd")]
        public int OccupyAdd
        {
            get
            {
                return _OccupyAdd;
            }
            private set
            {
                SetChange("OccupyAdd", value);
            }
        }
        private int _MaxUnit;
        /// <summary>
        /// 最大单位
        /// </summary>
        [EntityField("MaxUnit")]
        public int MaxUnit
        {
            get
            {
                return _MaxUnit;
            }
            private set
            {
                SetChange("MaxUnit", value);
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
                    case "Behavior": return Behavior;
                    case "ClearGrade": return ClearGrade;
                    case "Resource": return Resource;
                    case "OccupyAdd": return OccupyAdd;
                    case "MaxUnit": return MaxUnit;
                    default: throw new ArgumentException(string.Format("Config_Scene index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ID":
                        _ID = value.ToEnum<SceneType>();
                        break;
                    case "Name":
                        _Name = value.ToNotNullString();
                        break;
                    case "Behavior":
                        _Behavior = value.ToInt();
                        break;
                    case "ClearGrade":
                        _ClearGrade = value.ToInt();
                        break;
                    case "Resource":
                        _Resource = value.ToInt();
                        break;
                    case "OccupyAdd":
                        _OccupyAdd = value.ToInt();
                        break;
                    case "MaxUnit":
                        _MaxUnit = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Scene index[{0}] isn't exist.", index));
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