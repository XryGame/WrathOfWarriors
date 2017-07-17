using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
    public class StealReceipt
    {
        public StealReceipt()
        {
            StealList = new List<CombatMatchUserData>();
        }
        public bool Result { get; set; }

        public string Gold { get; set; }

        public UserLotteryCache Lottery { get; set; }

        public List<CombatMatchUserData> StealList { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Action1813 : BaseAction
    {
        private StealReceipt receipt;
        private int selectIndex;

        public Action1813(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1813, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("SelectIndex", ref selectIndex))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new StealReceipt();
            receipt.Result = false;

            if (GetLottery.StealTimes <= 0 || selectIndex >= 3)
            {
                return true;
            }
            StealRobTarget target = GetLottery.StealList[selectIndex];
            var rival = UserHelper.FindUserBasis(target.RivalUid);
            if (rival == null)
            {
                return true;
            }
            var rivalEnemy = UserHelper.FindUserEnemy(rival.UserID);
            if (rivalEnemy == null)
            {
                return true;
            }

            GetLottery.StealTimes--;
            receipt.Gold = target.Gold;

            UserHelper.RewardsGold(Current.UserId, target.Gold);
            BigInteger dummyGold = BigInteger.Parse(target.Gold);
            BigInteger realGold = rival.GoldNum <= dummyGold * 2 ? rival.GoldNum / 2 : dummyGold;
            UserHelper.ConsumeGold(rival.UserID, realGold);
            rivalEnemy.AddEnemy(new EnemyData() { UserId = Current.UserId });
            EnemyLogData log = new EnemyLogData()
            {
                RivalUid = Current.UserId,
                RivalName = GetBasis.NickName,
                RivalAvatarUrl = GetBasis.AvatarUrl,
                LogTime = DateTime.Now,
                LossGold = realGold.ToString(),
                IsSteal = true,
                RivalProfession = GetBasis.Profession
            };
            rivalEnemy.PushLog(log);
            PushMessageHelper.NewStealRobNotification(GameSession.Get(rival.UserID));

            if (target.IsPrimary)
            {
                GetLottery.StealList.Clear();
                UserHelper.RandomStealTarget(Current.UserId);
            }
            else
            {
                UserHelper.RandomStealTarget2(Current.UserId);
            }

            foreach (var v in GetLottery.StealList)
            {
                UserBasisCache basis = UserHelper.FindUserBasis(v.RivalUid);
                UserAttributeCache attribute = UserHelper.FindUserAttribute(v.RivalUid);
                UserEquipsCache equips = UserHelper.FindUserEquips(v.RivalUid);
                CombatMatchUserData data = new CombatMatchUserData()
                {
                    UserId = basis.UserID,
                    NickName = basis.NickName,
                    Profession = basis.Profession,
                    AvatarUrl = basis.AvatarUrl,
                    RankId = basis.LevelRankID,
                    UserLv = basis.UserLv,
                    VipLv = basis.VipLv,
                    FightingValue = attribute.FightValue,
                    Equips = equips,
                    // SkillCarryList = user.SkillCarryList
                };

                receipt.StealList.Add(data);
            }

            receipt.Lottery = GetLottery;
            receipt.Result = true;
            return true;
        }
    }
}