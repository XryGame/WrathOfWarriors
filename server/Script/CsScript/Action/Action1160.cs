using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1160_领取成就奖励
    /// </summary>
    public class Action1160 : BaseAction
    {
        private AchievementData receipt;
        private int id;
        private AchievementType type;
        private Random random = new Random();

        public Action1160(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1160, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1160;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref id))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var achievement = GetAchievement.AchievementList.Find(t => (t.ID == id));
            var config = new ShareCacheStruct<Config_Achievement>().FindKey(id);
            if (achievement == null || config == null
                || achievement.ID == 0 || achievement.Status != TaskStatus.Finished)
            {
                return false;
            }
            type = achievement.Type;

            switch (config.RewardsType)
            {
                case TaskAwardType.Gold:
                    {
                        BigInteger bi = Util.ConvertGameCoin(config.RewardsItemNum);
                        UserHelper.RewardsGold(Current.UserId, bi);
                    }
                    break;
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, Convert.ToInt32(config.RewardsItemNum));
                    }
                    break;
                case TaskAwardType.Item:
                    {
                        UserHelper.RewardsItem(Current.UserId, config.RewardsItemID, Convert.ToInt32(config.RewardsItemNum));
                    }
                    break;
            }


            // 新成就
            var stock = new ShareCacheStruct<Config_Achievement>().FindAll(t => (t.AchievementType == achievement.Type));
            var select = stock.Find(t => (t.id > achievement.ID));
            if (select != null)
            {
                achievement.ID = select.id;
                achievement.Status = TaskStatus.No;
                
            }
            else
            {
                achievement.Status = TaskStatus.Received;
            }

            //achievement.Count = 0;
            if (type == AchievementType.InlayGem)
                achievement.Count = "0";
            else if (type == AchievementType.OpenSoul)
                achievement.Count = "0";
           
            receipt = achievement;
            return true;
        }


        public override void TakeActionAffter(bool state)
        {
            // 成就检测
            UserHelper.AchievementProcess(GetBasis.UserID, type);

            base.TakeActionAffter(state);
        }
    }
}