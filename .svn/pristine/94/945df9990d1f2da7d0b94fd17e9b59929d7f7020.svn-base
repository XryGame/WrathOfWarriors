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
    /// 10900_领取VIP礼包
    /// </summary>
    public class Action10900 : BaseAction
    {
        private JPRequestSFOData receipt;
        private Random random = new Random();
        public Action10900(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10900, actionGetter)
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
            receipt = new JPRequestSFOData();
            if (ContextUser.VipGiftProgress >= ContextUser.VipLv)
            {
                receipt.ReturnId = ContextUser.VipLv;
                receipt.Result = EventStatus.Bad;
                return true;
            }




            var vip = new ShareCacheStruct<Config_Vip>().FindKey(ContextUser.VipGiftProgress + 1);
            if (vip == null)
            {
                return false;
            }

            ContextUser.VipGiftProgress++;
            receipt.ReturnId = ContextUser.VipGiftProgress;
            receipt.Result = EventStatus.Good;

            switch (vip.ObtainType)
            {
                case VipObtainType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, vip.ObtainNum);
                        receipt.AwardDiamondNum = vip.ObtainNum;
                        receipt.CurrDiamond = ContextUser.DiamondNum;
                    }
                    break;
                case VipObtainType.RandItemSkillBook:
                    {
                        if (random.Next(1000) < 750)
                        {// 道具
                            receipt.AwardItemList = ContextUser.RandItem(vip.ObtainNum);
                        }
                        else
                        {// 技能
                            receipt.AwardItemList = ContextUser.RandSkillBook(vip.ObtainNum);
                        }
                    }
                    break;
                case VipObtainType.Item:
                    {
                        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(vip.ObtainNum);
                        if (item != null)
                        {
                            ContextUser.UserAddItem(vip.ObtainNum, 1);

                            if (item.Type == ItemType.Skill)
                            {
                                ContextUser.CheckAddSkillBook(vip.ObtainNum, 1);
                            }
                            receipt.AwardItemList.Add(vip.ObtainNum);
                        }
                    }
                    break;
            }

            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}