using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1112_购买解锁场景地图
    /// </summary>
    public class Action1112 : BaseAction
    {
        private JPBuyData receipt;
        private int mapid;

        public Action1112(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1112, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MapId", ref mapid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            Config_SceneMap scenemap = new ShareCacheStruct<Config_SceneMap>().FindKey(mapid);
            if (scenemap == null
                || ContextUser.UnlockSceneMapList.Find(t => (t == mapid)) != 0
                || ContextUser.DiamondNum < scenemap.UnLockPay)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.UnlockSceneMapList.Add(mapid);
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, scenemap.UnLockPay);
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.Extend1 = mapid;
            return true;
        }
    }
}