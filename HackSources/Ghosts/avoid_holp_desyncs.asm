.n64

; load gMarioStates[0] into t2 for future processing
; this ignores that we could in theory be rendering Luigi instead
lui T1, gMarioStatesHi
ori t2, t1, gMarioStatesLo

; if the currently rendered object isn't the real Mario, skip updating the held object entirely
lui t9, gCurGraphNodeObjectHi
lw t9, gCurGraphNodeObjectLo (T9)
lui at, MarioObjectAddrHi
lw at, MarioObjectAddrLo (AT)
bne t9, AT, 0x178
nop

; padding
nop