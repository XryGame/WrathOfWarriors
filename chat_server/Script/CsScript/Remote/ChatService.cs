using GameServer.CsScript.Base;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{

    public class ChatService : RemoteStruct
    {
        private ChatType _chatType;
        private int _sender;
        private int _reveiver;
        private int _serverID;
        private long  _sendDate;
        private string _content;
        

        public ChatService(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {
            IsNotRespond = true;
        }

        protected override bool Check()
        {
            if (paramGetter.GetEnum("Type", ref _chatType)
                && paramGetter.GetInt("Sender", ref _sender)
                && paramGetter.GetInt("Receiver", ref _reveiver)
                && paramGetter.GetInt("ServerID", ref _serverID)
                && paramGetter.GetLong("SendDate", ref _sendDate)
                && paramGetter.GetString("Content", ref _content))
            {

                return true;
            }
            return false;
        }

        protected override void TakeRemote()
        {
            var remoteSession = paramGetter.GetSession();
            if (remoteSession == null)
            {
                ErrorCode = 10000;
                ErrorInfo = "RemoteSession is null.";
                return;
            }



            switch (_chatType)
            {
                case ChatType.AllService:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var Sender = cache.Find(t => (t.UserId == _sender));
                        if (Sender != null)
                        {
                            var sessionlist = GameSession.GetAll();
                            foreach (var on in sessionlist)
                            {
                                if (on.Connected && !on.IsRemote)
                                {
                                    MsgData data = new MsgData();
                                    data.Type = MsgType.Chat;
                                    data.UserId = on.UserId;

                                    var parameters = new Parameters();
                                    parameters["Type"] = ChatType.AllService;
                                    parameters["Sender"] = _sender;
                                    parameters["SenderName"] = Sender.UserName;
                                    parameters["ServerID"] = _serverID;
                                    parameters["SendDate"] = _sendDate;
                                    parameters["Content"] = _content;
                                    data.Param = parameters;
                                    MsgDispatcher.Push(data);
                                }
                            }
                        }
                    }
                    break;
                case ChatType.World:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var Sender = cache.Find(t => (t.UserId == _sender));
                        if (Sender != null)
                        {
                            var list = cache.FindAll(t => t.ServerID == Sender.ServerID);
                            foreach (var v in list)
                            {
                                var session = GameSession.Get(v.UserId);
                                if (session != null && session.Connected && !session.IsRemote)
                                {
                                    MsgData data = new MsgData();
                                    data.Type = MsgType.Chat;
                                    data.UserId = v.UserId;

                                    var parameters = new Parameters();
                                    parameters["Type"] = ChatType.World;
                                    parameters["Sender"] = _sender;
                                    parameters["SenderName"] = Sender.UserName;
                                    parameters["ServerID"] = _serverID;
                                    parameters["SendDate"] = _sendDate;
                                    parameters["Content"] = _content;
                                    data.Param = parameters;
                                    MsgDispatcher.Push(data);
                                }
                            }
                        }
                    }
                    break;
                case ChatType.Whisper:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var Sender = cache.Find(t => (t.UserId == _sender));
                        var Receiver = cache.Find(t => (t.UserId == _reveiver));
                        if (Sender != null && Receiver != null)
                        {
                            var session = GameSession.Get(_reveiver);
                            if (session != null && session.Connected && !session.IsRemote)
                            {
                                MsgData data = new MsgData();
                                data.Type = MsgType.Chat;
                                data.UserId = _reveiver;

                                var parameters = new Parameters();
                                parameters["Type"] = ChatType.Whisper;
                                parameters["Sender"] = _sender;
                                parameters["SenderName"] = Sender.UserName;
                                parameters["ServerID"] = _serverID;
                                parameters["SendDate"] = _sendDate;
                                parameters["Content"] = _content;
                                data.Param = parameters;
                                MsgDispatcher.Push(data);
                            }
                        }
                    }
                    break;
                case ChatType.Guild:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var Sender = cache.Find(t => (t.UserId == _sender));
                        if (Sender != null)
                        {
                            if (Sender.GuildID.IsEmpty())
                            {
                                return;
                            }
                            var list = cache.FindAll(t => t.GuildID == Sender.GuildID);
                            foreach (var v in list)
                            {
                                var session = GameSession.Get(v.UserId);
                                if (session != null && session.Connected && !session.IsRemote)
                                {
                                    MsgData data = new MsgData();
                                    data.Type = MsgType.Chat;
                                    data.UserId = v.UserId;

                                    var parameters = new Parameters();
                                    parameters["Type"] = ChatType.Guild;
                                    parameters["Sender"] = _sender;
                                    parameters["SenderName"] = Sender.UserName;
                                    parameters["ServerID"] = _serverID;
                                    parameters["SendDate"] = _sendDate;
                                    parameters["Content"] = _content;
                                    data.Param = parameters;
                                    MsgDispatcher.Push(data);
                                }
                            }
                        }
                    }
                    break;
                case ChatType.System:
                    {
                        var cache = new MemoryCacheStruct<ChatUser>();
                        var Receiver = cache.Find(t => (t.UserId == _reveiver));
                        if (Receiver != null)
                        {
                            var session = GameSession.Get(_reveiver);
                            if (session != null && session.Connected && !session.IsRemote)
                            {
                                MsgData data = new MsgData();
                                data.Type = MsgType.Chat;
                                data.UserId = _reveiver;

                                var parameters = new Parameters();
                                parameters["Type"] = ChatType.System;
                                parameters["Sender"] = 0;
                                parameters["SenderName"] = "系统";
                                parameters["ServerID"] = _serverID;
                                parameters["SendDate"] = _sendDate;
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
        }
    }
}