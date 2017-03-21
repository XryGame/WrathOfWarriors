
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 宝石合成
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Gem : ShareEntity
    {

        public Config_Gem()
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
            private set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 宝石id
        /// </summary>
        private int _ItemID;
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

        /// <summary>
        /// 合成后的宝石ID
        /// </summary>
        private int _GemID;
        [EntityField("GemID")]
        public int GemID
        {
            get
            {
                return _GemID;
            }
            private set
            {
                SetChange("GemID", value);
            }
        }

        /// <summary>
        /// 需要数量
        /// </summary>
        private int _Number;
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
                    case "ItemID": return ItemID;
                    case "GemID": return GemID;
                    case "Number": return Number;
                    default: throw new ArgumentException(string.Format("Config_Gem index[{0}] isn't exist.", index));
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
                    case "GemID":
                        _GemID = value.ToInt();
                        break;
                    case "Number":
                        _Number = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_SceneMap index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}