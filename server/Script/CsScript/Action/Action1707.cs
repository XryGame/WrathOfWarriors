using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 回复加入公会的请求
    /// </summary>
    public class Action1707 : BaseAction
    {
        private RequestGuildResult receipt;
        private bool _isAgree;
        private int _DestUid;

        public Action1707(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1707, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetBool("IsAgree", ref _isAgree)
                && httpGet.GetInt("DestUid", ref _DestUid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guildData = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
            if (guildData == null)
            {
                return false;
            }

            var guildcfg = new ShareCacheStruct<Config_Society>().FindKey(guildData.Lv);
            if (guildcfg == null)
            {
                return false;
            }


            if (guildData.FindAtevent().UserID != Current.UserId
                && guildData.FindVice(Current.UserId) == null)
            {
                receipt = RequestGuildResult.NoAuthority;
                return true;
            }

            var basis = UserHelper.FindUserBasis(_DestUid);
            var guild = UserHelper.FindUserGuild(_DestUid);
            if (basis == null)
            {
                return false;
            }



            if (_isAgree)
            {
                if (guildData.MemberList.Count >= guildcfg.Number)
                {
                    receipt = RequestGuildResult.Full;
                }

                else if (!guild.GuildID.IsEmpty())
                {
                    receipt = RequestGuildResult.HadGuild;
                }
                else
                {
                    GuildCharacter member = new GuildCharacter()
                    {
                        UserID = basis.UserID,
                        JobTitle = GuildJobTitle.Normal,
                        Liveness = 0
                    };

                    GuildLogData log = new GuildLogData()
                    {
                        UserId = basis.UserID,
                        UserName = basis.NickName,
                        LogTime = DateTime.Now,
                        Content = string.Format("玩家 {0} 加入了公会。", basis.NickName),
                    };
                    guildData.NewLog(log);

                    // 通知公会成员有新成员加入
                    foreach (var v in guildData.MemberList)
                    {
                        PushMessageHelper.NewGuildMemberNotification(GameSession.Get(v.UserID), _DestUid);
                        PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
                    }

                    guildData.AddNewMember(member);
                    guild.GuildID = guildData.GuildID;
                    guild.IsSignIn = false;

                    // 通知新成员公会信息
                    PushMessageHelper.JoinGuildNotification(GameSession.Get(_DestUid));

                    receipt = RequestGuildResult.Successfully;
                }

            }
            // 从邀请列表里清除
            guildData.RemoveRequest(_DestUid);
            
            foreach (var v in guildData.MemberList)
            {
                PushMessageHelper.GuildApplyRemoveNotification(GameSession.Get(v.UserID), _DestUid);
            }
            return true;
        }
    }
}