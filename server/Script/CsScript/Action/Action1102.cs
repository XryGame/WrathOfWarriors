using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1102_请求放弃任务
    /// </summary>
    public class Action1102 : BaseAction
    {
        private SubjectType subjectType;
        private JPGiveUpTaskData receipt;

        public Action1102(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1102, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1102;
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

                        receipt = new JPGiveUpTaskData()
                        {
                            SubjectT = subjectType,
                            SubjectId = ContextUser.StudyTaskData.SubjectID
                        };

                        ContextUser.StudyTaskData.SubjectID = 0;
                        ContextUser.StudyTaskData.Count = 0;


                    }
                    break;
                case SubjectType.Exercise:
                    {

                        if (ContextUser.ExerciseTaskData.SubjectID == 0)
                        {
                            ErrorInfo = Language.Instance.CanNotOperationOfNow;
                            return true;
                        }

                        receipt = new JPGiveUpTaskData()
                        {
                            SubjectT = subjectType,
                            SubjectId = ContextUser.ExerciseTaskData.SubjectID
                        };

                        ContextUser.ExerciseTaskData.SubjectID = 0;
                        ContextUser.ExerciseTaskData.Count = 0;
                    }
                    break;
                default: throw new ArgumentException(string.Format("SubjectType Error[{0}] isn't exist.", subjectType));
            }


            return true;
        }
    }
}