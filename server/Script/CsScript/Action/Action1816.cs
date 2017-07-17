using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class RobData
    {
        public RobData()
        {
            Rob = new StealRobTarget();
            FriendRobList = new List<JPFriendRobData>();
            EnemyRobList = new List<JPEnemyRobData>();
            TodayRobList = new List<int>();
        }
        public StealRobTarget Rob;

        public List<JPFriendRobData> FriendRobList;

        public List<JPEnemyRobData> EnemyRobList;

        public List<int> TodayRobList;
    }
    /// <summary>
    /// 
    /// </summary>
    public class Action1816 : BaseAction
    {
        /// <summary>
        ///
        /// </summary>
        private RobData receipt;
        private Random random = new Random();
        public Action1816(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1816, actionGetter)
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
            receipt = new RobData();
            receipt.Rob = GetLottery.Rob;

            var rob = UserHelper.FindUserBasis(GetLottery.Rob.RivalUid);
            receipt.Rob.RivalLevel = rob.UserLv;
            var lotterycfg = new ShareCacheStruct<Config_Lottery>().Find(t => (t.Type == LotteryAwardType.Rob));
            if (lotterycfg == null)
                return false;
            receipt.TodayRobList = GetFriends.TodayRobList.ToList();
            foreach (var v in GetFriends.FriendsList)
            {
                var friend = GetFriends.FindFriend(v.UserId);
                JPFriendRobData friendrob = new JPFriendRobData()
                {
                    UserId = v.UserId,
                };
                if (friend.RobGold.Equals("0"))
                {
                    var enemy = GetEnemys.FindEnemy(v.UserId);
                    if (enemy != null && !enemy.RobGold.Equals("0"))
                    {
                        friend.RobGold = enemy.RobGold;
                        continue;
                    }
                    var rival = UserHelper.FindUserBasis(v.UserId);
                    int baseValue = lotterycfg.Content.ToInt();
                    int goldMin = baseValue / 4;
                    int goldmax = random.Next(baseValue - goldMin) + goldMin;
                    BigInteger targetGold = Math.Ceiling(rival.UserLv / 50.0).ToInt() * goldmax;
                    friend.RobGold = targetGold.ToString();
                }
                friendrob.Gold = friend.RobGold;
                receipt.FriendRobList.Add(friendrob);
            }
            foreach (var v in GetEnemys.EnemyList)
            {
                var enemy = GetEnemys.FindEnemy(v.UserId);
                JPEnemyRobData enemyrob = new JPEnemyRobData()
                {
                    UserId = v.UserId,
                };
                if (enemy.RobGold.Equals("0"))
                {
                    var friend = GetFriends.FindFriend(v.UserId);
                    if (friend != null && !friend.RobGold.Equals("0"))
                    {
                        enemy.RobGold = friend.RobGold;
                        continue;
                    }
                    var rival = UserHelper.FindUserBasis(v.UserId);
                    int baseValue = lotterycfg.Content.ToInt();
                    int goldMin = baseValue / 4;
                    int goldmax = random.Next(baseValue - goldMin) + goldMin;
                    BigInteger targetGold = Math.Ceiling(rival.UserLv / 50.0).ToInt() * goldmax;
                    enemy.RobGold = targetGold.ToString();
                }
                enemyrob.Gold = enemy.RobGold;
                receipt.EnemyRobList.Add(enemyrob);
            }

            return true;
        }
    }
}