
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 邮件数据
    /// </summary>
    [Serializable, ProtoContract]
    public class MailData : EntityChangeEvent
    {

        public MailData()
            : base(false)
        {
            AppendItem = new CacheList<ItemData>();
            ApppendCoinType = CoinType.Gold;
            ApppendCoinNum = "0";
        }
        
        /// <summary>
        /// 邮件guid
        /// </summary>
        [ProtoMember(1)]
        public string ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [ProtoMember(2)]
        public string Title { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        [ProtoMember(3)]
        public string Sender { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [ProtoMember(4)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [ProtoMember(5)]
        public string Context { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [ProtoMember(6)]
        public bool IsRead { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        private CacheList<ItemData> _AppendItem;
        [ProtoMember(7)]
        public CacheList<ItemData> AppendItem
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

        /// <summary>
        /// 附加货币类型
        /// </summary>
        [ProtoMember(8)]
        public CoinType ApppendCoinType { get; set; }

        /// <summary>
        /// 附加货币数量
        /// </summary>
        [ProtoMember(9)]
        public string ApppendCoinNum { get; set; }

    }
}