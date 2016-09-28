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
    /// 1110_请求挑战
    /// </summary>
    public class Action1110 : BaseAction
    {
        private JPRequestFightData receipt;
        private int monsterId;
        private int mapid;

        public Action1110(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1110, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1110;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MonsterId", ref monsterId)
               && httpGet.GetInt("MapId", ref mapid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            
            Config_Role role = new ShareCacheStruct<Config_Role>().FindKey(monsterId);
            Config_SceneMap scenemap = new ShareCacheStruct<Config_SceneMap>().FindKey(mapid);
           
            if (role == null || scenemap == null || ContextUser.UnlockSceneMapList.Find(t => (t == mapid)) == 0)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }
            
            if (ContextUser.Vit < role.Time)
            {
                ErrorInfo = Language.Instance.NoVitError;
                return true;
            }

            ContextUser.UserStatus = UserStatus.Fighting;
            ContextUser.SelectedSceneMapId = mapid;
            ContextUser.Vit = MathUtils.Subtraction(ContextUser.Vit, role.Time, 0);
            receipt = new JPRequestFightData()
            {
                MonestId = monsterId,
                CurrVit = ContextUser.Vit
            };
            return true;
        }
    }
}