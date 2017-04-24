using GameServer.CsScript.Base;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{

    public class NoticeService : RemoteStruct
    {
        private NoticeMode _type;
        private int _serverID;
        private string _content;
        

        public NoticeService(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {
            IsNotRespond = true;
        }

        protected override bool Check()
        {
            if (paramGetter.GetEnum("Type", ref _type)
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

            switch (_type)
            {
                case NoticeMode.AllService:
                    {
                        var sessionlist = GameSession.GetAll();
                        foreach (var on in sessionlist)
                        {
                            if (on.Connected && !on.IsRemote)
                            {
                                MsgData data = new MsgData();
                                data.Type = MsgType.Notice;
                                data.UserId = on.UserId;

                                var parameters = new Parameters();
                                parameters["Type"] = NoticeMode.AllService;
                                parameters["ServerID"] = _serverID;
                                parameters["Content"] = _content;
                                data.Param = parameters;
                                MsgDispatcher.Push(data);
                            }
                        }
                    }
                    break;
                case NoticeMode.World:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var list = cache.FindAll(t => t.ServerID == _serverID);
                        foreach (var v in list)
                        {
                            var sess = GameSession.Get(v.UserId);
                            if (sess != null && sess.Connected && !sess.IsRemote)
                            {
                                MsgData data = new MsgData();
                                data.Type = MsgType.Notice;
                                data.UserId = sess.UserId;

                                var parameters = new Parameters();
                                parameters["Type"] = NoticeMode.World;
                                parameters["ServerID"] = _serverID;
                                parameters["Content"] = _content;
                                data.Param = parameters;
                                MsgDispatcher.Push(data);
                            }
                        }
                    }
                    break;
            }
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