using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1404_名人榜CD
    /// </summary>
    public class Action1404 : BaseAction
    {
        private EventStatus receipt = EventStatus.Bad;
        public Action1404(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1404, actionGetter)
        {

        }


        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            if (DateTime.Now < ContextUser.CombatData.LastFailedDate)
            {
                return false;

            }

            TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.CombatData.LastFailedDate);
            float mins = timeSpan.TotalMinutes.ToFloat();
            float surplus = MathUtils.Subtraction(ConfigEnvSet.GetInt("User.CombatFailedCD").ToFloat(), mins, 1.0f);
            int needDiamond = Math.Ceiling(surplus).ToInt();
            if (ContextUser.DiamondNum < needDiamond)
                return false;

            ContextUser.CombatData.LastFailedDate = DateTime.MinValue;
            receipt = EventStatus.Good;

            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needDiamond);
            return true;
        }

    }
}