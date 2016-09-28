using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class Action404 : BaseStruct
    {
        private string errorInfo = string.Empty;

        public Action404(HttpGet httpGet)
            : base(404, httpGet)
        {
        }

        public override bool TakeAction()
        {
            TraceLog.WriteComplement("客户端崩溃日志记录:{0}",errorInfo);
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack("返回中文测试!!!");
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("errorInfo", ref errorInfo))
            {
                return true;
            }
            return false;
        }
    }
}