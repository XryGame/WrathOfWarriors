using gm_tool.Source;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gm_tool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Log._listBox = listBoxLog;

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBox_PreviewTextInputLimitNumber(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
                return;
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }


        private void textBox_PreviewTextInputLimitAll(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }


        private void QueryRoleInfo(bool islog = true)
        {
            HttpRequest request = new HttpRequest();
            request.AddPostParam("ID", "Query");
            request.AddPostParam("UserID", textBoxInputUserID.Text);
            request.AddPostParam("UserName", textBoxInputUserName.Text);
            request.IsWriteLog = islog;
            if (request.HttpPostRequest())
            {
                QueryUserID.Text = request.GetReceiveValue("UserID");
                QueryUserName.Text = request.GetReceiveValue("UserName");
                QueryUserLv.Text = request.GetReceiveValue("UserLv");
                QueryVipLv.Text = request.GetReceiveValue("VipLv");
                QueryPayAmount.Text = request.GetReceiveValue("PayAmount");
                QueryRetailID.Text = request.GetReceiveValue("RetailID");
                QueryCreateDate.Text = request.GetReceiveValue("CreateDate");
                QueryLastLoginDate.Text = request.GetReceiveValue("LastLoginDate");
                QueryLoginNum.Text = request.GetReceiveValue("LoginNum");
                QueryFightValue.Text = request.GetReceiveValue("FightValue");
                QueryCombatRankID.Text = request.GetReceiveValue("CombatRankID");
                QueryGuildName.Text = request.GetReceiveValue("GuildName");
                QueryFriendNum.Text = request.GetReceiveValue("FriendNum");
            }
        }

        private void QueryButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textBoxInputUserID.Text == "0" && textBoxInputUserName.Text == "null")
                return;

            QueryRoleInfo();
            ResetResetCheckBox();
        }

        private void ResetButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (QueryUserID.Text.Length == 0)
                return;
            HttpRequest request = new HttpRequest();
            request.AddPostParam("ID", "Reset");
            request.AddPostParam("UserID", QueryUserID.Text);
            request.AddPostParam("IsResetEquip", checkBoxEquip.IsChecked.ToString());
            request.AddPostParam("IsResetPackage", checkBoxPackage.IsChecked.ToString());
            request.AddPostParam("IsResetSoul", checkBoxSoul.IsChecked.ToString());
            request.AddPostParam("IsResetPay", checkBoxPay.IsChecked.ToString());
            request.AddPostParam("IsResetEventAward", checkBoxEventAward.IsChecked.ToString());
            request.AddPostParam("IsResetSkill", checkBoxSkill.IsChecked.ToString());
            request.AddPostParam("IsResetAchievement", checkAchievement.IsChecked.ToString());
            request.AddPostParam("IsResetTask", checkBoxTask.IsChecked.ToString());
            request.AddPostParam("IsResetCombat", checkBoxCombat.IsChecked.ToString());
            if (request.HttpPostRequest())
            {
                QueryRoleInfo(false);
            }
            ResetResetCheckBox();
            ResetSetText();
        }

        private void SetButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (QueryUserID.Text.Length == 0)
                return;
            HttpRequest request = new HttpRequest();
            request.AddPostParam("ID", "Set");
            request.AddPostParam("UserID", QueryUserID.Text);
            request.AddPostParam("UserName", SetUserName.Text);
            request.AddPostParam("UserLv", SetUserLv.Text);
            request.AddPostParam("GoldNum", SetGoldNum.Text);
            request.AddPostParam("DiamondNum", SetDiamondNum.Text);
            request.AddPostParam("AddItemID", SetAddItemID.Text);
            request.AddPostParam("AddItemNum", SetAddItemNum.Text);
            if (request.HttpPostRequest())
            {
                QueryRoleInfo(false);
            }
            ResetSetText();
        }

        private void ResetResetCheckBox()
        {
            checkBoxEquip.IsChecked = false;
            checkBoxPackage.IsChecked = false;
            checkBoxSoul.IsChecked = false;
            checkBoxPay.IsChecked = false;
            checkBoxEventAward.IsChecked = false;
            checkBoxSkill.IsChecked = false;
            checkAchievement.IsChecked = false;
            checkBoxTask.IsChecked = false;
            checkBoxCombat.IsChecked = false;
        }

        private void ResetSetText()
        {
            SetUserName.Text = string.Empty;
            SetUserLv.Text = string.Empty;
            SetGoldNum.Text = string.Empty;
            SetDiamondNum.Text = string.Empty;
            SetAddItemID.Text = string.Empty;
            SetAddItemNum.Text = string.Empty;
        }

        private void ResetMailText()
        {
            MailTitle.Text = string.Empty;
            MailContent.Text = string.Empty;
            MailItem1ID.Text = string.Empty;
            MailItem1Num.Text = string.Empty;
            MailItem1ID.Text = string.Empty;
            MailItem1Num.Text = string.Empty;
            MailItem2ID.Text = string.Empty;
            MailItem2Num.Text = string.Empty;
            MailItem3ID.Text = string.Empty;
            MailItem3Num.Text = string.Empty;
            MailItem4ID.Text = string.Empty;
            MailItem4Num.Text = string.Empty;
            MailDiamond.Text = string.Empty;

        }

        private void SendMailButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (QueryUserID.Text.Length == 0 || MailTitle.Text.Length == 0 || MailContent.Text.Length == 0)
                return;
            HttpRequest request = new HttpRequest();
            request.AddPostParam("ID", "NewMail");
            request.AddPostParam("UserID", QueryUserID.Text);
            request.AddPostParam("MailTitle", MailTitle.Text);
            request.AddPostParam("MailContent", MailContent.Text);
            request.AddPostParam("MailDiamond", MailDiamond.Text);
            request.AddPostParam("AddItem1ID", MailItem1ID.Text);
            request.AddPostParam("AddItem1Num", MailItem1Num.Text);
            request.AddPostParam("AddItem2ID", MailItem2ID.Text);
            request.AddPostParam("AddItem2Num", MailItem2Num.Text);
            request.AddPostParam("AddItem3ID", MailItem3ID.Text);
            request.AddPostParam("AddItem3Num", MailItem3Num.Text);
            request.AddPostParam("AddItem4ID", MailItem4ID.Text);
            request.AddPostParam("AddItem4Num", MailItem4Num.Text);
            if (request.HttpPostRequest())
            {
                ResetMailText();
            }
        }

    }
}
