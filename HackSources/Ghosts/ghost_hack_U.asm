; register meanings:
; s4: permanent reference to Mario object
; s3: Mario animation buffer pointer for currently processed ghost node
; s2: Currently processed ghost node
; s1: Pointer to currently processed ghost node
; s0: Processed ghosts counter

set_mario_animation equ 0x802509B8
make_gfx_mario_alpha equ 0x80276F90

MarioObjectAddrHi equ 0x8036
MarioObjectAddrLo equ 0x1158

GlobalTimerAddrHi equ 0x8033
GlobalTimerAddrLo equ 0xD5D4

gBodyStatesAddrHi equ 0x8034
gBodyStatesAddrLo equ 0xB3B0

gCurGraphNodeObjectHi equ 0x8033
gCurGraphNodeObjectLo equ 0xDF00


.create "./build/ghost_loop_U.bin", 0x00000000
.include "./ghost_loop.asm"
.close

; 802770a4
.create "./build/geo_mirror_mario_set_alpha_U.bin", 0x00000000
.include "./geo_mirror_mario_set_alpha.asm"
.close

; 802776D8
.create "./build/geo_switch_mario_cap_effect_U.bin", 0x00000000
.include "./geo_switch_mario_cap_effect.asm"
.close
