
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 职称缓存
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class JobTitleDataCache : ShareEntity
    {

        public JobTitleDataCache()
            : base(AccessLevel.ReadWrite)
        {
            CampaignUserList = new CacheList<CampaignUserData>();
        }
        /// <summary>
        /// 职称ID
        /// </summary>
        private JobTitleType _TypeId;
        [ProtoMember(1)]
        [EntityField("TypeId", IsKey = true)]
        public JobTitleType TypeId
        {
            get
            {
                return _TypeId;
            }
            set
            {
                SetChange("TypeId", value);
            }
        }
        /// <summary>
        /// 职称
        /// </summary>
        private string _Title;
        [ProtoMember(2)]
        [EntityField("Title")]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                SetChange("Title", value);
            }
        }
        /// <summary>
        /// 获得该职称的用户id
        /// </summary>
        private CampaignStatus _Status;
        [ProtoMember(3)]
        [EntityField("Status")]
        public CampaignStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                SetChange("Status", value);
            }
        }
        /// <summary>
        /// 获得该职称的用户id
        /// </summary>
        private int _UserId;
        [ProtoMember(4)]
        [EntityField("UserId")]
        public int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                SetChange("UserId", value);
            }
        }


        /// <summary>
        /// 获得该职称的用户昵称
        /// </summary>
        private string _NickName;
        [ProtoMember(5)]
        [EntityField("NickName")]
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }

        /// <summary>
        /// 获得该职称时的用户所在班级
        /// </summary>
        private int _ClassId;
        [ProtoMember(6)]
        [EntityField("ClassId")]
        public int ClassId
        {
            get
            {
                return _ClassId;
            }
            set
            {
                SetChange("ClassId", value);
            }
        }

        /// <summary>
        /// 竞选人数据
        /// </summary>
        private CacheList<CampaignUserData> _CampaignUserList;
        [ProtoMember(7)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<CampaignUserData> CampaignUserList
        {
            get
            {
                return _CampaignUserList;
            }
            set
            {
                SetChange("CampaignUserList", value);
            }
        }

        /// <summary>
        /// 获得该职称用户LooksId
        /// </summary>
        private int _LooksId;
        [ProtoMember(8)]
        [EntityField("LooksId")]
        public int LooksId
        {
            get
            {
                return _LooksId;
            }
            set
            {
                SetChange("LooksId", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "TypeId": return TypeId;
                    case "Title": return Title;
                    case "Status": return Status;
                    case "UserId": return UserId;
                    case "NickName": return NickName;
                    case "ClassId": return ClassId;
                    case "CampaignUserList": return CampaignUserList;
                    case "LooksId": return LooksId;
                    default: throw new ArgumentException(string.Format("JobTitleDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "TypeId":
                        _TypeId = value.ToEnum<JobTitleType>();
                        break;
                    case "Title":
                        _Title = value.ToNotNullString();
                        break;
                    case "Status":
                        _Status = value.ToEnum<CampaignStatus>();
                        break;
                    case "UserId":
                        _UserId = value.ToInt();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "ClassId":
                        _ClassId = value.ToInt();
                        break;
                    case "CampaignUserList":
                        _CampaignUserList = ConvertCustomField<CacheList<CampaignUserData>>(value, index);
                        break;
                    case "LooksId":
                        _LooksId = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("JobTitleDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

    }
}