﻿
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 购买金币
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Purchase : ShareEntity
    {

        
        public Config_Purchase()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 次数id
        /// </summary>
        private int _id;
        [EntityField("id", IsKey = true)]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                SetChange("id", value);
            }
        }

        /// <summary>
        /// 金币
        /// </summary>
        private string _Gold;
        [EntityField("Gold")]
        public string Gold
        {
            get
            {
                return _Gold;
            }
            set
            {
                SetChange("Gold", value);
            }
        }

        /// <summary>
        /// 消耗钻石
        /// </summary>
        private int _SpendDiamond;
        [EntityField("SpendDiamond")]
        public int SpendDiamond
        {
            get
            {
                return _SpendDiamond;
            }
            set
            {
                SetChange("SpendDiamond", value);
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
                    case "Gold": return Gold;
                    case "SpendDiamond": return SpendDiamond;
                    default: throw new ArgumentException(string.Format("Config_Purchase index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToInt(); 
                        break; 
                    case "Gold":
                        _Gold = value.ToNotNullString("0"); 
                        break;
                    case "SpendDiamond":
                        _SpendDiamond = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Purchase index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
	}
}