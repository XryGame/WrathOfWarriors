
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 用户劳动任务数据
    /// </summary>
    [Serializable, ProtoContract]
    public class UserExerciseTaskData : EntityChangeEvent
    {
 
        public UserExerciseTaskData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 科目ID(为0表示没有科目任务)
        /// </summary>
        private int _SubjectID;
        [ProtoMember(1)]
        public int SubjectID
        {
            get
            {
                return _SubjectID;
            }
            set
            {
                _SubjectID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _StartTime;
        [ProtoMember(2)]
        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }
        /// <summary>
        /// 次数
        /// </summary>
        private int _Count;
        [ProtoMember(3)]
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }
        /// <summary>
        /// 场景ID
        /// </summary>
        private SceneType _SceneId;
        [ProtoMember(4)]
        public SceneType SceneId
        {
            get
            {
                return _SceneId;
            }
            set
            {
                _SceneId = value;
            }
        }
    }
}