using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1061_通知成就完成
    /// </summary>
    public class Action1061 : BaseAction
    {
        private AchievementData receipt;
        private AchievementType type;

        public Action1061(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1061, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("ID", ref type))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = GetAchievement.FindAchievement(type);
            return true;
        }
    }
}