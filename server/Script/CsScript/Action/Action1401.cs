using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1401_竞技场入口
    /// </summary>
    public class Action1401 : BaseAction
    {
        private JPCombatMatchData receipt;
        private Random random = new Random();

        public Action1401(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1401, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1401;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            
            int rankID = 0;
            UserRank rankInfo = null;
            if (ranking.TryGetRankNo(m => (m.UserID == ContextUser.UserID), out rankID))
            {
                rankInfo = ranking.Find(s => (s.UserID == ContextUser.UserID));
            }
            
            if (rankInfo == null)
            {
                rankInfo = new UserRank()
                {
                    UserID = ContextUser.UserID,
                    NickName = ContextUser.NickName,
                    UserLv = ContextUser.UserLv,
                    IsOnline = ContextUser.IsOnline,
                    RankId = int.MaxValue,
                    FightingValue = ContextUser.FightingValue,
                    RankDate = DateTime.Now,
                };
                ranking.TryAppend(rankInfo);
                rankInfo = ranking.Find(s => (s.UserID == ContextUser.UserID));
            }

            receipt = new JPCombatMatchData();
            receipt.RankId = rankInfo.RankId;
            receipt.CombatTimes = ContextUser.CombatData.CombatTimes;
            receipt.LastFailedTime = Util.ConvertDateTimeStamp(ContextUser.CombatData.LastFailedDate);
            //UserRank info = null;
            CacheList <int> MachList = new CacheList<int>();
            if (rankInfo.RankId <= 100)
            {
                if (rankInfo.RankId <= 5)
                {// 如果是前5名玩家，就取前4
                    for (int i = 1; i < 5; ++i)
                    {
                        MachList.Add(i);
                    }
                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 0 && num < 20; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }
            }
            else if (rankInfo.RankId <= 1000)
            {
                if (rankInfo.RankId <= 104)
                {
                    for (int i = rankInfo.RankId - 1; i > 100; --i)
                    {
                        MachList.Add(i);
                    }
                    CacheList<int> temp = new CacheList<int>();
                    int num = 0;
                    for (int i = 100; i > 80; --i)
                    {
                        temp.Add(i);
                        num++;
                    }
                    int mach;
                    for (int i = 0; i < 4 - MachList.Count; ++i)
                    {
                        RandMach(ref temp, out mach);
                        MachList.Add(mach);
                    }

                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 100 && num < 100; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }

            }
            else
            {
                int num = 0;
                for (int i = rankInfo.RankId - 1; i > 0 && num < 200; --i)
                {
                    MachList.Add(i);
                    num++;
                }
            }
            int mach_tops;
            int rid;
            for (int i = 0; MachList.Count > 0 && i < 4; ++i)
            {
                RandMach(ref MachList, out mach_tops);

                UserRank machinfo = null;
                if (ranking.TryGetRankNo(m => (m.RankId == mach_tops), out rid))
                {
                    machinfo = ranking.Find(s => (s.RankId == mach_tops));
                }
                if (machinfo != null)
                {
                    JPCombatMatchUserData data = new JPCombatMatchUserData()
                    {
                        UserId = machinfo.UserID,
                        NickName = machinfo.NickName,
                        LooksId = machinfo.LooksId,
                        RankId = machinfo.RankId,
                        UserLv = machinfo.UserLv,
                        FightingValue = machinfo.FightingValue
                    };

                    receipt.RivalList.Add(data);
                }

            }


            // 日志
            foreach (CombatLogData data in ContextUser.CombatLogList)
            {
                string logstr = UserHelper.FormatCombatLog(data);
                if (!string.IsNullOrEmpty(logstr))
                {
                    receipt.LogList.Add(logstr);
                }
            }

            return true;
        }

        private void RandMach(ref CacheList<int> queue, out int uid)
        {
            int randv = random.Next(queue.Count);
            uid = queue[randv];
            queue.Remove(uid);
        }
    }
}