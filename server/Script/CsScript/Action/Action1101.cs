using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1101_请求领取任务奖励
    /// </summary>
    public class Action1101 : BaseAction
    {
        private JPReceiveTaskAwardData receipt;
        private SubjectType subjectType;

        public Action1101(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1101, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1101;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("SubjectType", ref subjectType))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            
            switch (subjectType)
            {
                case SubjectType.Study:
                    {
                        if (ContextUser.StudyTaskData.SubjectID == 0)
                        {
                            ErrorInfo = Language.Instance.CanNotOperationOfNow;
                            return true;
                        }
                        
                        Config_SubjectExp subjectExp = new ShareCacheStruct<Config_SubjectExp>().FindKey(ContextUser.StudyTaskData.SubjectID);
                        if (subjectExp == null)
                        {
                            ErrorInfo = string.Format(Language.Instance.DBTableError, "SubjectExp");
                            return true;
                        }

                        bool isTimeEnough = false;
                        int mins = 0;
                        if (DateTime.Now > ContextUser.StudyTaskData.StartTime)
                        {
                            TimeSpan timeSpan = DateTime.Now.Date - ContextUser.StudyTaskData.StartTime.Date;
                            mins = (int)Math.Floor(timeSpan.TotalMinutes);
                            if (mins >= subjectExp.UnitTime * ContextUser.StudyTaskData.Count)
                            {
                                isTimeEnough = true;
                            }
                        }
                        if (!isTimeEnough)
                        {
                            ErrorInfo = Language.Instance.NoReachScheduleTime;
                            return true;
                        }

                        int addvalue = ContextUser.AdditionBaseExpValue((SubjectID)ContextUser.StudyTaskData.SubjectID, ContextUser.StudyTaskData.Count);

                        receipt = new JPReceiveTaskAwardData()
                        {
                            SubjectT = subjectType,
                            SubjectId = ContextUser.StudyTaskData.SubjectID,
                            AwardExp = addvalue,
                            //CurrBaseExp = gameUser.BaseExp,
                            CurrFightExp = ContextUser.FightExp,
                            CurrLv = ContextUser.UserLv,
                            CurrFightValue = ContextUser.FightingValue
                        };
                        object outexpdata;
                        UserHelper.buildBaseExpData(ContextUser, out outexpdata);
                        receipt.CurrBaseExp = outexpdata;

                        ContextUser.StudyTaskData.SubjectID = 0;
                        ContextUser.StudyTaskData.Count = 0;


                        // 每日
                        if (ContextUser.DailyQuestData.ID == TaskType.Study)
                        {
                            ContextUser.DailyQuestData.Count += mins;
                            if (ContextUser.DailyQuestData.Count > 45)
                            {
                                ContextUser.DailyQuestData.IsFinish = true;
                                PushMessageHelper.DailyQuestFinishNotification(Current);
                            }

                        }

                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, mins, AchievementType.StudyTime);

                    }
                    break;
                case SubjectType.Exercise:
                    {
                        
                        if (ContextUser.ExerciseTaskData.SubjectID == 0)
                        {
                            ErrorInfo = Language.Instance.CanNotOperationOfNow;
                            return true;
                        }

                        Config_SubjectExp subjectExp = new ShareCacheStruct<Config_SubjectExp>().FindKey(ContextUser.ExerciseTaskData.SubjectID);
                        if (subjectExp == null)
                        {
                            ErrorInfo = string.Format(Language.Instance.DBTableError, "SubjectExp");
                            return true;
                        }

                        bool isTimeEnough = false;
                        int mins = 0;
                        if (DateTime.Now > ContextUser.ExerciseTaskData.StartTime)
                        {
                            TimeSpan timeSpan = DateTime.Now.Date - ContextUser.ExerciseTaskData.StartTime.Date;
                            mins = (int)Math.Floor(timeSpan.TotalMinutes);
                            if (mins >= subjectExp.UnitTime * ContextUser.ExerciseTaskData.Count)
                            {
                                isTimeEnough = true;
                            }
                        }
                        if (!isTimeEnough)
                        {
                            ErrorInfo = Language.Instance.NoReachScheduleTime;
                            return true;
                        }
                        int addvalue = ContextUser.AdditionBaseExpValue((SubjectID)ContextUser.ExerciseTaskData.SubjectID, ContextUser.ExerciseTaskData.Count);


                        receipt = new JPReceiveTaskAwardData()
                        {
                            SubjectT = subjectType,
                            SubjectId = ContextUser.ExerciseTaskData.SubjectID,
                            AwardExp = addvalue,
                            //CurrBaseExp = gameUser.BaseExp,
                            CurrFightExp = ContextUser.FightExp,
                            CurrLv = ContextUser.UserLv,
                            CurrFightValue = ContextUser.FightingValue
                        };
                        object outexpdata;
                        UserHelper.buildBaseExpData(ContextUser, out outexpdata);
                        receipt.CurrBaseExp = outexpdata;

                        ContextUser.ExerciseTaskData.SubjectID = 0;
                        ContextUser.ExerciseTaskData.Count = 0;

                        // 每日
                        if (ContextUser.DailyQuestData.ID == TaskType.Exercise)
                        {
                            ContextUser.DailyQuestData.Count += mins;
                            if (ContextUser.DailyQuestData.Count >= 20)
                            {
                                ContextUser.DailyQuestData.IsFinish = true;
                                PushMessageHelper.DailyQuestFinishNotification(Current);
                            }

                        }

                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, mins, AchievementType.ExerciseTime);
                    }
                    break;
                default: throw new ArgumentException(string.Format("SubjectType Error[{0}] isn't exist.", subjectType));
            }


            return true;
        }
        
        //public override void TakeActionAffter(bool state)
        //{
        //    var parameters = new Parameters();
        //    parameters["ID"] = 123;
        //    var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1050, Current, parameters, httpGet.OpCode, null);
        //    ActionFactory.SendAction(Current, ActionIDDefine.Cst_Action1050, packet, (session, asyncResult) =>
        //    {
        //        Console.WriteLine("Action 1002 send result:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");

        //    }, 0);
        //    base.TakeActionAffter(state);
        //}
    }
}