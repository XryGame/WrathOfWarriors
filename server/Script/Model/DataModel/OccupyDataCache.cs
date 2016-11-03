
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 占领数据缓存
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class OccupyDataCache : ShareEntity
    {

        public OccupyDataCache()
            : base(AccessLevel.ReadWrite)
        {

        }
        /// <summary>
        /// 场景ID
        /// </summary>
        private SceneType _SceneId;
        [ProtoMember(1)]
        [EntityField("SceneId", IsKey = true)]
        public SceneType SceneId
        {
            get
            {
                return _SceneId;
            }
            set
            {
                SetChange("SceneId", value);
            }
        }
        /// <summary>
        /// 占领人Id
        /// </summary>
        private int _UserId;
        [ProtoMember(2)]
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
        /// 占领人昵称
        /// </summary>
        private string _NickName;
        [ProtoMember(3)]
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
        /// 挑战者id
        /// </summary>
        [ProtoMember(4)]
        public int ChallengerId
        {
            get;
            set;
        }
        /// <summary>
        /// 挑战者昵称
        /// </summary>
        [ProtoMember(5)]
        public string ChallengerNickName
        {
            get;
            set;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "SceneId": return SceneId;
                    case "UserId": return UserId;
                    case "NickName": return NickName;
                    default: throw new ArgumentException(string.Format("OccupyDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "SceneId":
                        _SceneId = value.ToEnum<SceneType>();
                        break;
                    case "UserId":
                        _UserId = value.ToInt();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("OccupyDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetOccupy()
        {
            UserId = 0;
            NickName = "";
        }
    }
}