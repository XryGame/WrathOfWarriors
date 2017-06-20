using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求接收物品
    /// </summary>
    public class Action1751 : BaseAction
    {
        private string id;
        private string passward;
        private TransferItemResult receipt;

        public Action1751(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1750, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("ID", ref id)
                && httpGet.GetString("Passward", ref passward))
            {
                return true;
            }
            return false;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            if (GetTransfer.ReceiveCount >= 3)
            {
                receipt = TransferItemResult.ReceiveCountOut;
                return true;
            }

            var receiveTransfer = GetTransfer.FindReceive(id);
            if (receiveTransfer == null)
            {
                receipt = TransferItemResult.Expire;
                return true;
            }

            if (receiveTransfer.IsReceived)
            {
                receipt = TransferItemResult.Received;
                return true;
            }

            var sendTransfer = UserHelper.FindUserTransfer(receiveTransfer.Sender).FindSend(id);
            if (sendTransfer.Password.CompareTo(passward) != 0)
            {
                receipt = TransferItemResult.ErrorPassword;
                return true;
            }

            receiveTransfer.IsReceived = true;
            sendTransfer.IsReceived = true;

            UserHelper.RewardsItem(Current.UserId, sendTransfer.AppendItem.ID, sendTransfer.AppendItem.Num);

            PushMessageHelper.ReceivedTransferItemNotification(GameSession.Get(receiveTransfer.Sender), id);

            GetTransfer.ReceiveCount++;
            receipt = TransferItemResult.Successfully;

            return true;
        }
    }
}