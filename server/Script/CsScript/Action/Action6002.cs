using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 6002_请求占领结果
    /// </summary>
    public class Action6002 : BaseAction
    {
        private EventStatus receipt = EventStatus.Bad;
        
        private EventStatus result;

        public Action6002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action6002, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if (ContextUser.OccupySceneType == SceneType.No)
            {
                return false;
            }
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            var findocc = occupycache.FindKey(ContextUser.OccupySceneType);
            if (findocc == null || findocc.ChallengerId != ContextUser.UserID || findocc.UserId == ContextUser.UserID)
            {
                ErrorInfo = string.Format(Language.Instance.RequestIDError, (int)ContextUser.OccupySceneType);
                return true;
            }
            if (result == EventStatus.Good)
            {
                SceneType oldscene = SceneType.No;
                var list = occupycache.FindAll();
                foreach (var v in list)
                {
                    if (v.UserId == ContextUser.UserID)
                    {
                        v.ResetOccupy();
                        oldscene = v.SceneId;
                    }
                }
                findocc.UserId = ContextUser.UserID;
                findocc.NickName = ContextUser.NickName;

                UserHelper.GiveAwayDiamond(ContextUser.UserID, DataHelper.OccupyAwardDiamond);

                var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(ContextUser.ClassData.ClassID);
                if (classdata != null)
                {
                    //foreach (int id in classdata.MemberList)
                    //{
                    //    GameUser mem = UserHelper.FindUser(id);
                    //    if (mem == null)
                    //        continue;
                    //    if (mem.OccupyAddList.Find(t => (t == oldscene)) == oldscene)
                    //        mem.OccupyAddList.Remove(oldscene);
                    //    if (mem.OccupyAddList.Find(t => (t == findocc.SceneId)) != findocc.SceneId)
                    //        mem.OccupyAddList.Add(findocc.SceneId);
                    //}
                    PushMessageHelper.ClassOccupyAddChangeNotification(ContextUser.ClassData.ClassID);
                }
                UserHelper.OccupySucceedNotification(findocc.SceneId);
            }
            findocc.ChallengerId = 0;
            findocc.ChallengerNickName = "";

            ContextUser.UserStatus = UserStatus.MainUi;
            ContextUser.OccupySceneType = SceneType.No;

            receipt = EventStatus.Good;
            return true;
        }

    }
}