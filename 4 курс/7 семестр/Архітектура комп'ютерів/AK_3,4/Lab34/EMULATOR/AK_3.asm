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
6: mul _, 4*, !1 to 7
7: mul _, _,  !0 to 8
8: out _, 1*, !0 to 8

;////////////////////////// f4 = (a2+b2+c2+d2)ab
9:  sqr a,0*, !0 to 11
10: sqr b,0*, !1 to 11
11: add _,_,  !0 to 15
12: sqr c,0*, !0 to 14
13: sqr d,0*, !1 to 14
14: add _,_,  !1 to 15
15: add _,_,  !0 to 16
16: mul _,a,  !0 to 17
17: mul _,b,  !0 to 18
18: out _,4*, !0 to 18

;////////////////////////// f3  = a-b-ca+12sqrt(b2+a2)
19: sub a,b,   !0 to 21
20: mul c,a,   !1 to 21
21: sub _,_,   !0 to 27
22: sqr b,0*,  !0 to 24
23: sqr a,0*,  !1 to 24
24: add _,_,   !0 to 25
25: sqrt _,0*, !0 to 26
26: mul _,12*, !1 to 27
27: add _,_,   !0 to 28
28: out _,3*,  !0 to 28

;////////////////////////// f2=sqrt(b+5a+c2)+2ac
29: mul 5*, a, !1 to 32
30: sqr c, 0*, !1 to 34
31: mul a, c, !1 to 33
32: add b, _, !0 to 34
33: mul 2*, _, !1 to 36
34: add _, _, !0 to 35
35: sqrt _, 0*, !0 to 36
36: add _, _, !0 to 37
37: out _, 2*, !0 to 37
    
;//////////////////////////f5=Eai*x^i
38: mul a, x, !0 to 42
39: mul x, x, !0 to 51
40: mul b, _, !1 to 42
41: mul x, _, !0 to 52
42: add _, _, !0 to 45
43: mul c, _, !1 to 45
44: mul x, _, !0 to 53
45: add _, _, !0 to 48
46: mul d, _, !1 to 48
47: mul x, _, !1 to 49
48: add _, _, !0 to 50
49: mul e, _, !1 to 50
50: add _, _, !0 to 54
51: x2 _, 41*, !1 to 40
52: x2 _, 44*, !1 to 43
53: x2 _, 47*, !1 to 46
54: out _, 5*, !0 to 54

end
