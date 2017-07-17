
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 仇人
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserEnemysCache : BaseEntity
    {

        public UserEnemysCache()
            : base(AccessLevel.ReadWrite)
        {
            EnemyList = new CacheList<EnemyData>();
            LogList = new CacheList<EnemyLogData>();
            //ResetCache();
        }
        
        private int _UserID;
        [ProtoMember(1)]
        [EntityField("UserID", IsKey = true)]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }


        /// <summary>
        ///  List
        /// </summary>
        private CacheList<EnemyData> _EnemyList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<EnemyData> EnemyList
        {
            get
            {
                return _EnemyList;
            }
            set
            {
                SetChange("EnemyList", value);
            }
        }

        /// <summary>
        /// 受攻击记录
        /// </summary>
        private CacheList<EnemyLogData> _LogList;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<EnemyLogData> LogList
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
        /// 是否有新的
        /// </summary>
        private bool _IsHaveNewLog;
        [ProtoMember(4)]
        [EntityField("IsHaveNewLog")]
        public bool IsHaveNewLog
        {
            get
            {
                return _IsHaveNewLog;
            }
            set
            {
                SetChange("IsHaveNewLog", value);
            }
        }


        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "EnemyList": return EnemyList;
                    case "LogList": return LogList;
                    case "IsHaveNewLog": return IsHaveNewLog;
                    default: throw new ArgumentException(string.Format("UserEnemysCache index[{0}] isn't exist.", index));
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
                    case "EnemyList":
                        _EnemyList = ConvertCustomField<CacheList<EnemyData>>(value, index);
                        break;
                    case "LogList":
                        _LogList = ConvertCustomField<CacheList<EnemyLogData>>(value, index);
                        break;
                    case "IsHaveNewLog":
                        _IsHaveNewLog = value.ToBool();
                        break;
                    default: throw new ArgumentException(string.Format("UserEnemysCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }
        
        /// <summary>
        /// 是否有此敌人
        /// </summary>
        /// <returns></returns>
        public bool IsHaveEnemy(int uid)
        {
            return EnemyList.Find(t => (t.UserId == uid)) != null;
        }


        /// <summary>
        /// 查找敌人
        /// </summary>
        /// <returns></returns>
        public EnemyData FindEnemy(int uid)
        {
            return EnemyList.Find(t => (t.UserId == uid));

        }

        /// <summary>
        /// 添加敌人
        /// </summary>
        /// <returns></returns>
        public void AddEnemy(EnemyData enemy)
        {
            if (enemy == null)
                return;
            var findv = EnemyList.Find(t => (t.UserId == enemy.UserId));
            if (findv == null)
            {
                if (EnemyList.Count >= DataHelper.CombatLogCountMax)
                {
                    EnemyList.RemoveAt(0);
                }
                EnemyList.Add(enemy);
            }

        }


        /// <summary>
        /// 删除敌人
        /// </summary>
        /// <returns></returns>
        public void RemoveEnemy(int uid)
        {
            var enemy = EnemyList.Find(t => (t.UserId == uid));
            if (enemy != null)
            {
                EnemyList.Remove(enemy);
            }
            
        }

        public void PushLog(EnemyLogData log)
        {
            if (LogList.Count >= DataHelper.MaxMailNum)
            {
                LogList.RemoveAt(0);
            }
            IsHaveNewLog = true;
            LogList.Add(log);
        }


        public void ResetCache()
        {
            EnemyList.Clear();
            LogList.Clear();
        }
    }
}