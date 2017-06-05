using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求赠送物品
    /// </summary>
    public class Action1750 : BaseAction
    {
        private int destId;
        private int itemId;
        private int itemNum;
        private SendTransferItemData receipt;

        public Action1750(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1750, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestID", ref destId)
                && httpGet.GetInt("ItemID", ref itemId)
                && httpGet.GetInt("ItemNum", ref itemNum))
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

            var itemdata = GetPackage.FindItem(itemId);
            if (itemdata == null || itemdata.Num < itemNum)
            {
                return false;
            }
            var dest = UserHelper.FindUserBasis(destId);
            if (dest == null)
                return false;
            var destTransfer = UserHelper.FindUserTransfer(destId);

            string newId = Guid.NewGuid().ToString("N");
            ReceiveTransferItemData receiveData = new ReceiveTransferItemData()
            {
                ID = newId,
                Sender = Current.UserId,
                SenderName = GetBasis.NickName,
                SenderProfession = GetBasis.Profession,
                SenderAvatar = GetBasis.AvatarUrl,
                Date = DateTime.Now,
                IsReceived = false,
            };
            receiveData.AppendItem.ID = itemId;
            receiveData.AppendItem.Num = itemNum;

            receipt = new SendTransferItemData()
            {
                ID = newId,
                Receiver = dest.UserID,
                ReceiverName = dest.NickName,
                ReceiverProfession = dest.Profession,
                ReceiverAvatar = dest.AvatarUrl,
                Date = DateTime.Now,
                IsReceived = false,
            };
            receipt.Password = Util.GetRandom4Pwd();
            receipt.AppendItem.ID = itemId;
            receipt.AppendItem.Num = itemNum;

            GetPackage.RemoveItem(itemId, itemNum);

            destTransfer.ReceiveList.Add(receiveData);
            PushMessageHelper.NewReceiveTransferItemNotification(GameSession.Get(destId), newId);

            GetTransfer.SendList.Add(receipt);
            
            return true;
        }
    }
}