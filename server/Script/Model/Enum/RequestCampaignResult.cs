

namespace GameServer.Script.Model.Enum
{
    public enum RequestCampaignResult
    {
        /// <summary>
        /// 当前没有加入任何班级，不能参加竞选
        /// </summary>
        NoClass = 1,

        /// <summary>
        /// 钻石不足
        /// </summary>
        NoDiamond,

        /// <summary>
        /// 不是班长不能参加竞选
        /// </summary>
        NotClassMonitor,

        /// <summary>
        /// 此竞选已结束
        /// </summary>
        Over,

        /// <summary>
        /// 此竞选未开始
        /// </summary>
        NoStart,

        /// <summary>
        /// 班级已经参与该竞选
        /// </summary>
        HadTakePartIn,

        /// <summary>
        /// OK
        /// </summary>
        OK
    }
}