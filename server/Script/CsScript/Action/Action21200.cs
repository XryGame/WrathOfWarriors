using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 21200_争霸赛申请报名
    /// </summary>
    public class Action21200 : BaseAction
    {
        private EventStatus receipt;

        public Action21200(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action21200, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = EventStatus.Bad;

            if (SystemGlobal.competition64.Stage != Com.CompetitionStage.ApplyStart)
            {
                return true;
            }

            var cacache = new ShareCacheStruct<CompetitionApply>();
            var findv = cacache.FindKey(GetBasis.UserID);
            if (findv != null)
            {
                return true;
            }
            CompetitionApply apply = new CompetitionApply()
            {
                UserId = GetBasis.UserID,
                NickName = GetBasis.NickName,
                ApplyDate = DateTime.Now
            };
            cacache.Add(apply);
            cacache.Update();
            receipt = EventStatus.Good;
            return true;
        }
    }
}