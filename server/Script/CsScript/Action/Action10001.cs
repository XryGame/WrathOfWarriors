using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10001_请求领取邮件附件
    /// </summary>
    public class Action10001 : BaseAction
    {
        private string mailid;
        private bool isall;
        private JPRequestSFOData receipt;

        public Action10001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10001, actionGetter)
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
                foreach (var m in ContextUser.MailBox)
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
                MailData mail = ContextUser.findMail(list[i]);
                if (mail == null)
                    continue;
                
                foreach (var item in mail.AppendItem)
                {
                    if (receipt == null)
                    {
                        receipt = new JPRequestSFOData();
                    }
                    var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(item.ID);
                    if (itemcfg == null)
                        continue;

                    ContextUser.UserAddItem(item.ID, item.Num);

                    if (itemcfg.Type == ItemType.Skill)
                    {
                        ContextUser.CheckAddSkillBook(item.ID, item.Num);
                    }
                    receipt.AwardItemList.Add(item.ID);
                }
                mail.AppendItem.Clear();

                if (mail.ApppendDiamond > 0)
                {
                    if (receipt == null)
                    {
                        receipt = new JPRequestSFOData();
                    }
                    UserHelper.GiveAwayDiamond(ContextUser.UserID, mail.ApppendDiamond);

                    receipt.AwardDiamondNum = mail.ApppendDiamond;
                    mail.ApppendDiamond = 0;
                }
            }
 

            if (receipt!= null)
            {
                receipt.CurrDiamond = ContextUser.DiamondNum;
                receipt.ItemList = ContextUser.ItemDataList;
                receipt.SkillList = ContextUser.SkillDataList;
            }
            return true;
        }
    }
}