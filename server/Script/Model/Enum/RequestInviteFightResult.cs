

namespace GameServer.Script.Model.Enum
{
    public enum RequestInviteFightResult
    {
        /// <summary>
        /// 邀请目标不在线
        /// </summary>
        Offine = 1,

        /// <summary>
        /// 邀请目标在战斗中
        /// </summary>
        Fighting,

        /// <summary>
        /// 邀请目标已有邀请
        /// </summary>
        HadInvite,

        /// <summary>
        /// OK
        /// </summary>
        OK
    }
}