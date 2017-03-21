using GameServer.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.Script.CsScript.Com
{
    class PushMessageHelper
    {
  
        /// <summary>
        /// 第二天在
        /// </summary>
        public static void RestoreUserNotification()
        {
            var list = UserHelper.GetOnlinesList();
            if (list.Count > 0)
                ActionFactory.SendAction(list, ActionIDDefine.Cst_Action1053, null, (s, r) => { }, OpCode.Text, 0);
        }


        /// <summary>
        /// 每日任务更新通知
        /// </summary>
        /// <param name="classid"></param>
        public static void DailyQuestUpdateNotification(GameSession session, TaskType type)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["ID"] = type;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1060, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1060, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 成就更新通知
        /// </summary>
        public static void AchievementUpdateNotification(GameSession session, AchievementType type)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["ID"] = type;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1061, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1061, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 属性改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserAttributeChangedNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1050, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1050, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 升级通知
        /// </summary>
        public static void UserLevelUpNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1052, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1052, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 钻石改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserDiamondChangedNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1051, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1051, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 金币改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserGoldChangedNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1049, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1049, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新物品通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserNewItemNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1058, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1058, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 切磋邀请通知
        /// </summary>
        public static void InviteFightNotification(GameSession session, int inviteuid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["InviterUid"] = inviteuid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1062, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1062, packet, (rsession, asyncResult) => { }, 0);
        }


        /// <summary>
        /// 切磋取消通知
        /// </summary>
        public static void CancelInviteFightNotification(GameSession session, string nickname)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["NickName"] = nickname;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1063, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1063, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 切磋拒绝通知
        /// </summary>
        public static void RefuseInviteFightNotification(GameSession session, string nickname)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["NickName"] = nickname;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1064, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1064, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 开始切磋通知
        /// </summary>
        public static void StartInviteFightNotification(GameSession session, int destUserId, EventStatus result)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["DestUid"] = destUserId;
            parameters["FightResult"] = result;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1065, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1065, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新邮件通知
        /// </summary>
        public static void NewMailNotification(GameSession session, string mailid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["NewMailId"] = mailid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1066, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1066, packet, (rsession, asyncResult) => { }, 0);
        }

        
        /// <summary>
        /// 好友上线通知
        /// </summary>
        public static void FriendOnlineNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["FriendUid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1069, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1069, packet, (rsession, asyncResult) => { }, 0);
        }
        /// <summary>
        /// 好友下线通知
        /// </summary>
        public static void FriendOffineNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["FriendUid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1070, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1070, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 充值成功通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserPaySucceedNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1099, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1099, packet, (rsession, asyncResult) => { }, 0);
        }

    }
}
