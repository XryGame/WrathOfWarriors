using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 7001_领取成就奖励
    /// </summary>
    public class Action7001 : BaseAction
    {
        private JPReceiveAchievementData receipt;
        private int destid;
        private Random random = new Random();

        public Action7001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action7001, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action7001;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestId", ref destid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPReceiveAchievementData();
            receipt.Result = EventStatus.Good;
            var achievement = ContextUser.AchievementList.Find(t => (t.ID == destid));
            var config = new ShareCacheStruct<Config_Achievement>().FindKey(destid);
            if (achievement == null || config == null
                || achievement.ID == 0 || !achievement.IsFinish || achievement.IsReceive)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            receipt.AwardNum = config.RewardsNum;
            switch (config.RewardsType)
            {
                case AwardType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, config.RewardsNum);
                    }
                    break;
                case AwardType.ItemSkillBook:
                    {
                        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(config.RewardsID);
                        if (item != null)
                        {
                            ContextUser.UserAddItem(config.RewardsID, config.RewardsNum);

                            if (item.Type == ItemType.Skill)
                            {
                                ItemData itemdata = ContextUser.ItemDataList.Find(t => (t.ID == config.RewardsID));
                                if (itemdata != null)
                                {
                                    ContextUser.UserAddSkill(config.RewardsID, config.RewardsNum);
                                }
                            }
                            receipt.AwardItemList = new List<int>();
                            receipt.AwardItemList.Add(config.RewardsID);
                        }
                    }
                    break;
                case AwardType.RandItemSkillBook:
                    {
                        if (random.Next(1000) < 500)
                        {// 道具
                            receipt.AwardItemList = ContextUser.RandItem(config.RewardsNum);
                        }
                        else
                        {// 技能
                            receipt.AwardItemList = ContextUser.RandSkillBook(config.RewardsNum);
                        }
                    }
                    break;
            }

            // 新成就
            var stock = new ShareCacheStruct<Config_Achievement>().FindAll(t => (t.AchievementType == achievement.Type));
            var select = stock.Find(t => (t.id > achievement.ID));
            if (select != null)
            {
                achievement.ID = select.id;
                achievement.IsFinish = false;
                achievement.IsReceive = false;
            }
            else
            {
                achievement.IsReceive = true;
            }
            
            achievement.Count = 0;
            if (achievement.Type == AchievementType.LevelCount)
                achievement.Count = ContextUser.UserLv;

            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;
            receipt.NewAchievement = achievement;
            return true;
        }

    }
}