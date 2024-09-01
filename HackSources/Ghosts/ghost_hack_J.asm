; register meanings:
; s4: permanent reference to Mario object
; s3: Mario animation buffer pointer for currently processed ghost node
; s2: Currently processed ghost node
; s1: Pointer to currently processed ghost node
; s0: Processed ghosts counter

set_mario_animation equ 0x802507e8
make_gfx_mario_alpha equ 0x802769E0

MarioObjectAddrHi equ 0x8036
MarioObjectAddrLo equ 0xFDE8

GlobalTimerAddrHi equ 0x8033
GlobalTimerAddrLo equ 0xC694

gBodyStatesAddrHi equ 0x8034
gBodyStatesAddrLo equ 0xA040

gCurGraphNodeObjectHi equ 0x8033
gCurGraphNodeObjectLo equ 0xCFA0


.create "ghost_loop_J.bin", 0x00000000
.include "./ghost_loop.asm"
.Close

; 80276af4
.create "geo_mirror_mario_set_alpha_J.bin", 0x00000000
.include "./geo_mirror_mario_set_alpha.asm"
.close

; 80277128
.create "geo_switch_mario_cap_effect_J.bin", 0x00000000
.include "./geo_switch_mario_cap_effect.asm"
.close