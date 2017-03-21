using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1111_通关关卡
    /// </summary>
    public class Action1111 : BaseAction
    {
        private UserAttributeCache receipt = null;

        private int _ID;
        public Action1111(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1111, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref _ID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if (new ShareCacheStruct<Config_RoleInitial>().FindKey(_ID) == null)
                return false;
            var transcriptCfg = new ShareCacheStruct<Config_TeneralTranscript>().FindKey(_ID);
            if (transcriptCfg == null)
                return false;

            if (_ID == GetBasis.UserLv + 1)
            {
                GetBasis.UserLv = _ID;
                UserHelper.UserLevelUp(Current.UserId);

                // 每日
                if (transcriptCfg.limitTime > 0)
                {
                    UserHelper.EveryDayTaskProcess(GetBasis.UserID, TaskType.PassStageBoss, 1);
                }
                else
                {
                    UserHelper.EveryDayTaskProcess(GetBasis.UserID, TaskType.PassStage, 1);
                }

                // 成就
                UserHelper.AchievementProcess(GetBasis.UserID, AchievementType.LevelCount);
            }

            receipt = GetAttribute;
            return true;
        }
    }
}