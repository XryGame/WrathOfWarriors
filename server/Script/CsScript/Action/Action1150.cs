using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1150_领取每日任务奖励
    /// </summary>
    public class Action1150 : BaseAction
    {
        private bool receipt;
        private TaskType id;
        private Random random = new Random();

        public Action1150(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1150, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("ID", ref id))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserDailyQuestData dailyQuest = GetTask.FindTask(id);
            if (dailyQuest.Status != TaskStatus.Finished)
            {
                return false;
            }
            var taskcfg = new ShareCacheStruct<Config_Task>().FindKey(id);
            if (taskcfg == null)
            {
                new BaseLog().SaveLog(string.Format("查找每日任务ID出错 ID={0}", id));
                return false;
            }

            dailyQuest.Status = TaskStatus.Received;
            GetTask.Liveness += taskcfg.Liveness;

            switch (taskcfg.RewardsType)
            {
                case TaskAwardType.Gold:
                    {
                        BigInteger bi = Util.ConvertGameCoin(taskcfg.RewardsNum);
                        UserHelper.RewardsGold(Current.UserId, bi);
                    }
                    break;
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, Convert.ToInt32(taskcfg.RewardsNum), UpdateDiamondType.Other);
                    }
                    break;
                case TaskAwardType.Item:
                    {

                    }
                    break;
            }

            receipt = true;
            return true;
            
        }
    }
}