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
    /// 11000_兑换CDK
    /// </summary>
    public class Action11000 : BaseAction
    {
        private JPRequestSFOData receipt;
        private Random random = new Random();
        private string _CDKey;
        public Action11000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action11000, actionGetter)
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
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Good;


            var acc = new ShareCacheStruct<Config_CdKey>().FindKey(_CDKey);
            if (acc == null)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            var cdkscache = new ShareCacheStruct<CDKeyCache>();
            if (cdkscache.FindKey(_CDKey) != null)
            {
                receipt.Result = EventStatus.Bad;
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

            for (int i = 0; i < randcount; ++i)
            {
                if (random.Next(1000) < 750)
                {// 道具
                    receipt.AwardItemList.AddRange(ContextUser.RandItem(1));
                }
                else
                {// 技能
                    receipt.AwardItemList.AddRange(ContextUser.RandSkillBook(1));
                }
            }

            foreach (var it in itemlist)
            {
                Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(it);
                if (item != null)
                {
                    ContextUser.UserAddItem(it, 1);

                    if (item.Type == ItemType.Skill)
                    {
                        ContextUser.CheckAddSkillBook(it, 1);
                    }
                    receipt.AwardItemList.Add(it);
                }
            }

            if (diamond > 0)
            {
                UserHelper.GiveAwayDiamond(ContextUser.UserID, diamond);
                receipt.AwardDiamondNum = diamond;
            }
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}