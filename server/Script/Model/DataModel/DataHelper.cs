using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.Script.Model.DataModel
{
    public static class DataHelper
    {
        static public List<Config_SubjectExp> StudyExplist;
        static public List<Config_SubjectExp> ExerciseExplist;
        static public List<Config_Item> CombatItemList;
        /// <summary>
        /// 用户初始体力
        /// </summary>
        static public int InitVit;
        /// <summary>
        /// 开启道具宝箱一次所需钻石
        /// </summary>
        static public int OpenItemBoxOnceNeedDiamond;
        /// <summary>
        /// 道具宝箱10连抽所需钻石
        /// </summary>
        static public int OpenItemBoxConsecutiveNeedDiamond;
        /// <summary>
        /// 开启技能宝箱一次所需钻石
        /// </summary>
        static public int OpenSkillBoxOnceNeedDiamond;
        /// <summary>
        /// 道技能宝箱10连抽所需钻石
        /// </summary>
        static public int OpenSkillBoxConsecutiveNeedDiamond;
        /// <summary>
        /// 名人榜日志最大数量
        /// </summary>
        static public int CombatLogCountMax;

        /// <summary>
        /// 好友最大人数
        /// </summary>
        static public int FriendCountMax;
        /// <summary>
        /// 好友申请最大数量
        /// </summary>
        static public int FriendApplyCountMax;
        /// <summary>
        /// 好友赠送最大次数
        /// </summary>
        static public int FriendGiveAwayCountMax;
        /// <summary>
        /// 赠送好友体力/时间值
        /// </summary>
        static public int FriendGiveAwayVitValue;
        /// <summary>
        /// 补签所需钻石数量
        /// </summary>
        static public int RepairSignNeedDiamond;
        /// <summary>
        /// 最大邮件数量
        /// </summary>
        static public int MaxMailNum;

        /// <summary>
        /// 开启每日任务系统用户等级
        /// </summary>
        static public int OpenTaskSystemUserLevel;
        /// <summary>
        /// 开启排行榜系统用户等级
        /// </summary>
        static public int OpenRankSystemUserLevel;

        static public string[] JobTitles = {
            "班级委员会", "体育部部长", "艺术部部长", "宣传部部长", "学习部部长", "秘书部部长", "学生会会长"
        };

        /// <summary>
        /// 挑战班长成功获得钻石数量
        /// </summary>
        static public int ChallengeTheMonitorAwardDiamond;
        /// <summary>
        /// 占领成功获得钻石数量
        /// </summary>
        static public int OccupyAwardDiamond;
        /// <summary>
        /// 邀请切磋成功获得钻石数量
        /// </summary>
        static public int InviteFightAwardDiamond;
        /// <summary>
        /// 每周切磋获得钻石最大数量
        /// </summary>
        static public int InviteFightDiamondWeekMax;


        static DataHelper()
        {

        }



        static public void InitData()
        {

            StudyExplist = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.Type == SubjectType.Study));
            ExerciseExplist = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.Type == SubjectType.Exercise));
            CombatItemList = new ShareCacheStruct<Config_Item>().FindAll(t => (t.Type == ItemType.Item && t.Map == 0));

            InitVit = ConfigEnvSet.GetInt("User.InitVit");
            OpenItemBoxOnceNeedDiamond = ConfigEnvSet.GetInt("User.OpenItemBoxDiamond");

            OpenItemBoxConsecutiveNeedDiamond = OpenItemBoxOnceNeedDiamond * 5 * 10 / 100 * ConfigEnvSet.GetInt("User.OpenTenTimesBoxDiscount") / 10;
            OpenSkillBoxOnceNeedDiamond = ConfigEnvSet.GetInt("User.OpenSkillBoxDiamond");
            OpenSkillBoxConsecutiveNeedDiamond = OpenSkillBoxOnceNeedDiamond * 5 * 10 / 100 * ConfigEnvSet.GetInt("User.OpenTenTimesBoxDiscount") / 10;
            CombatLogCountMax = ConfigEnvSet.GetInt("User.CombatLogCountMax");
            FriendCountMax = ConfigEnvSet.GetInt("User.FriendCountMax");
            FriendApplyCountMax = ConfigEnvSet.GetInt("User.FriendApplyCountMax");
            FriendGiveAwayCountMax = ConfigEnvSet.GetInt("User.FriendGiveAwayCountMax");
            FriendGiveAwayVitValue = ConfigEnvSet.GetInt("User.FriendGiveAwayVitValue");
            RepairSignNeedDiamond = ConfigEnvSet.GetInt("User.RepairSignNeedDiamond");
            MaxMailNum = ConfigEnvSet.GetInt("User.MaxMailNum");
            OpenTaskSystemUserLevel = ConfigEnvSet.GetInt("System.OpenTaskSystemUserLevel");
            OpenRankSystemUserLevel = ConfigEnvSet.GetInt("System.OpenRankSystemLevel");
            ChallengeTheMonitorAwardDiamond = ConfigEnvSet.GetInt("User.ChallengeTheMonitorAwardDiamond");
            OccupyAwardDiamond = ConfigEnvSet.GetInt("User.OccupyAwardDiamond");
            InviteFightAwardDiamond = ConfigEnvSet.GetInt("User.InviteFightAwardDiamond");
            InviteFightDiamondWeekMax = ConfigEnvSet.GetInt("User.InviteFightDiamondWeekMax");

            var classcache = new ShareCacheStruct<ClassDataCache>();
            if (classcache.IsEmpty)
            {
                // 每个年级创建3个班级(共有17个年级)
                for (int i = 0; i < 17; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        ClassDataCache tmp = new ClassDataCache()
                        {
                            ClassID = 1000 * (i + 1) + j,
                            Lv = i + 1
                        };
                        if (i == 0) tmp.Name = string.Format("学前{0}班", j + 1);
                        else if (i == 1) tmp.Name = string.Format("一年{0}班", j + 1);
                        else if (i == 2) tmp.Name = string.Format("二年{0}班", j + 1);
                        else if (i == 3) tmp.Name = string.Format("三年{0}班", j + 1);
                        else if (i == 4) tmp.Name = string.Format("四年{0}班", j + 1);
                        else if (i == 5) tmp.Name = string.Format("五年{0}班", j + 1);
                        else if (i == 6) tmp.Name = string.Format("六年{0}班", j + 1);
                        else if (i == 7) tmp.Name = string.Format("七年{0}班", j + 1);
                        else if (i == 8) tmp.Name = string.Format("八年{0}班", j + 1);
                        else if (i == 9) tmp.Name = string.Format("九年{0}班", j + 1);
                        else if (i == 10) tmp.Name = string.Format("高一{0}班", j + 1);
                        else if (i == 11) tmp.Name = string.Format("高二{0}班", j + 1);
                        else if (i == 12) tmp.Name = string.Format("高三{0}班", j + 1);
                        else if (i == 13) tmp.Name = string.Format("大一{0}班", j + 1);
                        else if (i == 14) tmp.Name = string.Format("大二{0}班", j + 1);
                        else if (i == 15) tmp.Name = string.Format("大三{0}班", j + 1);
                        else if (i == 16) tmp.Name = string.Format("大四{0}班", j + 1);
                        classcache.Add(tmp);
                    }
                }
                classcache.Update();
            }

            // 检索去除沉余的班级成员
            var classlist = classcache.FindAll();
            foreach (var classdata in classlist)
            {
                List<int> removelist = new List<int>();
                foreach (var memid in classdata.MemberList)
                {
                    GameUser memuser = new PersonalCacheStruct<GameUser>().FindKey(memid.ToString());
                    if (memuser == null)
                        continue;
                    int classlv = memuser.UserLv / 2 + 1;
                    if (classdata.Lv != classlv)
                    {
                        removelist.Add(memid);
                        memuser.ClassData.ClassID = 0;
                    }
                        
                }
                foreach (var rid in removelist)
                {
                    classdata.MemberList.Remove(rid);
                }
                if (removelist.Find(t => (t == classdata.Monitor)) != 0)
                    classdata.Monitor = classdata.MemberList.Count > 0 ? classdata.MemberList[0] : 0;


            }

            var jobcache = new ShareCacheStruct<JobTitleDataCache>();
            for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
            {
                if (jobcache.FindKey(i) == null)
                {
                    JobTitleDataCache data = new JobTitleDataCache()
                    {
                        TypeId = i,
                        Title = JobTitles[(int)i],
                    };
                    jobcache.Add(data);
                }
            }
            jobcache.Update();

            
            for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
            {
                var fd = jobcache.FindKey(i);
                if (fd.Status == CampaignStatus.Runing)
                {
                    fd.Status = CampaignStatus.Over;
                }
            }
            var fdnow = jobcache.FindKey((JobTitleType)DateTime.Now.DayOfWeek);
            if (fdnow != null)
            {
                fdnow.Status = CampaignStatus.Runing;
                fdnow.UserId = 0;
                fdnow.NickName = "";
                fdnow.ClassId = 0;
                fdnow.LooksId = 0;
            }


            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            for (SceneType st = SceneType.Piazza; st <= SceneType.MusicHall; ++st)
            {
                var occ = occupycache.FindKey(st);
                if (occupycache.FindKey(st) == null)
                {
                    OccupyDataCache data = new OccupyDataCache()
                    {
                        SceneId = st
                    };
                    occupycache.Add(data);
                }
                else
                {
                    occ.ChallengerId = 0;
                    occ.ChallengerNickName = "";
                }
            }
            occupycache.Update();
        }

        /// <summary>
        /// 扩展班级
        /// </summary>
        /// <param name="classLv"></param>
        /// <returns>扩展的班级ID</returns>
        public static int ExtendClass(int classLv)
        {
            int retClassID = 0;
            List<ClassDataCache> classeslist = new ShareCacheStruct<ClassDataCache>().FindAll(t => (t.Lv == classLv));
            List<ClassDataCache> findclasseslist = new ShareCacheStruct<ClassDataCache>().FindAll(
                                t => (t.Lv == classLv) && t.MemberList.Count < ConfigEnvSet.GetInt("Class.OpenNum")
                            );
            if (findclasseslist.Count == 0)
            {
                var classcache = new ShareCacheStruct<ClassDataCache>();

                ClassDataCache tmpcache = new ClassDataCache()
                {
                    ClassID = 1000 * classLv + classeslist.Count,
                    Lv = classLv
                };
                if (tmpcache.Lv == 1) tmpcache.Name = string.Format("学前{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 2) tmpcache.Name = string.Format("一年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 3) tmpcache.Name = string.Format("二年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 4) tmpcache.Name = string.Format("三年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 5) tmpcache.Name = string.Format("四年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 6) tmpcache.Name = string.Format("五年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 7) tmpcache.Name = string.Format("六年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 8) tmpcache.Name = string.Format("七年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 9) tmpcache.Name = string.Format("八年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 10) tmpcache.Name = string.Format("九年{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 11) tmpcache.Name = string.Format("高一{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 12) tmpcache.Name = string.Format("高二{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 13) tmpcache.Name = string.Format("高三{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 14) tmpcache.Name = string.Format("大一{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 15) tmpcache.Name = string.Format("大二{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 16) tmpcache.Name = string.Format("大三{0}班", classeslist.Count + 1);
                else if (tmpcache.Lv == 17) tmpcache.Name = string.Format("大四{0}班", classeslist.Count + 1);

                retClassID = tmpcache.ClassID;

                classcache.Add(tmpcache);
                classcache.Update();
                
            }

            return retClassID;
        }


    }
}