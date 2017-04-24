using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Message;
using ZyGames.Framework.Game.Model;

namespace GameServer.CsScript.Com
{
    public enum CompetitionStage
    {
        No,
        ApplyStart,
        ApplyEnd,
        F64,
        F32,
        F16,
        F8,
        F4,
        F2,
        F1,
        End,
    }

    public enum GroupMemberType
    {
        Player,         // 正常玩家
        Bot,            // 机器人
        Insider         // 内部人员
    }

    public class Member
    {
        public int id;
        public GroupMemberType type;
    }

    public class Group
    {
        public List<Member> member = new List<Member>();
        public DateTime winnertime;
    }

    public class GroupX4
    {
        public GroupX4()
        {
            for (int i = 0; i < 4; ++i)
                groups[i] = new Group();
        }
        public Group[] groups = new Group[4];
        public DateTime time;
    }

    public class GroupX2
    {
        public GroupX2()
        {
            for (int i = 0; i < 2; ++i)
                groups[i] = new Group();
        }
        public Group[] groups = new Group[2];
        public DateTime time;
    }

    public class GroupX1
    {
        public Group group = new Group();
        public DateTime time;
    }

    public class BetInfo
    {
        public int UserId;
        public int Diamond;     // 下注金额
        public int Dest;        // 下注目标
    }
    public class BetData
    {
        public List<BetInfo> list = new List<BetInfo>();
    }



    /// <summary>
    /// 64强争霸赛
    /// </summary>
    public class Competition64
    {
        private const int F64LoserAwardDiamond = 500;
        private const int F32LoserAwardDiamond = 1000;
        private const int ChampionNotificationInterval = 200; //10分钟

        private const string StageKey = "Competition64Stage";
        private const string Group64Key = "Competition64_64";
        private const string Group32Key = "Competition64_32";
        private const string Group16Key = "Competition64_16";
        private const string Group8Key = "Competition64_8";
        private const string Group4Key = "Competition64_4";
        private const string Group2Key = "Competition64_2";
        private const string Group1Key = "Competition64_1";
        private const string BetKey = "Competition64_Bet";

        private CompetitionStage _Stage;
        private DateTime StartApplyDate;
        private DateTime EndApplyDate;
        private DateTime StartDate;
        private GroupX4 _Group64;
        private GroupX4 _Group32;
        private GroupX4 _Group16;
        private GroupX4 _Group8;
        private GroupX4 _Group4;
        private GroupX2 _Group2;
        private GroupX1 _Group1;
        private BetData _BetData;

        private JPCompetition64Data receipt;

        // 间隔（分钟）
        private int AllocationIntervalMin = 20;

        private int ChampionNotificationTime = 0;

        public Competition64()
        {

        }

        public CompetitionStage Stage { get { return _Stage; } }

        public GroupX4 Group64 { get { return _Group64; } }

        public GroupX4 Group32 { get { return _Group32; } }

        public GroupX4 Group16 { get { return _Group16; } }

        public GroupX4 Group8 { get { return _Group8; } }

        public GroupX4 Group4 { get { return _Group4; } }

        public GroupX2 Group2 { get { return _Group2; } }

        public GroupX1 Group1 { get { return _Group1; } }

        public BetData BetData { get { return _BetData; } }


