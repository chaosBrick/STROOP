using STROOP.Structs.Configurations;

namespace STROOP.Tabs
{
    public partial class MusicTab : STROOPTab
    {
        public MusicTab()
        {
            InitializeComponent();
        }

        public override string GetDisplayName() => "Music";

        public override void InitializeTab()
        {
            base.InitializeTab();
            for (int i = 0; i < 3; i++)
            {
                listBoxMusic.Items.Add(i);
            }
        }

        public uint? GetMusicAddress()
        {
            object value = listBoxMusic.SelectedItem;
            if (value is int intValue)
            {
                uint baseAddress = 0x80222A18;
                uint size = 0x140;
                uint address = (uint)(baseAddress + intValue * size);
                return Config.Stream.GetUInt32(address);
            }
            return null;
        }

        public override void Update(bool updateView)
        {
            if (!updateView) return;
            base.Update(updateView);
        }
    }
}
