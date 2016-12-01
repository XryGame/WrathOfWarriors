using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 9000_请求签到
    /// </summary>
    public class Action9000 : BaseAction
    {
        private JPRequestSFOData receipt;
        private Random random = new Random();
        private bool isrepair; // 是否补签
        public Action9000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action9000, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetBool("IsRepair", ref isrepair))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Good;
            if (ContextUser.EventAwardData.IsTodaySign && !isrepair)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            if (!ContextUser.EventAwardData.IsTodaySign && isrepair)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            if (isrepair && ContextUser.DiamondNum < DataHelper.RepairSignNeedDiamond)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            var choose = new ShareCacheStruct<Config_Signin>().FindAll(
                t => (t.DateYear == DateTime.Now.Year && t.DateMonth == DateTime.Now.Month)
                );
            if (choose.Count == 0)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "Sign");
                new BaseLog().SaveLog("Choose DB Config_Signin Error.");
                return true;
            }

            if (ContextUser.EventAwardData.SignCount >= choose.Count)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            var signsurface = new ShareCacheStruct<Config_Signin>().Find(t => (
                t.DateYear == DateTime.Now.Year && t.DateMonth == DateTime.Now.Month
                && t.DateDay == ContextUser.EventAwardData.SignCount + 1
            ));
            if (signsurface == null)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.EventAwardData.IsTodaySign = true;
            ContextUser.EventAwardData.SignCount++;

            if (isrepair)
            {
                ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, DataHelper.RepairSignNeedDiamond);
            }

            
            
            switch (signsurface.AwardType)
            {
                case AwardType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, signsurface.AwardNum);
                        receipt.AwardDiamondNum = signsurface.AwardNum;
                        receipt.CurrDiamond = ContextUser.DiamondNum;
                    }
                    break;
                case AwardType.ItemSkillBook:
                    {
                        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(signsurface.AwardID);
                        if (item != null)
                        {
                            ContextUser.UserAddItem(signsurface.AwardID, signsurface.AwardNum);

                            if (item.Type == ItemType.Skill)
                            {
                                ContextUser.CheckAddSkillBook(signsurface.AwardID, signsurface.AwardNum);
                            }
                            receipt.AwardItemList.Add(signsurface.AwardID);
                        }
                    }
                    break;
                case AwardType.RandItemSkillBook:
                    {
                        if (random.Next(1000) < 750)
                        {// 道具
                            receipt.AwardItemList = ContextUser.RandItem(signsurface.AwardNum);
                        }
                        else
                        {// 技能
                            receipt.AwardItemList = ContextUser.RandSkillBook(signsurface.AwardNum);
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