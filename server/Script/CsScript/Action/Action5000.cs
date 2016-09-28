using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 5000_请求更换每日任务
    /// </summary>
    public class Action5000 : BaseAction
    {
        private JPRefreshDailyQuestData receipt;
       
        public Action5000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action5000, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPRefreshDailyQuestData();
            receipt.Result = RequestRefreshDailyQuestResult.OK;

            if (ContextUser.DailyQuestData.FinishCount >= 3)
            {
                receipt.Result = RequestRefreshDailyQuestResult.NoTimes;
                return true;
            }
            int needdiamond = ConfigEnvSet.GetInt("User.RefreshDailyQuestNeedDiamond");
            if (ContextUser.DiamondNum < needdiamond)
            {
                receipt.Result = RequestRefreshDailyQuestResult.NoDiamond;
                return true;
            }
            if (ContextUser.DailyQuestData.IsFinish)
            {
                return true;
            }

            // 刷新
            ContextUser.NextDailyQuest();
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needdiamond);

            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.DailyQuestData.ID = ContextUser.DailyQuestData.ID;
            receipt.DailyQuestData.IsFinish = ContextUser.DailyQuestData.IsFinish;
            receipt.DailyQuestData.RefreshCount = ContextUser.DailyQuestData.RefreshCount;
            receipt.DailyQuestData.FinishCount = ContextUser.DailyQuestData.FinishCount;
            receipt.DailyQuestData.Count = ContextUser.DailyQuestData.Count;
            return true;
        }
    }
}