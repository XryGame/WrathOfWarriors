using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
    /// 10800_一键拉票
    /// </summary>
    public class Action10800 : BaseAction
    {
        private RequestCanvassResult receipt;
        //private Random random = new Random();
        //private int receiveId;
        public Action10800(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10800, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = RequestCanvassResult.OK;
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            //var fdnow = jobcache.FindKey((JobTitleType)DateTime.Now.DayOfWeek);
            var fdnow = jobcache.Find(t => (t.Status == CampaignStatus.Runing));
            if (fdnow == null)
            {
                return false;
            }
            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.CanvassDate);
            float sec = timeSpan.TotalSeconds.ToFloat();
            if (sec < ConfigEnvSet.GetInt("User.CanvassIntervalTime"))
            {
                receipt = RequestCanvassResult.CD;
                return true;
            }
            if (fdnow.Status == CampaignStatus.Over)
            {
                return false;
            }
            var cud = fdnow.CampaignUserList.Find(t => (t.UserId == ContextUser.UserID));
            if (cud == null)
            {
                receipt = RequestCanvassResult.NoQualification;
                return true;
            }
            
            var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(cud.ClassId);
            if (classdata != null)
            {
                string context = string.Format("{0}{1} 正在参加 {2} 的竞选，支持一下吧！", classdata.Name, ContextUser.NickName, fdnow.Title);
                var chatService = new TryXChatService();
                chatService.SystemRedundantSend(context, ContextUser.UserID, ChatChildType.Canvass);
                PushMessageHelper.SendSystemChatToOnlineUser();
            }
            ContextUser.CanvassDate = DateTime.Now;

            return true;
        }
    }
}