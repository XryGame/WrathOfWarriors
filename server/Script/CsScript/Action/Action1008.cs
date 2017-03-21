using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1008_用户角色详情接口
    /// </summary>
    public class Action1008 : BaseAction
    {
        private JPUserDetailsData receipt;
        private Random random = new Random();
        public Action1008(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1008, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1008;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {

            receipt = new JPUserDetailsData()
            {
                UserId = GetBasis.UserID,
                NickName = GetBasis.NickName,
                Profession = GetBasis.Profession,
                UserLv = GetBasis.UserLv,
                Diamond = GetBasis.DiamondNum,
                BuyDiamond = GetBasis.BuyDiamond,
                VipLv = GetBasis.VipLv,
                Gold = GetBasis.Gold
            };
            receipt.Attribute = GetAttribute;
            receipt.Equips = GetEquips;
            receipt.Package = GetPackage;
            receipt.Soul = GetSoul;
            receipt.Skill = GetSkill;
            receipt.Task = GetTask;
            receipt.Achievement = GetAchievement;


            //receipt.Gold = Util.ConvertGameCoinUnits(GetBasis.Gold);

            //string ddd = Util.ConvertGameCoinString("102K");
            return true;
        }




        public override void TakeActionAffter(bool state)
        {

            for (ChatType type = ChatType.World; type < ChatType.System; ++type)
            {
                //var parameters = new Parameters();
                //parameters["ChatType"] = type;
                //var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, Current, parameters, OpCode.Text, null);
                //ActionFactory.SendAction(Current, ActionIDDefine.Cst_Action3002, packet, (session, asyncResult) => { }, 0);
            }
            string context = "";
            RankType ranktype = RankType.No;
            int rankid = 0;

            if (GetBasis.CombatRankID != 0 && GetBasis.CombatRankID < 10)
            {
                ranktype = RankType.Combat;
                rankid = GetBasis.CombatRankID;
            }

            if (ranktype != RankType.No)
            {
                switch (ranktype)
                {
                    case RankType.Level:
                        context = string.Format("排行榜排名第{0}名的 {1} 上线了！", rankid, GetBasis.NickName);
                        break;
                }

                if (GetBasis.UserLv >= DataHelper.OpenRankSystemUserLevel)
                {
                    ChatRemoteService.SendNotice(NoticeMode.World, context);
                }

            }

            // 通知好友上线
            //foreach (FriendData fd in GetBasis.FriendsData.FriendsList)
            //{
            //    GameSession session = GameSession.Get(fd.UserId);
            //    if (session != null)
            //    {
            //        PushMessageHelper.FriendOnlineNotification(session, GetBasis.UserID);
            //    }
            //}
            //context = "欢迎进入创想学院！";
            //RemoteClient.SendSystemChat(GetBasis.UserID, context);
           

            GetBasis.IsRefreshing = false;

            base.TakeActionAffter(state);
        }
    }
}