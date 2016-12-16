using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4015_请求参加竞选
    /// </summary>
    public class Action4011 : BaseAction
    {
        private JPRequestCampaignsData receipt;
        private JobTitleType index;

        public Action4011(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4011, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action4011;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Index", ref index))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestCampaignsData();
            receipt.Result = RequestCampaignResult.OK;
            // 检查资格
            if (ContextUser.ClassData.ClassID == 0)
            {
                receipt.Result = RequestCampaignResult.NoClass;
                return true;
            }
            ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(ContextUser.ClassData.ClassID);
            if (classdata == null || classdata.Monitor != ContextUser.UserID)
            {
                receipt.Result = RequestCampaignResult.NotClassMonitor;
                return true;
            }
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            var fdnow = jobcache.FindKey(index);
            if (fdnow == null)
                return false;

            if (fdnow.Status == CampaignStatus.NotStarted)
            {
                receipt.Result = RequestCampaignResult.NoStart;
                return true;
            }
            if (fdnow.Status == CampaignStatus.Over)
            {
                receipt.Result = RequestCampaignResult.Over;
                return true;
            }
  
            if (fdnow.CampaignUserList.Find(t => (t.ClassId == ContextUser.ClassData.ClassID)) != null)
            {
                receipt.Result = RequestCampaignResult.HadTakePartIn;
                return true;
            }

            int needdiamond = ConfigEnvSet.GetInt("User.CampaignNeedDiamond");
            if (ContextUser.DiamondNum < needdiamond)
            {
                receipt.Result = RequestCampaignResult.NoDiamond;
                return true;
            }

            //// 检查此玩家有竞选过其他职位并当选的情况，如有就去掉之前的职位
            //for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
            //{
            //    var fd = jobcache.FindKey(i);
            //    if (fd != null && fd.Status != CampaignStatus.Runing)
            //    {
            //        if (fd.UserId == ContextUser.UserID)
            //        {
            //            fd.UserId = 0;
            //            fd.NickName = "";
            //            fd.LooksId = 0;
            //            break;
            //        }
            //    }
            //}

            // 到这里就表示成功了
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needdiamond);
            CampaignUserData campaignuserdata = new CampaignUserData()
            {
                UserId = ContextUser.UserID,
                NickName = ContextUser.NickName,
                ClassId = ContextUser.ClassData.ClassID,
                VoteCount = 0,
                LooksId = ContextUser.LooksId
            };
            fdnow.CampaignUserList.Add(campaignuserdata);
            receipt.AddCampaignsUserData.UserId = campaignuserdata.UserId;
            receipt.AddCampaignsUserData.NickName = campaignuserdata.NickName;
            receipt.AddCampaignsUserData.LooksId = campaignuserdata.LooksId;
            var userclassdata = new ShareCacheStruct<ClassDataCache>().FindKey(campaignuserdata.ClassId);
            if (userclassdata != null)
                receipt.AddCampaignsUserData.ClassName = userclassdata.Name;
            receipt.AddCampaignsUserData.VoteCount = campaignuserdata.VoteCount;
            return true;
        }
    }
}