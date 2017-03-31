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
    /// 兑换CDK
    /// </summary>
    public class Action1930 : BaseAction
    {
        private JPRequestCdKeyData receipt;
        private Random random = new Random();
        private string _CDKey;
        public Action1930(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1930, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("CDKey", ref _CDKey))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestCdKeyData();
            receipt.Result = ReceiveCdKeyResult.OK;


            var acc = new ShareCacheStruct<Config_CdKey>().FindKey(_CDKey);
            if (acc == null)
            {
                receipt.Result = ReceiveCdKeyResult.Invalid;
                return true;
            }
            var cdkscache = new ShareCacheStruct<CDKeyCache>();
            if (cdkscache.FindKey(_CDKey) != null)
            {
                receipt.Result = ReceiveCdKeyResult.Received;
                return true;
            }

            CDKeyCache cdk = new CDKeyCache()
            {
                CDKey = _CDKey,
                UsedTime = DateTime.Now
            };

            cdkscache.Add(cdk);
            cdkscache.Update();

            int randcount = 0;
            List<int> itemlist = new List<int>();
            int diamond = 0;
            if (acc.AwardA == 1) randcount++;
            else if (acc.AwardA >= 10000) itemlist.Add(acc.AwardA);
            else diamond += acc.AwardA;
            if (acc.AwardB == 1) randcount++;
            else if (acc.AwardB >= 10000) itemlist.Add(acc.AwardB);
            else diamond += acc.AwardB;
            if (acc.AwardC == 1) randcount++;
            else if (acc.AwardC >= 10000) itemlist.Add(acc.AwardC);
            else diamond += acc.AwardC;
            if (acc.AwardD == 1) randcount++;
            else if (acc.AwardD >= 10000) itemlist.Add(acc.AwardD);
            else diamond += acc.AwardD;

            //for (int i = 0; i < randcount; ++i)
            //{
            //    if (random.Next(1000) < 750)
            //    {// 道具
            //        receipt.AwardItemList.AddRange(GetBasis.RandItem(1));
            //    }
            //    else
            //    {// 技能
            //        receipt.AwardItemList.AddRange(GetBasis.RandSkillBook(1));
            //    }
            //}

            //foreach (var it in itemlist)
            //{
            //    Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(it);
            //    if (item != null)
            //    {
            //        GetBasis.UserAddItem(it, 1);

            //        if (item.Type == ItemType.Skill)
            //        {
            //            GetBasis.CheckAddSkillBook(it, 1);
            //        }
            //        receipt.AwardItemList.Add(it);
            //    }
            //}

            //if (diamond > 0)
            //{
            //    UserHelper.RewardsDiamond(Current.UserId, diamond);
            //    receipt.AwardDiamondNum = diamond;
            //}
            //receipt.CurrDiamond = GetBasis.DiamondNum;
            //receipt.ItemList = GetBasis.ItemDataList;
            //receipt.SkillList = GetBasis.SkillDataList;

            return true;
        }
    }
}