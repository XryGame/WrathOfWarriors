/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Runtime;
using GameServer.Script.CsScript;
using ZyGames.Framework.RPC.Sockets;
using ZyGames.Framework.Common.Timing;
using GameServer.CsScript.Base;
using ZyGames.Framework.Cache.Generic;
using GameServer.Script.Model;
using ZyGames.Framework.Script;
using ZyGames.Framework.Game.Com.Rank;

namespace Game.Script
{
    public class MainClass : GameWebSocketHost
    {
        public MainClass()
        {
            GameEnvironment.Setting.ActionDispatcher = new WebSocketActionDispatcher();
        }
        protected override void OnStartAffer()
        {
            TimeListener.Append(PlanConfig.EveryMinutePlan(MsgDispatcher.Dispatcher, "Dispatcher", "00:00", "23:59", 1));

            TimeListener.Append(PlanConfig.EveryMinutePlan(DoEveryDayRefreshDataTask, "EveryDayRefreshDataTask", "00:00", "23:59", 600));
            //TimeListener.Append(PlanConfig.EveryDayPlan(DoEveryDayRefreshDataTask, "EveryDayRefreshDataTask", "03:10"));

            ServerSet.LoadServerConfig();
            
            RankingFactory.Add(new LevelRanking());
            //RankingFactory.Add(new GuildRanking());
            RankingFactory.Start(60);

            // 设置竞技场排行不刷新
            Ranking<UserRank> levelRanking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            levelRanking.SetIntervalTimes(int.MaxValue);

            //// 设置公会排行不刷新
            //Ranking<GuildRank> guildRanking = RankingFactory.Get<GuildRank>(GuildRanking.RankingKey);
            //guildRanking.SetIntervalTimes(int.MaxValue);

            LevelRankingAllServerSet.LoadServerRanking();
        }
        

        protected override void OnHandshaked(ISocket sender, ConnectionEventArgs e)
        {
            //Console.WriteLine("Client {0} connect to server.", e.Socket.RemoteEndPoint);
            base.OnHandshaked(sender, e);
        }

        protected override void OnRequested(ActionGetter actionGetter, BaseGameResponse response)
        {
            //Console.WriteLine("Receice actionId:{0}", actionGetter.GetActionId());
            base.OnRequested(actionGetter, response);
        }

        protected override void OnError(ISocket sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Server send to {0} data error.", e.Socket.RemoteEndPoint);
            base.OnError(sender, e);
        }

        protected override void OnClosedStatus(ExSocket socket, int closeStatusCode)
        {
            //Console.WriteLine("Receive client {0} close status code {1}.", socket.RemoteEndPoint, closeStatusCode);
        }

        protected override void OnDisconnected(GameSession session)
        {
            var user = session.User as SessionUser;
            if (user != null)
            {
                var cache = new MemoryCacheStruct<ChatUser>();
                ChatUser chatUser = cache.Find(t => t.UserId == user.UserId);
                if (chatUser != null && session.SessionId == chatUser.SessionId)
                {
                    cache.TryRemove(user.UserId.ToString());
                }
            }

            base.OnDisconnected(session);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="planconfig"></param>
        public void DoEveryDayRefreshDataTask(PlanConfig planconfig)
        {
            if (ScriptEngines.IsCompiling)
            {
                return;
            }
            //do something
            //LevelRankingTop50Set.LoadServerRanking();
            LevelRankingAllServerSet.LoadServerRanking();
        }
    }
}