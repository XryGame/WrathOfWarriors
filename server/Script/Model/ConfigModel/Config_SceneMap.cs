
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 场景地图背景
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_SceneMap : ShareEntity
    {

        public Config_SceneMap()
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
        private string _SceneName;
        /// <summary>
        /// 场景名字
        /// </summary>
        [EntityField("SceneName")]
        public string SceneName
        {
            get
            {
                return _SceneName;
            }
            private set
            {
                SetChange("SceneName", value);
            }
        }
        private bool _IfLock;
        /// <summary>
        /// 默认是否解锁
        /// </summary>
        [EntityField("IfLock")]
        public bool IfLock
        {
            get
            {
                return _IfLock;
            }
            private set
            {
                SetChange("IfLock", value);
            }
        }
        private int _UnLockPay;
        /// <summary>
        /// 解锁钻石
        /// </summary>
        [EntityField("UnLockPay")]
        public int UnLockPay
        {
            get
            {
                return _UnLockPay;
            }
            private set
            {
                SetChange("UnLockPay", value);
            }
        }


        private int _TranscriptAdd;
        /// <summary>
        /// 副本伤害加成
        /// </summary>
        [EntityField("TranscriptAdd")]
        public int TranscriptAdd
        {
            get
            {
                return _TranscriptAdd;
            }
            private set
            {
                SetChange("TranscriptAdd", value);
            }
        }
        private int _OtherAdd;
        /// <summary>
        /// 其他伤害加成
        /// </summary>
        [EntityField("OtherAdd")]
        public int OtherAdd
        {
            get
            {
                return _OtherAdd;
            }
            private set
            {
                SetChange("OtherAdd", value);
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
                    case "SceneName": return SceneName;
                    case "IfLock": return IfLock;
                    case "UnLockPay": return UnLockPay;
                    case "TranscriptAdd": return TranscriptAdd;
                    case "OtherAdd": return OtherAdd;
                    default: throw new ArgumentException(string.Format("Config_SceneMap index[{0}] isn't exist.", index));
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
                    case "SceneName":
                        _SceneName = value.ToNotNullString();
                        break;
                    case "IfLock":
                        _IfLock = value.ToBool();
                        break;
                    case "UnLockPay":
                        _UnLockPay = value.ToInt();
                        break;
                    case "TranscriptAdd":
                        _TranscriptAdd = value.ToInt();
                        break;
                    case "OtherAdd":
                        _OtherAdd = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_SceneMap index[{0}] isn't exist.", index));
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