
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
    public class Config_CdKey : ShareEntity
    {


        public Config_CdKey()
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
            set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// Key
        /// </summary>
        private string _Key;
        [EntityField("Key")]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                SetChange("Key", value);
            }
        }

        /// <summary>
        /// 奖励A
        /// </summary>
        private int _AAwardID;
        [EntityField("AAwardID")]
        public int AAwardID
        {
            get
            {
                return _AAwardID;
            }
            set
            {
                SetChange("AAwardID", value);
            }
        }

        /// <summary>
        /// 奖励A
        /// </summary>
        private int _AAwardN;
        [EntityField("AAwardN")]
        public int AAwardN
        {
            get
            {
                return _AAwardN;
            }
            set
            {
                SetChange("AAwardN", value);
            }
        }

        /// <summary>
        /// 奖励B
        /// </summary>
        private int _BAwardID;
        [EntityField("BAwardID")]
        public int BAwardID
        {
            get
            {
                return _BAwardID;
            }
            set
            {
                SetChange("BAwardID", value);
            }
        }

        /// <summary>
        /// 奖励B
        /// </summary>
        private int _BAwardN;
        [EntityField("BAwardN")]
        public int BAwardN
        {
            get
            {
                return _BAwardN;
            }
            set
            {
                SetChange("BAwardN", value);
            }
        }

        /// <summary>
        /// 奖励C
        /// </summary>
        private int _CAwardID;
        [EntityField("CAwardID")]
        public int CAwardID
        {
            get
            {
                return _CAwardID;
            }
            set
            {
                SetChange("CAwardID", value);
            }
        }

        /// <summary>
        /// 奖励C
        /// </summary>
        private int _CAwardN;
        [EntityField("CAwardN")]
        public int CAwardN
        {
            get
            {
                return _CAwardN;
            }
            set
            {
                SetChange("CAwardN", value);
            }
        }

        /// <summary>
        /// 奖励D
        /// </summary>
        private int _DAwardID;
        [EntityField("DAwardID")]
        public int DAwardID
        {
            get
            {
                return _DAwardID;
            }
            set
            {
                SetChange("DAwardID", value);
            }
        }

        /// <summary>
        /// 奖励D
        /// </summary>
        private int _DAwardN;
        [EntityField("DAwardN")]
        public int DAwardN
        {
            get
            {
                return _DAwardN;
            }
            set
            {
                SetChange("DAwardN", value);
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
                    case "Key": return Key;
                    case "AAwardID": return AAwardID;
                    case "AAwardN": return AAwardN;
                    case "BAwardID": return BAwardID;
                    case "BAwardN": return BAwardN;
                    case "CAwardID": return CAwardID;
                    case "CAwardN": return CAwardN;
                    case "DAwardID": return DAwardID;
                    case "DAwardN": return DAwardN;
                    default: throw new ArgumentException(string.Format("Config_CdKey index[{0}] isn't exist.", index));
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
                    case "Key":
                        _Key = value.ToNotNullString();
                        break;
                    case "AAwardID":
                        _AAwardID = value.ToInt();
                        break;
                    case "AAwardN":
                        _AAwardN = value.ToInt();
                        break;
                    case "BAwardID":
                        _BAwardID = value.ToInt();
                        break;
                    case "BAwardN":
                        _BAwardN = value.ToInt();
                        break;
                    case "CAwardID":
                        _CAwardID = value.ToInt();
                        break;
                    case "CAwardN":
                        _CAwardN = value.ToInt();
                        break;
                    case "DAwardID":
                        _DAwardID = value.ToInt();
                        break;
                    case "DAwardN":
                        _DAwardN = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_CdKey index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        #endregion
    }

}