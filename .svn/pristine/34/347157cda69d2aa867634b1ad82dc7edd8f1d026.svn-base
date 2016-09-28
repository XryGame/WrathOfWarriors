using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 6000_占领入口
    /// </summary>
    public class Action6000 : BaseAction
    {
        private JPOccupyData receipt;

        public Action6000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action6000, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action6000;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPOccupyData();
            receipt.OccupySceneList = ContextUser.OccupySceneList;
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            for (SceneType index = SceneType.Piazza; index <= SceneType.MusicHall; ++index)
            {
                var fo = occupycache.FindKey(index);
                if (fo == null)
                    continue;

                JPOccupySceneData data = new JPOccupySceneData();
                data.SceneId = fo.SceneId;
                data.UserId = fo.UserId;
                data.NickName = fo.NickName;
                data.ChallengerId = fo.ChallengerId;
                data.ChallengerNickName = fo.ChallengerNickName;

                receipt.ListData.Add(data);
            }
            return true;
        }

    }
}