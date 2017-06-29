using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Com.Rank;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.CsScript.Com;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using GameServer.Script.CsScript.Com;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;
using System.Configuration;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoFight
    {

        public class FightBot
        {
            public int UserId;
            /// <summary>
            /// 邀请时间
            /// </summary>
            public DateTime InviteTime;

            /// <summary>
            /// 邀请机器人战斗的玩家UserId
            /// </summary>
            public int PlayerUserId;

            /// <summary>
            /// 被邀请人的UserId
            /// </summary>
            public int DestUserId;

            public bool IsCanFight()
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(InviteTime);
                int inviteSecond = timeSpan.TotalSeconds.ToInt();
                return inviteSecond >= 3;
            }
        }
        
        

        private static List<FightBot> FightList = new List<FightBot>();
        

        
        public static void AddFightBot(FightBot fbot)
        {
            lock (FightList)
            {
                var player = FightList.Find(t => t.PlayerUserId == fbot.PlayerUserId);
                if (player == null)
                {
                    FightList.Add(fbot);
                }
                    
            }
            
        }
        public static void RemoveFightBot(int userId)
        {
            
            lock (FightList)
            {
                var player = FightList.Find(t => t.PlayerUserId == userId);
                if (player != null)
                {
                    FightList.Remove(player);
                }
                
            }

        }

        public static void FightResponse()
        {
            var list = FightList.FindAll(t => (t.IsCanFight()));
            foreach (var v in list)
            {
                var player = UserHelper.FindUserBasis(v.PlayerUserId);
                var playerAtt = UserHelper.FindUserAttribute(v.PlayerUserId);
                var destAtt = UserHelper.FindUserAttribute(v.DestUserId);
                if (player == null)
                    continue;
                player.UserStatus = UserStatus.Fighting;
                //bot.UserStatus = UserStatus.Fighting;

                EventStatus retresult = EventStatus.Good;
                //float diff = (float)GetBasis.GetCombatFightValue() / dest.GetCombatFightValue();
                float diff = (float)playerAtt.FightValue / destAtt.FightValue;
                if (diff > 1.1f)
                {
                    retresult = EventStatus.Good;
                }
                else if (diff < 0.9f)
                {
                    retresult = EventStatus.Bad;
                }


                PushMessageHelper.StartInviteFightNotification(GameSession.Get(player.UserID), v.DestUserId, retresult);
            }


            lock (FightList)
            {

                foreach (var v in list)
                {
                    FightList.Remove(v);
                }
            }
        }
        

    }
}