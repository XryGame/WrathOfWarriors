/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using ZyGames.Framework.Game.Service;


namespace GameServer.Script.Model.LogModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(CacheType.None, DbConfig.Log, "OldUserLog")]
    public class OldUserLog : BaseEntity
    {
        #region auto-generated static method
        static OldUserLog()
        {
            EntitySchemaSet.InitSchema(typeof(OldUserLog));
        }
        #endregion

        public OldUserLog()
            : base(AccessLevel.WriteOnly)
        {
        }

        #region auto-generated Property
        private int _UserID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("UserID", IsKey = true, IsIdentity = true)]
        public int UserID
        {
            private get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }
        private string _OpenID;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("OpenID")]
        public string OpenID
        {
            private get
            {
                return _OpenID;
            }
            set
            {
                SetChange("OpenID", value);
            }
        }
        private string _NickName;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("NickName")]
        public string NickName
        {
            private get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }
        private string _AvatarUrl;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("AvatarUrl")]
        public string AvatarUrl
        {
            private get
            {
                return _AvatarUrl;
            }
            set
            {
                SetChange("AvatarUrl", value);
            }
        }
        private DateTime _CreateDate;
        /// <summary>
        /// 
        /// </summary>
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            private get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "OpenID": return OpenID;
                    case "NickName": return NickName;
                    case "AvatarUrl": return AvatarUrl;
                    case "CreateDate": return CreateDate;
                    default: throw new ArgumentException(string.Format("OldUserRecord index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserID":
                        _UserID = value.ToInt();
                        break;
                    case "OpenID":
                        _OpenID = value.ToNotNullString();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "AvatarUrl":
                        _AvatarUrl = value.ToNotNullString();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    default: throw new ArgumentException(string.Format("OldUserRecord index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        #endregion

        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }
    }
}