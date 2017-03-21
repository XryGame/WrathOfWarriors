using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{

    public class NoticeService : RemoteStruct
    {
        private ChatType _chatType;
        private int _sender;
        private int _serverID;
        private string _content;
        

        public NoticeService(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {
            IsNotRespond = true;
        }

        protected override bool Check()
        {
            if (paramGetter.GetEnum("Type", ref _chatType)
                && paramGetter.GetInt("Sender", ref _sender)
                && paramGetter.GetInt("ServerID", ref _serverID)
                && paramGetter.GetString("Content", ref _content))
            {

                return true;
            }
            return false;
        }

        protected override void TakeRemote()
        {
            var session = paramGetter.GetSession();
            if (session == null)
            {
                ErrorCode = 10000;
                ErrorInfo = "Sessin is null.";
                return;
            }

            ////Here to do something
            //_gold = 100;
            //_items.Add(1001);
        }

        protected override void BuildPacket()
        {
            ////Send a reward here
            //response.PushIntoStack(_gold);
            //response.PushIntoStack(_items.Count);
            //foreach (var itemId in _items)
            //{
            //    var dsItem = new MessageStructure();
            //    dsItem.PushIntoStack(itemId);
            //    response.PushIntoStack(dsItem);
            //}
        }
    }
}