program lab3
constant
a = 1
b = 2
c = 3
d = 4
begin
 1: xn    a,  7, !0 to  4
 2: xn    b,  5, !1 to 11
 3: xn    c,  4, !1 to 16
 4: mul   _,  _, !1 to 31
 5: mul   _, 5*, !0 to 11
 6: sub   _,  _, !0 to 37
 7: mul   _,  _, !1 to 37
 8: sqr   _, 0*, !0 to 25
 9: mul   _,  _, !0 to 33
10: sqr   _, 0*, !0 to 26
11: add   _,  _, !0 to 28
12: eq2  0*,  _, !1 to  6
13: eq2  0*,  _, !0 to 22
14: eq2  0*,  _, !1 to  9
15: eq2  0*,  _, !0 to 23
16: eq2  0*,  _, !1 to  4
17: eq2  0*,  _, !0 to 21
18: eq2  0*,  _, !1 to  7
19: eq2  0*,  _, !0 to 24
20: sqr   d, 0*, !1 to 27
21: sqr   _, 0*, !1 to 28
22: sqr   _, 0*, !1 to 25
23: sqr   _, 0*, !1 to 26
24: sqr   _, 0*, !0 to 27
25: add   _,  _, !0 to 29
26: add   _,  _, !0 to 30
27: add   _,  _, !1 to 30
28: add   _,  _, !0 to 32
29: sqrt  _, 0*, !1 to 34
30: add   _,  _, !1 to 33
31: mul  2*,  _, !0 to 35
32: sqrt  _, 0*, !1 to 35
33: mul   _,  _, !0 to 36
34: mul 12*,  _, !1 to 39
35: add   _,  _, !0 to 38
36: out   _, 4*, !0 to 36
37: sub   _,  _, !0 to 39
38: out   _, 1*, !0 to 38
39: add   _,  _, !0 to 40
40: out   _, 2*, !0 to 40
end
