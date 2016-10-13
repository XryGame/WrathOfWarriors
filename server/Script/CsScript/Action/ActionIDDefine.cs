/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using System.Data;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// ActionID定义描述
    /// </summary>
    public class ActionIDDefine
    {
        ///<summary>
        ///GM命令接口
        ///</summary>
        public const short Cst_Action1000 = 1000;
        ///<summary>
        ///服务器列表协议接口
        ///</summary>
        public const short Cst_Action1001 = 1001;

        ///<summary>
        ///注册通行证ID获取接口
        ///</summary>
        public const short Cst_Action1002 = 1002;

        ///<summary>
        ///用户注册
        ///</summary>
        public const short Cst_Action1003 = 1003;

        ///<summary>
        ///用户登录
        ///</summary>
        public const short Cst_Action1004 = 1004;

        ///<summary>
        ///创建角色
        ///</summary>
        public const short Cst_Action1005 = 1005;

        ///<summary>
        ///密码更新接口
        ///</summary>
        public const short Cst_Action1006 = 1006;

        ///<summary>
        ///用户检测接口
        ///</summary>
        public const short Cst_Action1007 = 1007;

        ///<summary>
        ///用户角色详情接口
        ///</summary>
        public const short Cst_Action1008 = 1008;

        ///<summary>
        ///战斗力改变通知接口
        ///</summary>
        public const short Cst_Action1050 = 1050;

        ///<summary>
        ///钻石数量改变通知接口
        ///</summary>
        public const short Cst_Action1051 = 1051;

        ///<summary>
        ///等级改变通知接口
        ///</summary>
        public const short Cst_Action1052 = 1052;

        ///<summary>
        ///整点刷新通知接口
        ///</summary>
        public const short Cst_Action1053 = 1053;

        ///<summary>
        ///新的好友请求接口
        ///</summary>
        public const short Cst_Action1054 = 1054;

        ///<summary>
        ///一个好友添加
        ///</summary>
        public const short Cst_Action1055 = 1055;

        ///<summary>
        ///一个好友删除
        ///</summary>
        public const short Cst_Action1056 = 1056;

        ///<summary>
        ///一个好友赠送
        ///</summary>
        public const short Cst_Action1057 = 1057;

        ///<summary>
        ///竞选结果通知
        ///</summary>
        public const short Cst_Action1058 = 1058;

        ///<summary>
        ///班长更换通知
        ///</summary>
        public const short Cst_Action1059 = 1059;

        ///<summary>
        ///每日任务完成通知
        ///</summary>
        public const short Cst_Action1060 = 1060;

        ///<summary>
        ///成就完成通知
        ///</summary>
        public const short Cst_Action1061 = 1061;

        ///<summary>
        ///切磋请求通知
        ///</summary>
        public const short Cst_Action1062 = 1062;

        ///<summary>
        ///切磋取消通知
        ///</summary>
        public const short Cst_Action1063 = 1063;

        ///<summary>
        ///切磋拒绝通知
        ///</summary>
        public const short Cst_Action1064 = 1064;

        ///<summary>
        ///开始切磋通知
        ///</summary>
        public const short Cst_Action1065 = 1065;

        ///<summary>
        ///新的邮件通知
        ///</summary>
        public const short Cst_Action1066 = 1066;



        ///<summary>
        ///请求学习/劳动任务
        ///</summary>
        public const short Cst_Action1100 = 1100;

        ///<summary>
        ///请求领取任务奖励
        ///</summary>
        public const short Cst_Action1101 = 1101;

        ///<summary>
        ///请求放弃任务
        ///</summary>
        public const short Cst_Action1102 = 1102;

        ///<summary>
        ///请求加速任务
        ///</summary>
        public const short Cst_Action1103 = 1103;

        ///<summary>
        ///请求挑战
        ///</summary>
        public const short Cst_Action1110 = 1110;

        ///<summary>
        ///挑战结果
        ///</summary>
        public const short Cst_Action1111 = 1111;

        ///<summary>
        ///购买解锁场景地图
        ///</summary>
        public const short Cst_Action1112 = 1112;

        ///<summary>
        ///查询本年级所有班级
        ///</summary>
        public const short Cst_Action1201 = 1201;

        ///<summary>
        ///加入指定班级
        ///</summary>
        public const short Cst_Action1202 = 1202;

        ///<summary>
        ///查询同班同学
        ///</summary>
        public const short Cst_Action1203 = 1203;

        ///<summary>
        ///开宝箱
        ///</summary>
        public const short Cst_Action1301 = 1301;

        ///<summary>
        ///携带技能
        ///</summary>
        public const short Cst_Action1310 = 1310;

        ///<summary>
        ///名人榜入口
        ///</summary>
        public const short Cst_Action1401 = 1401;

        ///<summary>
        ///名人榜请求挑战
        ///</summary>
        public const short Cst_Action1402 = 1402;

        ///<summary>
        ///名人榜挑战结果
        ///</summary>
        public const short Cst_Action1403 = 1403;

        ///<summary>
        ///名人榜减CD
        ///</summary>
        public const short Cst_Action1404 = 1404;

        ///<summary>
        ///请求添加好友
        ///</summary>
        public const short Cst_Action1501 = 1501;

        ///<summary>
        ///添加好友回执
        ///</summary>
        public const short Cst_Action1502 = 1502;

        ///<summary>
        ///删除好友
        ///</summary>
        public const short Cst_Action1503 = 1503;

        ///<summary>
        ///好友馈赠
        ///</summary>
        public const short Cst_Action1504 = 1504;

        ///<summary>
        ///领取好友馈赠
        ///</summary>
        public const short Cst_Action1505 = 1505;

        ///<summary>
        ///请求查看用户数据
        ///</summary>
        public const short Cst_Action1600 = 1600;

        ///<summary>
        ///发送聊天
        ///</summary>
        public const short Cst_Action3001 = 3001;

        ///<summary>
        ///聊天列表
        ///</summary>
        public const short Cst_Action3002 = 3002;

        ///<summary>
        ///公告列表
        ///</summary>
        public const short Cst_Action3003 = 3003;

        ///<summary>
        ///请求排行榜数据
        ///</summary>
        public const short Cst_Action3004 = 3004;

        ///<summary>
        ///查询班长信息
        ///</summary>
        public const short Cst_Action4000 = 4000;

        ///<summary>
        ///请求挑战班长
        ///</summary>
        public const short Cst_Action4001 = 4001;

        ///<summary>
        ///请求挑战班长结果 
        ///</summary>
        public const short Cst_Action4002 = 4002;

        ///<summary>
        ///购买挑战班长资格 
        ///</summary>
        public const short Cst_Action4003 = 4003;

        ///<summary>
        ///请求竞选列表
        ///</summary>
        public const short Cst_Action4010 = 4010;

        ///<summary>
        ///请求竞选
        ///</summary>
        public const short Cst_Action4011 = 4011;

        ///<summary>
        ///请求竞选投票
        ///</summary>
        public const short Cst_Action4012 = 4012;

        ///<summary>
        ///购买选票
        ///</summary>
        public const short Cst_Action4013 = 4013;

        ///<summary>
        ///请求更换每日任务
        ///</summary>
        public const short Cst_Action5000 = 5000;

        ///<summary>
        ///领取每日任务奖励
        ///</summary>
        public const short Cst_Action5001 = 5001;

        ///<summary>
        ///请求占领数据
        ///</summary>
        public const short Cst_Action6000 = 6000;

        ///<summary>
        ///请求占领
        ///</summary>
        public const short Cst_Action6001 = 6001;

        ///<summary>
        ///请求占领结果
        ///</summary>
        public const short Cst_Action6002 = 6002;

        ///<summary>
        ///请求成就数据
        ///</summary>
        public const short Cst_Action7000 = 7000;

        ///<summary>
        ///请求领取成就奖励
        ///</summary>
        public const short Cst_Action7001 = 7001;


        ///<summary>
        ///请求邀请切磋
        ///</summary>
        public const short Cst_Action8000 = 8000;

        ///<summary>
        ///回应切磋请求
        ///</summary>
        public const short Cst_Action8001 = 8001;

        ///<summary>
        ///邀请人取消邀请
        ///</summary>
        public const short Cst_Action8002 = 8002;

        ///<summary>
        ///切磋完毕
        ///</summary>
        public const short Cst_Action8003 = 8003;

        ///<summary>
        ///请求签到
        ///</summary>
        public const short Cst_Action9000 = 9000;

        ///<summary>
        ///请求领取首周奖励
        ///</summary>
        public const short Cst_Action9010 = 9010;

        ///<summary>
        ///请求领取在线时间奖励
        ///</summary>
        public const short Cst_Action9020 = 9020;

        ///<summary>
        ///请求读取邮件
        ///</summary>
        public const short Cst_Action10000 = 10000;

        ///<summary>
        ///请求领取邮件附件
        ///</summary>
        public const short Cst_Action10001 = 10001;

        ///<summary>
        ///请求删除邮件
        ///</summary>
        public const short Cst_Action10002 = 10002;

        ///<summary>
        ///请求抽奖
        ///</summary>
        public const short Cst_Action10100 = 10100;
    }
}