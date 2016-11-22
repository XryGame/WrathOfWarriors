﻿using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4001_请求挑战班长
    /// </summary>
    public class Action4001 : BaseAction
    {
        private JPChallengeClassMonitorData receipt;
        private int mapid;
        public Action4001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4001, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action4001;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MapId", ref mapid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            Config_SceneMap scenemap = new ShareCacheStruct<Config_SceneMap>().FindKey(mapid);
            if (scenemap == null || ContextUser.UnlockSceneMapList.Find(t => (t == mapid)) == 0)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return false;
            }


            receipt = new JPChallengeClassMonitorData();
            receipt.result = RequestChallengeClassMonitorResult.OK;
            if (ContextUser.ChallengeMonitorTimes != 0)
            {
                receipt.result = RequestChallengeClassMonitorResult.NoTimes;
                return true;
            }

            var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
            if (classdata != null && ContextUser.UserID != classdata.Monitor)
            {
                if (classdata.IsChallenging)
                {
                    receipt.result = RequestChallengeClassMonitorResult.DestIsFinging;
                    return true;
                }
                GameUser monitor = UserHelper.FindUser(classdata.Monitor);
                if (monitor != null)
                {
                    Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(monitor.UserLv);
                    if (rolegrade == null)
                    {
                        ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
                        return true;
                    }
                    receipt.UserId = monitor.UserID;
                    receipt.NickName = monitor.NickName;
                    receipt.LooksId = monitor.LooksId;
                    receipt.Attack = rolegrade.Attack;
                    receipt.Defense = rolegrade.Defense;
                    receipt.HP = rolegrade.HP;
                    receipt.FightValue = monitor.FightingValue;
                    receipt.ItemList = monitor.ItemDataList;
                    receipt.SkillList = monitor.SkillDataList;
                    receipt.SkillCarryList = monitor.SkillCarryList;
                    var rankuser = UserHelper.FindCombatRankUser(monitor.UserID);
                    if (rankuser != null)
                        receipt.RankId = rankuser.RankId;

                    classdata.IsChallenging = true;
                    classdata.ChallengeUserId = ContextUser.UserID;

                    ContextUser.UserStatus = UserStatus.Fighting;
                    ContextUser.SelectedSceneMapId = mapid;
                }
                else
                {
                    ErrorInfo = Language.Instance.NoFoundUser;
                    return true;
                }
            }

            return true;
        }
    }
}