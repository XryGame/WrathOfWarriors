using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Web;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;

namespace GameServer.CsScript.Remote
{

    public class PostParameter
    {
        public string param;

        public void AddParam(string name, string value)
        {
            if (!param.IsEmpty())
            {
                param += "&";
            }
            param = param + name + "=" + value;
        }
    }

    public class ResultStringBone
    {
        public string bonestr;
        public void AddStrBone(string str)
        {
            if (!bonestr.IsEmpty())
            {
                bonestr += "&";
            }
            bonestr = bonestr + str;
        }
    }

    public class OnGMCommon : HttpMessageInterface
    {
        private string _value;
        private string _OperateName;
        

        //private readonly string md5key = "6sd5744smnye33pkr1b15678fdvsfgqutw";

        //public enum GmCommonID
        //{
        //    /// <summary>
        //    /// 查询角色
        //    /// </summary>
        //    QueryRole,

        //}

        //public class JsonBase
        //{
        //    public GmCommonID ID { get; set; }

        //    public int UserId { get; set; }

        //    public string UserName { get; set; }

        //    public string Sign { get; set; }
        //}


        public class JsonResult
        {
            public int ResultCode { get; set; }

            public string ResultString { get; set; }

            public object Data { get; set; }

        }

        public void ActiveHttp(NewHttpResponse client, Dictionary<string, string> parms)
        {
            PostParameter receipt = verifyDeliver(parms);
            string responseData = HttpUtility.UrlEncode(receipt.param);
            client.OutputStream.WriteLine(responseData);
            client.Close();
        }

