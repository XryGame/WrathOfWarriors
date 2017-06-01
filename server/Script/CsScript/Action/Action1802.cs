using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class RequestDeleteMailData
    {
        public RequestDeleteMailData()
        {
            MailList = new CacheList<MailData>();
        }

        public bool IsAll { get; set; }

        public EventStatus Result { get; set; }

        public CacheList<MailData> MailList { get; set; }
    }
    /// <summary>
    /// 请求删除邮件
    /// </summary>
    public class Action1802 : BaseAction
    {
        private string mailid;
        private bool isall;
        private RequestDeleteMailData receipt;

        public Action1802(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1802, actionGetter)
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
            receipt = new RequestDeleteMailData();
            receipt.IsAll = isall;
            receipt.Result = EventStatus.Good;
            if (isall)
            {
                foreach (var m in GetMailBox.MailList)
                {

                    if (string.IsNullOrEmpty(m.ApppendCoinNum))
                    {
                        m.ApppendCoinNum = "0";
                    }
                    BigInteger bigint = BigInteger.Parse(m.ApppendCoinNum);
                    if (bigint != 0 || m.AppendItem.Count > 0)
                    {
                        MailData tm = new MailData()
                        {
                            ID = m.ID,
                            Title = m.Title,
                            Sender = m.Sender,
                            Date = m.Date,
                            Context = m.Context,
                            IsRead = m.IsRead,
                            AppendItem = m.AppendItem,
                            ApppendCoinType = m.ApppendCoinType,
                            ApppendCoinNum = m.ApppendCoinNum,
                        };
                        receipt.MailList.Add(tm);
                    }
                }
                if (receipt.MailList.Count > 0)
                    receipt.Result = EventStatus.Bad;

                GetMailBox.MailList = receipt.MailList;

            }
            else
            {
                MailData mail = GetMailBox.findMail(mailid);
                if (mail == null)
                    return false;
                if (string.IsNullOrEmpty(mail.ApppendCoinNum))
                {
                    mail.ApppendCoinNum = "0";
                }
                BigInteger bigint = BigInteger.Parse(mail.ApppendCoinNum);
                if (bigint != 0 || mail.AppendItem.Count > 0)
                {
                    receipt.Result = EventStatus.Bad;
                }
                else
                {
                    GetMailBox.MailList.Remove(mail);
                }

                receipt.MailList = GetMailBox.MailList;
            }
            

            return true;
        }
    }
}