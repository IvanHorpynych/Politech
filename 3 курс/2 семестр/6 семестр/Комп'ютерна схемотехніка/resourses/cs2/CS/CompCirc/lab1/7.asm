accept rq: 000Fh \ x1 = 15
accept r5: 0fffdh \ x2 = -3
accept r6: 000ch \ x4 = 12
accept r0: 0000h \ F

{field bus_d, 000ah, 0; sub sll, r0, r0, bus_d, nz; } \ F = 2(F - x3)

{add r0, r0, r5, z; } \ F = F + x2
{field bus_d, 000ah, 0; sub sll, r0, r0, bus_d, nz; } \ F = 2(F - x3)

{add r0, r0, rq, z; } \ F = F + x1
{add sll, r0, r0, r6, z; } \ F = 2(F + x4)

{add r0, r0, r5, z; } \ F = F + x2
{field bus_d, 000ah, 0; sub r0, r0, bus_d, nz; } \ F = F - x3
{add sll, r0, r0, r6, z; } \ F = 2(F + x4)
