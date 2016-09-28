
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.LogModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(CacheType.None, DbConfig.Log, "UserChatLog")]
    public class UserChatLog : BaseEntity
    {
        #region auto-generated static method
        static UserChatLog()
        {
            EntitySchemaSet.InitSchema(typeof(UserChatLog));
        }
        #endregion

        public UserChatLog()
            : base(AccessLevel.WriteOnly)
        {
        }

        #region auto-generated Property
        private string _ChatID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ChatID", IsKey = true)]
        public string ChatID
        {
            private get
            {
                return _ChatID;
            }
            set
            {
                SetChange("ChatID", value);
            }
        }
        private string _FromUserID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("FromUserID")]
        public string FromUserID
        {
            private get
            {
                return _FromUserID;
            }
            set
            {
                SetChange("FromUserID", value);
            }
        }
        private string _FromUserName;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("FromUserName")]
        public string FromUserName
        {
            private get
            {
                return _FromUserName;
            }
            set
            {
                SetChange("FromUserName", value);
            }
        }
        private string _ToUserID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ToUserID")]
        public string ToUserID
        {
            private get
            {
                return _ToUserID;
            }
            set
            {
                SetChange("ToUserID", value);
            }
        }
        private string _ToUserName;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ToUserName")]
        public string ToUserName
        {
            private get
            {
                return _ToUserName;
            }
            set
            {
                SetChange("ToUserName", value);
            }
        }
        private string _Content;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("Content")]
        public string Content
        {
            private get
            {
                return _Content;
            }
            set
            {
                SetChange("Content", value);
            }
        }
        private ChatType _ChatType;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ChatType")]
        public ChatType ChatType
        {
            private get
            {
                return _ChatType;
            }
            set
            {
                SetChange("ChatType", value);
            }
        }
        private DateTime _SendDate;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("SendDate")]
        public DateTime SendDate
        {
            private get
            {
                return _SendDate;
            }
            set
            {
                SetChange("SendDate", value);
            }
        }
        private int _ClassID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("ClassID")]
        public int ClassID
        {
            private get
            {
                return _ClassID;
            }
            set
            {
                SetChange("ClassID", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "ChatID": return ChatID;
                    case "FromUserID": return FromUserID;
                    case "FromUserName": return FromUserName;
                    case "ToUserID": return ToUserID;
                    case "ToUserName": return ToUserName;
                    case "Content": return Content;
                    case "ChatType": return ChatType;
                    case "SendDate": return SendDate;
                    case "ClassID": return ClassID;
                    default: throw new ArgumentException(string.Format("UserChatLog index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "ChatID":
                        _ChatID = value.ToNotNullString();
                        break;
                    case "FromUserID":
                        _FromUserID = value.ToNotNullString();
                        break;
                    case "FromUserName":
                        _FromUserName = value.ToNotNullString();
                        break;
                    case "ToUserID":
                        _ToUserID = value.ToNotNullString();
                        break;
                    case "ToUserName":
                        _ToUserName = value.ToNotNullString();
                        break;
                    case "Content":
                        _Content = value.ToNotNullString();
                        break;
                    case "ChatType":
                        _ChatType = value.ToEnum<ChatType>();
                        break;
                    case "SendDate":
                        _SendDate = value.ToDateTime();
                        break;
                    case "ClassID":
                        _ClassID = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserChatLog index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        #endregion

        protected override int GetIdentityId()
        {
            //allow modify return value
            return FromUserID.ToInt();
        }
    }
}