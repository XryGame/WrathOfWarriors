

namespace GameServer.Script.Model.Enum
{
    public enum TaskType
    {
        No = 0,
        /// <summary>
        /// 学习至少45分钟
        /// </summary>
        Study = 1,
        /// <summary>
        /// 劳动至少20分钟
        /// </summary>
        Exercise,
        /// <summary>
        /// 挑战老师
        /// </summary>
        FightTeacher,
        /// <summary>
        /// 道具宝箱开启1次
        /// </summary>
        RandItem,
        /// <summary>
        /// 技能宝箱开启1次
        /// </summary>
        RandSkillBook,
        /// <summary>
        /// 学霸榜中挑战同学1次
        /// </summary>
        CombatFight,
        /// <summary>
        /// 邀请同学实时对战1次
        /// </summary>
        InviteFight,
        /// <summary>
        /// 竞选系统中投票1次
        /// </summary>
        Vote,
        /// <summary>
        /// 购买1次时间
        /// </summary>
        BuyTime,
        /// <summary>
        /// 赠送好友体力1次
        /// </summary>
        GiveAwayFriend,
    }
}