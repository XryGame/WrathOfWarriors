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
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class RobReceipt
    {
        public bool Result { get; set; }

        public string Gold { get; set; }

        public UserLotteryCache Lottery { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Action1814 : BaseAction
    {
        private RobReceipt receipt;
        private int selectId;
        private EventStatus result;
        private Random random = new Random();
        public Action1814(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1814, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("SelectId", ref selectId)
                && httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new RobReceipt();
            receipt.Result = false;

            if (GetLottery.RobTimes <= 0)
            {
                return true;
            }
            if (GetLottery.Rob.RivalUid != selectId
                && !GetEnemys.IsHaveEnemy(selectId)
                && !GetFriends.IsHaveFriend(selectId))
            {
                return true;
            }

            var rival = UserHelper.FindUserBasis(selectId);
            if (rival == null)
            {
                return true;
            }
            var rivalEnemy = UserHelper.FindUserEnemy(rival.UserID);
            if (rivalEnemy == null)
            {
                return true;
            }

            var lotterycfg = new ShareCacheStruct<Config_Lottery>().Find(t => (t.Type == LotteryAwardType.Rob));
            if (lotterycfg == null)
                return false;

            GetLottery.RobTimes--;

            BigInteger dummyGold = 0;
            if (GetLottery.Rob.RivalUid == selectId)
            {
                dummyGold = BigInteger.Parse(GetLottery.Rob.Gold);

                GetLottery.Rob.RivalUid = 0;
                GetLottery.Rob.RivalName = string.Empty;
                GetLottery.Rob.RivalAvatarUrl = string.Empty;
                GetLottery.Rob.Gold = "0";
                UserHelper.RandomRobTarget(Current.UserId);
            }
            else if (GetFriends.IsHaveFriend(selectId))
            {
                var frienddata = GetFriends.FindFriend(selectId);
                dummyGold = BigInteger.Parse(frienddata.RobGold);

                frienddata.RobGold = "0";
                GetFriends.AddRobRecord(selectId);
                
            }
            else if (GetEnemys.IsHaveEnemy(selectId))
            {
                var enemydata = GetEnemys.FindEnemy(selectId);
                dummyGold = BigInteger.Parse(enemydata.RobGold);

                GetEnemys.RemoveEnemy(rival.UserID);
            }

            if (result == EventStatus.Bad)
            {
                dummyGold = dummyGold / 5;
            }

            receipt.Gold = dummyGold.ToString();
            UserHelper.RewardsGold(Current.UserId, dummyGold);
            BigInteger realGold = rival.GoldNum <= dummyGold * 2 ? rival.GoldNum / 2 : dummyGold;
            UserHelper.ConsumeGold(rival.UserID, realGold);

            int levelDown = 0;
            if (result == EventStatus.Good)
            {
                levelDown = Math.Max(rival.UserLv - 10, 0);
                levelDown = Math.Min(levelDown, 10);
                //rival.UserLv = Math.Max(rival.UserLv - levelDown, 10);
                //UserHelper.UserLvChange(rival.UserID);
                //PushMessageHelper.UserLvChangeNotification(GameSession.Get(rival.UserID));
                rival.BackLevelNum += 10;
            }

            rivalEnemy.AddEnemy(new EnemyData() { UserId = Current.UserId });



            EnemyLogData log = new EnemyLogData()
            {
                RivalUid = Current.UserId,
                RivalName = GetBasis.NickName,
                RivalAvatarUrl = GetBasis.AvatarUrl,
                LogTime = DateTime.Now,
                LossGold = realGold.ToString(),
                LevelDown = levelDown,
                IsSteal = false,
                Status = result,
                RivalProfession = GetBasis.Profession
            };
            rivalEnemy.PushLog(log);
            PushMessageHelper.NewStealRobNotification(GameSession.Get(rival.UserID));



            receipt.Lottery = GetLottery;
            receipt.Result = true;


            return true;
        }
    }
}