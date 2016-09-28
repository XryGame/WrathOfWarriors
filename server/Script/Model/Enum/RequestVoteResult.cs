

namespace GameServer.Script.Model.Enum
{
    public enum RequestVoteResult
    {
        /// <summary>
        /// 票数不足
        /// </summary>
        NoVote = 1,

        /// <summary>
        /// 购买选票钻石不足
        /// </summary>
        NoDiamond,

        /// <summary>
        /// 今天已经不能购买更多选票了
        /// </summary>
        NoBuy,

        /// <summary>
        /// 竞选数据过期
        /// </summary>
        Overdue,
       
        /// <summary>
        /// OK
        /// </summary>
        OK
    }
}