using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 公告消息
    /// </summary>
    public class Action2001 : BaseAction
    {
        private NoticeData receipt = null;
        private NoticeMode _type;
        private int _serverID;
        private string _content;

        public Action2001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action2001, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action2001;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Type", ref _type)
                && httpGet.GetInt("ServerID", ref _serverID)
                && httpGet.GetString("Content", ref _content))
            {

                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new NoticeData()
            {
                Type = _type,
                ServerID = _serverID,
                Content = _content,
            };
            return true;
        }
    }
}