using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求领取邮件附件
    /// </summary>
    public class Action1801 : BaseAction
    {
        private string mailid;
        private bool isall;
        private bool receipt;

        public Action1801(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1801, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("MailId", ref mailid)
                && httpGet.GetBool("IsAll", ref isall))
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
            List<string> list = new List<string>();
            if (isall)
            {
                foreach (var m in GetMailBox.MailList)
                {
                    list.Add(m.ID);
                }
            }
            else
            {
                list.Add(mailid);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                MailData mail = GetMailBox.findMail(list[i]);
                if (mail == null)
                    continue;

                UserHelper.RewardsItems(Current.UserId, mail.AppendItem.ToList());
                mail.AppendItem.Clear();
                if (!mail.ApppendCoinNum.IsEmpty())
                {
                    BigInteger bigint = BigInteger.Parse(mail.ApppendCoinNum);
                    if (bigint > 0)
                    {
                        switch (mail.ApppendCoinType)
                        {
                            case CoinType.Gold:
                                UserHelper.RewardsGold(Current.UserId, bigint);
                                break;
                            case CoinType.Diamond:
                                UserHelper.RewardsDiamond(Current.UserId, mail.ApppendCoinNum.ToInt());
                                break;
                                //case CoinType.CombatCoin:
                                //    UserHelper.RewardsDiamond(Current.UserId, mail.ApppendDiamond, UpdateDiamondType.Other);
                                //    break;
                                //case CoinType.GuildCoin:
                                //    UserHelper.RewardsDiamond(Current.UserId, mail.ApppendDiamond, UpdateDiamondType.Other);
                                //    break;
                        }

                        mail.ApppendCoinNum = "0";
                    }
                }
  
            }

            receipt = true;
            return true;
        }
    }
}