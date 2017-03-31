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
    /// 领取VIP礼包
    /// </summary>
    public class Action1920 : BaseAction
    {
        private bool receipt;
        private Random random = new Random();
        public Action1920(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1920, actionGetter)
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
            if (GetBasis.VipGiftProgress >= GetBasis.VipLv)
            {
                return true;
            }
            
            var vip = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipGiftProgress + 1);
            if (vip == null)
            {
                return false;
            }

            GetBasis.VipGiftProgress++;


            //switch (vip.ObtainType)
            //{
            //    case VipObtainType.Diamond:
            //        {
            //            UserHelper.RewardsDiamond(Current.UserId, vip.ObtainNum);
            //            receipt.AwardDiamondNum = vip.ObtainNum;
            //            receipt.CurrDiamond = GetBasis.DiamondNum;
            //        }
            //        break;
            //    case VipObtainType.RandItemSkillBook:
            //        {
            //            int count = vip.ObtainNum;
            //            while (count > 0)
            //            {
            //                count--;
            //                if (random.Next(1000) < 750)
            //                {// 道具
            //                    receipt.AwardItemList.AddRange(GetBasis.RandItem(1));
            //                }
            //                else
            //                {// 技能
            //                    receipt.AwardItemList.AddRange(GetBasis.RandSkillBook(1));
            //                }
            //            }

            //        }
            //        break;
            //    case VipObtainType.Item:
            //        {
            //            Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(vip.ObtainNum);
            //            if (item != null)
            //            {
            //                GetBasis.UserAddItem(vip.ObtainNum, 1);

            //                if (item.Type == ItemType.Skill)
            //                {
            //                    GetBasis.CheckAddSkillBook(vip.ObtainNum, 1);
            //                }
            //                receipt.AwardItemList.Add(vip.ObtainNum);
            //            }
            //        }
            //        break;
            //}

            //receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt = true;
            return true;
        }
    }
}