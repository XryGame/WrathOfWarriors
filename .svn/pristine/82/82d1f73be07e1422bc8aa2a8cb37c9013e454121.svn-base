using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4003_购买挑战班长资格
    /// </summary>
    public class Action4003 : BaseAction
    {
        private JPBuyData receipt;

        public Action4003(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4003, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action4001;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            int needdiamond = ConfigEnvSet.GetInt("User.BuyChallengeClassMonitorNeedDiamond");
            if (ContextUser.ChallengeMonitorTimes == 0 || ContextUser.DiamondNum < needdiamond)
            {
                receipt.Result = EventStatus.Bad;
                receipt.CurrDiamond = ContextUser.DiamondNum;
                return true;
            }

            ContextUser.ChallengeMonitorTimes = 0;
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needdiamond);
            receipt.CurrDiamond = ContextUser.DiamondNum;
            return true;
        }
    }
}