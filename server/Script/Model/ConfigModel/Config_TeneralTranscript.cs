
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_TeneralTranscript : ShareEntity
    {

        public Config_TeneralTranscript()
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
            private set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 关卡名字
        /// </summary>
        private string _Name;
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

        /// <summary>
        /// 限制时间
        /// </summary>
        private int _limitTime;
        [EntityField("limitTime")]
        public int limitTime
        {
            get
            {
                return _limitTime;
            }
            private set
            {
                SetChange("limitTime", value);
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
                    case "limitTime": return limitTime;
                    default: throw new ArgumentException(string.Format("Config_TeneralTranscript index[{0}] isn't exist.", index));
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
                    case "Name":
                        _Name = value.ToNotNullString(); 
                        break;
                    case "limitTime":
                        _limitTime = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_RoleGrade index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}