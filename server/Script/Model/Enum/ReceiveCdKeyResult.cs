

namespace GameServer.Script.Model.Enum
{
    public enum ReceiveCdKeyResult
    {
        /// <summary>
        /// 无效的CdKey
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// 这个CdKey已经兑换过了
        /// </summary>
        Received,
        /// <summary>
        /// Ok
        /// </summary>
        OK,
    }
}