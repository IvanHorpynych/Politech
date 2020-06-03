program lab3
constant
a = 1
b = 2
c = 3
begin
 1: xn    a,  3, !0 to  3
 2: xn    b,  2, !1 to  6
 3: sub   _,  _, !0 to 12
 4: mul   _,  c, !1 to 12
 5: sqr   _, 0*, !0 to  9
 6: eq2  0*,  _, !1 to  3
 7: eq2  0*,  _, !0 to  8
 8: sqr   _, 0*, !1 to  9
 9: add   _,  _, !0 to 10
10: sqrt  _, 0*, !1 to 11
11: mul  12*, _, !1 to 13
12: sub   _,  _, !0 to 13
13: add   _,  _, !0 to 14
14: out   _, 2*, !0 to 14
end
