
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 接收赠送物品数据
    /// </summary>
    [Serializable, ProtoContract]
    public class ReceiveTransferItemData : EntityChangeEvent
    {

        public ReceiveTransferItemData()
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
        /// 发送人
        /// </summary>
        [ProtoMember(2)]
        public int Sender { get; set; }
        /// <summary>
        /// 发送人昵称
        /// </summary>
        [ProtoMember(3)]
        public string SenderName { get; set; }

        /// <summary>
        /// 发送人职业
        /// </summary>
        [ProtoMember(4)]
        public int SenderProfession { get; set; }

        /// <summary>
        /// 发送人头像
        /// </summary>
        [ProtoMember(5)]
        public string SenderAvatar { get; set; }

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
        /// 附件
        /// </summary>
        [ProtoMember(8)]
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