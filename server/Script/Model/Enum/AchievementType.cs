

namespace GameServer.Script.Model.Enum
{
    public enum AchievementType
    {
        /// <summary>
        /// 等级达到
        /// </summary>
        LevelCount = 1,
        /// <summary>
        /// 学习时间达到
        /// </summary>
        StudyTime,
        /// <summary>
        /// 劳动时间达到
        /// </summary>
        ExerciseTime,
        /// <summary>
        /// 累计邀请同学对战数量
        /// </summary>
        InviteFightCount,
        /// <summary>
        /// 累积挑战且胜利
        /// </summary>
        ChallengeCount,
        /// <summary>
        /// 在道具商店中累积开宝箱
        /// </summary>
        OpenItemBoxCount,
        /// <summary>
        /// 在技能商店中累积开宝箱
        /// </summary>
        OpenSkillBoxCount,
        /// <summary>
        /// 在投票系统中投票数量
        /// </summary>
        VoitCount,
        /// <summary>
        /// 至少当选1次/竞选
        /// </summary>
        CompaignsCount,
        /// <summary>
        /// 在游戏中获得钻石数量
        /// </summary>
        AwardDiamondCount,
    }
}