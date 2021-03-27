﻿using System;
using System.Collections.Generic;
using STROOP.Structs;
using System.Windows.Forms;
using STROOP.Utilities;
using STROOP.Controls;
using STROOP.Structs.Configurations;


namespace STROOP.Tabs
{
    public partial class SnowTab : STROOPTab
    {
        private static readonly List<VariableGroup> ALL_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
                VariableGroup.Snow,
            };

        private static readonly List<VariableGroup> VISIBLE_VAR_GROUPS =
            new List<VariableGroup>()
            {
                VariableGroup.Basic,
                VariableGroup.Intermediate,
                VariableGroup.Advanced,
                VariableGroup.Snow,
            };

        private short _numSnowParticles;
        private List<List<WatchVariableControl>> _snowParticleControls;

        public SnowTab()
        {
            InitializeComponent();
            watchVariablePanelSnow.SetGroups(ALL_VAR_GROUPS, VISIBLE_VAR_GROUPS);
        }

        public override void InitializeTab()
        {
            base.InitializeTab();

            _numSnowParticles = 0;
            _snowParticleControls = new List<List<WatchVariableControl>>();


            buttonSnowRetrieve.Click += (sender, e) =>
            {
                int? snowIndexNullable = ParsingUtilities.ParseIntNullable(textBoxSnowIndex.Text);
                if (!snowIndexNullable.HasValue) return;
                int snowIndex = snowIndexNullable.Value;
                if (snowIndex < 0 || snowIndex > _numSnowParticles) return;
                ButtonUtilities.RetrieveSnow(snowIndex);
            };

            ControlUtilities.InitializeThreeDimensionController(
                CoordinateSystem.Euler,
                true,
                groupBoxSnowPosition,
                "SnowPosition",
                (float hOffset, float vOffset, float nOffset, bool useRelative) =>
                {
                    int? snowIndexNullable = ParsingUtilities.ParseIntNullable(textBoxSnowIndex.Text);
                    if (!snowIndexNullable.HasValue) return;
                    int snowIndex = snowIndexNullable.Value;
                    if (snowIndex < 0 || snowIndex > _numSnowParticles) return;
                    ButtonUtilities.TranslateSnow(
                        snowIndex,
                        hOffset,
                        nOffset,
                        -1 * vOffset,
                        useRelative);
                });
        }

        private List<WatchVariableControl> GetSnowParticleControls(int index)
        {
            uint structOffset = (uint)index * SnowConfig.ParticleStructSize;
            List<uint> offsets = new List<uint>()
            {
                structOffset + SnowConfig.XOffset,
                structOffset + SnowConfig.YOffset,
                structOffset + SnowConfig.ZOffset,
            };
            List<string> names = new List<string>()
            {
                String.Format("Particle {0} X", index),
                String.Format("Particle {0} Y", index),
                String.Format("Particle {0} Z", index),
            };

            List<WatchVariableControl> controls = new List<WatchVariableControl>();
            for (int i = 0; i < 3; i++)
            {
                WatchVariable watchVar = new WatchVariable(
                    memoryTypeName: "int",
                    specialType: null,
                    baseAddressType: BaseAddressTypeEnum.Snow,
                    offsetUS: null,
                    offsetJP: null,
                    offsetSH: null,
                    offsetEU: null,
                    offsetDefault: offsets[i],
                    mask: null,
                    shift: null,
                    handleMapping: true);
                WatchVariableControlPrecursor precursor = new WatchVariableControlPrecursor(
                    name: names[i],
                    watchVar: watchVar,
                    subclass: WatchVariableSubclass.Number,
                    backgroundColor: null,
                    displayType: null,
                    roundingLimit: null,
                    useHex: null,
                    invertBool: null,
                    isYaw: null,
                    coordinate: i == 0 ? Coordinate.X : i == 1 ? Coordinate.Y : Coordinate.Z,
                    groupList: new List<VariableGroup>() { VariableGroup.Snow });
                controls.Add(precursor.CreateWatchVariableControl());
            }
            return controls;
        }

        public override void Update(bool updateView)
        {
            if (!updateView) return;

            short numSnowParticles = Config.Stream.GetInt16(SnowConfig.CounterAddress);
            if (numSnowParticles > _numSnowParticles) // need to add controls
            {
                for (int i = _numSnowParticles; i < numSnowParticles; i++)
                {
                    List<WatchVariableControl> snowParticle = GetSnowParticleControls(i);
                    _snowParticleControls.Add(snowParticle);
                    watchVariablePanelSnow.AddVariables(snowParticle);
                }
                _numSnowParticles = numSnowParticles;
            }
            else if (numSnowParticles < _numSnowParticles) // need to remove controls
            {
                for (int i = _numSnowParticles - 1; i >= numSnowParticles; i--)
                {
                    List<WatchVariableControl> snowParticle = _snowParticleControls[i];
                    _snowParticleControls.Remove(snowParticle);
                    watchVariablePanelSnow.RemoveVariables(snowParticle);
                }
                _numSnowParticles = numSnowParticles;
            }

            base.Update(updateView);
        }

    }
}