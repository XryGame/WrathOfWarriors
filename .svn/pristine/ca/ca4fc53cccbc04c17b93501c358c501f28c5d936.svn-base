using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1405_购买名人榜挑战次数
    /// </summary>
    public class Action1405 : BaseAction
    {
        private JPBuyData receipt;

        public Action1405(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1405, actionGetter)
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
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            var vip = new ShareCacheStruct<Config_Vip>().FindKey(ContextUser.VipLv == 0 ? 1 : ContextUser.VipLv);
            if (vip == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "Config_Vip");
                return true;
            }

            int canBuyTimes = vip.BuyAthletics;
            if (ContextUser.VipLv == 0)
                canBuyTimes -= 1;
            
            if (ContextUser.CombatData.ButTimes >= canBuyTimes)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            int needDiamond = ConfigEnvSet.GetInt("User.BuyCombatTimesNeedDiamond");
            
            if (ContextUser.DiamondNum < needDiamond)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needDiamond);
            ContextUser.CombatData.CombatTimes = MathUtils.Addition(ContextUser.CombatData.CombatTimes, 1);
            ContextUser.CombatData.ButTimes++;
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.Extend1 = ContextUser.CombatData.CombatTimes;
            return true;
        }
    }
}