

namespace GameServer.Script.Model.Enum
{
    public enum TaskType
    {
        No = 0,
        /// <summary>
        /// 登录一次
        /// </summary>
        Login = 1,
        /// <summary>
        /// 签到一次
        /// </summary>
        SignIn,
        /// <summary>
        /// 闯关1关
        /// </summary>
        PassStage,
        /// <summary>
        /// 闯关1关（BOSS关）
        /// </summary>
        PassStageBoss,
        /// <summary>
        /// 战魂1次
        /// </summary>
        OpenSoul,
        /// <summary>
        /// 装备升级1次
        /// </summary>
        UpgradeEquip,
        /// <summary>
        /// 技能升级1次
        /// </summary>
        UpgradeSkill,
        /// <summary>
        /// 公会签到1次
        /// </summary>
        GuildSignIn,
        /// <summary>
        /// 竞技场1次
        /// </summary>
        Combat,
        /// <summary>
        /// 好友切磋1次
        /// </summary>
        FriendCompare,
        /// <summary>
        /// 购买金币1次
        /// </summary>
        BuyGold,
        /// <summary>
        /// 世界发言1次
        /// </summary>
        WorldChat,
    }
}