program lab3
constant
a = 1
b = 2
c = 3
d = 4
begin
 1: xn   a,  2, !0 to  3
 2: xn   b,  2, !1 to  5
 3: mul  _,  _, !0 to 13
 4: sqr  _, 0*, !0 to 10
 5: eq2 0*,  _, !1 to  3
 6: eq2 0*,  _, !0 to  7
 7: sqr  _, 0*, !1 to 10
 8: sqr  c, 0*, !0 to 11
 9: sqr  d, 0*, !1 to 11
10: add  _,  _, !0 to 12
11: add  _,  _, !1 to 12
12: add  _,  _, !1 to 13
13: mul  _,  _, !0 to 14
14: out  _, 4*, !0 to 14
end
