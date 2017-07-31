using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class CombatMachEndReceipt
    {
        public int AwardCombatCoin { get; set; }

        public string AwardGold { get; set; }
    }
    /// <summary>
    /// 竞技场匹配战斗完毕
    /// </summary>
    public class Action1407 : BaseAction
    {
        private CombatMachEndReceipt receipt;
        private EventStatus result;
        public Action1407(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1407, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }
        public override bool TakeAction()
        {
            receipt = new CombatMachEndReceipt();
            int addv = 0;
            if (result == EventStatus.Good)
            {
                addv = ConfigEnvSet.GetInt("Combat.MatchWinAwardCombatCoin");
            }
            else
            {
                addv = ConfigEnvSet.GetInt("Combat.MatchFailedAwardCombatCoin");
                GetCombat.LastMatchFightFailedDate = DateTime.Now;
            }
            //GetCombat.CombatCoin = MathUtils.Addition(GetCombat.CombatCoin, addv, int.MaxValue);

            UserHelper.RewardsCombatCoin(Current.UserId, addv);
            receipt.AwardCombatCoin = addv;

            BigInteger gold = ConfigEnvSet.GetInt("Combat.MatchWinAwardGold");
            BigInteger awardValue = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * gold;
            if (result == EventStatus.Good)
            {
                receipt.AwardGold = awardValue.ToString();
                UserHelper.RewardsGold(Current.UserId, awardValue, UpdateCoinOperate.NormalReward, true);
            }
            else
            {
                awardValue /= 4;
                receipt.AwardGold = awardValue.ToString();
                UserHelper.RewardsGold(Current.UserId, awardValue, UpdateCoinOperate.NormalReward, true);
            }

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.CombatMatch, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.CombatMatch, "1");

           
            return true;
        }
    }
}