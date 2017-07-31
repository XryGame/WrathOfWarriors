using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 领取重复充值奖励 
    /// </summary>
    public class Action1960 : BaseAction
    {
        private bool receipt;

        public Action1960(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1960, actionGetter)
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
            receipt = false;
            if (GetPay.PayMoney / 200 <= GetPay.ReceivedRepeatPayCount)
            {
                return true;
            }

            GetPay.ReceivedRepeatPayCount++;

            List<ItemData> items = new List<ItemData>();
            items.Add(new ItemData() { ID = 10003, Num = 2 });
            items.Add(new ItemData() { ID = 20004, Num = 1 });
            items.Add(new ItemData() { ID = 23004, Num = 1 });
            items.Add(new ItemData() { ID = 24004, Num = 1 });
            UserHelper.RewardsItems(Current.UserId, items);
            receipt = true;
            return true;
        }
    }
}