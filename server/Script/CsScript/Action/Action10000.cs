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
    /// 10000_请求读取邮件
    /// </summary>
    public class Action10000 : BaseAction
    {
        private string mailid;

        public Action10000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10000, actionGetter)
        {
            IsNotRespond = true;
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("MailId", ref mailid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            MailData mail = ContextUser.findMail(mailid);
            if (mail == null)
                return false;

            if (!mail.IsRead)
                mail.IsRead = true;


            return true;
        }
    }
}