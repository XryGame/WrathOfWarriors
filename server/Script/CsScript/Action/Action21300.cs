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
    /// 21300_红包
    /// </summary>
    public class Action21300 : BaseAction
    {
        private JPBuyData receipt;

        public Action21300(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action21300, actionGetter)
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
            receipt = new JPBuyData();

            if (GetBasis.IsReceivedRedPacket)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            Random random = new Random();
            int randv = random.Next(10000);
            int diamondNum = 0;
            if (randv < 9000)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 10;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 50;
                }
                else
                {
                    diamondNum = random.Next(20) + 80;
                }

            }
            else if (randv < 9400)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 100;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 150;
                }
                else
                {
                    diamondNum = random.Next(20) + 180;
                }
            }
            else if (randv < 9600)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 200;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 250;
                }
                else
                {
                    diamondNum = random.Next(20) + 280;
                }
            }
            else if (randv < 9750)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 300;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 350;
                }
                else
                {
                    diamondNum = random.Next(20) + 380;
                }
            }
            else if (randv < 9850)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 400;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 450;
                }
                else
                {
                    diamondNum = random.Next(20) + 480;
                }
            }
            else if (randv < 9900)
            {
                int rv = random.Next(1000);
                if (rv < 600)
                {
                    diamondNum = random.Next(40) + 400;
                }
                else if (rv < 900)
                {
                    diamondNum = random.Next(30) + 450;
                }
                else
                {
                    diamondNum = random.Next(20) + 480;
                }
            }
            else
            {
                diamondNum = 666;
            }
            //GetBasis.IsReceivedRedPacket = true;
            UserHelper.RewardsDiamond(GetBasis.UserID, diamondNum);
            receipt.Result = EventStatus.Good;
            receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt.Extend1 = diamondNum;
            return true;
        }
    }
}