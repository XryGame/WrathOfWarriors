using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4012_请求投票
    /// </summary>
    public class Action4012 : BaseAction
    {
        private JPRequestVoteData receipt;
        private JobTitleType index;
        private int destid;
        private int votecount;
        private bool isbuy;

        public Action4012(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4012, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Index", ref index)
                && httpGet.GetInt("DestId", ref destid)
                && httpGet.GetInt("Count", ref votecount)
                && httpGet.GetBool("IsBuy", ref isbuy))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestVoteData();
            receipt.Result = RequestVoteResult.OK;
            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            var fdnow = jobcache.FindKey(index);
            if (fdnow == null)
                return false;

            if (fdnow.Status != CampaignStatus.Runing)
            {
                receipt.Result = RequestVoteResult.Overdue;
                return true;
            }

            var campaignuserdata = fdnow.CampaignUserList.Find(t => (t.UserId == destid));
            if (campaignuserdata == null)
            {
                receipt.Result = RequestVoteResult.Overdue;
                return true;
            }
            
            if (ContextUser.CampaignTicketNum < votecount)
            {
                if (isbuy)
                {
                    int buynum = votecount - ContextUser.CampaignTicketNum;
                    if (ContextUser.DiamondNum < buynum)
                    {
                        receipt.Result = RequestVoteResult.NoDiamond;
                        return true;
                    }
                    else if (ContextUser.BuyCampaignTicketNum >= ConfigEnvSet.GetInt("User.BuyCampaignTicketNumMax"))
                    {
                        receipt.Result = RequestVoteResult.NoBuy;
                        return true;
                    }
                    else
                    {// 购买选票
                        ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, buynum);
                        ContextUser.BuyCampaignTicketNum = MathUtils.Addition(ContextUser.BuyCampaignTicketNum, buynum);
                        ContextUser.CampaignTicketNum = MathUtils.Addition(ContextUser.CampaignTicketNum, buynum);
                    }
                }
                else
                {
                    receipt.Result = RequestVoteResult.NoVote;
                    return true;
                }
            }

            // 到这里就表示成功了
            ContextUser.CampaignTicketNum = MathUtils.Subtraction(ContextUser.CampaignTicketNum, votecount, 0);
            campaignuserdata.VoteCount = MathUtils.Addition(campaignuserdata.VoteCount, votecount);

            receipt.DestUid = destid;
            receipt.CurrVoteNum = ContextUser.CampaignTicketNum;
            receipt.CampaignsUserVoteNum = campaignuserdata.VoteCount;
            receipt.CurrCanBuyVoteNum = MathUtils.Subtraction(
                ConfigEnvSet.GetInt("User.BuyCampaignTicketNumMax"), ContextUser.BuyCampaignTicketNum, 0
                );
            receipt.CurrDiamond = ContextUser.DiamondNum;



            // 每日
            if (ContextUser.DailyQuestData.ID == TaskType.Vote)
            {
                ContextUser.DailyQuestData.IsFinish = true;
                PushMessageHelper.DailyQuestFinishNotification(Current);
            }

            // 成就
            UserHelper.AchievementProcess(ContextUser.UserID, votecount, AchievementType.VoitCount);
            return true;
        }
    }
}