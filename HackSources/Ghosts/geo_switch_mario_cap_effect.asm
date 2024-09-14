.n64

addiu SP, SP, 0xFFF8
lui t9, gBodyStatesAddrHi
addiu t9, t9, gBodyStatesAddrLo
lui t0, gCurGraphNodeObjectHi
lw t0, gCurGraphNodeObjectLo (t0)
lb t2, 0x61 (t0)
beq t2, r0, @@RETURN
lb t1, 0x8 (t8)
sh t1, 0x1E (a1)
lui at, MarioObjectAddrHi
lw at, MarioObjectAddrLo (at)
beq t0, at, @@RETURN
nop
sh t2, 0x1e (a1)

@@RETURN:
or v0, r0, r0
jr ra
addiu SP, SP, 0x0008