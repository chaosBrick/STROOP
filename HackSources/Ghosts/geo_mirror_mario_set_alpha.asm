.n64


addiu SP, SP, 0xFFD0
sw ra, 0x14 (SP)
addiu at, r0, 0x1
bne a0, at, @@RETURN
or v0, r0, r0
or a0, a1, r0
lui t0, gBodyStatesAddrHi
addiu t0, t0, gBodyStatesAddrLo
lw t8, 0x18 (a1)
sll t9, t8, 0x2
addu t9, t9, t8
sll t9, t9, 0x3
addu t1, t9, t0
lui t0, gCurGraphNodeObjectHi
lw t0, gCurGraphNodeObjectLo (t0)
lui at, MarioObjectAddrHi
lw at, MarioObjectAddrLo (at)
bne t0, at, @@IsGhost
lh t4, 0x8 (t1)
andi t5, t4, 0x0100
beq t5, r0, @@JAL
ori a1, r0, 0xFF
beq r0, r0, @@JAL
andi a1, t4, 0xFF
@@IsGhost:
lb t8, 0x61 (t0)
beq t8, r0, @@JAL
ori a1, r0, 0xFF
ori A1, r0, 0x7F
@@JAL:
jal make_gfx_mario_alpha;
nop
@@RETURN:
lw ra, 0x14 (SP)
jr ra
addiu SP, SP, 0x30