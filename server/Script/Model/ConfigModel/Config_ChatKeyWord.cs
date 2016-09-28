
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class ChatKeyWord : ShareEntity
    {

        
        public ChatKeyWord()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private string _KeyID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("KeyID", IsKey = true)]
        public string KeyID
        {
            get
            {
                return _KeyID;
            }
            set
            {
                SetChange("KeyID", value);
            }
        }
        private string _KeyWord;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("KeyWord")]
        public string KeyWord
        {
            get
            {
                return _KeyWord;
            }
            set
            {
                SetChange("KeyWord", value);
            }
        }
    
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "KeyID": return KeyID;
                    case "KeyWord": return KeyWord;
					default: throw new ArgumentException(string.Format("ChatKeyWord index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "KeyID": 
                        _KeyID = value.ToNotNullString(); 
                        break; 
                    case "KeyWord": 
                        _KeyWord = value.ToNotNullString(); 
                        break; 
					default: throw new ArgumentException(string.Format("ChatKeyWord index[{0}] isn't exist.", index));
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