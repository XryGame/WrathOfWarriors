
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 发送赠送物品数据
    /// </summary>
    [Serializable, ProtoContract]
    public class SendTransferItemData : EntityChangeEvent
    {

        public SendTransferItemData()
            : base(false)
        {
            AppendItem = new ItemData();
        }
        
        /// <summary>
        /// Guid
        /// </summary>
        [ProtoMember(1)]
        public string ID { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [ProtoMember(2)]
        public int Receiver { get; set; }
        /// <summary>
        /// 接收人昵称
        /// </summary>
        [ProtoMember(3)]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 接收人职业
        /// </summary>
        [ProtoMember(4)]
        public int ReceiverProfession { get; set; }

        /// <summary>
        /// 接收人头像
        /// </summary>
        [ProtoMember(5)]
        public string ReceiverAvatar { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [ProtoMember(6)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 是否已领取
        /// </summary>
        [ProtoMember(7)]
        public bool IsReceived { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [ProtoMember(8)]
        public string Password { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [ProtoMember(9)]

        private ItemData _AppendItem;
        public ItemData AppendItem
        {
            get
            {
                return _AppendItem;
            }
            set
            {
                _AppendItem = value;
            }
        }


    }
}