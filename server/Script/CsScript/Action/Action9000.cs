using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 9000_请求签到
    /// </summary>
    public class Action9000 : BaseAction
    {
        private bool receipt;
        private Random random = new Random();
        private bool isrepair; // 是否补签
        public Action9000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action9000, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetBool("IsRepair", ref isrepair))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            if (GetEventAward.IsTodaySign && !isrepair)
            {
                return true;
            }
            if (!GetEventAward.IsTodaySign && isrepair)
            {
                return true;
            }
            if (isrepair && GetBasis.DiamondNum < DataHelper.RepairSignNeedDiamond)
            {
                return true;
            }

            var choose = new ShareCacheStruct<Config_Signin>().FindAll();
            if (choose.Count == 0)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "Sign");
                new BaseLog().SaveLog("Choose DB Config_Signin Error.");
                return true;
            }

            if (GetEventAward.SignCount >= choose.Count)
            {
                return true;
            }

            var signsurface = new ShareCacheStruct<Config_Signin>().Find(t => (
                    t.ID == DataHelper.SignStartID + GetEventAward.SignCount
            ));
            if (signsurface == null)
            {
                return true;
            }

            GetEventAward.IsTodaySign = true;
            GetEventAward.SignCount++;

            if (isrepair)
            {
                UserHelper.ConsumeDiamond(Current.UserId, DataHelper.RepairSignNeedDiamond);
            }

            
            
            switch (signsurface.AwardType)
            {
                case TaskAwardType.Gold:
                    {
                        UserHelper.RewardsGold(Current.UserId, signsurface.AwardNum);
                    }
                    break;
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, signsurface.AwardNum.ToInt());
                    }
                    break;
                case TaskAwardType.Item:
                    {
                        UserHelper.RewardsItem(Current.UserId, signsurface.AwardID, signsurface.AwardNum.ToInt());
                    }
                    break;
            }

            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.SignIn, 1);

            receipt = true;
            return true;
        }
    }
}