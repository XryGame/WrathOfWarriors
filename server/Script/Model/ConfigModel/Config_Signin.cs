﻿
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Signin : ShareEntity
    {

        
        public Config_Signin()
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
        /// 奖励类型
        /// </summary>
        private TaskAwardType _AwardType;
        [EntityField("AwardType")]
        public TaskAwardType AwardType
        {
            get
            {
                return _AwardType;
            }
            set
            {
                SetChange("AwardType", value);
            }
        }

        /// <summary>
        /// 奖励ID
        /// </summary>
        private int _AwardID;
        [EntityField("AwardID")]
        public int AwardID
        {
            get
            {
                return _AwardID;
            }
            set
            {
                SetChange("AwardID", value);
            }
        }

        /// <summary>
        /// 奖励数量
        /// </summary>
        private string _AwardNum;
        [EntityField("AwardNum")]
        public string AwardNum
        {
            get
            {
                return _AwardNum;
            }
            set
            {
                SetChange("AwardNum", value);
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
                    case "AwardType": return AwardType;
                    case "AwardID": return AwardID;
                    case "AwardNum": return AwardNum;
                    default: throw new ArgumentException(string.Format("Config_Signin index[{0}] isn't exist.", index));
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
                    case "AwardType":
                        _AwardType = value.ToEnum<TaskAwardType>(); 
                        break;
                    case "AwardID":
                        _AwardID = value.ToInt();
                        break;
                    case "AwardNum":
                        _AwardNum = value.ToNotNullString("0");
                        break;
                    default: throw new ArgumentException(string.Format("Config_Signin index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}