﻿using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1100_请求学习/劳动任务
    /// </summary>
    public class Action1100 : BaseAction
    {
        private JPRequestTaskData receipt;
        private SubjectType subjectType;
        private SceneType sceneId;
        private SubjectID subjectId;
        private int count;

        public Action1100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1100, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1100;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("SubjectType", ref subjectType)
                && httpGet.GetEnum("SceneId", ref sceneId)
                && httpGet.GetEnum("SubjectId", ref subjectId)
                && httpGet.GetInt("Count", ref count)
                && count > 0)
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            
            //var roleGradeCache = new ShareCacheStruct<Config_RoleGrade>();
            //Config_RoleGrade rolegrade = roleGradeCache.FindKey(ContextUser.UserLv);
            //if (rolegrade == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
            //    return true;
            //}
            var sceneCache = new ShareCacheStruct<Config_Scene>();
            Config_Scene scene = sceneCache.FindKey(sceneId);
            if (scene == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "Scene");
                return true;
            }
            var subjectExpCache = new ShareCacheStruct<Config_SubjectExp>();
            Config_SubjectExp subjectExp = subjectExpCache.FindKey(subjectId);
            if (subjectExp == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "SubjectExp");
                return true;
            }
            if (ContextUser.UserStage != SubjectStage.PreschoolSchool)
            {
                if (ContextUser.UserLv < scene.ClearGrade || ContextUser.UserStage != subjectExp.Stage)
                {
                    ErrorInfo = Language.Instance.RequestIDError;
                    return true;
                }
            }


            switch (subjectType)
            {
                case SubjectType.Study:
                    {
                        if (subjectExp.Type != SubjectType.Study)
                        {
                            ErrorInfo = Language.Instance.RequestIDError;
                            return true;
                        }
                        
                        if (ContextUser.StudyTaskData != null && ContextUser.StudyTaskData.SubjectID != 0)
                        {
                            ErrorInfo = Language.Instance.CanNotOperationOfNow;
                            return true;
                        }

                        
                        int needTime = subjectExp.UnitTime * count;
                        if (ContextUser.Vit < needTime)
                        {
                            ErrorInfo = Language.Instance.NoVitError;
                            return true;
                        }

                        ContextUser.Vit = MathUtils.Subtraction(ContextUser.Vit, needTime, 0);
                        DateTime starttime = DateTime.Now;

                        if (ContextUser.StudyTaskData == null)
                        {
                            ContextUser.StudyTaskData = new UserStudyTaskData();
                        }
                        ContextUser.StudyTaskData.SubjectID = subjectId;
                        ContextUser.StudyTaskData.StartTime = starttime;
                        ContextUser.StudyTaskData.Count = count;
                        ContextUser.StudyTaskData.SceneId = sceneId;

                        receipt = new JPRequestTaskData()
                        {
                            SubjectT = subjectType,
                            SceneId = sceneId,
                            SubjectId = subjectId,
                            StartTime = Util.GetTimeStamp(),
                            Count = count
                        };
                    }
                    break;
                case SubjectType.Exercise:
                    {
                        if (subjectExp.Type != SubjectType.Exercise)
                        {
                            ErrorInfo = Language.Instance.RequestIDError;
                            return true;
                        }
                        
                        if (ContextUser.ExerciseTaskData != null && ContextUser.ExerciseTaskData.SubjectID != 0)
                        {
                            ErrorInfo = Language.Instance.CanNotOperationOfNow;
                            return true;
                        }

                        int needTime = subjectExp.UnitTime * count;
                        if (ContextUser.Vit < needTime)
                        {
                            ErrorInfo = Language.Instance.NoVitError;
                            return true;
                        }

                        ContextUser.Vit = MathUtils.Subtraction(ContextUser.Vit, needTime, 0);

                        DateTime starttime = DateTime.Now;

                        if (ContextUser.ExerciseTaskData == null)
                        {
                            ContextUser.ExerciseTaskData = new UserExerciseTaskData();
                        }
                        ContextUser.ExerciseTaskData.SubjectID = subjectId;
                        ContextUser.ExerciseTaskData.StartTime = starttime;
                        ContextUser.ExerciseTaskData.Count = count;
                        ContextUser.ExerciseTaskData.SceneId = sceneId;

                        receipt = new JPRequestTaskData()
                        {
                            SubjectT = subjectType,
                            SceneId = sceneId,
                            SubjectId = subjectId,
                            StartTime = Util.GetTimeStamp(),
                            Count = count
                        };
                    }
                    break;
                default: throw new ArgumentException(string.Format("SubjectType Error[{0}] isn't exist.", subjectType));
            }
  
            
            return true;
        }
    }
}