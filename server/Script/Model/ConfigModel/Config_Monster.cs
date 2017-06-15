
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
        /// 等级
        /// </summary>
        private int _Grade;
        [EntityField("Grade", IsKey = true)]
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
                
	}
}