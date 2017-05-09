using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 商城购买道具
    /// </summary>
    public class Action1840 : BaseAction
    {
        private BuyItemResult receipt;
        private int _id;
        private int _num;
        public Action1840(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1840, actionGetter)
        {

        }


        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref _id)
                && httpGet.GetInt("Num", ref _num))
            {
                return true;
            }
            return false;
        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            var shopcfg = new ShareCacheStruct<Config_Shop>().FindKey(_id);
            if (shopcfg == null)
                return false;


            float discount = shopcfg.Discount / 10.0f;
            int needCoin = Convert.ToInt32(shopcfg.Price * discount) * _num;
            if (shopcfg.CurrencyType == CoinType.Diamond)
            {
                if (GetBasis.DiamondNum < needCoin)
                {
                    receipt = BuyItemResult.NoDiamond;
                    return true;
                }
                else
                {
                    UserHelper.ConsumeDiamond(Current.UserId, needCoin);
                }
                
            }
            else if (shopcfg.CurrencyType == CoinType.GuildCoin)
            {
                if (GetGuild.GuildCoin < needCoin)
                {
                    receipt = BuyItemResult.NoGuildGold;
                    return true;
                }
                else
                {
                    GetGuild.GuildCoin = MathUtils.Subtraction(GetGuild.GuildCoin, needCoin, 0);
                    UserHelper.ConsumeGuildCoin(Current.UserId, needCoin);
                }
            }

            UserHelper.RewardsItem(Current.UserId, shopcfg.ItemID, _num);

            receipt = BuyItemResult.Successfully;
            return true;
        }

    }
}