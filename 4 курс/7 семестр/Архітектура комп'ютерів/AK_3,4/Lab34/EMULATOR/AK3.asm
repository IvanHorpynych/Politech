program lab3
	constant
		a = 1
		b = 2
		c = 3
		d = 4
		e = 5
		x = 1
		y = 2
begin	

;////////////////////////// f1 = y2+by+2ab+4
1: sqr y, 0*, !0 to 5
2: mul b, y,  !1 to 5
3: mul a, b,  !1 to 4
4: mul 2*, _, !0 to 6
5: add _, _,  !0 to 7
6: add _, 4*, !1 to 7
7: add _, _,  !0 to 8
8: out _, 2*, !0 to 8


;////////////////////////// f2=sqrt(b+5a+c2)+2ac
9: mul 5*, a, !1 to 12
10: sqr c, 0*, !1 to 14
11: mul a, c, !1 to 13
12: add b, _, !0 to 14
13: mul 2*, _, !1 to 16
14: add _, _, !0 to 15
15: sqrt _, 0*, !0 to 16
16: add _, _, !0 to 17
17: out _, 3*, !0 to 17
    
;//////////////////////////f5=Eai*x^i
18: mul a, x, !0 to 22
19: mul x, x, !0 to 31
20: mul b, _, !1 to 22
21: mul x, _, !0 to 32
22: add _, _, !0 to 25
23: mul c, _, !1 to 25
24: mul x, _, !0 to 33
25: add _, _, !0 to 28
26: mul d, _, !1 to 28
27: mul x, _, !1 to 29
28: add _, _, !0 to 30
29: mul e, _, !1 to 30
30: add _, _, !0 to 34
31: x2 _, 21*, !1 to 20
32: x2 _, 24*, !1 to 23
33: x2 _, 27*, !1 to 26
34: out _, 4*, !0 to 34

end
