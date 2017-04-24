

namespace GameServer.Script.Model.Enum
{
    public enum UsedItemResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Successfully,
        /// <summary>
        /// 无法使用的物品
        /// </summary>
        Cannot,
        /// <summary>
        /// 无此物品
        /// </summary>
        NoItem,
        /// <summary>
        /// 物品数量出错
        /// </summary>
        ItemNumError,
        /// <summary>
        /// 不可出售的物品
        /// </summary>
        Unavailable,
    }
}