

namespace GameServer.Script.Model.Enum
{
    public enum UpdateGoldType
    {
        /// <summary>
        /// 消耗
        /// </summary>
        Consume = 1,
        /// <summary>
        /// 普通奖励
        /// </summary>
        NormalReward,
        /// <summary>
        /// 战斗杀怪掉落
        /// </summary>
        KillMonsterReward,
        /// <summary>
        /// 离线收益
        /// </summary>
        OffineReward,
        /// <summary>
        /// 背包使用奖励
        /// </summary>
        UserItemReward,
    }
}