        /// <summary>
        /// 初始化争霸赛数据
        /// </summary>
        public void Initialize()
        {
            _Stage = CompetitionStage.No;
            //Group64.groups[0].member.Add(new Member { id = 1, type = GroupMemberType.Bot });
            //Group64.groups[0].member.Add(new Member { id = 2, type = GroupMemberType.Bot });
            //Group64.groups[2].member.Add(new Member { id = 1, type = GroupMemberType.Bot });
            //Group64.groups[3].member.Add(new Member { id = 1, type = GroupMemberType.Bot });
            //Group64.time = DateTime.Now;
            //string json = JsonUtils.Serialize(Group64);
            //var applyCache = new ShareCacheStruct<CompetitionApply>();

            string startApplyDateStr = ConfigEnvSet.GetString("System.Competition64ApplyDate");
            string endApplyDateStr = ConfigEnvSet.GetString("System.Competition64ApplyEndDate");
            string startDateStr = ConfigEnvSet.GetString("System.Competition64Date");
            StartApplyDate = Convert.ToDateTime(startApplyDateStr);
            EndApplyDate = Convert.ToDateTime(endApplyDateStr);
            StartDate = Convert.ToDateTime(startDateStr);

            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(StageKey);
            if (CompetStage == null)
            {
                CompetStage = new GameCache();
                CompetStage.Key = StageKey;
                CompetStage.Value = string.Format("{0}", (int)CompetitionStage.No);
                gameCache.Add(CompetStage);
                gameCache.Update();
            }

            _Stage = (CompetitionStage)Convert.ToInt32(CompetStage.Value);

            GameCache f64 = gameCache.FindKey(Group64Key);
            if (f64 == null)
            {
                f64 = new GameCache();
                f64.Key = Group64Key;
                f64.Value = "";
                gameCache.Add(f64);
                gameCache.Update();
            }
            _Group64 = JsonUtils.Deserialize<GroupX4>(f64.Value);
            if (_Group64 == null)
                _Group64 = new GroupX4();

            GameCache f32 = gameCache.FindKey(Group32Key);
            if (f32 == null)
            {
                f32 = new GameCache();
                f32.Key = Group32Key;
                f32.Value = "";
                gameCache.Add(f32);
                gameCache.Update();
            }
            _Group32 = JsonUtils.Deserialize<GroupX4>(f32.Value);
            if (_Group32 == null)
                _Group32 = new GroupX4();

            GameCache f16 = gameCache.FindKey(Group16Key);
            if (f16 == null)
            {
                f16 = new GameCache();
                f16.Key = Group16Key;
                f16.Value = "";
                gameCache.Add(f16);
                gameCache.Update();
            }
            _Group16 = JsonUtils.Deserialize<GroupX4>(f16.Value);
            if (_Group16 == null)
                _Group16 = new GroupX4();

            GameCache f8 = gameCache.FindKey(Group8Key);
            if (f8 == null)
            {
                f8 = new GameCache();
                f8.Key = Group8Key;
                f8.Value = "";
                gameCache.Add(f8);
                gameCache.Update();
            }
            _Group8 = JsonUtils.Deserialize<GroupX4>(f8.Value);
            if (_Group8 == null)
                _Group8 = new GroupX4();

            GameCache f4 = gameCache.FindKey(Group4Key);
            if (f4 == null)
            {
                f4 = new GameCache();
                f4.Key = Group4Key;
                f4.Value = "";
                gameCache.Add(f4);
                gameCache.Update();
            }
            _Group4 = JsonUtils.Deserialize<GroupX4>(f4.Value);
            if (_Group4 == null)
                _Group4 = new GroupX4();

            GameCache f2 = gameCache.FindKey(Group2Key);
            if (f2 == null)
            {
                f2 = new GameCache();
                f2.Key = Group2Key;
                f2.Value = "";
                gameCache.Add(f2);
                gameCache.Update();
            }
            _Group2 = JsonUtils.Deserialize<GroupX2>(f2.Value);
            if (_Group2 == null)
                _Group2 = new GroupX2();

            GameCache f1 = gameCache.FindKey(Group1Key);
            if (f1 == null)
            {
                f1 = new GameCache();
                f1.Key = Group1Key;
                f1.Value = "";
                gameCache.Add(f1);
                gameCache.Update();
            }
            _Group1 = JsonUtils.Deserialize<GroupX1>(f1.Value);
            if (_Group1 == null)
                _Group1 = new GroupX1();

            GameCache bet = gameCache.FindKey(BetKey);
            if (bet == null)
            {
                bet = new GameCache();
                bet.Key = BetKey;
                bet.Value = "";
                gameCache.Add(bet);
                gameCache.Update();
            }
            _BetData = JsonUtils.Deserialize<BetData>(bet.Value);
            if (_BetData == null)
                _BetData = new BetData();
        }

