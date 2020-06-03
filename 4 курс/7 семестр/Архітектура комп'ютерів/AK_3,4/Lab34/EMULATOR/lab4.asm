program Lab4
constant
	i=2
	n=6
	f=1
begin
///////////f = n!
1: cmple i, n*, !0 to 2
2: xn _, 3*,    !1 to 3
3: ifnot f,_,   !0 to 14
4: if f,_,      !0 to 7 
5: if i,_,      !0 to 6
6: x2 _,7*,     !1 to 8
7: mul _,_,     !0 to 9
8: add 1*,_,    !0 to 10
9: x2 _,11*,    !0 to 4
10: x2 _,12*,   !0 to 5
11: x2 _,13*,   !0 to 3
12: eq1 _,0*,   !1 to 13
13: eq2 _,_,    !0 to 1
14: out _,1*,    !0 to 14
end
