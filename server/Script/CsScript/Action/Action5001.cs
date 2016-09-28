﻿using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 5001_领取每日任务奖励
    /// </summary>
    public class Action5001 : BaseAction
    {
        private JPReceiveDailyQuestData receipt;
        private Random random = new Random();

        public Action5001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action5001, actionGetter)
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
            receipt = new JPReceiveDailyQuestData();
            receipt.Result = EventStatus.Good;

            if (ContextUser.DailyQuestData.ID == TaskType.No || !ContextUser.DailyQuestData.IsFinish
                || ContextUser.DailyQuestData.FinishCount >= 3)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            var taskconfig = new ShareCacheStruct<Config_Task>().FindKey(ContextUser.DailyQuestData.ID);
            if (taskconfig == null)
            {
                new BaseLog().SaveLog(string.Format("查找每日任务ID出错 ID={0}", ContextUser.DailyQuestData.ID));
                return true;
            }

            ContextUser.DailyQuestData.FinishCount++;
            ContextUser.NextDailyQuest();

            receipt.New.ID = ContextUser.DailyQuestData.ID;
            receipt.New.IsFinish = ContextUser.DailyQuestData.IsFinish;
            receipt.New.RefreshCount = ContextUser.DailyQuestData.RefreshCount;
            receipt.New.FinishCount = ContextUser.DailyQuestData.FinishCount;
            receipt.New.Count = ContextUser.DailyQuestData.Count;

            receipt.AwardType = taskconfig.RewardsType;
            receipt.AwardValue = taskconfig.RewardsNum;
            switch (taskconfig.RewardsType)
            {
                case TaskAwardType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, taskconfig.RewardsNum);
                        receipt.CurrDiamond = ContextUser.DiamondNum;
                    }
                    break;
                case TaskAwardType.StudyExp:
                    {
                        int addvalue = ContextUser.AdditionBaseExtExpValue(taskconfig.RewardsNum);
                        receipt.AwardValue = addvalue;
                    }
                    break;
                case TaskAwardType.FightExp:
                    {
                        int addvalue = ContextUser.AdditionFightExpValue(taskconfig.RewardsNum);
                        receipt.AwardValue = addvalue;
                    }
                    break;
                case TaskAwardType.RandItem:
                    {
                        receipt.RandGoods = ContextUser.RandItem(taskconfig.RewardsNum);
                    }
                    break;
                case TaskAwardType.RandSkillBook:
                    {
                        receipt.RandGoods = ContextUser.RandSkillBook(taskconfig.RewardsNum);
                    }
                    break;
            }
            receipt.CurrExtendExp = ContextUser.ExpData.Ext;
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;
            receipt.CurrFightValue = ContextUser.FightingValue;
            return true;
        }
    }
}