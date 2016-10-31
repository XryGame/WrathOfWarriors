using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1301_开宝箱 
    /// </summary>
    public class Action1301 : BaseAction
    {
        private JPOpenBoxData receipt;
        private OpenBoxType opentype;
        private OpenBoxMode openmode;
        private Random random = new Random();

        public Action1301(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1301, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1301;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("OpenType", ref opentype)
                && httpGet.GetEnum("OpenMode", ref openmode))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            int count = openmode.ToInt();
            int needdiamond = 0;
            switch (openmode)
            {
                case OpenBoxMode.Once:
                    needdiamond = DataHelper.OpenBoxOnceNeedDiamond;
                    break;
                case OpenBoxMode.Consecutive:
                    needdiamond = DataHelper.OpenBoxConsecutiveNeedDiamond;
                    break;
            }
            if (ContextUser.DiamondNum < needdiamond)
            {
                ErrorInfo = Language.Instance.NoDiamondError;
                return true;
            }

            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needdiamond);


            receipt = new JPOpenBoxData();
            receipt.Type = opentype;
            switch (opentype)
            {
                case OpenBoxType.Item:
                    {
                        receipt.IDList = ContextUser.RandItem(count);
                        // 每日
                        if (ContextUser.DailyQuestData.ID == TaskType.RandItem)
                        {
                            ContextUser.DailyQuestData.IsFinish = true;
                            PushMessageHelper.DailyQuestFinishNotification(Current);
                        }

                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, receipt.IDList.Count, AchievementType.AwardItemCount);
                    }

                    break;
                case OpenBoxType.Skill:
                    {
                        receipt.IDList = ContextUser.RandSkillBook(count);
                        // 每日
                        if (ContextUser.DailyQuestData.ID == TaskType.RandSkillBook)
                        {
                            ContextUser.DailyQuestData.IsFinish = true;
                            PushMessageHelper.DailyQuestFinishNotification(Current);
                        }

                        // 成就
                        UserHelper.AchievementProcess(ContextUser.UserID, receipt.IDList.Count, AchievementType.AwardItemCount);
                    }
                    break;
            }
            ContextUser.RefreshFightValue();
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;
            receipt.CurrFightValue = ContextUser.FightingValue;
            return true;
        }
    }
}