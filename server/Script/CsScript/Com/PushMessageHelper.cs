using GameServer.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
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

        ///// <summary>
        ///// 升级通知
        ///// </summary>
        //public static void UserLevelUpNotification(GameSession session)
        //{
        //    if (session == null || !session.Connected)
        //        return;
        //    var parameters = new Parameters();
        //    var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1052, session, null, OpCode.Text, null);
        //    ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1052, packet, (rsession, asyncResult) => { }, 0);
        //}

        ///// <summary>
        ///// 钻石改变通知
        ///// </summary>
        ///// <param name="session"></param>
        //public static void UserDiamondChangedNotification(GameSession session, UpdateDiamondType updateType)
        //{
        //    if (session == null || !session.Connected)
        //        return;
        //    var parameters = new Parameters();
        //    parameters["UpdateType"] = updateType;
        //    var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1051, session, parameters, OpCode.Text, null);
        //    ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1051, packet, (rsession, asyncResult) => { }, 0);
        //}

        /// <summary>
        /// 货币改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserCoinChangedNotification(GameSession session, CoinType coinType, UpdateCoinOperate updateType)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["CoinType"] = coinType;
            parameters["UpdateType"] = updateType;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1049, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1049, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新物品通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserNewItemNotification(GameSession session, List<ItemData> items)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["Items"] = MathUtils.ToJson(items);
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1058, session, parameters, OpCode.Text, null);
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
        /// 新的好友请求
        /// </summary>
        public static void NewFriendRequestNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["Uid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1054, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1054, packet, (sessions, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新好友通知
        /// </summary>
        public static void NewFriendNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["Uid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1055, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1055, packet, (sessions, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 删除好友通知
        /// </summary>
        public static void FriendRemoveNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["Uid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1056, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1056, packet, (sessions, asyncResult) => { }, 0);
        }
        /// <summary>
        /// 好友赠送通知
        /// </summary>
        public static void FriendGiveAwayNotification(GameSession session, int friendUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["Uid"] = friendUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1057, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1057, packet, (sessions, asyncResult) => { }, 0);
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
        /// 新的公会加入请求通知
        /// </summary>
        public static void NewGuildRequestNotification(GameSession session, int userId)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["UserId"] = userId;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1071, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1071, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新成员加入公会通知
        /// </summary>
        public static void NewGuildMemberNotification(GameSession session, int userId)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["UserId"] = userId;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1072, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1072, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 更新公会成员信息
        /// </summary>
        public static void GuildMemberChangeNotification(GameSession session, int userId)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["UserId"] = userId;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1073, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1073, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 新公会日志通知
        /// </summary>
        public static void NewGuildLogNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1074, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1074, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 公会公告修改通知
        /// </summary>
        public static void GuildNoticeChangeNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1075, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1075, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 加入公会通知
        /// </summary>
        public static void JoinGuildNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1076, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1076, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 更新公会属性通知
        /// </summary>
        public static void GuildBasisChangeNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1077, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1077, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 公会成员上线通知
        /// </summary>
        public static void GuildMemberOnlineNotification(GameSession session, int memberUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["MemberUid"] = memberUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1078, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1078, packet, (rsession, asyncResult) => { }, 0);
        }
        /// <summary>
        /// 公会成员下线通知
        /// </summary>
        public static void GuildMemberOffineNotification(GameSession session, int memberUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["MemberUid"] = memberUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1079, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1079, packet, (rsession, asyncResult) => { }, 0);
        }
        /// <summary>
        /// 一个公会成员移除通知
        /// </summary>
        public static void GuildMemberRemoveNotification(GameSession session, int memberUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["MemberUid"] = memberUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1080, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1080, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 一个请求入会申请移除
        /// </summary>
        public static void GuildApplyRemoveNotification(GameSession session, int applyUid)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["ApplyUid"] = applyUid;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1081, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1081, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 通天塔新日志通知
        /// </summary>
        /// <param name="session"></param>
        public static void NewCombatLogNotification(GameSession session)
        {
            if (session == null || !session.Connected)
                return;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1082, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1082, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 获得新宠物通知
        /// </summary>
        public static void NewElfNotification(GameSession session, int elfId)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["ElfID"] = elfId;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1083, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1083, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 获得新技能通知
        /// </summary>
        public static void NewSkillNotification(GameSession session, int skillId)
        {
            if (session == null || !session.Connected)
                return;
            var parameters = new Parameters();
            parameters["SkillID"] = skillId;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1084, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1084, packet, (rsession, asyncResult) => { }, 0);
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
