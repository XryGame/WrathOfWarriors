using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.JsonProtocol
{
    /// <summary>
    /// Json result data
    /// </summary>
    public class ResultData
    {
        public ResultData()
        {

        }
        public ResultData(ref ActionGetter actionGetter)
        {
            MsgId = actionGetter.GetMsgId();
            ActionId = actionGetter.GetActionId();
        }

        public void intend(int errorcode, string errorinfo)
        {
            ErrorCode = errorcode;
            ErrorInfo = errorinfo;
        }
        /// <summary>
        /// 
        /// </summary>
        public int MsgId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorInfo { get; set; }

        /// <summary>
        /// logic data
        /// </summary>
        public object Data { get; set; }
    }
}
