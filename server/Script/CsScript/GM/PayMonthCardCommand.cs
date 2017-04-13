
//using GameServer.CsScript.Com;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using System;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Common;

//namespace GameServer.CsScript.GM
//{
//    public class PayMonthCardCommand : TryXGMCommand
//    {
//        public static readonly PayMonthCardCommand instance = new PayMonthCardCommand();

//        public override void ProcessCmd()
//        {
//            PayMonthCard(UserId);
//        }

//        /// <summary>
//        /// 添加月卡
//        /// </summary>
//        /// <param name="userId"></param>
//        public bool PayMonthCard(int userId)
//        {
//            UserPayCache userpay = UserHelper.FindUserPay(userId);
            
//            bool isAward = false;
//            if (userpay.MonthCardDays < 0)
//            {
//                isAward = true;
//                userpay.MonthCardDays = 29;
//            }
//            else
//            {
//                userpay.MonthCardDays += 30;
//            }

//            userpay.MonthCardAwardDate = DateTime.Now;
//            MailData mail = new MailData()
//            {
//                ID = Guid.NewGuid().ToString(),
//                Title = "恭喜您成功充值月卡",
//                Sender = "系统",
//                Date = DateTime.Now,
//                Context = string.Format("月卡有效期间，每天都会发送给您 {0} 钻石奖励哦！ ", 
//                            ConfigEnvSet.GetInt("System.MonthCardDiamond")),
//            };

//            UserHelper.AddNewMail(userId, mail);

//            if (isAward)
//            {
//                mail = new MailData()
//                {
//                    ID = Guid.NewGuid().ToString(),
//                    Title = "月卡奖励",
//                    Sender = "系统",
//                    Date = DateTime.Now,
//                    Context = string.Format("这是今天您的月卡奖励，您的月卡剩余时间还有 {0} 天！", userpay.MonthCardDays),
//                    ApppendDiamond = ConfigEnvSet.GetInt("System.MonthCardDiamond")
//                };

//                UserHelper.AddNewMail(userId, mail);
//            }
//            return true;
//        }
//    }
//}