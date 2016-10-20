
using GameServer.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;

namespace GameServer.CsScript.GM
{
    public class PayMonthCardCommand : TryXGMCommand
    {
        public static readonly PayMonthCardCommand instance = new PayMonthCardCommand();

        public override void ProcessCmd()
        {
            PayMonthCard(UserId);
        }

        /// <summary>
        /// 添加月卡
        /// </summary>
        /// <param name="userId"></param>
        private void PayMonthCard(int userId)
        {
            GameUser user = UserHelper.FindUser(userId);
            UserPayCache userpay = UserHelper.FindUserPay(userId);
            
            if (userpay == null || user == null)
                return;

            userpay.MonthCardDays += 29;
            userpay.MonthCardAwardDate = DateTime.Now;
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "恭喜您成功充值月卡",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("月卡有效期间，每天都会发送给您 {0} 钻石奖励哦！ ", 
                            ConfigEnvSet.GetInt("System.MonthCardDiamond")),
            };

            user.AddNewMail(ref mail);

            mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "月卡奖励",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("这是今天您的月卡奖励，您的月卡剩余时间还有 {0} 天！", userpay.WeekCardDays),
                ApppendDiamond = ConfigEnvSet.GetInt("System.MonthCardDiamond")
            };

            user.AddNewMail(ref mail);
        }
    }
}