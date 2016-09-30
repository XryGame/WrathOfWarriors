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
        /// 发送公告消息到在线用户通知
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        public static void SendNoticeToOnlineUser(NoticeType type, string context)
        {
            var parameter = new Parameters();
            parameter["NoticeType"] = type;
            parameter["Context"] = context;
            ActionFactory.SendAction(GameSession.GetOnlineAll(), ActionIDDefine.Cst_Action3003, parameter, (s, r) => { }, OpCode.Text, 0);
        }
        /// <summary>
        /// 发送公告消息到用户通知
        /// </summary>
        /// <param name="session"></param>
        /// <param name="type"></param>
        /// <param name="context"></param>
        public static void SendNoticeToUser(GameSession session, NoticeType type, string context)
        {
            var parameter = new Parameters();
            parameter["NoticeType"] = type;
            parameter["Context"] = context;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, session, parameter, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action3002, packet, (sessionUser, asyncResult) => { }, 0);
        }
        /// <summary>
        /// 发送世界消息通知
        /// </summary>
        public static void SendWorldChatToOnlineUser()
        {
            var parameters = new Parameters();
            parameters["ChatType"] = ChatType.World;
            ActionFactory.SendAction(GameSession.GetOnlineAll(), ActionIDDefine.Cst_Action3002, parameters, (s, r) => { }, OpCode.Text, 0);
        }
        /// <summary>
        /// 发送聊天消息到指定班级成员通知
        /// </summary>
        /// <param name="classid"></param>
        public static void SendClassChatToClassMember(int classid)
        {
            var userList = new List<IUser>();
            ClassDataCache classes = new ShareCacheStruct<ClassDataCache>().FindKey(classid);
            foreach (int memuid in classes.MemberList)
            {
                var mem = UserHelper.FindUser(memuid);
                if (mem != null)
                {
                    userList.Add(new SessionUser(mem));
                }
            }
            var parameters = new Parameters();
            parameters["ChatType"] = ChatType.Class;
            ActionFactory.SendAction(userList, ActionIDDefine.Cst_Action3002, parameters, (asyncResult) => { }, OpCode.Text, 0);
        }
        /// <summary>
        /// 发送私聊消息通知
        /// </summary>
        /// <param name="whisperuid"></param>
        public static void SendWhisperChatToUser(int fromuid, int whisperuid)
        {
            var wsession = GameSession.Get(whisperuid);
            if (wsession != null)
            {
                var parameters = new Parameters();
                parameters["ChatType"] = ChatType.Whisper;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, wsession, parameters, OpCode.Text, null);
                ActionFactory.SendAction(wsession, ActionIDDefine.Cst_Action3002, packet, (session, asyncResult) => { }, 0);
            }
            wsession = GameSession.Get(fromuid);
            if (wsession != null)
            {
                var parameters = new Parameters();
                parameters["ChatType"] = ChatType.Whisper;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, wsession, parameters, OpCode.Text, null);
                ActionFactory.SendAction(wsession, ActionIDDefine.Cst_Action3002, packet, (session, asyncResult) => { }, 0);
            }
        }
        /// <summary>
        /// 发送系统消息到在线用户通知
        /// </summary>
        public static void SendSystemChatToOnlineUser()
        {
            var parameters = new Parameters();
            parameters["ChatType"] = ChatType.System;
            ActionFactory.SendAction(GameSession.GetOnlineAll(), ActionIDDefine.Cst_Action3002, parameters, (s, r) => { }, OpCode.Text, 0);
        }

        /// <summary>
        /// 发送系统消息到指定用户通知
        /// </summary>
        public static void SendSystemChatToUser(GameSession session)
        {
            var parameters = new Parameters();
            parameters["ChatType"] = ChatType.System;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action3002, packet, (rsession, asyncResult) => { }, 0);
        }



        /// <summary>
        /// 第二天在
        /// </summary>
        public static void RestoreUserNotification()
        {
            var onlinelist = GameSession.GetOnlineAll();
            if (onlinelist.Count > 0)
                ActionFactory.SendAction(GameSession.GetOnlineAll(), ActionIDDefine.Cst_Action1053, null, (s, r) => { }, OpCode.Text, 0);
        }

        /// <summary>
        /// 班级班长更换通知
        /// </summary>
        /// <param name="classid"></param>
        public static void ClassMonitorChangeNotification(int classid)
        {
            var userList = new List<IUser>();
            ClassDataCache classes = new ShareCacheStruct<ClassDataCache>().FindKey(classid);
            foreach (int memuid in classes.MemberList)
            {
                var mem = UserHelper.FindUser(memuid);
                if (mem != null)
                {
                    userList.Add(new SessionUser(mem));
                }
            }

            ActionFactory.SendAction(userList, ActionIDDefine.Cst_Action1059, null, (asyncResult) => { }, OpCode.Text, 0);
        }

        /// <summary>
        /// 每日任务完成通知
        /// </summary>
        /// <param name="classid"></param>
        public static void DailyQuestFinishNotification(GameSession session)
        {
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1060, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1060, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 成就完成通知
        /// </summary>
        public static void AchievementFinishNotification(GameSession session, int id)
        {
            var parameters = new Parameters();
            parameters["ID"] = id;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1061, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1061, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 战斗力改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserFightValueChangedNotification(GameSession session)
        {
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1050, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1050, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 升级通知
        /// </summary>
        public static void UserLevelUpNotification(GameSession session, bool ischangeclass)
        {
            var parameters = new Parameters();
            parameters["IsChangeClass"] = ischangeclass;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1052, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1052, packet, (rsession, asyncResult) => { }, 0);
        }

        /// <summary>
        /// 钻石改变通知
        /// </summary>
        /// <param name="session"></param>
        public static void UserDiamondChangedNotification(GameSession session)
        {
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1051, session, null, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1051, packet, (rsession, asyncResult) => { }, 0);
        }


        /// <summary>
        /// 切磋邀请通知
        /// </summary>
        public static void InviteFightNotification(GameSession session, int inviteuid)
        {
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
            var parameters = new Parameters();
            parameters["DestUid"] = destUserId;
            parameters["FightResult"] = result;
            var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1065, session, parameters, OpCode.Text, null);
            ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1065, packet, (rsession, asyncResult) => { }, 0);
        }
    }
}
