using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4010_请求竞选列表
    /// </summary>
    public class Action4010 : BaseAction
    {
        private JPRequestCampaignsListData receipt;

        public Action4010(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4010, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action4001;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestCampaignsListData();
            receipt.CurrVoteNum = ContextUser.CampaignTicketNum;
            receipt.CurrCanBuyVoteNum = MathUtils.Subtraction(
                ConfigEnvSet.GetInt("User.BuyCampaignTicketNumMax"), ContextUser.BuyCampaignTicketNum, 0
                );
            receipt.CurrDiamond = ContextUser.DiamondNum;
            //JPCampaignsData[] data = new JPCampaignsData[7];
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            var classcache = new ShareCacheStruct<ClassDataCache>();
            for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
            {
                var fd = jobcache.FindKey(i);
                if (fd != null)
                {
                    JPCampaignsData data = new JPCampaignsData();
                    data = new JPCampaignsData();
                    data.JobTitleTypeId = fd.TypeId;
                    data.Status = fd.Status;
    
                    data.UserId = fd.UserId;
                    data.NickName = fd.NickName;
                    data.LooksId = fd.LooksId;
                    ClassDataCache classdata = classcache.FindKey(fd.ClassId);
                    if (classdata != null)
                    {
                        data.ClassName = classdata.Name;
                    }

                    if (fd.Status == CampaignStatus.Runing)
                    {
                        foreach (var userdata in fd.CampaignUserList)
                        {
                            JPCampaignsUserData jpuserdata = new JPCampaignsUserData();
                            jpuserdata.UserId = userdata.UserId;
                            jpuserdata.NickName = userdata.NickName;
                            var userclassdata = classcache.FindKey(userdata.ClassId);
                            if (userclassdata != null)
                                jpuserdata.ClassName = userclassdata.Name;
                            jpuserdata.VoteCount = userdata.VoteCount;
                            jpuserdata.LooksId = userdata.LooksId;
                            data.CampaignsUserDataList.Add(jpuserdata);
                        }
                    }
                    receipt.CampaignsList.Add(data);
                }
            }

            return true;
        }
    }
}