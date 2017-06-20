

namespace GameServer.Script.Model.Enum
{
    public enum TransferItemResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Successfully = 0,
        /// <summary>
        /// 已经领取
        /// </summary>
        Received,
        /// <summary>
        /// 已过期
        /// </summary>
        Expire,
        /// <summary>
        /// 提取码错误
        /// </summary>
        ErrorPassword,
        /// <summary>
        /// 发送次数超出
        /// </summary>
        SendCountOut,
        /// <summary>
        /// 领取次数超出
        /// </summary>
        ReceiveCountOut,
    }
}