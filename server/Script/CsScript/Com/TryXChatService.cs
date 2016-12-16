
using System;
using System.Collections.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Message;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Net;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Lang;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.LogModel;

namespace GameServer.CsScript.Com
{
    /// <summary>
    /// 聊天功能
    /// </summary>
    public class TryXChatService : ChatService
    {
        private readonly GameUser _user;
        private const int MsgTimeOut = 1;//分钟
        private const int IntervalSend = 5;
        private ChatType ChatType;
        public static List<Config_ChatKeyWord> ChatKeyWordList
        {
            get;
            private set;
        }

        static TryXChatService()
        {
            InitChatKeyWord();
        }

        public static void InitChatKeyWord()
        {
            ChatKeyWordList = new ShareCacheStruct<Config_ChatKeyWord>().FindAll();
        }

        public TryXChatService()
            : this(new GameUser())
        {

        }
        public TryXChatService(GameUser user)
            : base(user.UserID)
        {
            _user = user;
        }

        public TryXChatService(GameUser user, ChatType chattype)
            : base(user.UserID)
        {
            _user = user;
            ChatType = chattype;
        }

        public List<ChatMessage> PopMessages()
        {
            return Receive();
        }

        public static bool IsAllow(GameUser user, ChatType chatType)
        {
            if (user != null)
            {
                if (chatType == ChatType.World && (DateTime.Now - user.ChatDate).TotalSeconds >= IntervalSend)
                {
                    return true;
                }
                else if (chatType != ChatType.World)
                {
                    return true;
                }
            }
            return false;
        }

        public void SystemSend(string content)
        {
            var chat = new ChatData
            {
                Version = NextVersion,
                FromUserID = Language.Instance.SystemUserId,
                FromUserName = Language.Instance.KingName,
                FromUserVip = 0,
                ToUserID = 0,
                ToUserName = string.Empty,
                ToUserVip = 0,
                ChatType = ChatType.System,
                Content = content,
                SendDate = DateTime.Now,
                LooksId = 0,
                ChildType = ChatChildType.Normal,
            };
            Send(chat);
        }

        public void SystemRedundantSend(string content, int userId, ChatChildType childtype)
        {
            var chat = new ChatData
            {
                Version = NextVersion,
                FromUserID = userId,
                FromUserName = Language.Instance.KingName,
                FromUserVip = 0,
                ToUserID = 0,
                ToUserName = string.Empty,
                ToUserVip = 0,
                ChatType = ChatType.System,
                Content = content,
                SendDate = DateTime.Now,
                LooksId = 0,
                ChildType = childtype,
            };
            Send(chat);
        }

        public void SystemClassSend(ChatType chatType, string content)
        {
            int classid = 0;
            if (chatType == ChatType.Whisper) return;

            classid = _user.ClassData.ClassID;
            var chat = new ChatData
            {
                Version = NextVersion,
                FromUserID = Language.Instance.SystemUserId,
                FromUserName = Language.Instance.KingName,
                FromUserVip = 0,
                ToUserID = 0,
                ToUserName = string.Empty,
                ToUserVip = 0,
                ChatType = chatType,
                Content = content,
                SendDate = DateTime.Now,
                ClassID = classid,
                LooksId = 0
            };
            Send(chat);
        }

        public void Send(ChatType chatType, string content)
        {
            int classid = 0;
            if (chatType == ChatType.Class)
            {
                classid = _user.ClassData.ClassID;
            }
            var chat = new ChatData
            {
                Version = NextVersion,
                FromUserID = _user.UserID,
                FromUserName = _user.NickName,
                FromUserVip = (short)_user.VipLv,
                ToUserID = 0,
                ToUserName = string.Empty,
                ToUserVip = 0,
                ChatType = chatType,
                Content = FilterMessage(content),
                SendDate = DateTime.Now,
                ClassID = classid,
                LooksId = _user.LooksId
            };
            if (chatType == ChatType.World)
            {
                _user.ChatDate = DateTime.Now;
            }
            Send(chat);
        }


