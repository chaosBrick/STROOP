.n64

addiu SP, SP, 0xFFC0
; Return early if there's no Mario object
lui t0, MarioObjectAddrHi
lw t0, MarioObjectAddrLo (t0)
beq r0, t0, @@EARLY_RETURN

; Push static registers to stack
sw ra, 0x34 (SP)
sw s4, 0x30 (SP)
sw s3, 0x2C (SP)
sw s2, 0x28 (SP)
sw s1, 0x24 (SP)
sw s0, 0x20 (SP)

; s4 shall be a permanent reference to the Mario object
or s4, r0, t0
; skip initializing if already Mario object is flagged
lh t0, 0x2 (s4)
andi t1, t0, 0x40
bnez t1, @@SKIP_INIT
; set initialized flag on Mario object
ori t1, t0, 0x40
sh t1, 0x2 (s4)
; clean up (this can cause failure?)
lui s1, 0x8040
beq r0, r0, @@CLEAN_UP_EARLY
ori s1, s1, 0x7FF8
@@SKIP_INIT:
; set up dummy Mario struct
lui s3, 0x8050
lui t8, 0x8037
ori at, r0, 0xBD
sh at, 0x5A8 (t8)
ori at, t8, 0x5B8
sw at, 0x598 (t8)
li at, 0x80064040
sw at, 0x5B8 (t8)

; do something
or s0, r0, r0
lui at, 0x8040
ori s1, at, 0x7FF8
lb at, 0x7FFF (at)
beq r0, at, @@CLEAN_UP_EARLY
nop

@@ITERATE_GHOSTS:
lw t0, 0x0 (s1)
bnez t0, @@GHOST_EXISTS
; create a new ghost object graph node
or a0, r0, r0
addiu a1, s1, 0xFF9C
lw a2, 0x14 (s4)
lui a3, 0x8038
ori at, a3, 0x5FDC
sw at, 0x10 (SP)
ori at, a3, 0x5FE4
sw at, 0x14 (SP)
jal 0x8037B9E0
ori a3, a3, 0x5FD0
; add the new graph node object as child to Mario's parent node
sw v0, 0x0 (s1)
lw a0, 0xC (s4)
jal 0x8037C044
or a1, v0, r0
@@GHOST_EXISTS:
; copy Mario's area and something else into the ghost node
lw s2, 0x0 (s1)
lb t1, 0x18 (s4)
sb t1, 0x18 (s2)
lw t1, 0x38 (s4)
sw t1, 0x38 (s2)
; find offset in ghost data buffer
lui at, GlobalTimerAddrHi
lw t0, GlobalTimerAddrLo (at)
andi t0, t0, 0x7F
sll t0, t0, 0x5
sll t1, s0, 0xC
addu t0, t0, t1
lui at, 0x8041
addu t0, t0, at
addiu t0, t0, 0x8800
; copy data from buffer to ghost node
; position
lw t1, 0x00 (t0)
sw t1, 0x20 (s2)
lw t1, 0x04 (t0)
sw t1, 0x24 (s2)
lw t1, 0x08 (t0)
sw t1, 0x28 (s2)
; angles (TODO: store angles as s16 in file?)
lw t1, 0x10 (t0)
sh t1, 0x1A (s2)
lw t1, 0x14 (t0)
sh t1, 0x1C (s2)
lw t1, 0x18 (t0)
sh t1, 0x1E (s2)
; do the hacky thing with Mario animations
lui t8, 0x8037
sw s2, 0x0580 (t8)
sw s3, 0x05C0 (t8)
lh t1, 0x1C (t0)
sh t1, 0x38 (SP)
ori a0, t8, 0x04F8
sh r0, 0x38 (s2)
sw r0, 0x05BC (t8)
jal set_mario_animation
lw a1, 0xC (t0)
lh t1, 0x38 (SP)
sh t1, 0x40 (s2)
;
addiu s3, s3, 0x4000
addiu s1, s1, 0xFF98
addiu s0, s0, 0x1
lui at, 0x8040
lb at, 0x7FFF (at)
sltu t0, s0, at
bnez t0, @@ITERATE_GHOSTS
sb s0, 0x60 (s2)

; delete leftover ghosts
lw s2, 0x0 (s1)
beq s2, r0, @@RETURN
or a0, r0, s2
@@CLEAN_UP_LOOP:
jal 0x8037C0BC
sw r0, 0x0 (s1)
@@CLEAN_UP_EARLY:
lw a0, 0x0 (s1)
bnez a0, @@CLEAN_UP_LOOP
addiu s1, s1, 0xFF98

@@RETURN:
; Pop static registers from stack
lw ra, 0x34 (SP)
lw s4, 0x30 (SP)
lw s3, 0x2C (SP)
lw s2, 0x28 (SP)
lw s1, 0x24 (SP)
lw s0, 0x20 (SP)
@@EARLY_RETURN:
jr ra
addiu SP, SP, 0x40