        public void Run()
        {
            DateTime NowDate = DateTime.Now;
            if ((_Stage == CompetitionStage.No || _Stage == CompetitionStage.End)
                && NowDate >= StartApplyDate && NowDate < EndApplyDate)
            {
                ResetStart();
                _Stage = CompetitionStage.ApplyStart;
                UpdateStage();
                return;
            }
            else if (_Stage == CompetitionStage.ApplyStart && NowDate >= EndApplyDate)
            {
                _Stage = CompetitionStage.ApplyEnd;
                UpdateStage();
                return;
            }
            else if (_Stage == CompetitionStage.ApplyEnd && NowDate >= StartDate)
            {
                _Stage = CompetitionStage.F64;
                Allocation64();
                UpdateStage();
                UpdateF64();
                return;
            }

            if (_Stage == CompetitionStage.F64)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group64.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.F32;
                    Allocation32();
                    UpdateStage();
                    UpdateF32();
                }
            }
            else if (_Stage == CompetitionStage.F32)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group32.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.F16;
                    Allocation16();
                    UpdateStage();
                    UpdateF16();
                    
                }
            }
            else if (_Stage == CompetitionStage.F16)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group16.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.F8;
                    Allocation8();
                    UpdateStage();
                    UpdateF8();
                }
            }
            else if (_Stage == CompetitionStage.F8)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group8.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.F4;
                    Allocation4();
                    UpdateStage();
                    UpdateF4();
                }
            }
            else if (_Stage == CompetitionStage.F4)
            {
                DateTime startTime = StartDate.AddDays(1);
                if (NowDate >= startTime)
                {
                    //TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
                    //int min = (int)timeSpan.TotalMinutes;
                    //if (min >= AllocationIntervalMin)
                    //{
                        _Stage = CompetitionStage.F2;
                        Allocation2();
                        UpdateStage();
                        UpdateF2();
                    //}
                }
            }
            else if (_Stage == CompetitionStage.F2)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group2.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.F1;
                    Allocation1();
                    UpdateStage();
                    UpdateF1();

                    AwardBetWinner();
                }
            }
            else if (_Stage == CompetitionStage.F1)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Group2.time);
                int min = (int)timeSpan.TotalMinutes;
                if (min >= AllocationIntervalMin)
                {
                    _Stage = CompetitionStage.End;
                    UpdateStage();
                   ClearCompetitionApply();
                }
            }
            else if (_Stage == CompetitionStage.End)
            {
                ChampionNotificationTime++;
                if (ChampionNotificationTime >= ChampionNotificationInterval)
                {
                    ChampionNotificationTime = 0;
                    ChampionNotification();
                    
                }
            }
            //if (NowDate <= StartDate.AddDays(1))
        }

        /// <summary>
        /// 比较两个对手的胜负关系
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        private Member CheckWinner(Member m1, Member m2)
        {
            Member winner = null;
            if (m1.type == GroupMemberType.Insider && m2.type == GroupMemberType.Insider)
            {
                UserBasisCache g1 = UserHelper.FindUserBasis(m1.id);
                UserBasisCache g2 = UserHelper.FindUserBasis(m2.id);
                //winner = g1.FightingValue > g2.FightingValue ? m1 : m2;
            }
            else if (m1.type == GroupMemberType.Insider)
            {
                winner = m1;
            }
            else if (m2.type == GroupMemberType.Insider)
            {
                winner = m2;
            }
            else if (m1.type == GroupMemberType.Bot && m2.type == GroupMemberType.Bot)
            {
                UserBasisCache g1 = UserHelper.FindUserBasis(m1.id);
                UserBasisCache g2 = UserHelper.FindUserBasis(m2.id);
                //winner = g1.FightingValue > g2.FightingValue ? m1 : m2;
            }
            else if (m1.type == GroupMemberType.Bot)
            {
                winner = m1;
            }
            else if (m2.type == GroupMemberType.Bot)
            {
                winner = m2;
            }
            else
            {
                UserBasisCache g1 = UserHelper.FindUserBasis(m1.id);
                UserBasisCache g2 = UserHelper.FindUserBasis(m2.id);
                //winner = g1.FightingValue > g2.FightingValue ? m1 : m2;

            }

            return winner;
        }
        private void Allocation64()
        {

            for (int i = 0; i < 4; ++i)
            {
                _Group64.groups[i].member.Clear();
            }
            _Group64.time = DateTime.Now;

            var applyCache = new ShareCacheStruct<CompetitionApply>();
            var ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);

            //int rankID = 0;
            //UserRank rankInfo = null;
            //if (ranking.TryGetRankNo(m => (m.UserID == Current.UserId), out rankID))
            //{
            //    rankInfo = ranking.Find(s => (s.UserID == Current.UserId));
            //}
            Random random = new Random();
            int pagecout;
            var list = ranking.GetRange(0, 100, out pagecout);
            List<int> indexs = new List<int>() { 0, 1, 2, 3 };
            int ranv, index;
            foreach (var data in list)
            {
                CompetitionApply cp = applyCache.FindKey(data.UserID);
                if (cp == null)
                    continue;
                while (true)
                {
                    if (indexs.Count <= 0)
                        break;
                    ranv = random.Next(indexs.Count);
                    index = indexs[ranv];
                    if (_Group64.groups[indexs[ranv]].member.Count < 16)
                    {
                        if (!SearchMember(_Group64, data.UserID))
                        {
                            Member mem = new Member()
                            {
                                id = data.UserID,
                                type = data.RankId > 2 ? GroupMemberType.Player : GroupMemberType.Insider
                            };
                            _Group64.groups[indexs[ranv]].member.Add(mem);
                        }

                        break;
                    }
                    else
                    {
                        indexs.RemoveAt(ranv);
                    }
                }

            }
            indexs = null;
            indexs = new List<int>() { 0, 1, 2, 3 };
            var applylist = applyCache.FindAll();
            foreach (var v in applylist)
            {
                if (indexs.Count <= 0)
                    break;

                ranv = random.Next(indexs.Count);
                index = indexs[ranv];

                if (_Group64.groups[index].member.Count < 12)
                {
                    if (!SearchMember(_Group64, v.UserId))
                    {
                        Member mem = new Member()
                        {
                            id = v.UserId,
                            type = GroupMemberType.Player
                        };
                        _Group64.groups[index].member.Add(mem);
                    }
                }
                else
                {
                    indexs.RemoveAt(ranv);
                }
            }

            indexs = null;
            indexs = new List<int>() { 0, 1, 2, 3 };
            int[] count = new int[4];

            foreach (var v in Bots.getBots)
            {
                if (indexs.Count <= 0)
                    break;

                ranv = random.Next(indexs.Count);
                index = indexs[ranv];

                if (_Group64.groups[index].member.Count < 16)
                {
                    if (!SearchMember(_Group64, v.UserID))
                    {
                        Member mem = new Member()
                        {
                            id = v.UserID,
                            type = GroupMemberType.Bot
                        };
                        if (count[index] >= 4)
                            _Group64.groups[index].member.Add(mem);
                        else
                            _Group64.groups[index].member.Insert(count[index] * 4, mem);
                        count[index]++;
                    }
                }
                else
                {
                    indexs.RemoveAt(ranv);
                }
            }
            
            
        }

        /// <summary>
        /// 搜索GroupX4的成员
        /// </summary>
        /// <param name="gx4"></param>
        /// <param name="findId"></param>
        /// <returns></returns>
        public bool SearchMember(GroupX4 gx4, int findId)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (gx4.groups[i].member.Find(t => (t.id == findId)) != null)
                    return true;
            }
            return false;
        }

        private void Allocation32()
        {
            for (int i = 0; i < 4; ++i)
            {
                _Group32.groups[i].member.Clear();
            }
            _Group32.time = DateTime.Now;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < _Group64.groups[i].member.Count; j+=2)
                {
                    Member m1 = _Group64.groups[i].member[j];
                    Member m2 = _Group64.groups[i].member[j + 1];

                    Member winner = CheckWinner(m1, m2);
                    Member loser = winner.id == m1.id ? m2 : m1;
                    AwardF64Loser(loser.id);

                    Member mem = new Member()
                    {
                        id = winner.id,
                        type = winner.type
                    };
                    _Group32.groups[i].member.Add(mem);
                }

            }

        }

        private void Allocation16()
        {
            for (int i = 0; i < 4; ++i)
            {
                _Group16.groups[i].member.Clear();
            }
            _Group16.time = DateTime.Now;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < _Group32.groups[i].member.Count; j += 2)
                {
                    Member m1 = _Group32.groups[i].member[j];
                    Member m2 = _Group32.groups[i].member[j + 1];

                    Member winner = CheckWinner(m1, m2);
                    Member loser = winner.id == m1.id ? m2 : m1;
                    AwardF32Loser(loser.id);

                    Member mem = new Member()
                    {
                        id = winner.id,
                        type = winner.type
                    };
                    _Group16.groups[i].member.Add(mem);
                }

            }

        }

        private void Allocation8()
        {
            for (int i = 0; i < 4; ++i)
            {
                _Group8.groups[i].member.Clear();
            }
            _Group8.time = DateTime.Now;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < _Group16.groups[i].member.Count; j += 2)
                {
                    Member m1 = _Group16.groups[i].member[j];
                    Member m2 = _Group16.groups[i].member[j + 1];

                    Member winner = CheckWinner(m1, m2);

                    Member mem = new Member()
                    {
                        id = winner.id,
                        type = winner.type
                    };
                    _Group8.groups[i].member.Add(mem);
                }

            }

        }

        private void Allocation4()
        {
            for (int i = 0; i < 4; ++i)
            {
                _Group4.groups[i].member.Clear();
            }
            _Group4.time = DateTime.Now;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < _Group8.groups[i].member.Count; j += 2)
                {
                    Member m1 = _Group8.groups[i].member[j];
                    Member m2 = _Group8.groups[i].member[j + 1];

                    Member winner = CheckWinner(m1, m2);

                    Member mem = new Member()
                    {
                        id = winner.id,
                        type = winner.type
                    };
                    _Group4.groups[i].member.Add(mem);
                }

            }

        }

        private void Allocation2()
        {
            for (int i = 0; i < 2; ++i)
            {
                _Group2.groups[i].member.Clear();
            }
            _Group2.time = DateTime.Now;

            int index = 0;
            for (int i = 0; i <= 2; i += 2)
            {
                Member m1 = _Group4.groups[i].member[0];
                Member m2 = _Group4.groups[i + 1].member[0];

                Member winner = CheckWinner(m1, m2);

                Member mem = new Member()
                {
                    id = winner.id,
                    type = winner.type
                };
                _Group2.groups[index++].member.Add(mem);
            }

        }

        private void Allocation1()
        {

            _Group1.group.member.Clear();
            _Group1.time = DateTime.Now;
            
            Member m1 = _Group2.groups[0].member[0];
            Member m2 = _Group2.groups[1].member[0];

            Member winner = CheckWinner(m1, m2);

            Member mem = new Member()
            {
                id = winner.id,
                type = winner.type
            };
            _Group1.group.member.Add(mem);
        }

        private void UpdateStage()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(StageKey);
            CompetStage.Value = string.Format("{0}", (int)_Stage);
        }

        private void UpdateF64()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group64Key);
            CompetStage.Value = JsonUtils.Serialize(_Group64);
        }

        private void UpdateF32()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group32Key);
            CompetStage.Value = JsonUtils.Serialize(_Group32);
        }

        private void UpdateF16()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group16Key);
            CompetStage.Value = JsonUtils.Serialize(_Group16);
        }

        private void UpdateF8()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group8Key);
            CompetStage.Value = JsonUtils.Serialize(_Group8);
        }

        private void UpdateF4()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group4Key);
            CompetStage.Value = JsonUtils.Serialize(_Group4);
        }

        private void UpdateF2()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group2Key);
            CompetStage.Value = JsonUtils.Serialize(_Group2);
        }

        private void UpdateF1()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(Group1Key);
            CompetStage.Value = JsonUtils.Serialize(_Group1);
        }

        private void UpdateBet()
        {
            var gameCache = new ShareCacheStruct<GameCache>();
            GameCache CompetStage = gameCache.FindKey(BetKey);
            CompetStage.Value = JsonUtils.Serialize(_BetData);
        }

        public JPCompetition64Data getReceipt()
        {
            if (receipt == null)
            {
                BuildReceipt();
            }

            if (receipt.Stage != _Stage)
            {
                BuildReceipt();
            }

            return receipt;
        }

        /// <summary>
        /// 构建客户端显示数据
        /// </summary>
        public void BuildReceipt()
        {
            receipt = null;
            receipt = new JPCompetition64Data();
            
            receipt.Stage = _Stage;
            for (int i = 0; i < 4; ++i)
            {
                foreach (var v in _Group64.groups[i].member)
                {
                    UserBasisCache user = UserHelper.FindUserBasis(v.id);
                    if (user == null)
                        continue;
                    JPComp64Role cr = new JPComp64Role()
                    {
                        UserId = v.id,
                        NickName = user.NickName,
                        Profession = user.Profession,
                        VipLv = user.VipLv
                    };
                    receipt.Comp64RoleList.Add(cr);

                    receipt.Group64.Set[i].Add(v.id);
                }

                foreach (var v in _Group32.groups[i].member)
                {
                    receipt.Group32.Set[i].Add(v.id);
                }

                foreach (var v in _Group16.groups[i].member)
                {
                    receipt.Group16.Set[i].Add(v.id);
                }

                foreach (var v in _Group8.groups[i].member)
                {
                    receipt.Group8.Set[i].Add(v.id);
                }

                foreach (var v in _Group4.groups[i].member)
                {
                    receipt.Group4.Set[i].Add(v.id);
                }
            }

            for (int i = 0; i < 2; ++i)
            {
                foreach (var v in _Group2.groups[i].member)
                {
                    receipt.Group2.Set[i].Add(v.id);
                }
            }


            foreach (var v in _Group1.group.member)
            {
                receipt.Group1.Set.Add(v.id);
            }
        }

        public void ResetStart()
        {
            _Stage = CompetitionStage.No;
            _Group64 = new GroupX4();
            _Group32 = new GroupX4();
            _Group16 = new GroupX4();
            _Group8 = new GroupX4();
            _Group4 = new GroupX4();
            _Group2 = new GroupX2();
            _Group1 = new GroupX1();
            _BetData = new BetData();
            receipt = null;

            UpdateF64();
            UpdateF32();
            UpdateF16();
            UpdateF8();
            UpdateF4();
            UpdateF2();
            UpdateF1();
            UpdateBet();

        }

        public void ClearCompetitionApply()
        {
            var compapply = new ShareCacheStruct<CompetitionApply>();
            List<int> applyUserIdList = new List<int>();
            var applylist = compapply.FindAll();
            foreach (var v in applylist)
            {
                applyUserIdList.Add(v.UserId);
            }
            foreach (var v in applyUserIdList)
            {
                var findv = compapply.FindKey(v);
                if (findv != null)
                {
                    compapply.Delete(findv);
                }
            }
            compapply.Update();
        }

        /// <summary>
        /// 64强奖励
        /// </summary>
        /// <param name="UserId"></param>
        public void AwardF64Loser(int UserId)
        {
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "校园争霸赛奖励发放",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("感谢您参加本次校园争霸赛，您本次争霸赛止步64强，获得{0}钻石奖励，请查收！", 
                                        F64LoserAwardDiamond),
                ApppendDiamond = F64LoserAwardDiamond
            };

            UserHelper.AddNewMail(UserId, mail);
        }
        /// <summary>
        /// 32强奖励
        /// </summary>
        /// <param name="UserId"></param>
        public void AwardF32Loser(int UserId)
        {
            if (UserId == 1000054)
            {
                return;
            }
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "校园争霸赛奖励发放",
                Sender = "系统",
                Date = DateTime.Now,
                Context = string.Format("感谢您参加本次校园争霸赛，您本次争霸赛止步32强，获得{0}钻石奖励，请查收！",
                                        F32LoserAwardDiamond),
                ApppendDiamond = F32LoserAwardDiamond
            };

            UserHelper.AddNewMail(UserId, mail);
        }

        /// <summary>
        /// 下注奖励
        /// </summary>
        /// <param name="UserId"></param>
        public void AwardBetWinner()
        {
            int championUserId = _Group1.group.member[0].id;
            UserBasisCache championUser = UserHelper.FindUserBasis(championUserId);
            if (championUser == null)
                return;
            foreach (var betinfo in _BetData.list)
            {
                if (betinfo.Dest == championUserId)
                {
                    MailData mail = new MailData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Title = "校园争霸赛投注结果",
                        Sender = "系统",
                        Date = DateTime.Now,
                        Context = string.Format("恭喜您本次校园争霸赛竞猜的玩家{0}获得冠军，竞猜成功，获得{1}钻石奖励，请查收！",
                                                championUser.NickName, betinfo.Diamond * 2),
                        ApppendDiamond = betinfo.Diamond * 2
                    };

                    UserHelper.AddNewMail(betinfo.UserId, mail);
                }
                else
                { 
                    MailData mail = new MailData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Title = "校园争霸赛投注结果",
                        Sender = "系统",
                        Date = DateTime.Now,
                        Context = string.Format("您本次校园争霸赛竞猜失败，系统返还{0}竞猜钻石，请查收！",
                                                betinfo.Diamond / 2),
                        ApppendDiamond = betinfo.Diamond / 2
                    };

                    UserHelper.AddNewMail(betinfo.UserId, mail);
                }
            }

        }

        public BetInfo findBetData(int userId)
        {
            return _BetData.list.Find(t => (t.UserId == userId));
        }

        /// <summary>
        /// 下注
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="diamond"></param>
        public void NewBetData(int userId, int diamond, int dest)
        {
            BetInfo info = new BetInfo()
            {
                UserId = userId,
                Diamond = diamond,
                Dest = dest
            };
            BetData.list.Add(info);

            UpdateBet();
        }

        public void ChampionNotification()
        {
            if (_Group1.group.member.Count == 0)
                return;
            UserBasisCache championUser = UserHelper.FindUserBasis(_Group1.group.member[0].id);
            if (championUser == null)
                return;
            string context = string.Format("恭喜 {0} 获得本次校园争霸赛冠军，奖励iPhone 7 Plus一部！", championUser.NickName);
            // PushMessageHelper.SendNoticeToOnlineUser(NoticeMode.Game, context);
            GlobalRemoteService.SendNotice(NoticeMode.World, context);

            //var chatService = new TryXChatService();
            //chatService.SystemSend(context);
            //PushMessageHelper.SendSystemChatToOnlineUser();
        }
    }
}
