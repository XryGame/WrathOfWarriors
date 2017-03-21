﻿using ZyGames.Framework.Game.Service;
using GameServer.Script.CsScript.Action;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Model;
using GameServer.Script.Model.Enum;
using GameServer.CsScript.Remote;
using GameServer.Script.Model.DataModel;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 聊天消息
    /// </summary>
    public class Action3001 : BaseAction
    {
        private ChatType _chatType;
        private string _content;
        private int _whisperUserID;

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
            if (httpGet.GetEnum("ChatType", ref _chatType)
                && httpGet.GetString("Content", ref _content)
                && httpGet.GetInt("WhisperUserID", ref _whisperUserID))
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
            switch (_chatType)
            {
                case ChatType.AllService:
                    {
                        ChatRemoteService.SendAllServerChat(GetBasis.UserID, _content);
                    }
                    break;
                case ChatType.World:
                    {
                        ChatRemoteService.SendWorldChat(GetBasis.UserID, _content);
                        // 每日
                        UserHelper.EveryDayTaskProcess(GetBasis.UserID, TaskType.WorldChat, 1);
                    }
                    break;
                case ChatType.Whisper:
                    {
                        ChatRemoteService.SendWhisperChat(GetBasis.UserID, _whisperUserID, _content);
                    }
                    break;
                case ChatType.Guild:
                    {
                        ChatRemoteService.SendGuildChat(GetBasis.UserID, _content);
                    }
                    break;
            }
            return true;
        }

    }
}

