using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public abstract class BaseAction : JsonAuthorizeAction
    {
        private ResultData _resultData;

        protected BaseAction(int aActionId, ActionGetter actionGetter)
            : base(aActionId, actionGetter)
        {
            _resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorInfo = "",
            };
        }

        public ChatUser ContextUser
        {
            get
            {
                return new MemoryCacheStruct<ChatUser>().Find(t => t.UserId == Current.UserId);
            }
        }
       

        public void setErrorCode(int errorcode)
        {
            _resultData.ErrorCode = errorcode;
        }

        public void setErrorInfo(string errorinfo)
        {
            _resultData.ErrorInfo = errorinfo;
        }
        public object body
        {
            set { _resultData.Data = value; }
        }



        protected override string BuildJsonPack()
        {
            _resultData.intend(ErrorCode, ErrorInfo);
            string retString = MathUtils.ToJson(_resultData);
            return retString;
        }
    }
}
