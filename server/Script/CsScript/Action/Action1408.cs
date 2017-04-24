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
    /// 竞技场购买匹配次数
    /// </summary>
    public class Action1408 : BaseAction
    {
        private bool receipt;

        public Action1408(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1408, actionGetter)
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
            
            if (GetCombat.BuyMatchTimes >= canBuyTimes)
            {
                return true;
            }
            int needDiamond = ConfigEnvSet.GetInt("User.BuyCombatMatchTimesNeedDiamond");
            
            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }
            
            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            GetCombat.MatchTimes = MathUtils.Addition(GetCombat.MatchTimes, 1);
            GetCombat.BuyMatchTimes++;

            receipt = true;
            return true;
        }
    }
}