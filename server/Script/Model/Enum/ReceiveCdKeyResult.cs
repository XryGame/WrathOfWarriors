

namespace GameServer.Script.Model.Enum
{
    public enum ReceiveCdKeyResult
    {
        /// <summary>
        /// Ok
        /// </summary>
        OK,
        /// <summary>
        /// 无效的CdKey
        /// </summary>
        Invalid,
        /// <summary>
        /// 这个CdKey已经兑换过了
        /// </summary>
        Received,
        /// <summary>
        /// 您已经兑换过了
        /// </summary>
        Had,
    }
}