using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1406_竞技场匹配
    /// </summary>
    public class Action1406 : BaseAction
    {
        private MatchRivalData receipt;

        private Random random = new Random();

        public Action1406(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1406, actionGetter)
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
            receipt = new MatchRivalData();
            receipt.Result = MatchRivalResult.OK;
   
            if (GetCombat.MatchTimes <= 0)
            {
                receipt.Result = MatchRivalResult.NoTimes;
                return true;
            }
            
            int minv, maxv;
            minv = Math.Max(GetBasis.UserLv - 25, 0);
            maxv = minv + 25;

            var onlinelist = UserHelper.GetOnlinesList();
            List<int> matchlist = new List<int>();
            foreach (var v in onlinelist)
            {
                if (v.UserId == Current.UserId)
                    continue;
                var basis = UserHelper.FindUserBasis(v.UserId);
                if (basis != null && basis.UserLv >= minv && basis.UserLv <= maxv)
                    matchlist.Add(v.UserId);
            }

            if (matchlist.Count == 0)
            {
                var ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                for (int i = minv; i <= maxv; ++i)
                {

                    var findlist = ranking.FindAll(s => (s.UserLv == i));
                    foreach (var rank in findlist)
                    {
                        if (rank.UserID != Current.UserId)
                        {
                            matchlist.Add(rank.UserID);
                        }
                    }

                }
            }

            if (matchlist.Count == 0)
            {
                receipt.Result = MatchRivalResult.NoMatchRival;
                return true;
            }

            int randv = random.Next(matchlist.Count);
            int rivalUid = matchlist[randv];
            UserBasisCache rival = UserHelper.FindUserBasis(rivalUid);

            GetCombat.MatchTimes = MathUtils.Subtraction(GetCombat.MatchTimes, 1, 0);
           
            receipt.UserId = rivalUid;
            receipt.NickName = rival.NickName;
            receipt.Profession = rival.Profession;
            receipt.AvatarUrl = rival.AvatarUrl;
            receipt.CombatRankID = rival.CombatRankID;
            receipt.LevelRankID = rival.LevelRankID;
            receipt.UserLv = rival.UserLv;
            receipt.Equips = UserHelper.FindUserEquips(rivalUid);
            receipt.Attribute = UserHelper.FindUserAttribute(rivalUid);
            receipt.Skill = UserHelper.FindUserSkill(rivalUid);
            receipt.ElfID = UserHelper.FindUserElf(rivalUid).SelectID;
            var pay = UserHelper.FindUserPay(rivalUid);
            receipt.IsAutoFight = pay.MonthCardDays >= 0 || pay.QuarterCardDays >= 0;
            return true;
        }
    }
}