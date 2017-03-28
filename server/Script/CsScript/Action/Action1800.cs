using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求读取邮件
    /// </summary>
    public class Action1800 : BaseAction
    {
        private string mailid;

        public Action1800(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1800, actionGetter)
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
            MailData mail = GetMailBox.findMail(mailid);
            if (mail == null)
                return false;

            if (!mail.IsRead)
                mail.IsRead = true;


            return true;
        }
    }
}