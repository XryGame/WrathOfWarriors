using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum.Enum;
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
        private CombatMatchData receipt;
        private Random random = new Random();
        private const int MaxCount = 5;

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
            UserRank rankInfo = null;
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            rankInfo = UserHelper.FindRankUser(Current.UserId, RankType.Combat);
            
            if (rankInfo == null)
            {
                rankInfo = new UserRank()
                {
                    UserID = Current.UserId,
                    NickName = GetBasis.NickName,
                    UserLv = GetBasis.UserLv,
                    VipLv = GetBasis.VipLv,
                    RankId = int.MaxValue,
                    RankDate = DateTime.Now,
                };
                ranking.TryAppend(rankInfo);
                rankInfo = ranking.Find(s => (s.UserID == Current.UserId));
            }

            receipt = new CombatMatchData();
            receipt.RankId = GetBasis.CombatRankID;
            receipt.CombatTimes = GetCombat.CombatTimes;
            if (GetCombat.LastFailedDate != DateTime.MinValue)
                receipt.LastFailedTime = Util.ConvertDateTimeStamp(GetCombat.LastFailedDate);
            //UserRank info = null;
            CacheList <int> MachList = new CacheList<int>();

            if (rankInfo.RankId <= MaxCount)
            {
                for (int i = MaxCount; i > 0; --i)
                {
                    MachList.Add(i);
                }
            }
            else if (rankInfo.RankId <= 30)
            {// 前30名去前5位
                int num = 0;
                for (int i = rankInfo.RankId - 1; i > 0 && num < MaxCount; --i)
                {
                    MachList.Add(i);
                    num++;
                }
            }
            else if (rankInfo.RankId <= 100)
            {// 前100名去前10位
                if (rankInfo.RankId <= 30 + MaxCount)
                {
                    for (int i = rankInfo.RankId - 1; i > 30; --i)
                    {
                        MachList.Add(i);
                    }
                    int currcount = MachList.Count;
                    for (int i = 30; i > 30 - (MaxCount - currcount); --i)
                    {
                        MachList.Add(i);
                    }
                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 30 && num < 10; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }
            }
            else if (rankInfo.RankId <= 500)
            {// 前500名去前30位
                if (rankInfo.RankId <= 100 + MaxCount)
                {
                    for (int i = rankInfo.RankId - 1; i > 100; --i)
                    {
                        MachList.Add(i);
                    }
                    CacheList<int> temp = new CacheList<int>();
                    for (int i = 100; i > 90; --i)
                    {
                        temp.Add(i);
                    }
                    int mach;
                    int currcount = MachList.Count;
                    for (int i = 0; i < MaxCount - currcount; ++i)
                    {
                        RandMach(ref temp, out mach);
                        MachList.Add(mach);
                    }
                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 100 && num < 30; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }
            }
            else if (rankInfo.RankId <= 1000)
            {
                if (rankInfo.RankId <= 500 + MaxCount)
                {
                    for (int i = rankInfo.RankId - 1; i > 500; --i)
                    {
                        MachList.Add(i);
                    }
                    CacheList<int> temp = new CacheList<int>();
                    for (int i = 500; i > 470; --i)
                    {
                        temp.Add(i);
                    }
                    int mach;
                    int currcount = MachList.Count;
                    for (int i = 0; i < MaxCount - currcount; ++i)
                    {
                        RandMach(ref temp, out mach);
                        MachList.Add(mach);
                    }

                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 500 && num < 100; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }

            }
            else
            {
                if (rankInfo.RankId <= 1000 + MaxCount)
                {
                    for (int i = rankInfo.RankId - 1; i > 1000; --i)
                    {
                        MachList.Add(i);
                    }
                    CacheList<int> temp = new CacheList<int>();
                    for (int i = 1000; i > 900; --i)
                    {
                        temp.Add(i);
                    }
                    int mach;
                    int currcount = MachList.Count;
                    for (int i = 0; i < MaxCount - currcount; ++i)
                    {
                        RandMach(ref temp, out mach);
                        MachList.Add(mach);
                    }

                }
                else
                {
                    int num = 0;
                    for (int i = rankInfo.RankId - 1; i > 1000 && num < 200; --i)
                    {
                        MachList.Add(i);
                        num++;
                    }
                }
            }
            int mach_tops;
            for (int i = 0; MachList.Count > 0 && i < MaxCount; ++i)
            {
                RandMach(ref MachList, out mach_tops);

                int rankID = 0;
                UserRank machinfo = null;
                if (ranking.TryGetRankNo(m => (m.RankId == mach_tops), out rankID))
                {
                    machinfo = ranking.Find(s => (s.RankId == mach_tops));
                }

                if (machinfo != null)
                {
                    UserAttributeCache attribute = UserHelper.FindUserAttribute(machinfo.UserID);
                    CombatMatchUserData data = new CombatMatchUserData()
                    {
                        UserId = machinfo.UserID,
                        NickName = machinfo.NickName,
                        Profession = machinfo.Profession,
                        RankId = machinfo.RankId,
                        UserLv = machinfo.UserLv,
                        VipLv = machinfo.VipLv,
                        FightingValue = attribute.FightValue,
                       // SkillCarryList = user.SkillCarryList
                    };

                    receipt.RivalList.Add(data);
                }

            }


            // 日志
            //foreach (CombatLogData data in GetCombat.LogList)
            //{
            //    UserRank info = null;
            //    if (ranking.TryGetRankNo(m => (m.UserID == data.UserId), out rankID))
            //    {
            //        info = ranking.Find(s => (s.UserID == data.UserId));
            //    }

            //    JPCombatLogData cld = new JPCombatLogData();
            //    cld.UserId = data.UserId;
            //    if (info != null)
            //        cld.RivalCurrRankId = info.RankId;
            //    cld.Type = data.Type;
            //    cld.FightResult = data.Status;
            //    cld.Log = UserHelper.FormatCombatLog(data);
            //    receipt.LogList.Add(cld);
            //}

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