using gm_tool.Source;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
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

        public string SelectServerUrl = "";

        private int SelectMailAppendCoinType = 0;
        private int SelectSetEquipID = 0;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            HttpRequest request = new HttpRequest();
            if (request.HttpGetRequest("http://168.254.0.150:8181/GmToolUpdateVersion.txt"))
            {
                string currVersion = ConfigurationManager.AppSettings["Version"];
                string newVersion = request.GetReceiveValue();
                if (currVersion.CompareTo(newVersion) != 0)
                {
                    Process.Start("AutoUpdate.exe");
                    System.Environment.Exit(0);
                }
            }
        }

        private void comboBox_ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = sender as ComboBox;
            if (select.SelectedIndex == 0)
                SelectServerUrl = "http://168.254.0.254:8091/GMCommon.aspx?"; 
            else if (select.SelectedIndex == 1)
                SelectServerUrl = "http://118.89.234.233:8091/GMCommon.aspx?";
        }

        private void comboBox_MailAppendCoinTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = sender as ComboBox;
            if (select.SelectedIndex == 0) // 金币
                SelectMailAppendCoinType = 1;
            else if (select.SelectedIndex == 1)  // 钻石
                SelectMailAppendCoinType = 2;
            else if (select.SelectedIndex == 2)  // 竞技币
                SelectMailAppendCoinType = 3;
            else if (select.SelectedIndex == 3)  // 公会币
                SelectMailAppendCoinType = 4;
        }

        private void comboBox_SetEquipLevelEquipIDSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = sender as ComboBox;
            if (select.SelectedIndex == 0) // 衣服
                SelectSetEquipID = 10001;
            else if (select.SelectedIndex == 1)  // 武器
                SelectSetEquipID = 10002;
            else if (select.SelectedIndex == 2)  // 鞋子
                SelectSetEquipID = 10003;
            else if (select.SelectedIndex == 3)  // 饰品
                SelectSetEquipID = 10004;
            else if (select.SelectedIndex == 4)  // 戒指
                SelectSetEquipID = 10005;
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
            if (request.HttpPostRequest(SelectServerUrl))
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
                QueryOpenID.Text = request.GetReceiveValue("OpenID");
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
            request.AddPostParam("IsResetAttribute", checkBoxAttribute.IsChecked.ToString());
            if (request.HttpPostRequest(SelectServerUrl))
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
            request.AddPostParam("PayID", PayID.Text);
            request.AddPostParam("CombatCoinNum", SetCombatCoinNum.Text);
            request.AddPostParam("ElfID", SetElfID.Text);
            request.AddPostParam("ElfLevel", SetElfLevel.Text);
            request.AddPostParam("SkillID", SetSkillID.Text);
            request.AddPostParam("SkillLevel", SetSkillLevel.Text);
            request.AddPostParam("EquipID", SelectSetEquipID.ToString());
            request.AddPostParam("EquipLevel", SetEquipLevel.Text);
            request.AddPostParam("LevelUp", SetLevelUp.Text);
            request.AddPostParam("NoviceGuide", SetNoviceGuide.Text);
            if (request.HttpPostRequest(SelectServerUrl))
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
            checkBoxAttribute.IsChecked = false;
        }

        private void ResetSetText()
        {
            SetUserName.Text = string.Empty;
            SetUserLv.Text = string.Empty;
            SetGoldNum.Text = string.Empty;
            SetDiamondNum.Text = string.Empty;
            SetAddItemID.Text = string.Empty;
            SetAddItemNum.Text = string.Empty;
            SetCombatCoinNum.Text = string.Empty;
            SetElfID.Text = string.Empty;
            SetElfLevel.Text = string.Empty;
            SetSkillID.Text = string.Empty;
            SetSkillLevel.Text = string.Empty;
            SetEquipLevel.Text = string.Empty;
            SetLevelUp.Text = string.Empty;
            SetNoviceGuide.Text = string.Empty;
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
            AppendCoinNum.Text = string.Empty;

        }

        private void ResetNoticeText()
        {
            textBoxNotice.Text = string.Empty;

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
            request.AddPostParam("AppendCoinType", SelectMailAppendCoinType.ToString());
            request.AddPostParam("AppendCoinNum", AppendCoinNum.Text);
            request.AddPostParam("AddItem1ID", MailItem1ID.Text);
            request.AddPostParam("AddItem1Num", MailItem1Num.Text);
            request.AddPostParam("AddItem2ID", MailItem2ID.Text);
            request.AddPostParam("AddItem2Num", MailItem2Num.Text);
            request.AddPostParam("AddItem3ID", MailItem3ID.Text);
            request.AddPostParam("AddItem3Num", MailItem3Num.Text);
            request.AddPostParam("AddItem4ID", MailItem4ID.Text);
            request.AddPostParam("AddItem4Num", MailItem4Num.Text);
            if (request.HttpPostRequest(SelectServerUrl))
            {
                ResetMailText();
            }
        }

        private void SendNoticeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textBoxNotice.Text.Length == 0)
                return;
            HttpRequest request = new HttpRequest();
            request.AddPostParam("ID", "NewNotice");
            int mode = checkBoxAllService.IsChecked == true ? 1 : 2;
            request.AddPostParam("Mode", mode.ToString());
            request.AddPostParam("Content", textBoxNotice.Text);
            if (request.HttpPostRequest(SelectServerUrl))
            {
                ResetNoticeText();
            }
        }

        private void OnSelectNoticeTypeCheck(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.Name.CompareTo("checkBoxCurrService") == 0)
            {
                checkBoxAllService.IsChecked = !checkbox.IsChecked;
            }
            else
            {
                checkBoxCurrService.IsChecked = !checkbox.IsChecked;
            }
        }
    }
}
