

namespace GameServer.Script.Model.Enum
{
    public enum UsedItemResult
    {
        /// <summary>
        /// 无法使用的物品
        /// </summary>
        Cannot = 1,
        /// <summary>
        /// 无此物品
        /// </summary>
        NoItem,
        /// <summary>
        /// 物品数量出错
        /// </summary>
        ItemNumError,
        /// <summary>
        /// 成功
        /// </summary>
        Successfully,
    }
}