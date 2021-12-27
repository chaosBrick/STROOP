using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;
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

        public override Lazy<Image> GetInternalImage()=>currentMapTab.GetBackgroundImage(_backgroundChoice);

        public override string GetName()
        {
            return "Custom Background";
        }

        protected override ContextMenuStrip GetContextMenuStrip(MapTracker targetTracker)
        {
            if (_contextMenuStrip == null)
            {
                List<BackgroundImage> backgroundImages = Config.MapAssociations.GetAllBackgroundImages();
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
                _contextMenuStrip = new ContextMenuStrip();
                _contextMenuStrip.Items.Add(itemSelectMap);
            }

            return _contextMenuStrip;
        }
    }
}
