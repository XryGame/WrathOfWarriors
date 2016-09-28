
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 用户班级信息
    /// </summary>
    [Serializable, ProtoContract]
    public class UserClassData : EntityChangeEvent
    {

        public UserClassData()
            : base(false)
        {
        }
      
        /// <summary>
        /// 所在班级
        /// </summary>
        private int _ClassID;
        [ProtoMember(1)]
        public int ClassID
        {
            get
            {
                return _ClassID;
            }
            set
            {
                _ClassID = value;
            }
        }
  

    }
}