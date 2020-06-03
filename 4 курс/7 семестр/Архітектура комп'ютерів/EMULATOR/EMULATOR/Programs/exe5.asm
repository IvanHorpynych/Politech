program example
constant
           a=1
           b=2
           c=3
           d=4
begin
	1: add a, b, !0 to 3
	2: add c, d, !1 to 3
	3: mul* _,_, !1 to 4
	4: cmpm 1,_, !0 to 5
	5: xn _, 3*, !1 to 6
	6: if C, _, !0 to 16
	7: ifnot 2, _, !0 to 9
	8: ifnot 3, _, !0 to 10
	9: add _, 3*, !0 to 11
	10: add _, 3*, !0 to 12
	11: x2 _, 7*, !0 to 13
 	12: x2 _, 8*, !0 to 14
	13: x2 _, 6*, !0 to 15
	14: eq1 _, 3*, !1 to 15
	15: eq2 _, _, !0 to 4
	16: out _ , 2, !0 to 16
end
