using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class LotteryReceipt
    {
        public LotteryReceipt()
        {
            StealList = new List<CombatMatchUserData>();
        }
        public UserLotteryCache Lottery { get; set; }

        public List<CombatMatchUserData> StealList { get; set; }
    }

    /// <summary>
    /// 请求抽奖数据
    /// </summary>
    public class Action1811 : BaseAction
    {
        private LotteryReceipt receipt;
        private const int RestoreLotteryTimesSec = 3600;

        public Action1811(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1811, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            receipt = new LotteryReceipt();
            var timespan = DateTime.Now.Subtract(GetLottery.StartRestoreLotteryTimesDate);
            int sec = (int)Math.Floor(timespan.TotalSeconds);
            int canAddTimes = ConfigEnvSet.GetInt("User.LotteryTimesMax") - GetLottery.LotteryTimes;
            int addtimes = Math.Min(sec / RestoreLotteryTimesSec * 4, canAddTimes);// 一次恢复4次
            GetLottery.LotteryTimes += addtimes;
            GetLottery.RemainTimeSec = RestoreLotteryTimesSec - sec % RestoreLotteryTimesSec;

            GetLottery.StartRestoreLotteryTimesDate = GetLottery.StartRestoreLotteryTimesDate.AddSeconds(
                Math.Min(sec / RestoreLotteryTimesSec, canAddTimes) * RestoreLotteryTimesSec
                );

            UserHelper.RandomStealTarget(Current.UserId);
            UserHelper.RandomRobTarget(Current.UserId);

            receipt.Lottery = GetLottery;
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
            return true;
        }
    }
}