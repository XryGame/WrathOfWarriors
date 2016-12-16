using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 6001_请求占领
    /// </summary>
    public class Action6001 : BaseAction
    {
        private JPRequestOccupyData receipt;

        private SceneType scenetype;
        private int mapid;
        public Action6001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action6001, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action6001;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("SceneId", ref scenetype)
                && httpGet.GetInt("MapId", ref mapid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestOccupyData();
            receipt.Result = RequestOccupyResult.Normal;

            Config_SceneMap scenemap = new ShareCacheStruct<Config_SceneMap>().FindKey(mapid);
            if (scenemap == null || ContextUser.UnlockSceneMapList.Find(t => (t == mapid)) == 0)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return false;
            }

            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            var findocc = occupycache.FindKey(scenetype);
            if (findocc == null)
            {
                ErrorInfo = string.Format(Language.Instance.RequestIDError, (int)scenetype);
                return true;
            }

            if (ContextUser.OccupySceneList.Find(t => (t == scenetype)) != SceneType.No)
            {
                receipt.Result = RequestOccupyResult.NoTimes;
                return true;
            }

            if (findocc.UserId == ContextUser.UserID || findocc.ChallengerId > 0)
            {
                receipt.Result = RequestOccupyResult.CurrentFighting;
                return true;
            }

            if (findocc.UserId == 0)
            {
                var list = occupycache.FindAll();
                foreach (var v in list)
                {
                    if (v.UserId == ContextUser.UserID)
                    {
                        v.ResetOccupy();
                    }
                }

                findocc.UserId = ContextUser.UserID;
                findocc.NickName = ContextUser.NickName;

                receipt.Result = RequestOccupyResult.NoOccupy;
                receipt.SceneData.SceneId = scenetype;
                receipt.SceneData.UserId = ContextUser.UserID;
                receipt.SceneData.NickName = ContextUser.NickName;

                ContextUser.OccupySceneList.Add(scenetype);
                receipt.OccupySceneList = ContextUser.OccupySceneList;

                UserHelper.GiveAwayDiamond(ContextUser.UserID, DataHelper.OccupyAwardDiamond);

                ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(ContextUser.ClassData.ClassID);
                if (classdata != null)
                {
                    //foreach (int id in classdata.MemberList)
                    //{
                    //    GameUser mem = UserHelper.FindUser(id);
                    //    if (mem == null)
                    //        continue;
                    //    if (mem.OccupyAddList.Find(t => (t == scenetype)) != scenetype)
                    //        mem.OccupyAddList.Add(scenetype);

                    //}
                    PushMessageHelper.ClassOccupyAddChangeNotification(ContextUser.ClassData.ClassID);
                }

                UserHelper.OccupySucceedNotification(findocc.SceneId);

                return true;
            }

            findocc.ChallengerId = ContextUser.UserID;
            findocc.ChallengerNickName = ContextUser.NickName;

            GameUser dest = UserHelper.FindUser(findocc.UserId);
            if (dest == null)
                return false;
            //Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(dest.UserLv);
            //if (rolegrade == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
            //    return true;
            //}
            ContextUser.UserStatus = UserStatus.Fighting;
            ContextUser.SelectedSceneMapId = mapid;

            ContextUser.OccupySceneType = scenetype;
            ContextUser.OccupySceneList.Add(scenetype);
            receipt.OccupySceneList = ContextUser.OccupySceneList;

            receipt.RivalData.UserId = dest.UserID;
            receipt.RivalData.NickName = dest.NickName;
            receipt.RivalData.LooksId = dest.LooksId;
            receipt.RivalData.UserLv = dest.UserLv;
            receipt.RivalData.VipLv = dest.VipLv;
            receipt.RivalData.Attack = dest.Attack;
            receipt.RivalData.Defense = dest.Defense;
            receipt.RivalData.HP = dest.Hp;
            var rankuser = UserHelper.FindCombatRankUser(dest.UserID);
            if (rankuser != null)
                receipt.RivalData.RankId = rankuser.RankId;
            if (dest.ClassData.ClassID > 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(dest.ClassData.ClassID);
                if (classdata != null)
                {
                    receipt.RivalData.ClassName = classdata.Name;
                }
            }
            receipt.RivalData.UserStage = dest.UserStage;
            receipt.RivalData.FightValue = dest.FightingValue;
            GameSession session = GameSession.Get(dest.UserID);
            if (session != null && session.Connected)
                receipt.RivalData.IsOnline = true;
            receipt.RivalData.ItemList = dest.ItemDataList;
            receipt.RivalData.SkillList = dest.SkillDataList;
            receipt.RivalData.SkillCarryList = dest.SkillCarryList;
            if (dest.UnlockSceneMapList.Find(t => (t == ContextUser.SelectedSceneMapId)) != 0)
            {
                receipt.RivalData.IsUnlockSelectMap = true;
            }
            return true;
        }

    }
}