using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class PassLevelReceipt
    {
        public int UserLv { get; set; }

        public int NextMap { get; set; }
        
    }
    /// <summary>
    /// 1111_通关关卡结算
    /// </summary>
    public class Action1111 : BaseAction
    {
        //private UserAttributeCache receipt = null;
        private PassLevelReceipt receipt = null;

        private int _ID;

        private int att;
        private int crit;
        private int hit;
        public Action1111(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1111, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref _ID)
                && httpGet.GetInt("Att", ref att)
                && httpGet.GetInt("Crit", ref crit)
                && httpGet.GetInt("Hit", ref hit))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new PassLevelReceipt();
            bool isDataError = false;
            if (att > GetAttribute.Atk
                || crit > GetAttribute.Crit
                || hit > GetAttribute.Hit)
            {
                isDataError = true;
            }

            if (!isDataError)
            {
                if (GetBasis.LastPassLevelID != _ID && _ID % 5 != 0 && _ID > 10)
                {
                    var timespan = DateTime.Now.Subtract(GetBasis.LastPassLevelTime);
                    if (timespan.TotalSeconds < 30)
                    {
                        isDataError = true;
                    }
                }
            }

            if (isDataError)
            {
                PushMessageHelper.UserGameDataExceptionNotification(Current);
                return false;
            }

            GetBasis.LastPassLevelTime = DateTime.Now;
            GetBasis.LastPassLevelID = _ID;

            if (GetBasis.BackLevelNum > 0)
            {
                int levelDown = GetBasis.BackLevelNum;
                GetBasis.UserLv = Math.Max(GetBasis.UserLv - levelDown, 10);
                UserHelper.UserLvChange(Current.UserId);
                GetBasis.BackLevelNum = 0;
                receipt.UserLv = GetBasis.UserLv;
                receipt.NextMap = GetBasis.UserLv;
                PushMessageHelper.UserLvChangeNotification(Current);
            }
            else if (UserHelper.UserLevelUpCheck(Current.UserId, _ID))
            {
                receipt.UserLv = GetBasis.UserLv;
                receipt.NextMap = GetBasis.UserLv;
            }
            else
            {
                receipt.UserLv = GetBasis.UserLv;
                receipt.NextMap = _ID;
            }
            

            return true;
        }
    }
}