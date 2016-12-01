using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1067_占领加成改变
    /// </summary>
    public class Action1067 : BaseAction
    {
        public CacheList<SceneType> receipt;

        public Action1067(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1067, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1066;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            ContextUser.OccupyAddList.Clear();
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            for (SceneType i = SceneType.Piazza; i <= SceneType.MusicHall; ++i)
            {
                var os = occupycache.FindKey(i);
                if (os == null)
                    continue;

                if (ContextUser.ClassData.ClassID != 0)
                {
                    var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                    if (classdata != null)
                    {
                        if (classdata.MemberList.Find(t => (t == os.UserId)) != 0)
                        {
                            ContextUser.OccupyAddList.Add(i);
                        }
                    }
                }
            }
            receipt = ContextUser.OccupyAddList;
            
            return true;
        }
    }
}