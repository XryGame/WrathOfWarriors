

namespace GameServer.Script.Model.Enum
{
    public enum RequestGuildResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Successfully,
        /// <summary>
        /// 公会人数已满
        /// </summary>
        Full,
        /// <summary>
        /// 已请求
        /// </summary>
        HadRequest,
        /// <summary>
        /// 没有该公会
        /// </summary>
        NoGuild,
        /// <summary>
        /// 公会名称重复
        /// </summary>
        HadName,
        /// <summary>
        /// 钻石不足
        /// </summary>
        NoDiamond,
        /// <summary>
        /// 没有权限
        /// </summary>
        NoAuthority,
        /// <summary>
        /// 对方已有公会
        /// </summary>
        HadGuild,
        /// <summary>
        /// 已经签过到了
        /// </summary>
        HadSignIn,
    }
}