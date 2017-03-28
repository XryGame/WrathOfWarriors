
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 公会信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class GuildsCache : ShareEntity
    {

        public GuildsCache()
            : base(AccessLevel.ReadWrite)
        {
            MemberList = new CacheList<GuildCharacter>();
            ApplyList = new CacheList<GuildCharacter>();
            LogList = new CacheList<GuildLogData>();
        }
        
        /// <summary>
        /// 公会ID
        /// </summary>
        private string _GuildID;
        [ProtoMember(1)]
        [EntityField("GuildID", IsKey = true)]
        public string GuildID
        {
            get
            {
                return _GuildID;
            }
            set
            {
                SetChange("GuildID", value);
            }
        }


        /// <summary>
        /// 公会名称
        /// </summary>
        private string _GuildName;
        [ProtoMember(2)]
        [EntityField("GuildName")]
        public string GuildName
        {
            get
            {
                return _GuildName;
            }
            set
            {
                SetChange("GuildName", value);
            }
        }

        /// <summary>
        ///  活跃度
        /// </summary>
        private int _Liveness;
        [ProtoMember(3)]
        [EntityField("Liveness")]
        public int Liveness
        {
            get
            {
                return _Liveness;
            }
            set
            {
                SetChange("Liveness", value);
            }
        }


        /// <summary>
        /// 公会公告
        /// </summary>
        private string _Notice;
        [ProtoMember(4)]
        [EntityField("Notice")]
        public string Notice
        {
            get
            {
                return _Notice;
            }
            set
            {
                SetChange("Notice", value);
            }
        }

        /// <summary>
        /// 公会排名
        /// </summary>
        private int _RankID;
        [ProtoMember(5)]
        [EntityField("RankID")]
        public int RankID
        {
            get
            {
                return _RankID;
            }
            set
            {
                SetChange("RankID", value);
            }
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _CreateDate;
        [ProtoMember(6)]
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        /// <summary>
        /// 成员列表
        /// </summary>
        private CacheList<GuildCharacter> _MemberList;
        [ProtoMember(7)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<GuildCharacter> MemberList
        {
            get
            {
                return _MemberList;
            }
            set
            {
                SetChange("MemberList", value);
            }
        }

        /// <summary>
        /// 加入公会请求列表
        /// </summary>
        private CacheList<GuildCharacter> _ApplyList;
        [ProtoMember(8)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<GuildCharacter> ApplyList
        {
            get
            {
                return _ApplyList;
            }
            set
            {
                SetChange("ApplyList", value);
            }
        }

        /// <summary>
        /// 加入公会请求列表
        /// </summary>
        private CacheList<GuildLogData> _LogList;
        [ProtoMember(9)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<GuildLogData> LogList
        {
            get
            {
                return _LogList;
            }
            set
            {
                SetChange("LogList", value);
            }
        }

        /// <summary>
        /// 公会等级
        /// </summary>
        [ProtoMember(100)]
        public int Lv
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
                    case "GuildID": return GuildID;
                    case "GuildName": return GuildName;
                    case "Liveness": return Liveness;
                    case "Notice": return Notice;
                    case "RankID": return RankID;
                    case "CreateDate": return CreateDate;
                    case "MemberList": return MemberList;
                    case "ApplyList": return ApplyList;
                    case "LogList": return LogList;
                    default: throw new ArgumentException(string.Format("GuildsCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "GuildID":
                        _GuildID = value.ToNotNullString();
                        break;
                    case "GuildName":
                        _GuildName = value.ToNotNullString();
                        break;
                    case "Liveness":
                        _Liveness = value.ToInt();
                        break;
                    case "Notice":
                        _Notice = value.ToNotNullString();
                        break;
                    case "RankID":
                        _RankID = value.ToInt();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    case "MemberList":
                        _MemberList = ConvertCustomField<CacheList<GuildCharacter>>(value, index);
                        break;
                    case "ApplyList":
                        _ApplyList = ConvertCustomField<CacheList<GuildCharacter>>(value, index);
                        break;
                    case "LogList":
                        _LogList = ConvertCustomField<CacheList<GuildLogData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("GuildsCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        /// <summary>
        /// 查找会长
        /// </summary>
        /// <returns></returns>
        public GuildCharacter FindAtevent()
        {
            return MemberList.Find(t => t.JobTitle == GuildJobTitle.Atevent);
        }

        public GuildCharacter FindAtevent(int userId)
        {
            return MemberList.Find(t => (t.UserID == userId && t.JobTitle == GuildJobTitle.Atevent));
        }

        /// <summary>
        /// 查找副会长
        /// </summary>
        /// <returns></returns>
        public List<GuildCharacter> FindVice()
        {
            return MemberList.FindAll(t => t.JobTitle == GuildJobTitle.Vice);
        }

        public GuildCharacter FindVice(int userId)
        {
            return MemberList.Find(t => (t.UserID == userId && t.JobTitle == GuildJobTitle.Vice));
        }

        /// <summary>
        /// 查找成员
        /// </summary>
        /// <returns></returns>
        public GuildCharacter FindMember(int userId)
        {
            return MemberList.Find(t => t.UserID == userId);
        }


        /// <summary>
        /// 查找邀请人
        /// </summary>
        /// <returns></returns>
        public GuildCharacter FindRequest(int userId)
        {
            return ApplyList.Find(t => t.UserID == userId);
        }

        public void AddNewMember(GuildCharacter character)
        {
            MemberList.Add(character);

        }

        public void RemoveMember(GuildCharacter character)
        {
            MemberList.Remove(character);

        }

        public void AddNewRequest(GuildCharacter character)
        {
            ApplyList.Insert(0, character);
            if (ApplyList.Count > 20)
            {
                ApplyList.RemoveAt(ApplyList.Count - 1);
            }
        }

        public void RemoveRequest(int userId)
        {
            ApplyList.RemoveAll(t => t.UserID == userId);
        }

        public int ConvertLevel()
        {
            var societySet = new ShareCacheStruct<Config_Society>();
            var socitylist = societySet.FindAll(t => t.Liveness <= Liveness);

            return socitylist[socitylist.Count - 1].ID;
        }

        public void NewLog(GuildLogData log)
        {
            LogList.Add(log);
            if (LogList.Count > ConfigEnvSet.GetInt("Guild.GuildLogCountMax"))
            {
                LogList.RemoveAt(0);
            }
        }
    }
}