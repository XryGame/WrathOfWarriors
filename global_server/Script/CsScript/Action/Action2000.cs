using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 聊天消息
    /// </summary>
    public class Action2000 : BaseAction
    {
        private ChatData receipt = null;
        private ChatType _type;
        private int _sender;
        private string _senderName;
        private int _senderVipLv;
        private int _senderProfession;
        private string _senderAvatarUrl;
        private int _serverID;
        private long _sendDate;
        private string _content;

        public Action2000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2000, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action2000;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Type", ref _type)
                && httpGet.GetInt("Sender", ref _sender)
                && httpGet.GetString("SenderName", ref _senderName)
                && httpGet.GetInt("SenderVipLv", ref _senderVipLv)
                && httpGet.GetInt("SenderProfession", ref _senderProfession)
                && httpGet.GetString("SenderAvatarUrl", ref _senderAvatarUrl)
                && httpGet.GetInt("ServerID", ref _serverID)
                && httpGet.GetLong("SendDate", ref _sendDate)
                && httpGet.GetString("Content", ref _content))
            {

                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            receipt = new ChatData()
            {
                Type = _type,
                Sender = _sender,
                SenderName = _senderName,
                VipLv = _senderVipLv,
                Profession = _senderProfession,
                AvatarUrl = _senderAvatarUrl,
                ServerID = _serverID,
                SendDate = _sendDate,
                Content = _content,
            };
            

            return true;
        }
    }
}