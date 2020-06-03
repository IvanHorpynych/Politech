\accept r11: 2
\accept r10: 0FFFDh \\ -3
accept r11: 8
accept r10: 3

link l1: ct

\ 1st operand is in R11.
\ 2nd operand is in R10.
\ Result is in R15.
\ Modifies r2, r3, r4, r7.

\ 1. Form result sign.
{ and r2, r11, 8000h; }
{ and r3, r10, 8000h; }
{ xor r2, r2, r3; }
{ or r15, r2, z; }

\ 2.1. Convert operand 1.
{ load rn, flags; and nil, r11, 8000h; }
{ cjp rn_z, pos_1; }
{ sub r11, z, r11, nz; }
pos_1

\ 2.2. Convert operand 2.
{ load rn, flags; and nil, r10, 8000h; }
{ cjp rn_z, pos_2; }
{ sub r10, z, r10, nz; }

equ x: r11
equ y: r10

pos_2
{ load rn, flags; sub nil, x, y, nz; } \ Check if x >= y.
{ cjp not rn_n, div; }
\ x < y.
{ xor r3, r3; }
{ jmap result; }

div
\ 4. Division.
{ xor r3, r3; } \ r3 is quotient.

div_loop
{ load rn, flags; sub x, x, y, nz; }
{ cjp rn_n, result ; }
{ add r3, 1; }
{ jmap div_loop; }

result
\ 5. Form result.
{ or r15, r15, r3; }
{ or r2, x, z; }

END
{}
