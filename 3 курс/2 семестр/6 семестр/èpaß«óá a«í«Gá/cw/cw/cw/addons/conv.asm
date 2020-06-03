\ Int argument is in R11.
\ Real result is in R11.
\ Modifies R11, R2, R3, R4.

{ load rn, flags; and nil, r11, 8000h; } \ Check sign bit.
{ cjp rn_z, positive; }
\ Not positive - invert, add 1.
{ xor r11, r11, 7fffh; } \ Invert.
{ add r11, r11, 1; } \ Add 1.

positive
{ and r13, r11, 8000h; } \ Copy sign bit.
{ and r4, r11, 7fffh; } \ Copy of mantiss.

\ Find order.
{ xor r2, r2, r2; }
{ or r2, 15, r2; } \ Order counter - maximum by default.
{ xor r3, r3, r3; }
{ or r3, 4000h, r3; } \ Order mask.

order_loop
{ load rn, flags; and nil, r11, r3; } \ Test current bit.
{ cjp not rn_z, order_end; }

{ sub r2, r2, 1, nz; } \ Dec order.
{ add srl, r3, r3, 0; } \ Shift right mask.
{ jmap order_loop; }

order_end

\ Remember order.
{ or r3, r2, 0; }

\ Prepare order.
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).
{ add sll, r2, r2, 0; } \ Shift order 1 left (1).

\ Write order to format.
{ or r13, r13, r2; }

\ Shift mantiss.
\ Determine mantiss position.
{ load rn, flags; sub nil, 8, r3, nz; }

{ cjp rn_n, mant_h_pt; }

\ A case, when order <= 8.
{ sub r2, 8, r3, nz; } \ R2 = 8 - R3

m_l_1
{ load rn, flags; or nil, r2, r2; }
{ cjp rn_z, m_l_1_end; }
{ sub r2, r2, 1, nz; }

{ add sll, r4, r4, 0; }

{ jmap m_l_1 ; }

m_l_1_end
{ and r4, r4, 0ffh; } \ Write mantiss to result.
{ or r13, r13, r4; }

{ jmap conv_end; }

mant_h_pt
\ A case, when order > 8.
{ sub r2, 16, r3, nz; } \ R2 = 15 - R3

m_l_2
{ load rn, flags; or nil, r2, r2; }
{ cjp rn_z, m_l_2_end; }
{ sub r2, r2, 1, nz; }

{ add sll, r4, r4, 0; }

{ jmap m_l_2 ; }

m_l_2_end
{ and r4, r4, 0ff00h; } \ Write mantiss to result.
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ add srl, r4, r4, 0; }
{ or r13, r13, r4; }

{ or r11, r13, z; }
{ or r15, r13, z; }

conv_end
{ }
