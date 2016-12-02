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
    /// 6003_购买占领资格
    /// </summary>
    public class Action6003 : BaseAction
    {
        private JPBuyData receipt;
        private SceneType scenetype;
        public Action6003(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action6003, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("SceneId", ref scenetype))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            if (ContextUser.OccupySceneList.Find(t => (t == scenetype)) == SceneType.No)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            int needDiamond = ConfigEnvSet.GetInt("User.BuyOccupyNeedDiamond");
            
            if (ContextUser.DiamondNum < needDiamond)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needDiamond);
            ContextUser.OccupySceneList.Remove(scenetype);
            receipt.CurrDiamond = ContextUser.DiamondNum;
            return true;
        }
    }
}