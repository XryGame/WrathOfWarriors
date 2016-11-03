using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10200_领取首充奖励
    /// </summary>
    public class Action10200 : BaseAction
    {
        private JPRequestSFOData receipt;
        private int AwardDiamondNum = ConfigEnvSet.GetInt("System.FirstPayAwardDiamondNum");
        private int AwardItemId = ConfigEnvSet.GetInt("System.FirstPayAwardItemID");
        private int AwardSkillBookId = ConfigEnvSet.GetInt("System.FirstPayAwardSkillBookID");
        public Action10200(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10200, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action10200;
            }
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            UserPayCache usepay = UserHelper.FindUserPay(ContextUser.UserID);
            if (usepay == null || usepay.PayMoney == 0)
                return false;
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Bad;
            if (usepay.IsReceiveFirstPay)
                return true;
            usepay.IsReceiveFirstPay = true;
            receipt.Result = EventStatus.Good;

            UserHelper.GiveAwayDiamond(ContextUser.UserID, AwardDiamondNum);
            Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(AwardItemId);
            if (item != null)
            {
                ContextUser.UserAddItem(AwardItemId, 1);
            }
            item = new ShareCacheStruct<Config_Item>().FindKey(AwardSkillBookId);
            if (item != null)
            {
                ContextUser.UserAddItem(AwardSkillBookId, 1);

                if (item.Type == ItemType.Skill)
                {
                    ContextUser.CheckAddSkillBook(AwardSkillBookId, 1);
                }
            }

            receipt.AwardDiamondNum = AwardDiamondNum;
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.AwardItemList.Add(AwardItemId);
            receipt.AwardItemList.Add(AwardSkillBookId);
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}