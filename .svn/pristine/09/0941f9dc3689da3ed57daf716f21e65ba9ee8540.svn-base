using GameServer.CsScript.Com;
using GameServer.CsScript.GM;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Game.Command;
using ZyGames.Framework.Game.Config;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// Send chat
    /// </summary>
    public class Action3001 : BaseAction
    {
        private ChatType _chattype;
        private string _message;
        private int _whisperuid;
        private bool IsSendSucceed = false;
        public Action3001(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action3001, actionGetter)
        {
            IsNotRespond = true;
        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetString("Message", ref _message)
                && !string.IsNullOrEmpty(_message)
                && httpGet.GetEnum("ChatType", ref _chattype)
                && httpGet.GetInt("WhisperUid", ref _whisperuid))
            {
                if (_chattype != ChatType.System)
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
            
            if (_chattype == ChatType.Class && ContextUser.ClassData.ClassID == 0)
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.St3001_ChaTypeNotGuildMember;
                return false;
            }

            if (_message.Trim().Length == 0)
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.St3001_ContentNotEmpty;
                return false;
            }

            if (!TryXChatService.IsAllow(ContextUser, _chattype))
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.St3001_ChatNotSend;
                return false;
            }



            bool IsGmCommand = true;
            var section = ConfigManager.Configger.GetFirstOrAddConfig<MiddlewareSection>();
            if (section.EnableGM)
            {
                try
                {
                    string lower = _message.ToLower();
                    if (_message.Trim() != "" && lower.StartsWith("gm ", StringComparison.OrdinalIgnoreCase))
                    {
                        TryXGMCommand command = null; 
                        if ("gm cache".StartsWith(lower))
                        {
                            CacheFactory.UpdateNotify(true);
                        }
                        else if (lower.StartsWith("gm diamond"))
                        {
                            command = new DiamondCommand();
                        }
                        else if (lower.StartsWith("gm pay"))
                        {
                            command = new PayMoneyCommand();
                        }
                        else if (lower.StartsWith("gm weekcard"))
                        {
                            command = new PayWeekCardCommand();
                        }
                        else if (lower.StartsWith("gm monthcard"))
                        {
                            command = new PayMonthCardCommand();
                        }
                        else
                        {
                            var chatService = new TryXChatService(ContextUser);
                            chatService.SystemSendWhisper(ContextUser, string.Format(TryXGMCommand.CmdError, _message));

                            _chattype = ChatType.Whisper;
                            IsSendSucceed = true;
                        }
                        if (command != null)
                        {
                            command.Parse(ContextUser.UserID, _message);
                            command.ProcessCmd();
                        }
                            
                    }
                    else
                    {
                        IsGmCommand = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    SaveLog(ex);
                    ErrorCode = Language.Instance.ErrorCode;
                    ErrorInfo = ex.Message;
                    return false;
                }
            }

            if (IsGmCommand)
            {
                var chatService = new TryXChatService(ContextUser);
                chatService.SystemSendWhisper(ContextUser, _message);

                _chattype = ChatType.Whisper;
                IsSendSucceed = true;
            }
            else
            {
                //NoviceHelper.WingFestival(ContextUser.UserID, _content);
                //NoviceHelper.WingZhongYuanFestival(ContextUser, _content);
                //使用聊天道具
                //UserItemHelper.UseUserItem(ContextUser.UserID, chatItemId, 1);
                var chatService = new TryXChatService(ContextUser);
                if (_chattype == ChatType.Whisper)
                {
                    GameUser whisper = UserHelper.FindUser(_whisperuid);
                    if (whisper != null)
                        chatService.SendWhisper(whisper, _message);
                }
                else
                {
                    chatService.Send(_chattype, _message);
                }
                IsSendSucceed = true;
            }

            return true;
        }

        protected override string BuildJsonPack()
        {
            body = true;
            return base.BuildJsonPack();
        }


        public override void TakeActionAffter(bool state)
        {
            if (IsSendSucceed)
            {
                switch (_chattype)
                {
                    case ChatType.World:
                        {
                            PushMessageHelper.SendWorldChatToOnlineUser();
                        }
                        break;
                    case ChatType.Class:
                        {
                            PushMessageHelper.SendClassChatToClassMember(ContextUser.ClassData.ClassID);
                        }
                        break;
                    case ChatType.Whisper:
                        {
                            PushMessageHelper.SendWhisperChatToUser(ContextUser.UserID, _whisperuid);
                        }
                        break;
                }
            }

            
            base.TakeActionAffter(state);
        }
    }
}
