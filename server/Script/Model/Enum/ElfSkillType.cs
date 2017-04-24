

namespace GameServer.Script.Model.Enum
{
    public enum ElfSkillType
    {
        /// <summary>
        /// 在线金币收益增加（包括各种渠道获得的金币）
        /// </summary>
        OnlineGold = 1,
        /// <summary>
        /// 离线挂机收益增加
        /// </summary>
        OffineGold,
        /// <summary>
        /// BOSS挑战时间增加
        /// </summary>
        BossTime,
        /// <summary>
        /// 怒气伤害值额外增加
        /// </summary>
        XpCD,
        /// <summary>
        /// 攻击力额外增加
        /// </summary>
        Attack,
        /// <summary>
        /// 暴击与暴击伤害提高
        /// </summary>
        Crit,
    }
}