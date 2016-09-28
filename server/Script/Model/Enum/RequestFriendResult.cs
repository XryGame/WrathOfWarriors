

namespace GameServer.Script.Model.Enum
{
    public enum RequestFriendResult
    {
        /// <summary>
        /// 好友人数已满
        /// </summary>
        FriendNumFull = 1,

        /// <summary>
        /// 对方已经是你的好友
        /// </summary>
        HadFriend,

        /// <summary>
        /// 已经申请添加对方为好友
        /// </summary>
        HadApply,

        /// <summary>
        /// 对方好友人数已满
        /// </summary>
        DestFriendNumFull,

        /// <summary>
        /// 无此申请/该申请已处理
        /// </summary>
        NoApply,

        /// <summary>
        /// OK
        /// </summary>
        OK
    }
}