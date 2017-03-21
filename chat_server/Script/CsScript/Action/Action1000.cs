using System;
using GameServer.Script.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.Sockets;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// login
    /// </summary>
    public class Action1000 : BaseStruct
    {
        private int _userId;

        public Action1000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1000, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("UserId", ref _userId))
            {
                return true;
            }
            return false;
        }
        

        public override bool TakeAction()
        {
            SessionUser user = null;
            try
            {

                var cache = new MemoryCacheStruct<ChatUser>();
                ChatUser chatUser = cache.Find(t => t.UserId == _userId);

                if (chatUser == null)
                {
                    chatUser = new ChatUser()
                    {
                        UserId = _userId,
                    };
                    cache.TryAdd(_userId.ToString(), chatUser);
                }

                user = new SessionUser() { PassportId = _userId.ToString(), UserId = _userId };
                Current.Bind(user);
                
                return true;
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.ValidateError;
                return false;
            }
        }

        protected override string BuildJsonPack()
        {
            ResultData resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = ErrorCode,
                ErrorInfo = ErrorInfo,
                Data = true
            };
            return MathUtils.ToJson(resultData);
        }
    }
}
