accept r11: 0000010010000000% \ 8 = '1 * 2^4 = 100b = 8.
accept r10: 0000001010000000% \ 2 = '1 * 2^2 = 10b = 2.
\ Result: 0000010110000000 = '1 * 2^5 = 10000b = 16.
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

\ 2. Form result order.
{ and r2, r11, 7f00h; }
{ push nz, 7; }
{ rfct; or srl, r2, z; }

{ and r3, r10, 7f00h; }
{ push nz, 7; }
{ rfct; or srl, r3, z; }

{ add r2, r2, r3, z; }
{ or r7, r2, z; }

\ 3. Mantissa multiplication.
{ and r2, r11, 0ffh; } \ X.
{ and r3, r10, 0ffh; } \ Y.
{ xor r4, r4; }

{ push nz, 7; }

{ load rn, flags; and nil, r3, 1; }
{ cjp rn_z, m_1; }
{ add r4, r4, r2, z; }

m_1
{ or srl, r4, r4, z; }
{ or srl, r3, r3, z; }

{ rfct; }

\ 4. Result normalization.
m_n_s \ Multiply normalization start.
{ load rn, flags; and nil, r4, 80h; }
{ cjp not rn_z, m_n_e; }

{ or sll, r4, r4, z; }
{ sub r7, r7, 1, nz; }

{ jmap m_n_s; }

m_n_e \ Multiply normalization end.
\ Write result to r15.
{ push nz, 7; }
{ rfct; or sll, r7, z; }
{ and r7, r7, 7f00h; }

{ or r15, r7, z; }
{ or r15, r15, r4; }

END
{ }
