using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1402_名人榜请求挑战
    /// </summary>
    public class Action1402 : BaseAction
    {
        private JPCombatRivalData receipt;
        private int rivaluid;
        private int mapid;

        public Action1402(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1402, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1402;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("RivalUid", ref rivaluid)
                && httpGet.GetInt("MapId", ref mapid))
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

            Ranking<UserRank> ranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            int rankID = 0;
            UserRank rankinfo = null;
            UserRank rivalrankinfo = null;
            if (ranking.TryGetRankNo(m => m.UserID == ContextUser.UserID, out rankID))
            {
                rankinfo = ranking.Find(s => s.UserID == ContextUser.UserID);
            }
            if (ranking.TryGetRankNo(m => m.UserID == rivaluid, out rankID))
            {
                rivalrankinfo = ranking.Find(s => s.UserID == rivaluid);
            }
            if (rankinfo == null || rivalrankinfo == null)
            {
                int erroruid = rankinfo == null ? ContextUser.UserID : rivaluid;
                new BaseLog("Action1402").SaveLog(string.Format("Not found user combat rank. UserId={0}", erroruid));
                ErrorInfo = Language.Instance.CombatRankDataException;
                return true;
            }

            receipt = new JPCombatRivalData();
            receipt.Result = CombatReqRivalResult.OK;
            if (rankinfo.RankId <= rivalrankinfo.RankId)
            {
                receipt.Result = CombatReqRivalResult.RankOverdue;
                return true;
            }
            if (rivalrankinfo.IsFighting)
            {
                receipt.Result = CombatReqRivalResult.RivalIsFinging;
                return true;
            }
            if (rankinfo.IsFighting)
            {
                receipt.Result = CombatReqRivalResult.SelfIsFinging;
                return true;
            }
            if (ContextUser.CombatData.CombatTimes <= 0)
            {
                receipt.Result = CombatReqRivalResult.NoTimes;
                return true;
            }
            
            GameUser rival = UserHelper.FindUser(rivaluid);
            if (rival == null)
            {
                ErrorInfo = Language.Instance.NoFoundUser;
                return true;
            }
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(rivalrankinfo.UserLv);
            if (rolegrade == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
                return true;
            }

            ContextUser.UserStatus = UserStatus.Fighting;
            ContextUser.SelectedSceneMapId = mapid;
            ContextUser.CombatData.CombatTimes = MathUtils.Subtraction(ContextUser.CombatData.CombatTimes, 1, 0);
            rankinfo.IsFighting = true;
            rankinfo.FightDestUid = rivaluid;
            rivalrankinfo.IsFighting = true;
            /////rivalrankinfo.FightDestUid = ContextUser.UserID;

            receipt.UserId = rivaluid;
            receipt.NickName = rivalrankinfo.NickName;
            receipt.LooksId = rivalrankinfo.LooksId;
            receipt.RankId = rivalrankinfo.RankId;
            receipt.UserLv = rivalrankinfo.UserLv;
            receipt.FightingValue = rivalrankinfo.FightingValue;
            receipt.Attack = rolegrade.Attack;
            receipt.Defense = rolegrade.Defense;
            receipt.HP = rolegrade.HP;
            receipt.ItemList = rival.ItemDataList;
            foreach (int skid in rival.SkillCarryList)
            {
                SkillData sd = rival.SkillDataList.Find(t => (t.ID == skid));
                if (sd != null)
                {
                    receipt.SkillList.Add(sd);
                }
            }
            
            return true;
        }

    }
}