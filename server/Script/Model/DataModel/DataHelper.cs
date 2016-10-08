﻿using GameServer.Script.Model.ConfigModel;
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
        /// <summary>
        /// 开启宝箱一次所需钻石
        /// </summary>
        static public int OpenBoxOnceNeedDiamond;
        /// <summary>
        /// 宝箱10连抽所需钻石
        /// </summary>
        static public int OpenBoxConsecutiveNeedDiamond;
        /// <summary>
        /// 竞技场日志最大数量
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


        static public string[] JobTitles = {
            "学生会会长", "秘书部部长", "学习部部长", "宣传部部长", "艺术部部长", "体育部部长", "班级委员会"
        };





        static DataHelper()
        {

        }

        static public void InitData()
        {

            StudyExplist = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.Type == SubjectType.Study));
            ExerciseExplist = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.Type == SubjectType.Exercise));

            OpenBoxOnceNeedDiamond = ConfigEnvSet.GetInt("User.OpenBoxDiamond");
            OpenBoxConsecutiveNeedDiamond = OpenBoxOnceNeedDiamond * 10 / 100 * ConfigEnvSet.GetInt("User.OpenTenTimesBoxDiscount");
            CombatLogCountMax = ConfigEnvSet.GetInt("User.CombatLogCountMax");
            FriendCountMax = ConfigEnvSet.GetInt("User.FriendCountMax");
            FriendApplyCountMax = ConfigEnvSet.GetInt("User.FriendApplyCountMax");
            FriendGiveAwayCountMax = ConfigEnvSet.GetInt("User.FriendGiveAwayCountMax");
            FriendGiveAwayVitValue = ConfigEnvSet.GetInt("User.FriendGiveAwayVitValue");
            RepairSignNeedDiamond = ConfigEnvSet.GetInt("User.RepairSignNeedDiamond");
            MaxMailNum = ConfigEnvSet.GetInt("User.MaxMailNum");

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
                        if (i == 0) tmp.Name = string.Format("学前（{0}）班", j + 1);
                        else if (i == 1) tmp.Name = string.Format("一年（{0}）班", j + 1);
                        else if (i == 2) tmp.Name = string.Format("二年（{0}）班", j + 1);
                        else if (i == 3) tmp.Name = string.Format("三年（{0}）班", j + 1);
                        else if (i == 4) tmp.Name = string.Format("四年（{0}）班", j + 1);
                        else if (i == 5) tmp.Name = string.Format("五年（{0}）班", j + 1);
                        else if (i == 6) tmp.Name = string.Format("六年（{0}）班", j + 1);
                        else if (i == 7) tmp.Name = string.Format("七年（{0}）班", j + 1);
                        else if (i == 8) tmp.Name = string.Format("八年（{0}）班", j + 1);
                        else if (i == 9) tmp.Name = string.Format("九年（{0}）班", j + 1);
                        else if (i == 10) tmp.Name = string.Format("高一（{0}）班", j + 1);
                        else if (i == 11) tmp.Name = string.Format("高二（{0}）班", j + 1);
                        else if (i == 12) tmp.Name = string.Format("高三（{0}）班", j + 1);
                        else if (i == 13) tmp.Name = string.Format("大一（{0}）班", j + 1);
                        else if (i == 14) tmp.Name = string.Format("大二（{0}）班", j + 1);
                        else if (i == 15) tmp.Name = string.Format("大三（{0}）班", j + 1);
                        else if (i == 16) tmp.Name = string.Format("大四（{0}）班", j + 1);
                        classcache.Add(tmp);
                    }
                }
                classcache.Update();
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
                if (occupycache.FindKey(st) == null)
                {
                    OccupyDataCache data = new OccupyDataCache()
                    {
                        SceneId = st
                    };
                    occupycache.Add(data);
                }
            }
            occupycache.Update();
        }

        
    }
}