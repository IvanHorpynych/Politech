accept poh: 2b0h, 1fh, 2, 42ch, 2dh, 5, 6, 10ch, 6eah, 8, 7ach, 11, 12, 13, 1aah, 14
\           R0    R1   R2 R3    R4   R5 R6 R7    R8    R9 R10   R11 R12 R13 R14   R15
accept rq: 4d6h

{add r11, r10, rq, nz; } \ R11 = R10 + RQ + 1
{field bus_d, 1aah, 0; add r6, r14, bus_d, z; } \ R6 = R14 + D
{field bus_d, 1aah, 0; sub sll, r13, r7, bus_d, z; } \ R13 = 2 * (R7 - D - 1)
{sub srl, r0, r0, r4, nz; } \ R0 = (R0 - R4) / 2
{sub sll, r3, r1, r3, nz; } \ R3 = 2 * (R1 - R3)
{sub srl, r9, r8, rq, z; } \ R9 = (R8 - RQ - 1) / 2
