using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1405_购买竞技场挑战次数
    /// </summary>
    public class Action1405 : BaseAction
    {
        private bool receipt;

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

            var vip = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipLv);
            if (vip == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "Config_Vip");
                return true;
            }

            int canBuyTimes = vip.BuyAthletics;
            if (GetBasis.VipLv == 0)
                canBuyTimes -= 1;
            
            if (GetCombat.BuyTimes >= canBuyTimes)
            {
                return true;
            }
            int needDiamond = ConfigEnvSet.GetInt("User.BuyCombatTimesNeedDiamond");
            
            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }
            
            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            GetCombat.CombatTimes = MathUtils.Addition(GetCombat.CombatTimes, 1);
            GetCombat.BuyTimes++;

            receipt = true;
            return true;
        }
    }
}