
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
    public class Config_BotsChat : ShareEntity
    {

        public Config_BotsChat()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private int _ID;
        /// <summary>
        /// id
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
        private string _Word;
        /// <summary>
        /// 内容
        /// </summary>
        [EntityField("Word")]
        public string Word
        {
            get
            {
                return _Word;
            }
            private set
            {
                SetChange("Word", value);
            }
        }
        private string _Reply;
        /// <summary>
        /// 回复
        /// </summary>
        [EntityField("Reply")]
        public string Reply
        {
            get
            {
                return _Reply;
            }
            private set
            {
                SetChange("Reply", value);
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
                    case "Word": return Word;
                    case "Reply": return Reply;
                    default: throw new ArgumentException(string.Format("Config_BotsChat index[{0}] isn't exist.", index));
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
                    case "Word":
                        _Word = value.ToNotNullString(); 
                        break;
                    case "Reply":
                        _Reply = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_BotsChat index[{0}] isn't exist.", index));
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