        public void SystemSendWhisper(GameUser toUser, string content)
        {
            if (toUser == null)
            {
                throw new Exception("接收人为空值");
            }
            SystemSendWhisper(toUser.UserID, toUser.NickName, (short)toUser.VipLv, content);
        }

        public void SystemSendWhisper(int userId, string userName, short vipLv, string content)
        {
            if (userId.Equals(Language.Instance.SystemUserId.ToString()))
            {
                throw new Exception("不能给系统发私聊");
            }

            var chat = new ChatData
            {
                Version = 0,
                FromUserID = Language.Instance.SystemUserId,
                FromUserName = Language.Instance.KingName,
                FromUserVip = 0,
                ToUserID = userId,
                ToUserName = userName,
                ToUserVip = vipLv,
                ChatType = ChatType.Whisper,
                Content = content,
                SendDate = DateTime.Now,
                LooksId = 0
            };
            SendWhisper(userId, chat);
        }

        public void SendWhisper(GameUser toUser, string content)
        {
            if (_user == null || toUser == null)
            {
                throw new Exception("发送人或接收人为空值");
            }

            _user.ChatDate = DateTime.Now;
            var chat = new ChatData
            {
                Version = 0,
                FromUserID = _user.UserID,
                FromUserName = _user.NickName,
                FromUserVip = (short)_user.VipLv,
                ToUserID = toUser.UserID,
                ToUserName = toUser.NickName,
                ToUserVip = (short)toUser.VipLv,
                ChatType = ChatType.Whisper,
                Content = FilterMessage(content),
                SendDate = DateTime.Now,
                LooksId = _user.LooksId
            };
            SendWhisper(toUser.UserID, chat);

        }


        protected override List<ChatMessage> GetRange(List<ChatMessage> msgList)
        {
            if (msgList.Count > 50)
            {
                int pageCount;
                return msgList.GetPaging(1, 50, out pageCount);
            }
            return msgList;
        }

        protected override bool HasReceive(ChatMessage message)
        {
            var m = message as ChatData;
            if (m == null)
                return false;
            if (m.Version <= _user.ChatVesion)
                return false;
            if (m.SendDate.AddMinutes(MsgTimeOut) <= DateTime.Now)
                return false;
            if (m.ChatType != ChatType)
                return false;
            if (m.ChatType == ChatType.Class)
            {
                if (m.ClassID != _user.ClassData.ClassID)
                    return false;
            }

            //_user.ChatVesion = m.Version;
            return true;
        }

        protected override string FilterMessage(string message)
        {
            foreach (Config_ChatKeyWord chatKeyWord in ChatKeyWordList)
            {
                message = message.Replace(chatKeyWord.KeyWord, new string('*', chatKeyWord.KeyWord.Length));
            }
            return message;
        }

        protected override void WriteLog(ChatMessage message)
        {
            var chatData = message as ChatData;
            if (chatData == null) return;

            int classid = 0;
            if (chatData.ChatType == ChatType.Class)
            {
                GameUser user = UserHelper.FindUser(chatData.FromUserID);
                if (user == null)
                {
                    user = UserHelper.FindUser(chatData.ToUserID);
                }
                if (user != null)
                {
                    classid = user.ClassData.ClassID;
                }
            }
            var chatLog = new UserChatLog
            {
                ChatID = Guid.NewGuid().ToString(),
                FromUserID = chatData.FromUserID.ToString(),
                FromUserName = chatData.FromUserName,
                ToUserID = chatData.ToUserID.ToString(),
                ToUserName = chatData.ToUserName,
                ChatType = chatData.ChatType,
                Content = chatData.Content,
                SendDate = chatData.SendDate,
                ClassID = classid,
            };
            using (var sender = DataSyncManager.GetDataSender())
            {
                sender.Send(chatLog);
            }
        }
    }
}