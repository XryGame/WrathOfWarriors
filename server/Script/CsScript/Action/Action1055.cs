using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1055_添加新的好友通知接口
    /// </summary>
    public class Action1055 : BaseAction
    {
        private JPFriendData receipt;
        private int Uid;
        public Action1055(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1055, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1055;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Uid", ref Uid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            GameUser dest = UserHelper.FindUser(Uid);
            if (dest == null)
                return true;

            receipt = new JPFriendData();
            receipt.UserId = Uid;
            receipt.NickName = dest.NickName;
            receipt.LooksId = dest.LooksId;
            receipt.UserLv = dest.UserLv;
            receipt.FightValue = dest.FightingValue;
            receipt.VipLv = dest.VipLv;
            receipt.IsOnline = GameSession.Get(dest.UserID) != null;

            return true;
        }
    }
}