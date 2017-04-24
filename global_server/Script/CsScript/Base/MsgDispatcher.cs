using GameServer.CsScript.Action;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;
using ZyGames.Framework.Script;

namespace GameServer.CsScript.Base
{

    public class MsgData
    {
        public int UserId;

        public MsgType Type;

        public Parameters Param;

        public bool IsRemove;
    }
    /// <summary>
    /// 消息分发器
    /// </summary>
    public static class MsgDispatcher
    {
        private static CacheList<MsgData> MsgList = new CacheList<MsgData>();


        public static void Push(MsgData data)
        {
            MsgList.Add(data);
        }
        
        public static void Dispatcher(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }

            if (MsgList.Count <= 0)
            {
                return;
            }
                

            int count = Math.Min(MsgList.Count, 100);
            var list = MsgList.GetListRange(0, count);


            foreach (var v in list)
            {
                v.IsRemove = true;
                GameSession session = GameSession.Get(v.UserId);
                if (session != null && session.Connected)
                {
                    switch (v.Type)
                    {
                        case MsgType.Chat:
                            {
                                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action2000,
                                                    session, v.Param, OpCode.Text, null
                                                    );
                                ActionFactory.SendAction(session, ActionIDDefine.Cst_Action2000, packet, (rsession, asyncResult) => { }, 0);
                            }
                            break;
                        case MsgType.Notice:
                            {
                                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action2001,
                                                    session, v.Param, OpCode.Text, null
                                                    );
                                ActionFactory.SendAction(session, ActionIDDefine.Cst_Action2001, packet, (rsession, asyncResult) => { }, 0);
                            }
                            break;
                    }

                }
            }

            MsgList.RemoveAll(t => t.IsRemove == true);
        }
    }
}