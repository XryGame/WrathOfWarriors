
//using GameServer.CsScript.Com;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using GameServer.Script.Model.Enum.Enum;
//using System;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Common;

//namespace GameServer.CsScript.GM
//{
//    public class PayMoneyCommand : TryXGMCommand
//    {
//        public static readonly PayMoneyCommand instance = new PayMoneyCommand();

//        public override void ProcessCmd()
//        {
//            int num = Args.Length > 2 ? Args[2].Trim().ToInt() : 0;
//            if (num > 0)
//            {
//                PayMoney(UserId, num);
//            }
//        }

//        /// <summary>
//        /// 充值
//        /// </summary>
//        /// <param name="userId"></param>
//        public bool PayMoney(int userId, int money)
//        {
//            UserBasisCache user = UserHelper.FindUserBasis(userId);
//            UserPayCache userpay = UserHelper.FindUserPay(userId);
            
//            if (userpay == null || user == null)
//                return false;

//            userpay.PayMoney += money;
//            user.VipLv = userpay.ConvertPayVipLevel();

//            // 这里刷新排行榜数据
//            var combat = UserHelper.FindRankUser(userId, RankType.Combat);
//            combat.VipLv = user.VipLv;
//            var level = UserHelper.FindRankUser(userId, RankType.Level);
//            level.VipLv = user.VipLv;
//            var fightvaluer = UserHelper.FindRankUser(userId, RankType.FightValue);
//            fightvaluer.VipLv = user.VipLv;

//            return true;
//        }
//    }
//}