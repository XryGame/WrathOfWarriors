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
    /// 获得新宠物通知
    /// </summary>
    public class Action1083 : BaseAction
    {

        private ElfData receipt;

        private int _elfId;
        public Action1083(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1083, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ElfID", ref _elfId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = GetElf.FindElf(_elfId);
            return true;
        }
    }
}