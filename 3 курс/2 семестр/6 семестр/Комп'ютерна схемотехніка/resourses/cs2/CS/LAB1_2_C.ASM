accept poh: 2F4h, 43Dh, 2, 2E5h, 67Bh, 5, 6, 12Fh, 32Bh, 9, 7C5h, 11, 12, 13, 43Fh, 15
accept rq: 6ffh
{add r11, r10, rq, nz;}
{field bus_d, 1EEh, 0; add r6,  r14, bus_d, z;}
{field bus_d, 1EEh, 0; sub sll, r13, r7, bus_d, z;}
{sub srl, r0, r0, r4, nz;}
{sub sll, r3, r1, r3, nz;}
{sub srl, r9, r8, rq, z;}
