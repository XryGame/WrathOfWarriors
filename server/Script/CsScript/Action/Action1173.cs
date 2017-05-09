using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 精灵卡片合成
    /// </summary>
    public class Action1173 : BaseAction
    {
        private bool receipt;
        private int debrisId;

        public Action1173(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1173, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DebrisID", ref debrisId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(debrisId);
            if (itemcfg == null || itemcfg.ItemType != ItemType.Debris)
            {
                return false;
            }

            ItemData gemData = GetPackage.FindItem(debrisId);
            int needNumber = ConfigEnvSet.GetInt("Elf.CompoundCardNeedNumber");
            if (gemData == null || gemData.Num < needNumber)
            {
                return false;
            }

            
            GetPackage.RemoveItem(debrisId, needNumber);
            //GetPackage.AddItem(gemcfg.GemID, 1);
            UserHelper.RewardsItem(Current.UserId, itemcfg.ResourceNum.ToInt(), 1);

            receipt = true;
            return true;
        }
    }
}