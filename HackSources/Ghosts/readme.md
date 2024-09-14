# Ghost Hack
This hack is designed to hook into the game's update logic to add and remove Gfx nodes similar to Mario's Gfx node,
and updating the state of said nodes to render a number previously recorded runs in-game.  

The hack is built to work with the NTSC and JP version of the game. Other versions are currently unsupported.

Supported features are many ghosts at once (up to ~18 to 20 without moving the gfx pool, ~30 with a moved gfx pool due to unknown limitations),
as well as individually coloring the Marios' hats, including the "live Mario" that is not actually a ghost,
and making ghosts "transparent" (i.e. apply the vanish cap effect) individually.  

While the ghost hack is running, cap effects (i.e. Metal cap, wing cap and vanish cap) will not be rendered as usual.
Also, Mario will be unable to blink his eyes.

# How to build
The process of "building" the hack shall result in the `GhostHackUS.hck` and `GhostHackJP.hck` files respectively.
`.hck` files are really just text files with a specific format, in which each line represents a hexadecimal RAM location,
followed by a colon and then the payload to inject at that location as a hexadecimal representation, e.g.
```
8027ABD8: 0C 10 20 00
80276BEC: 34 0C 00 01
```
Download [armips](https://github.com/Kingcom/armips) and invoke it on `ghost_hack_US.asm` or `ghost_hack_JP` respectively. This will produce binary files in the `build/` directory.
Each file name starts with the version of the `.hck` file it is intended for, followed by the RAM address at which its contents are to be injected.  
All file contents must be present in the `.hck` file, plus the additional payloads described in `additional hacks.txt`.

# How it works
The hack places a hook into `area_update_objects` to jump into the "ghost_loop.asm" function placed at RAM address 0x80408000.
This function creates `GraphNodeObject` instances for ghosts as appropriate,
and updates their animation and transform state as appropriate by reading from dedicated buffers that STROOP writes to,
starting from 0x80409B00, with each buffer being 0x1000 bytes large and covering 0x80 frames.
This ensures smooth playback even when STROOP is falling behind on RAM writes into said buffers.
The number of ghosts to render is stored at 0x80407FFF as a single byte.  
To ensure that ghosts are only being rendered when they should be, the code checks if `gMarioObject` is set.
It also skips over initializing ghosts for one frame by flagging `gMarioObject` with a custom flag in order to avoid some odd object loading bugs.  

To produce the "ghost" effect, `gfx_mirror_mario_set_alpha` and `geo_siwtch_mario_cap_effect` are modified to respect the individual ghosts' information.
Additionally, `geo_switch_mario_hand_grab_pos` is modified to update the HOLP only for the real Mario object, preventing desyncs stemming from ghosts updating the HOLP.

# Colored Hats
What seems to be a neat little gimmick - namely giving each ghost Mario a different hat color - is actually not only surprisingly helpful in distinguishing ghosts while TASing,
but was also rather tricky to implement.  
Changing the color of Mario's hat requires changing the geo layout of Mario - the most complex in the entire game - after it has been loaded in.
Rather than rendering the hat with "hard coded light values", the RSP instructions need to be generated dynamically.
The process involves finding (technically guessing) geo layout nodes in RAM bank 0x04 and replacing them with a different kind of geo layout node
(that is technically too large to fit, but it never actually breaks anything fortunately).  
To see how this is done in detail, see `STROOP/Tabs/GhostTab/ColoredHats.cs`.