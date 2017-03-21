using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace gm_tool.Source
{
    public static class Log
    {
        public static ListBox _listBox = null;

        public static void Write(string logStr)
        {
            string content = DateTime.Now.ToString("HH:mm:ss") + ": " + logStr;
            ListBoxItem item = new ListBoxItem();
            item.Content = content;
            item.Height = 20;
            _listBox.Items.Add(item);
        }

        public static void Write(HttpParameters param)
        {
            string content = DateTime.Now.ToString("HH:mm:ss") + ": "
             + param.GetValue("OperateName") + "-->" + param.GetValue("ResultString");
            ListBoxItem item = new ListBoxItem();
            item.Content = content;
            item.Height = 20;
            _listBox.Items.Add(item);
        }
    }
}
