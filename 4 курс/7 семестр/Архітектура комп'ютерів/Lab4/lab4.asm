program lab4
constant
i = 2
n = 5
f = 1
begin
 1: eq2   0*,  _, !0 to  7
 2: if     f,  _, !0 to  9
 3: ifnot  f,  _, !1 to  5
 4: ifnot  i,  _, !0 to 10
 5: mul    i,  _, !0 to 11
 6: eq1    _, 0*, !1 to  1
 7: cmpm   i, n*, !0 to  8
 8: xn     _, 3*, !1 to  2
 9: out    _, 4*, !0 to  9
10: add    _, 1*, !0 to 12
11: xn     _, 3*, !0 to  1
12: xn     _, 3*, !0 to  4
end
