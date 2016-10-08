using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1103_请求加速任务
    /// </summary>
    public class Action1103 : BaseAction
    {
        private JPBuyData receipt;
        private SubjectType subjectType;
        

        public Action1103(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1103, actionGetter)
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
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            receipt.Extend1 = (int)subjectType;
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

                        int passmins = 0;
                        int residuemins = 0;
                        if (DateTime.Now > ContextUser.StudyTaskData.StartTime)
                        {
                            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.StudyTaskData.StartTime);
                            passmins = (int)Math.Floor(timeSpan.TotalMinutes);
                        }
                        residuemins = MathUtils.Subtraction(
                            subjectExp.UnitTime * ContextUser.StudyTaskData.Count, passmins, 0
                            );

                        if (ContextUser.DiamondNum < residuemins)
                        {
                            receipt.Result = EventStatus.Bad;
                        }
                        else
                        {
                            ContextUser.StudyTaskData.StartTime = DateTime.MinValue;
                            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, residuemins);
                        }
                    }
                    break;
                case SubjectType.Exercise:
                    {

                        Config_SubjectExp subjectExp = new ShareCacheStruct<Config_SubjectExp>().FindKey(ContextUser.ExerciseTaskData.SubjectID);
                        if (subjectExp == null)
                        {
                            ErrorInfo = string.Format(Language.Instance.DBTableError, "SubjectExp");
                            return true;
                        }

                        int passmins = 0;
                        int residuemins = 0;
                        if (DateTime.Now > ContextUser.ExerciseTaskData.StartTime)
                        {
                            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.ExerciseTaskData.StartTime);
                            passmins = (int)Math.Floor(timeSpan.TotalMinutes);
                        }
                        residuemins = MathUtils.Subtraction(
                            subjectExp.UnitTime * ContextUser.ExerciseTaskData.Count, passmins, 0
                            );

                        if (ContextUser.DiamondNum < residuemins)
                        {
                            receipt.Result = EventStatus.Bad;
                        }
                        else
                        {
                            ContextUser.ExerciseTaskData.StartTime = DateTime.MinValue;
                            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, residuemins);
                        }
                    }
                    break;
                default: throw new ArgumentException(string.Format("SubjectType Error[{0}] isn't exist.", subjectType));
            }

            if (receipt.Result == EventStatus.Good)
            {
                receipt.CurrDiamond = ContextUser.DiamondNum;
            }

            return true;
        }
    }
}