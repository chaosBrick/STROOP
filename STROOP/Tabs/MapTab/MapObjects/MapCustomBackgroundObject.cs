using System.Collections.Generic;
using System.Windows.Forms;
using STROOP.Forms;

namespace STROOP.Tabs.MapTab.MapObjects
{
    [ObjectDescription("Custom Background", "Misc")]
    public class MapCustomBackgroundObject : MapBackgroundObject
    {
        private object _backgroundChoice;

        public MapCustomBackgroundObject()
            : base()
        {
            _backgroundChoice = "Recommended";
        }

        protected override BackgroundImage GetBackgroundImage() => currentMapTab.GetBackgroundImage(_backgroundChoice);

        public override string GetName()
        {
            return "Custom Background";
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            List<BackgroundImage> backgroundImages = MapTab.MapAssociations.GetAllBackgroundImages();
            List<object> backgroundImageChoices = new List<object>() { "Recommended" };
            backgroundImages.ForEach(backgroundImage => backgroundImageChoices.Add(backgroundImage));

            ToolStripMenuItem itemSelectMap = new ToolStripMenuItem("Select Background");
            itemSelectMap.Click += (sender, e) =>
            {
                SelectionForm form = new SelectionForm();
                form.Initialize(
                    "Select a Background",
                    "Set Background",
                    backgroundImageChoices,
                    backgroundChoice => _backgroundChoice = backgroundChoice);
                form.Show();
            };

            var _contextMenuStrip = new ContextMenuStrip();
            _contextMenuStrip.Items.Add(itemSelectMap);
            return _contextMenuStrip;
        }
    }
}
