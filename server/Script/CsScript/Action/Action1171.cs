using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 精灵出战
    /// </summary>
    public class Action1171 : BaseAction
    {
        private bool receipt;
        private int elfId;

        public Action1171(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1171, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ElfID", ref elfId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var elfcfg = new ShareCacheStruct<Config_Elves>().Find(t => t.ElvesID == elfId);
            if (elfcfg == null)
            {
                return false;
            }

            if (GetElf.FindElf(elfId) == null)
            {
                return false;
            }

            GetElf.SelectID = elfId;
            GetElf.SelectElfType = elfcfg.ElvesType;
            GetElf.SelectElfValue = elfcfg.ElvesNum;
            receipt = true;
            return true;
        }
    }
}