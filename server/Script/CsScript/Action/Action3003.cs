using ZyGames.Framework.Game.Service;
using GameServer.Script.CsScript.Action;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Model;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 公告列表接口
    /// </summary>
    public class Action3003 : BaseAction
    {
        private JPBroadcastData receipt;
        //private List<NoticeMessage> broadcastlist;

        private NoticeType _noticetype;
        private string _context;
        //private string _sender;

        public Action3003(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action3003, actionGetter)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("NoticeType", ref _noticetype)
                && httpGet.GetString("Context", ref _context))
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
            receipt = new JPBroadcastData()
            {
                Type = _noticetype,
                Context = _context
            };
            return true;
        }


        protected override string BuildJsonPack()
        {
            if (receipt != null)
                body = receipt;
            else
                body = true;
            return base.BuildJsonPack();
        }

    }
}

