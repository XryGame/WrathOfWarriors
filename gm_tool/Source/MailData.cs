namespace gm_tool.Source
{
    public class MailData
    {
        public class AppendItem
        {
            public int ID { get; set; }

            public int Num { get; set; }
        }

        public MailData()
        {
            for (int i = 0; i < 4; ++i)
            {
                AppendItems[i] = new AppendItem();
            }
        }
        public string Title { get; set; }

        public string Content { get; set; }

        public int ApppendDiamond { get; set; }

        public AppendItem[] AppendItems = new AppendItem[4];
    }
}
