accept r9: 9h
accept rq: 0Ah
accept r12: 0fff5h
equ  d:  0Ch

{ xor  r3, r3; }
{ add sla, r3, r9; }
{ add sla, r3, r9; }
{ add  r3, r12; }
{ sub sla, r3, d, nz; }
{ sub sla, r3, rq, nz; }
