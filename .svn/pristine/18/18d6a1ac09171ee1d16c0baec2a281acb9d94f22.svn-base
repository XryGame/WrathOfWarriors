
using GameServer.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;

namespace GameServer.CsScript.GM
{
    public class PayMoneyCommand : TryXGMCommand
    {
        public static readonly PayMoneyCommand instance = new PayMoneyCommand();

        public override void ProcessCmd()
        {
            int num = Args.Length > 2 ? Args[2].Trim().ToInt() : 0;
            if (num > 0)
            {
                PayMoney(UserId, num);
            }
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userId"></param>
        private void PayMoney(int userId, int money)
        {
            GameUser user = UserHelper.FindUser(userId);
            UserPayCache userpay = UserHelper.FindUserPay(userId);
            
            if (userpay == null || user == null)
                return;

            userpay.PayMoney += money;
        }
    }
}