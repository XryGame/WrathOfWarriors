
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
    public class Config_Share : ShareEntity
    {

        
        public Config_Share()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 唯一id
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
        /// 类型
        /// </summary>
        private ShareType _Type;
        [EntityField("Type")]
        public ShareType Type
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
        /// 数量
        /// </summary>
        private int _Number;
        [EntityField("Number")]
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                SetChange("Number", value);
            }
        }

        /// <summary>
        /// 奖励1类型
        /// </summary>
        private TaskAwardType _RewardType;
        [EntityField("RewardType")]
        public TaskAwardType RewardType
        {
            get
            {
                return _RewardType;
            }
            set
            {
                SetChange("RewardType", value);
            }
        }


        /// <summary>
        /// 奖励1数量
        /// </summary>
        private string _RewardNum;
        [EntityField("RewardNum")]
        public string RewardNum
        {
            get
            {
                return _RewardNum;
            }
            set
            {
                SetChange("RewardNum", value);
            }
        }

        /// <summary>
        /// 奖励2道具ID
        /// </summary>
        private int _AddRewardItem;
        [EntityField("AddRewardItem")]
        public int AddRewardItem
        {
            get
            {
                return _AddRewardItem;
            }
            set
            {
                SetChange("AddRewardItem", value);
            }
        }

        /// <summary>
        /// 奖励2数量
        /// </summary>
        private string _AddRewardNum;
        [EntityField("AddRewardNum")]
        public string AddRewardNum
        {
            get
            {
                return _AddRewardNum;
            }
            set
            {
                SetChange("AddRewardNum", value);
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
                    case "Number": return Number;
                    case "RewardType": return RewardType;
                    case "RewardNum": return RewardNum;
                    case "AddRewardItem": return AddRewardItem;
                    case "AddRewardNum": return AddRewardNum;
                    default: throw new ArgumentException(string.Format("Config_Share index[{0}] isn't exist.", index));
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
                        _Type = value.ToEnum<ShareType>();
                        break;
                    case "Number":
                        _Number = value.ToInt();
                        break;
                    case "RewardType":
                        _RewardType = value.ToEnum<TaskAwardType>();
                        break;
                    case "RewardNum":
                        _RewardNum = value.ToNotNullString("0");
                        break;
                    case "AddRewardItem":
                        _AddRewardItem = value.ToInt();
                        break;
                    case "AddRewardNum":
                        _AddRewardNum = value.ToNotNullString("0");
                        break;
                    default: throw new ArgumentException(string.Format("Config_Share index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
               
	}
}