        private PostParameter verifyDeliver(Dictionary<string, string> parms)
        {
            PostParameter param = new PostParameter();
            int ResultCode = 0;
            string ResultString = "Successfully";
            ResultStringBone bone = new ResultStringBone();
            try
            {
                while (true)
                {
                    parms.TryGetValue("ID", out _OperateName);

                    if (_OperateName == "LevelRankingData")
                    {
                        parms.TryGetValue("UserID", out _value);
                        int UserId = _value.ToInt();
                        parms.TryGetValue("UserName", out _value);
                        string UserName = _value;


                        var user = new ShareCacheStruct<UserCenterUser>().FindKey(UserId);
                        if (user == null)
                        {
                            user = new ShareCacheStruct<UserCenterUser>().Find(t => t.NickName == UserName);
                            if (user == null)
                            {
                                bone.AddStrBone("没有找到该用户");
                                break;
                            }
                            UserId = user.UserID;
                        }

                        var basis = UserHelper.FindUserBasis(UserId);
                        if (basis == null)
                        {
                            bone.AddStrBone("没有找到该用户");
                            break;
                        }

                        var pay = UserHelper.FindUserPay(UserId);
                        var attribute = UserHelper.FindUserAttribute(UserId);
                        var friend = UserHelper.FindUserFriends(UserId);

                        param.AddParam("UserID", basis.UserID.ToString());
                        param.AddParam("UserName", basis.NickName);
                        param.AddParam("UserLv", basis.UserLv.ToString());
                        param.AddParam("VipLv", basis.VipLv.ToString());
                        param.AddParam("PayAmount", pay.PayMoney.ToString());
                        param.AddParam("RetailID", user.RetailID);
                        param.AddParam("CreateDate", Util.FormatDate(basis.CreateDate));
                        param.AddParam("LastLoginDate", Util.FormatDate(basis.LoginDate));
                        param.AddParam("LoginNum", user.LoginNum.ToString());
                        param.AddParam("FightValue", attribute.FightValue.ToString());
                        param.AddParam("CombatRankID", basis.CombatRankID.ToString());
                        param.AddParam("GuildName", "暂无公会");
                        param.AddParam("FriendNum", friend.FriendsList.Count.ToString());
                    }
                    if (_OperateName == "Query")
                    {
                        parms.TryGetValue("UserID", out _value);
                        int UserId = _value.ToInt();
                        parms.TryGetValue("UserName", out _value);
                        string UserName = _value;

                        
                        var user = new ShareCacheStruct<UserCenterUser>().FindKey(UserId);
                        if (user == null)
                        {
                            user = new ShareCacheStruct<UserCenterUser>().Find(t => t.NickName == UserName);
                            if (user == null)
                            {
                                bone.AddStrBone("没有找到该用户");
                                break;
                            }
                            UserId = user.UserID;
                        }
                        
                        var basis = UserHelper.FindUserBasis(UserId);
                        if (basis == null)
                        {
                            bone.AddStrBone("没有找到该用户");
                            break;
                        }

                        var pay = UserHelper.FindUserPay(UserId);
                        var attribute = UserHelper.FindUserAttribute(UserId);
                        var friend = UserHelper.FindUserFriends(UserId);

                        param.AddParam("UserID", basis.UserID.ToString());
                        param.AddParam("UserName", basis.NickName);
                        param.AddParam("UserLv", basis.UserLv.ToString());
                        param.AddParam("VipLv", basis.VipLv.ToString());
                        param.AddParam("PayAmount", pay.PayMoney.ToString());
                        param.AddParam("RetailID", user.RetailID);
                        param.AddParam("CreateDate", Util.FormatDate(basis.CreateDate));
                        param.AddParam("LastLoginDate", Util.FormatDate(basis.LoginDate));
                        param.AddParam("LoginNum", user.LoginNum.ToString());
                        param.AddParam("FightValue", attribute.FightValue.ToString());
                        param.AddParam("CombatRankID", basis.CombatRankID.ToString());
                        param.AddParam("GuildName", "暂无公会");
                        param.AddParam("FriendNum", friend.FriendsList.Count.ToString());
                    }
                    else if (_OperateName == "Reset")
                    {
                        bool isResetEquip, isResetPackage, isResetSoul, isResetPay, isResetEventAward,
                            isResetSkill, isResetAchievement, isResetTask, isResetCombat;
                        parms.TryGetValue("UserID", out _value);
                        int UserId = _value.ToInt();
                        parms.TryGetValue("IsResetEquip", out _value);
                        isResetEquip = _value.ToBool();
                        parms.TryGetValue("IsResetPackage", out _value);
                        isResetPackage = _value.ToBool();
                        parms.TryGetValue("IsResetSoul", out _value);
                        isResetSoul = _value.ToBool();
                        parms.TryGetValue("IsResetPay", out _value);
                        isResetPay = _value.ToBool();
                        parms.TryGetValue("IsResetEventAward", out _value);
                        isResetEventAward = _value.ToBool();
                        parms.TryGetValue("IsResetSkill", out _value);
                        isResetSkill = _value.ToBool();
                        parms.TryGetValue("IsResetAchievement", out _value);
                        isResetAchievement = _value.ToBool();
                        parms.TryGetValue("IsResetTask", out _value);
                        isResetTask = _value.ToBool();
                        parms.TryGetValue("isResetCombat", out _value);
                        isResetCombat = _value.ToBool();

                        var user = new ShareCacheStruct<UserCenterUser>().FindKey(UserId);
                        if (user == null)
                        {
                            bone.AddStrBone("没有找到该用户");
                            break;
                        }
                        if (isResetEquip)
                        {
                            var equips = UserHelper.FindUserEquips(UserId);
                            equips.ResetCache();
                            UserHelper.RefreshUserFightValue(UserId);
                        }
                        if (isResetPackage)
                        {
                            var package = UserHelper.FindUserPackage(UserId);
                            package.ResetCache();
                        }
                        if (isResetSoul)
                        {
                            var soul = UserHelper.FindUserSoul(UserId);
                            soul.ResetCache();
                            UserHelper.RefreshUserFightValue(UserId);
                        }
                        if (isResetPay)
                        {
                            var pay = UserHelper.FindUserPay(UserId);
                            pay.ResetCache();
                        }
                        if (isResetEventAward)
                        {
                            var eventaward = UserHelper.FindUserEventAward(UserId);
                            eventaward.ResetCache();
                        }
                        if (isResetSkill)
                        {
                            var basis = UserHelper.FindUserBasis(UserId);
                            var skill = UserHelper.FindUserSkill(UserId);
                            skill.ResetCache(basis.Profession);
                        }
                        if (isResetAchievement)
                        {
                            var achievement = UserHelper.FindUserAchievement(UserId);
                            achievement.ResetCache();
                        }
                        if (isResetTask)
                        {
                            var task = UserHelper.FindUserTask(UserId);
                            task.ResetCache();
                        }
                        if (isResetCombat)
                        {
                            var combat = UserHelper.FindUserCombat(UserId);
                            combat.ResetCache();
                        }
                    }
                    else if (_OperateName == "Set")
                    {
                        string UserName;
                        int UserLv, GoldNum, DiamondNum, AddItemID, AddItemNum, PayID;
                        parms.TryGetValue("UserID", out _value);
                        int UserId = _value.ToInt();
                        parms.TryGetValue("UserName", out UserName);
                        parms.TryGetValue("UserLv", out _value);
                        UserLv = _value.ToInt();
                        parms.TryGetValue("GoldNum", out _value);
                        GoldNum = _value.ToInt();
                        parms.TryGetValue("DiamondNum", out _value);
                        DiamondNum = _value.ToInt();
                        parms.TryGetValue("AddItemID", out _value);
                        AddItemID = _value.ToInt();
                        parms.TryGetValue("AddItemNum", out _value);
                        AddItemNum = _value.ToInt();
                        parms.TryGetValue("PayID", out _value);
                        PayID = _value.ToInt();

                        var user = new ShareCacheStruct<UserCenterUser>().FindKey(UserId);
                        if (user == null)
                        {
                            bone.AddStrBone("没有找到该用户");
                            break;
                        }
                        

                        if (UserName != string.Empty)
                        {
                            var nickNameCheck = new NickNameCheck();
                            var KeyWordCheck = new KeyWordCheck();
                            string msg;

                            if (nickNameCheck.VerifyRange(UserName, out msg) ||
                                KeyWordCheck.VerifyKeyword(UserName, out msg) ||
                                nickNameCheck.IsExistNickName(UserName, out msg))
                            {
                                bone.AddStrBone(msg);
                            }
                            else
                            {
                                var basis = UserHelper.FindUserBasis(UserId);
                                basis.NickName = UserName;
                                user.NickName = UserName;
                            }
                        }
                        if (UserLv != 0)
                        {
                            var list = new ShareCacheStruct<Config_RoleInitial>().FindAll();
                            if (UserLv <= list.Count)
                            {
                                var basis = UserHelper.FindUserBasis(UserId);
                                basis.UserLv = UserLv;
                            }
                            else
                            {
                                bone.AddStrBone("输入等级超过最高级别");
                            }

                        }
                        if (GoldNum > 0)
                        {
                            GoldNum = Math.Min(GoldNum, 1000000);
                            UserHelper.RewardsGold(UserId, GoldNum);
                        }
                        if (DiamondNum > 0)
                        {
                            DiamondNum = Math.Min(DiamondNum, 1000000);
                            UserHelper.RewardsDiamond(UserId, DiamondNum, UpdateDiamondType.Other);
                        }
                        if (AddItemID > 0 && AddItemNum > 0)
                        {
                            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(AddItemID);
                            if (itemcfg != null)
                            {
                                UserHelper.RewardsItem(UserId, AddItemID, AddItemNum);
                            }
                            else
                            {
                                bone.AddStrBone("无此物品");
                            }
                        }
                        if (PayID > 0)
                        {
                            var paycfg = new ShareCacheStruct<Config_Pay>().FindKey(PayID);
                            if (paycfg != null)
                            {
                                if (!UserHelper.OnWebPay(UserId, PayID))
                                {
                                    bone.AddStrBone("发货失败");
                                }
                            }
                            else
                            {
                                bone.AddStrBone("充值ID错误");
                            }
                        }
                    }
                    else if (_OperateName == "NewMail")
                    {
                        string MailTitle, MailContent;
                        int MailDiamond, AddItem1ID, AddItem1Num, AddItem2ID, AddItem2Num, AddItem3ID, AddItem3Num, AddItem4ID, AddItem4Num;
                        parms.TryGetValue("UserID", out _value);
                        int UserId = _value.ToInt();
                        parms.TryGetValue("MailTitle", out MailTitle);
                        parms.TryGetValue("MailContent", out MailContent);
                        parms.TryGetValue("MailDiamond", out _value);
                        MailDiamond = _value.ToInt();
                        parms.TryGetValue("AddItem1ID", out _value);
                        AddItem1ID = _value.ToInt();
                        parms.TryGetValue("AddItem1Num", out _value);
                        AddItem1Num = _value.ToInt();
                        parms.TryGetValue("AddItem2ID", out _value);
                        AddItem2ID = _value.ToInt();
                        parms.TryGetValue("AddItem2Num", out _value);
                        AddItem2Num = _value.ToInt();
                        parms.TryGetValue("AddItem3ID", out _value);
                        AddItem3ID = _value.ToInt();
                        parms.TryGetValue("AddItem3Num", out _value);
                        AddItem3Num = _value.ToInt();
                        parms.TryGetValue("AddItem4ID", out _value);
                        AddItem4ID = _value.ToInt();
                        parms.TryGetValue("AddItem4Num", out _value);
                        AddItem4Num = _value.ToInt();

                        var user = new ShareCacheStruct<UserCenterUser>().FindKey(UserId);
                        if (user == null)
                        {
                            bone.AddStrBone("没有找到该用户");
                            break;
                        }
                        var mailbox = UserHelper.FindUserMailBox(UserId);
                        MailData mail = new MailData()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Title = MailTitle,
                            Sender = "系统",
                            Date = DateTime.Now,
                            Context = MailContent,
                            ApppendDiamond = MailDiamond
                        };
                        if (AddItem1ID > 0 && AddItem1Num > 0)
                        {
                            ItemData item = new ItemData()
                            {
                                ID = AddItem1ID,
                                Num = AddItem1Num,
                            };
                            mail.AppendItem.Add(item);
                        }
                        if (AddItem2ID > 0 && AddItem2Num > 0)
                        {
                            ItemData item = new ItemData()
                            {
                                ID = AddItem2ID,
                                Num = AddItem2Num,
                            };
                            mail.AppendItem.Add(item);
                        }
                        if (AddItem3ID > 0 && AddItem3Num > 0)
                        {
                            ItemData item = new ItemData()
                            {
                                ID = AddItem3ID,
                                Num = AddItem3Num,
                            };
                            mail.AppendItem.Add(item);
                        }
                        if (AddItem4ID > 0 && AddItem4Num > 0)
                        {
                            ItemData item = new ItemData()
                            {
                                ID = AddItem4ID,
                                Num = AddItem4Num,
                            };
                            mail.AppendItem.Add(item);
                        }

                        bool IsSucceed = true;
                        foreach (var v in mail.AppendItem)
                        {
                            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(v.ID);
                            if (itemcfg == null)
                            {
                                bone.AddStrBone("邮件附加道具错误");
                                IsSucceed = false;
                                break;
                            }
                        }
                        if (IsSucceed)
                        {
                            UserHelper.AddNewMail(UserId, mail);
                        }
                    }
                    else if (_OperateName == "NewNotice")
                    {
                        string Content;
                        NoticeMode Mode;
                        parms.TryGetValue("Mode", out _value);
                        Mode = (NoticeMode)_value.ToInt();
                        parms.TryGetValue("Content", out Content);
                        if (!string.IsNullOrEmpty(Content))
                        {
                            GlobalRemoteService.SendNotice(Mode, Content);
                        }
                    }
                    else
                    {
                        bone.AddStrBone("该功能暂未实现");
                    }
                    break;
                }

            }
            catch (Exception e)
            {
                ResultString = "Url参数格式错误";
                TraceLog.WriteError(string.Format("{0} {1}", ResultString, e));
            }
            if (!bone.bonestr.IsEmpty())
            {
                ResultString = bone.bonestr;
                ResultCode = 1;
            }
            param.AddParam("OperateName", _OperateName);
            param.AddParam("ResultCode", ResultCode.ToString());
            param.AddParam("ResultString", ResultString);
            return param;
        }
    }

  
}