using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class CoinNotificationData
    {
        public CoinType UpdateCoinType { get; set; }

        public UpdateCoinOperate UpdateCoinOperate { get; set; }

        public string NumString { get; set; }
    }
    /// <summary>
    /// 1049_货币数量改变通知接口
    /// </summary>
    public class Action1049 : BaseAction
    {
        private CoinNotificationData receipt;

        private CoinType _coinType;
        private UpdateCoinOperate _updateGoldType;
        public Action1049(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1049, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("CoinType", ref _coinType)
                && httpGet.GetEnum("UpdateType", ref _updateGoldType))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new CoinNotificationData();

            switch (_coinType)
            {
                case CoinType.Gold:
                    receipt.NumString = GetBasis.Gold;
                    break;
                case CoinType.Diamond:
                    receipt.NumString = GetBasis.DiamondNum.ToString();
                    break;
                case CoinType.CombatCoin:
                    receipt.NumString = GetCombat.CombatCoin.ToString();
                    break;
                case CoinType.GuildCoin:
                    receipt.NumString = GetGuild.GuildCoin.ToString();
                    break;
            }
            receipt.UpdateCoinType = _coinType;
            receipt.UpdateCoinOperate = _updateGoldType;
            

            return true;
        }
    }
}