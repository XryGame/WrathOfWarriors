
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 争霸赛报名信息
    /// </summary>
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class CompetitionApply : ShareEntity
    {

        public CompetitionApply()
            : base(AccessLevel.ReadWrite)
        {

        }
        
        /// <summary>
        /// 报名用户ID
        /// </summary>
        private int _UserId;
        [ProtoMember(1)]
        [EntityField("UserId", IsKey = true)]
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
        /// 报名用户昵称
        /// </summary>
        private string _NickName;
        [ProtoMember(2)]
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
        /// 报名时间
        /// </summary>
        private DateTime _ApplyDate;
        [ProtoMember(3)]
        [EntityField("ApplyDate")]
        public DateTime ApplyDate
        {
            get
            {
                return _ApplyDate;
            }
            set
            {
                SetChange("ApplyDate", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserId": return UserId;
                    case "NickName": return NickName;
                    case "ApplyDate": return ApplyDate;
                    default: throw new ArgumentException(string.Format("CompetitionApply index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserId":
                        _UserId = value.ToInt();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "ApplyDate":
                        _ApplyDate = value.ToDateTime();
                        break;
                    default: throw new ArgumentException(string.Format("CompetitionApply index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

    }
}