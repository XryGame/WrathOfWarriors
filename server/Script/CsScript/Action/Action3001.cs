using ZyGames.Framework.Game.Service;
using GameServer.Script.CsScript.Action;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Model;
using GameServer.Script.Model.Enum;
using GameServer.CsScript.Remote;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Common;

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
            return true;
        }

        public override void TakeActionAffter(bool state)
        {
            switch (_chatType)
            {
                case ChatType.AllService:
                    {
                        GlobalRemoteService.SendAllServerChat(Current.UserId, _content);
                    }
                    break;
                case ChatType.World:
                    {
                        GlobalRemoteService.SendWorldChat(Current.UserId, _content);
                        // 每日
                        UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.WorldChat, 1);
                    }
                    break;
                case ChatType.Whisper:
                    {
                        GlobalRemoteService.SendWhisperChat(Current.UserId, _whisperUserID, _content);
                    }
                    break;
                case ChatType.Guild:
                    {
                        if (!GetGuild.GuildID.IsEmpty())
                        {
                            GlobalRemoteService.SendGuildChat(Current.UserId, _content);
                        }
                    }
                    break;
            }
        }
    }
}

