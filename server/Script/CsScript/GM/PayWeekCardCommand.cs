﻿
//using GameServer.CsScript.Com;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using System;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Common;

//namespace GameServer.CsScript.GM
//{
//    public class PayWeekCardCommand : TryXGMCommand
//    {
//        public static readonly PayWeekCardCommand instance = new PayWeekCardCommand();

//        public override void ProcessCmd()
//        {
//            PayWeekCard(UserId);
//        }

//        /// <summary>
//        /// 添加周卡
//        /// </summary>
//        /// <param name="userId"></param>
//        public bool PayWeekCard(int userId)
//        {
//            UserPayCache userpay = UserHelper.FindUserPay(userId);
            

//            bool isAward = false;
//            if (userpay.WeekCardDays < 0)
//            {
//                isAward = true;
//                userpay.WeekCardDays = 6;
//            }
//            else
//            {
//                userpay.WeekCardDays += 7;
//            }
//            userpay.WeekCardAwardDate = DateTime.Now;
//            MailData mail = new MailData()
//            {
//                ID = Guid.NewGuid().ToString(),
//                Title = "恭喜您成功充值周卡",
//                Sender = "系统",
//                Date = DateTime.Now,
//                Context = string.Format("周卡有效期间，每天都会发送给您 {0} 钻石奖励哦！ ", 
//                            ConfigEnvSet.GetInt("System.WeekCardDiamond")),
//            };

//            UserHelper.AddNewMail(userId, mail);

//            if (isAward)
//            {
//                mail = new MailData()
//                {
//                    ID = Guid.NewGuid().ToString(),
//                    Title = "周卡奖励",
//                    Sender = "系统",
//                    Date = DateTime.Now,
//                    Context = string.Format("这是今天您的周卡奖励，您的周卡剩余时间还有 {0} 天！", userpay.WeekCardDays),
//                    ApppendDiamond = ConfigEnvSet.GetInt("System.WeekCardDiamond")
//                };
//                UserHelper.AddNewMail(userId, mail);
//            }

//            return true;
            
//        }
//    }
//}