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
    /// 购买抽奖次数
    /// </summary>
    public class Action1812 : BaseAction
    {
        private bool receipt;

        public Action1812(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1812, actionGetter)
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

            //var vip = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipLv);
            //if (vip == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "Config_Vip");
            //    return true;
            //}

            //int canBuyTimes = vip.BuyAthletics;
            
            //if (GetCombat.BuyTimes >= canBuyTimes)
            //{
            //    return true;
            //}
            int needDiamond = ConfigEnvSet.GetInt("User.BuyLotteryTimesNeedDiamond");
            
            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }
            
            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            GetBasis.LotteryTimes = MathUtils.Addition(GetBasis.LotteryTimes, 1);

            receipt = true;
            return true;
        }
    }
}