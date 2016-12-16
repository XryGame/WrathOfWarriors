using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1500_请求好友在线状态
    /// </summary>
    public class Action1500 : BaseAction
    {
        private List<JPFriendOnlineData> receipt;

        public Action1500(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1500, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1500;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new List<JPFriendOnlineData>();
            foreach (var fd in ContextUser.FriendsData.FriendsList)
            {
                JPFriendOnlineData fod = new JPFriendOnlineData();
                fod.UserId = fd.UserId;
                GameSession session = GameSession.Get(fd.UserId);
                if (session != null && session.Connected)
                {
                    fod.IsOnline = true;
                }
                receipt.Add(fod);
            }
            return true;
        }

    }
}