program lab3
constant
a = 1
b = 2
c = 3
begin
 1: xn    a,  2, !0 to  3
 2: xn    c,  2, !1 to  5
 3: mul   _,  _, !1 to 10
 4: mul   _, 5*, !0 to  8
 5: eq2  0*,  _, !1 to  3
 6: eq2  0*,  _, !0 to  7
 7: sqr   _, 0*, !1 to  9
 8: add   _,  b, !0 to  9
 9: add   _,  _, !0 to 11
10: mul  2*,  _, !0 to 12
11: sqrt  _, 0*, !1 to 12
12: add   _,  _, !0 to 13
13: out   _, 1*, !0 to 13
end
