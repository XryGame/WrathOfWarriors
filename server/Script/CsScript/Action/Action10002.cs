using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10002_请求删除邮件
    /// </summary>
    public class Action10002 : BaseAction
    {
        private string mailid;
        private bool isall;
        private JPRequestDeleteMailData receipt;

        public Action10002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10002, actionGetter)
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
            receipt = new JPRequestDeleteMailData();
            receipt.IsAll = isall;
            receipt.Result = EventStatus.Good;
            if (isall)
            {
                foreach (var m in ContextUser.MailBox)
                {
                    if (m.ApppendDiamond != 0 || m.AppendItem.Count > 0)
                    {
                        receipt.MailBox.Add(m);
                    }
                }
                if (receipt.MailBox.Count != ContextUser.MailBox.Count)
                    receipt.Result = EventStatus.Bad;

                ContextUser.MailBox = receipt.MailBox;

            }
            else
            {
                MailData mail = ContextUser.findMail(mailid);
                if (mail == null)
                    return false;

                if (mail.ApppendDiamond != 0 || mail.AppendItem.Count > 0)
                {
                    receipt.Result = EventStatus.Bad;
                }

                receipt.MailBox = ContextUser.MailBox;
            }


            return true;
        }
    }
}