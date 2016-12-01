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
                        if (ContextUser.StudyTaskData.SubjectID == SubjectID.id0)
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
                        int needtime = subjectExp.UnitTime * ContextUser.StudyTaskData.Count;
                        int mins = 0;
                        if (DateTime.Now > ContextUser.StudyTaskData.StartTime)
                        {
                            
                            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.StudyTaskData.StartTime);
                            mins = (int)Math.Floor(timeSpan.TotalMinutes);
                            if (mins >= needtime)
                            {
                                isTimeEnough = true;
                            }
                        }
                        if (!isTimeEnough)
                        {
                            ErrorInfo = Language.Instance.NoReachScheduleTime;
                            return true;
                        }

                        int addvalue = ContextUser.AdditionBaseExpValue(
                            ContextUser.StudyTaskData.SubjectID, 
                            ContextUser.StudyTaskData.Count,
                            ContextUser.StudyTaskData.SceneId);

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

                        ContextUser.StudyTaskData.SubjectID = SubjectID.id0;
                        ContextUser.StudyTaskData.Count = 0;
                        if (addvalue > 0)
                        {
                            ContextUser.RefreshFightValue();
                        }

                        receipt.Attack = ContextUser.Attack;
                        receipt.Defense = ContextUser.Defense;
                        receipt.HP = ContextUser.Hp;

                        // 每日
                        UserHelper.EveryDayTaskProcess(ContextUser.UserID, TaskType.Study, needtime);


                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, needtime, AchievementType.StudyTime);

                    }
                    break;
                case SubjectType.Exercise:
                    {
                        
                        if (ContextUser.ExerciseTaskData.SubjectID == SubjectID.id0)
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

                        int needtime = subjectExp.UnitTime * ContextUser.ExerciseTaskData.Count;
                        bool isTimeEnough = false;
                        int mins = 0;
                        if (DateTime.Now > ContextUser.ExerciseTaskData.StartTime)
                        {
                            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.ExerciseTaskData.StartTime);
                            mins = (int)Math.Floor(timeSpan.TotalMinutes);
                            if (mins >= needtime)
                            {
                                isTimeEnough = true;
                            }
                        }
                        if (!isTimeEnough)
                        {
                            ErrorInfo = Language.Instance.NoReachScheduleTime;
                            return true;
                        }
                        int addvalue = ContextUser.AdditionBaseExpValue(
                            ContextUser.ExerciseTaskData.SubjectID, 
                            ContextUser.ExerciseTaskData.Count,
                            ContextUser.ExerciseTaskData.SceneId);


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

                        ContextUser.ExerciseTaskData.SubjectID = SubjectID.id0;
                        ContextUser.ExerciseTaskData.Count = 0;
                        if (addvalue > 0)
                        {
                            ContextUser.RefreshFightValue();
                        }
                        receipt.Attack = ContextUser.Attack;
                        receipt.Defense = ContextUser.Defense;
                        receipt.HP = ContextUser.Hp;

                        // 每日
                        UserHelper.EveryDayTaskProcess(ContextUser.UserID, TaskType.Exercise, needtime);

                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, needtime, AchievementType.ExerciseTime);
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