using ZyGames.Framework.Game.Service;
using System.Collections.Generic;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model.DataModel;
using GameServer.Script.CsScript.Action;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.Enum;
using GameServer.CsScript.Base;
using GameServer.Script.Model.Config;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// chat list
    /// </summary>
    public class Action3002 : BaseAction
    {
        private JPListChatData receipt;
        private List<ChatMessage> chatList;
        private ChatType chattype;

        public Action3002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action3002, actionGetter)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("ChatType", ref chattype))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            var chatservice = new TryXChatService(ContextUser, chattype);
            //ContextUser.ChatVesion = chatservice.CurrVersion;
            chatList = chatservice.PopMessages();
            return true;
        }


        protected override string BuildJsonPack()
        {
            receipt = new JPListChatData();
            receipt.ChatType = chattype;
            //var result = new JPChatData[chatList.Count];
            for (int i = 0; i < chatList.Count; i++)
            {
                var chat = chatList[i] as ChatData;
                JPChatData JPChatData = new JPChatData()
                {
                    Id = chat.Version,
                    Type = chattype,
                    UserId = chat.FromUserID,
                    Message = chat.Content,
                    Sender = chat.FromUserName,
                    SendTime = chat.SendDate.ToString("HH:mm:ss"),
                    SendTimestamp = Util.ConvertDateTimeStamp(chat.SendDate),
                    LooksId = chat.LooksId

                };
                receipt.List.Add(JPChatData);
            }
            body = receipt;
            return base.BuildJsonPack();
        }


    }
}

