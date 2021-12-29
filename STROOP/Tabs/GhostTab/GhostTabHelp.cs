﻿using System;

namespace STROOP.Tabs.GhostTab
{
    partial class GhostTab
    {
        private void buttonTutorialRecord_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(850, 400);
            frm.SetText("Ghost Help",
                        "How to record ghosts",
@"The best way to record a ghost file is to use the 'recordghost.lua' script.
This script should be located next to your STROOP executable. (You can move it to a different location though.)
When you press 'Start', a new recording will begin at the current frame.
Hitting 'Stop' will save the ghost to 'tmp.ghost' at the location of the script file.
You can then load this file into STROOP to play it back later, or store it somewhere else.

Alternatively, you can record a ghost directly using STROOP using the 'Record Ghost' button.
To do this, you must first enable the ghost hack such that the respective panel reads 'Ghost hack is running'.
Then, when you hit 'Record Ghost', a new recording will begin at the current frame.
'Stop Recording' will end the recording and create a new entry in the ghost list directly.
You can then watch this ghost back immediately or save it to a file using the 'Save Selected' button.
IMPORTANT: Using this method to record ghosts may (and likely will) generate game lag to record all frame data.
Because of this, movie VI counts can desync and end movie playback prematurely.");
            frm.ShowDialog();
        }

        private void buttonTutorialPlayback_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(900, 500);
            frm.SetText("Ghost Help",
                        "How to play ghosts back",
@"To play back a ghost, you must first enable the ghost hack.
If no ghost recording is currently selected, STROOP will instead inject a ghost that will hover around Mario.
This way you can see if the hack is enabled and working correctly.

You can then load up any ghost file or select a previously recorded ghost from the list of ghosts.
Each recording has a 'Playback Start' value that indicates SM64's Global Timer value on the first frame of recorded data.
This value is used to determine when STROOP should inject which ghost frame, so if you're comparing against a ghost recorded from the same savestate as you're currently playing from the ghost should always be synchronized.

If, however, the Global Timer has a different value (i.e. different savestate, different star order, ...), you can resync the playback by modifying the 'Start Playback at Global Timer' value to match the Global Timer of your current run.
For convenience, you can check the game's current Global Timer value in the data view on the right.
Depending on when in the game the ghost recording started (i.e. Star Select screen, First frame of running, ...) you may need to adjust this value until your runs sync up properly.

IMPORTANT: Enabling the ghost hack may break some in-game objects.
Do not start m64's from savestates with the hack already enabled.
Always make sure your runs work from a clean savestate.
");
            frm.ShowDialog();
        }

        private void buttonTutorialNotes_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(900, 400);
            frm.SetText("Ghost Help",
                        "Notes",
@"The ghost hack currently works only on the US version of Super Mario 64 (and therefore also on numerous ROM hacks).

While in most cases the use of the ghost hack is unproblematic, there are several instances in which enabling the ghost hack may alter game behavior. It is therefore recommended that you only use the hack during a recording or playback, but never in a savestate from which an m64 file stars.
Always verify that your runs work without the hack.

Disabling the ghost hack via the 'Disabel Ghost hack' button may have unintended side effects, including crashing the game.
Try to keep a savestate around that doesn't have the hack enabled instead.

Ghost recordings are purely visual recordings. Some visual effects, such as Mario's upper body tilt, will not perfectly match.
Certain data may look misleading in the map tab. (For instance, the ghost's 'graphics angle' during a sideflip looks inverted.)
The cap state of the ghost is the same as the real Mario's, except that the vanish cap effect will always be active.
");
            frm.ShowDialog();
        }

        private void buttonTutorialFileWatch_Click(object sender, EventArgs e)
        {
            Forms.InfoForm frm = new Forms.InfoForm();
            frm.Size = new System.Drawing.Size(700, 300);
            frm.SetText("Ghost Help",
                        "Using the file watch list",
@"You can let STROOP automatically detect when 'recordghost.lua' saves a new ghost recording.
To do so, click the 'Edit File Watch List' button, then hit 'Add...' and navigate to wherever you have located your 'recordghost.lua' file and select it.

You can add as many instances of the script as you please. The information for paths will be stored in 'Config/Config.xml'
");
            frm.ShowDialog();
        }
    }
}