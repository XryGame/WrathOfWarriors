using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 7000_成就入口
    /// </summary>
    public class Action7000 : BaseAction
    {
        private CacheList<AchievementData> receipt;

        public Action7000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action7000, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action7000;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = ContextUser.AchievementList;
            return true;
        }

    }
}