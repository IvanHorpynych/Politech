; File: IRRATNAL.LSP  (C)	04/29/89	Soft Warehouse, Inc.


;		    Section 6.2:  Irrational Functions

; This file provides functions for computing exponential, logarithmic,
; trigonometric, and hyperbolic functions.  The results are as accurate
; as the current precision setting will allow.


(DEFUN EXACT-MODEP ()
; Returns T if in exact mode, NIL otherwise.
  (AND *EXACT-MODE* (NEQ *EXACT-MODE* 'RATIONAL)) )


(DEFUN EXP (N)
; If N is a number, returns e raised to the Nth power, where e is the base
; of the natural logarithms (2.71828...).
  ((NUMBERP N)
    ((ZEROP N) 1)
    ((EXACT-MODEP)
      (BREAK (LIST 'EXP N) "Exact Mode") )
    (EXP-CALC N) )
  (BREAK (LIST 'EXP N) "Nonnumeric Argument") )


(DEFUN EXPT (N M
; If N and M are numbers, returns N raised to the Mth power.
    ANS)
  ((AND (NUMBERP N) (NUMBERP M))
    ((= N 1) 1)
    ((ZEROP M) 1)
    ((= M 1) N)
    ((MINUSP M)
      (/ 1 (EXPT N (- M))) )
    ((ZEROP N) 0)
    ((INTEGERP M)
      (INTEGER-POWER N M) )
    ((MINUSP N)
      ((AND (ODDP (DENOMINATOR M))
	    (SETQ ANS (FRACTIONAL-POWER (- N) M)))
	((EVENP (NUMERATOR M)) ANS)
	(- ANS) )
      (BREAK (LIST 'EXPT N M) "Invalid Argument") )
    ((FRACTIONAL-POWER N M))
    (BREAK (LIST 'EXPT N M) "Exact Mode") )
  (BREAK (LIST 'EXPT N M) "Nonnumeric Argument") )

(DEFUN SQRT (N)
; If N is a nonnegative number, returns the square root of N.
  (EXPT N 1/2) )

(DEFUN ISQRT (N)
; If N is a nonnegative integer, returns the greatest integer less than
; or equal to the positive square root of N.
  ((INTEGERP N)
    ((>= N 0)
      ((ZEROP N) 0)
      (ISQRT-CALC N) )
    (BREAK (LIST 'ISQRT N) "Invalid Argument") )
  (BREAK (LIST 'ISQRT N) "Noninteger Argument") )


(DEFUN LN (N)
; If N is a positive number, returns the natural log of N.
  ((NUMBERP N)
    ((PLUSP N)
      ((= N 1) 0)
      ((EXACT-MODEP)
	(BREAK (LIST 'LN N) "Exact Mode") )
      (LN-CALC N) )
    (BREAK (LIST 'LN N) "Invalid Argument") )
  (BREAK (LIST 'LN N) "Nonnumeric Argument") )

(DEFUN LOG (N M)
; If N and M are positive numbers, returns the log of N to base M.
; M defaults to e, the base of the natural logarithms.
  ((NOT M)
    (LN N) )
  (/ (LN N) (LN M)) )


(DEFUN PI ()
; Returns an approximation to pi accurate to the current precision.
  ((EXACT-MODEP)
    (BREAK (LIST 'PI) "Exact Mode") )
  (PI-CALC) )


(DEFUN SIN (N)
; Returns the sine of N radians.
  ((NUMBERP N)
    ((ZEROP N) 0)
    ((EXACT-MODEP)
      (BREAK (LIST 'SIN N) "Exact Mode") )
    (SETQ N (/ N (/ (PI) 4)))
    (SINCOS-CALC (REM N) (TRUNCATE N)) )
  (BREAK (LIST 'SIN N) "Nonnumeric Argument") )

(DEFUN COS (N)
; Returns the cosine of N radians.
  ((NUMBERP N)
    ((ZEROP N) 1)
    ((EXACT-MODEP)
      (BREAK (LIST 'COS N) "Exact Mode") )
    (SETQ N (/ N (/ (PI) 4)))
    (SINCOS-CALC (REM N) (+ (TRUNCATE N) 2)) )
  (BREAK (LIST 'COS N) "Nonnumeric Argument") )

(DEFUN TAN (N)
; Returns the tangent of N radians.
  (/ (SIN N) (COS N)) )

(DEFUN COT (N)
; Returns the cotangent of N radians.
  (/ (COS N) (SIN N)) )

(DEFUN SEC (N)
; Returns the secant of N radians.
  (/ (COS N)) )

(DEFUN CSC (N)
; Returns the cosecant of N radians.
  (/ (SIN N)) )


(DEFUN ATAN (N M)
; Returns in radians the angle corresponding to the vector whose opposite
; component is N and whose adjacent component is M.  M defaults to 1.
; For all n and m,  -pi < (ATAN n m) <= pi  and  -pi/2 <= (ATAN n) <= pi/2.
  ((AND (NUMBERP N) (OR (NULL M) (NUMBERP M)))
    ((AND (ZEROP N) (OR (NULL M) (= M 1)))  0)
    ((EXACT-MODEP)
      (BREAK (LIST 'ATAN N M) "Exact Mode") )
    ((NUMBERP M)			; Reduce to one argument case
      ((ZEROP M)
	((ZEROP N)
	  (BREAK (LIST 'ATAN N M) "Invalid Argument") )
	(* (SIGNUM N) (/ (PI) 2)) )
      ((ZEROP N)
	(* (- 1 (SIGNUM M)) (/ (PI) 2)) )
      (+ (ATAN (/ N M)) (* (SIGNUM N) (- 1 (SIGNUM M)) (/ (PI) 2))) )
    (ATAN-CALC N) )
  ((NULL M)
    (BREAK (LIST 'ATAN N) "Nonnumeric Argument") )
  (BREAK (LIST 'ATAN N M) "Nonnumeric Argument") )

(DEFUN ACOT (N M)
; Returns in radians the angle corresponding to the vector whose opposite
; component is M and whose adjacent component is N.
  ((NOT M)
    (- (/ (PI) 2) (ATAN N)) )
  (ATAN M N) )

(DEFUN ASIN (N)
; Returns the arcsine of N in radians.
; For all -1 <= n <= 1, -pi/2 <= (ASIN n) <= pi/2.
  ((NUMBERP N)
    ((EXACT-MODEP)
      (BREAK (LIST 'ASIN N) "Exact Mode") )
    ((MINUSP N)
      (- (ASIN (- N))) )
    ((= N 1)
      (/ (PI) 2) )
    ((< N 1)
      (ATAN (/ N (SQRT (- 1 (* N N))))) )
    (BREAK (LIST 'ASIN N) "Invalid Argument") )
  (BREAK (LIST 'ASIN N) "Nonnumeric Argument") )

(DEFUN ACOS (N)
; Returns the arccosine of N in radians.
; For all -1 <= n <= 1, 0 <= (ACOS n) <= pi.
  ((NUMBERP N)
    ((= N 1) 0)
    ((EXACT-MODEP)
      (BREAK (LIST 'ACOS N) "Exact Mode") )
    ((= N -1)
      (PI) )
    ((< -1 N 1)
      (- (/ (PI) 2) (ATAN (/ N (SQRT (- 1 (* N N)))))) )
    (BREAK (LIST 'ACOS N) "Invalid Argument") )
  (BREAK (LIST 'ACOS N) "Nonnumeric Argument") )

(DEFUN ASEC (N)
; Returns the arcsecant of N in radians.
  (ACOS (/ N)) )

(DEFUN ACSC (N)
; Returns the arccosecant of N in radians.
  (ASIN (/ N)) )


(DEFUN SINH (N)
; Returns the hyperbolic sine of N.
  (SETQ N (EXP N))
  (/ (- N (/ N)) 2) )

(DEFUN COSH (N)
; Returns the hyperbolic cosine of N.
  (SETQ N (EXP N))
  (/ (+ N (/ N)) 2) )

(DEFUN TANH (N)
; Returns the hyperbolic tangent of N.
  (/ (SINH N) (COSH N)) )

(DEFUN COTH (N)
; Returns the hyperbolic cotangent of N.
  (/ (COSH N) (SINH N)) )

(DEFUN SECH (N)
; Returns the hyperbolic secant of N.
  (/ (COSH N)) )

(DEFUN CSCH (N)
; Returns the hyperbolic cosecant of N.
  (/ (SINH N)) )


(DEFUN ASINH (N)
; Returns the hyperbolic arcsine of N.
  (LN (+ N (SQRT (+ (* N N) 1)))) )

(DEFUN ACOSH (N)
; Returns the hyperbolic arccosine of N.
  (LN (+ (ABS N) (SQRT (- (* N N) 1)))) )

(DEFUN ATANH (N)
; Returns the hyperbolic arctangent of N.
  (/ (LN (/ (+ 1 N) (- 1 N))) 2) )

(DEFUN ACOTH (N)
; Returns the hyperbolic arccotangent of N.
  (/ (LN (/ (+ N 1) (- N 1))) 2) )

(DEFUN ASECH (N)
; Returns the hyperbolic arcsecant of N.
  (ACOSH (/ N)) )

(DEFUN ACSCH (N)
; Returns the hyperbolic arccosecant of N.
  (ASINH (/ N)) )

;	* * *	Series Approximation Functions	 * * *

(SETQ *PRECISION* 16)

(DEFUN STORED-VALUE (SYM)
; Returns the stored value of SYM accurate to current precision, else NIL.
  ((EQL (GET SYM 'PRECISION) (PRECISION))
    (EVAL SYM) )
  ((AND (GET SYM 'PRECISION) (> (GET SYM 'PRECISION) (PRECISION)))
    (SET SYM (ROUND/ (NUMERATOR (EVAL SYM)) (DENOMINATOR (EVAL SYM)))) ) )

(DEFUN ROUND/ (NMB1 NMB2
    *EXACT-MODE* )
  (/ NMB1 NMB2) )


(DEFUN EXP-CALC (N
; Returns e^N accurate to the current precision.
; Basis: e^(p/q) = ((...(((q+p)2q+p^2)3q+p^3)4q+...+p^(m-1))mq + p^m)/(m! q^m)
    M NUMR DENR TERM P Q PRECISION BITS)
  ((ZEROP N) 1)
  ((MINUSP N)
    (/ 1 (EXP-CALC (- N))) )
  ((AND (= N 1) (STORED-VALUE '*E*)))
  ((> N 1)
    (* (INTEGER-POWER (EXP-CALC 1) (TRUNCATE N)) (EXP-CALC (REM N))) )
  (SETQ PRECISION (+ (* *PRECISION* (PRECISION)) 12) ;(12=accurate, 7=fast)
	P (NUMERATOR N)
	Q (DENOMINATOR N)
	TERM P
	NUMR (+ P Q)
	DENR Q
	M 1)
  (LOOP
    (INCQ M)
    (SETQ TERM (* P TERM)
	  NUMR (+ TERM (* Q NUMR M))
	  DENR (* Q DENR M)
	  BITS (- PRECISION (INTEGER-LENGTH DENR)))
    (IF (MINUSP BITS)
	(SETQ TERM (SHIFT TERM BITS)
	      NUMR (SHIFT NUMR BITS)
	      DENR (SHIFT DENR BITS)) )
    ((ZEROP TERM)
      ((= N 1)
	(PUT '*E* 'PRECISION (PRECISION))
	(SETQ *E* (ROUND/ NUMR DENR)) )
      (ROUND/ NUMR DENR) ) ) )


(DEFUN FRACTIONAL-POWER (M N
; If M is a positive number and N is a number, returns M^N if it is exactly
; representable, otherwise it returns NIL.
    NUMR DENR)
  ((MINUSP N)
    ((SETQ M (FRACTIONAL-POWER M (- N)))
      (/ 1 M) ) )
  ((EXACT-MODEP)
    (SETQ M (INTEGER-POWER M (NUMERATOR N))
	  N (DENOMINATOR N))
    ((INTEGERP M)
      (SETQ NUMR (INTEGER-ROOT M N))
      ((= (INTEGER-POWER NUMR N) M) NUMR) )
    (SETQ NUMR (INTEGER-ROOT (NUMERATOR M) N)
	  DENR (INTEGER-ROOT (DENOMINATOR M) N))
    ((AND (= (INTEGER-POWER NUMR N) (NUMERATOR M))
	  (= (INTEGER-POWER DENR N) (DENOMINATOR M)))
      (/ NUMR DENR) ) )
  ((> (+ (* 2 (NUMERATOR N)) (DENOMINATOR N)) 15)
    (EXP (* N (LN M))) )
  (SETQ M (INTEGER-POWER M (NUMERATOR N))
	N (DENOMINATOR N))
  ((INTEGERP M)
    (RATIONAL-ROOT M N) )
  (/ (RATIONAL-ROOT (NUMERATOR M) N) (RATIONAL-ROOT (DENOMINATOR M) N)) )


(DEFUN INTEGER-POWER (M N
; If M is a number and N is an integer, returns M^N.
    NUM DEN NUMR DENR)
  ((EQ N 1) M)
  (IF (MINUSP N)
      (SETQ M (/ 1 M)))
  ((INTEGERP M)
    (SETQ NUMR 1)
    (LOOP
      ( ((ODDP N)
	  (SETQ NUMR (* M NUMR)) ) )
      ((ZEROP (SETQ N (SHIFT N -1))) NUMR)
      (SETQ M (* M M)) ) )
  (SETQ NUM (NUMERATOR M)
	DEN (DENOMINATOR M)
	NUMR 1
	DENR 1)
  (LOOP
    ( ((ODDP N)
	(SETQ NUMR (* NUM NUMR)
	      DENR (* DEN DENR)) ) )
    ((ZEROP (SETQ N (SHIFT N -1)))
      (/ NUMR DENR) )
    (SETQ NUM (* NUM NUM)
	  DEN (* DEN DEN)) ) )

(DEFUN ISQRT-CALC (N
; If N is a positive integer, returns the greatest integer less than
; or equal to N^(1/2) using Newton's method.
    SHIFT INC ANS)
  (SETQ ANS 1
	INC 1
	SHIFT (* (- (TRUNCATE (SUB1 (INTEGER-LENGTH N)) 2)) 2))
  (LOOP 	;Start with 1-bit precision & repeatedly doubling
    ((ZEROP SHIFT))
    (SETQ ANS (SHIFT ANS INC)
	  SHIFT (+ SHIFT (SHIFT INC 1))
	  ANS (+ ANS (FLOOR (- (SHIFT N SHIFT) (* ANS ANS)) (* 2 ANS)))
	  INC (MIN (SHIFT INC 1) (TRUNCATE (- SHIFT) 2))) )
  ((<= (* ANS ANS) N) ANS)
  (SUB1 ANS) )


(DEFUN RATIONAL-ROOT (M N
; If M > 0 and N > 1 are integers, returns M^(1/N) accurate to the
; current precision.
    PRECISION)
  (SETQ PRECISION (MAX 0 (- (+ (* *PRECISION* (PRECISION)) 7) ;7=acc, 5=fast
			    (TRUNCATE (INTEGER-LENGTH M) N))))
  (ROUND/ (INTEGER-ROOT (SHIFT M (* N PRECISION)) N) (SHIFT 1 PRECISION)) )


(DEFUN INTEGER-ROOT (M N
; If M > 0 and N > 1 are integers, returns M^(1/N) rounded to the
; nearest integer.
    NM1 SHIFT PWR INC UPBND ANS)
  (SETQ ANS 1
	INC 1
	NM1 (- N 1)
	UPBND M
	SHIFT (* (- (TRUNCATE (SUB1 (INTEGER-LENGTH M)) N)) N))
  (LOOP 			;Newton's method with 1-bit precision
    ((ZEROP SHIFT))
    (SETQ ANS (SHIFT ANS INC)
	  SHIFT (+ SHIFT (* N INC))
	  PWR (INTEGER-POWER ANS NM1)
	  ANS (+ ANS (FLOOR (- (SHIFT M SHIFT) (* PWR ANS)) (* N PWR)))
	  INC (MIN (SHIFT INC 1) (TRUNCATE (- SHIFT) N))) )
  (LOOP 			;Newton's method at full precision
    (SETQ PWR (INTEGER-POWER ANS NM1)
	  INC (FLOOR (- M (* PWR ANS)) (* N PWR)))
    ((OR (ZEROP INC) (< N 4))
      (+ ANS INC) )		;Square & cube roots already converged
    ( ((PLUSP INC)
	(SETQ SHIFT ANS) )
      (SETQ UPBND ANS) )
    ((<= UPBND (SETQ ANS (+ ANS INC)))
      (ROUND (+ UPBND SHIFT) 2) )
    ((<= ANS SHIFT)
      (ROUND (+ UPBND SHIFT) 2) ) ) )

(DEFUN LN-CALC (N
; If N > 0, returns the natural log of N accurate to the current precision.
; Basis:  If p/q = (x-1)/(x+1) = u, then
;  ln x = 2 (u + u^3/3 + u^5/5 + ... + u^m/m)
;	= (...(((6pq^2 + 2p^3)5q^2 + 2 3p^5)7q^2 + 2 3 5p^7)9q^2 +
;		... + 2 3 5 ... (m-2)p^m) / (3 5 ... m q^m)
    M NUMR DENR TERM P Q SHIFT PRECISION BITS PLEN QLEN TMP)
  ((< N 1)
    (- (LN-CALC (/ 1 N))) )
  ((AND (= N 2) (STORED-VALUE '*LN2*)))
  (SETQ PRECISION (+ (* *PRECISION* (PRECISION)) 12) ;12=accurate,10=fast
	SHIFT 0
	TMP N)
  ( ((= N 2))
    (SETQ P (NUMERATOR N)
	  Q (DENOMINATOR N)
	  PLEN (INTEGER-LENGTH P)
	  QLEN (INTEGER-LENGTH Q))
    ((> PLEN QLEN)			;Make  3/4 < N <= 3/2
      ((> (SHIFT P 1) (* 3 Q))
	(SETQ SHIFT (- PLEN QLEN))
	((> PLEN PRECISION)
	  (SETQ P (SHIFT P (- PRECISION PLEN))
		Q (SHIFT Q (- PRECISION QLEN)))
	  ((> (SHIFT P 1) (* 3 Q))
	    (SETQ SHIFT (ADD1 SHIFT)
		  N (/ P (SHIFT Q 1))) )
	  ((< (SHIFT P 2) (* 3 Q))
	    (SETQ SHIFT (SUB1 SHIFT)
		  N (/ P (SHIFT Q -1))) )
	  (SETQ N (/ P Q)) )
	((> (SHIFT P 1) (* 3 (SETQ Q (SHIFT Q SHIFT))))
	  (SETQ SHIFT (ADD1 SHIFT)
		N (/ P (SHIFT Q 1))) )
	((< (SHIFT P 2) (* 3 Q))
	  (SETQ SHIFT (SUB1 SHIFT)
		N (/ P (SHIFT Q -1))) )
	(SETQ N (/ P Q)) ) )
    ((> (SHIFT P 1) (* 3 Q))
      (SETQ SHIFT 1
	    N (/ P (SHIFT Q 1))) ) )
  (SETQ N (/ (- (NUMERATOR N) (DENOMINATOR N))
	     (+ (NUMERATOR N) (DENOMINATOR N)))
	PLEN (* N N)
	P (NUMERATOR PLEN)
	Q (DENOMINATOR PLEN)
	NUMR (SHIFT (NUMERATOR N) 1)
	TERM NUMR
	DENR (DENOMINATOR N)
	PRECISION (- PRECISION (INTEGER-LENGTH SHIFT))
	M 1)
  (LOOP
    (SETQ TERM (* M TERM P)
	  M (+ M 2)
	  NUMR (+ TERM (* NUMR Q M))
	  DENR (* M Q DENR)
	  BITS (- PRECISION (INTEGER-LENGTH NUMR)))
    (IF (MINUSP BITS)
	(SETQ TERM (SHIFT TERM BITS)
	      NUMR (SHIFT NUMR BITS)
	      DENR (SHIFT DENR BITS)) )
    ((ZEROP TERM)
      ((ZEROP SHIFT)
	((= TMP 2)
	  (PUT '*LN2* 'PRECISION (PRECISION))
	  (SETQ *LN2* (ROUND/ NUMR DENR)) )
	(ROUND/ NUMR DENR) )
      (+ (* SHIFT (LN-CALC 2)) (ROUND/ NUMR DENR)) ) ) )


(DEFUN PI-CALC (
; Calculates and returns pi accurate to the current precision.
; Basis:  4/pi = SUM ((-1)^m (1123 + 21460 m) (1 3 ...(2m-1)) (1 3 ...(4m-1))
;			/ (882^(2m+1) 32^m (m!)^3), m, 0, PINF)
; rearranged over a common denominator.
; Ref: Ramanujan, Quart. J. Pure & Appl. Math. 45, p. 350, 1914.
    N M NUMR DENR TERM PRECISION BITS)
  ((STORED-VALUE '*PI*))
  (SETQ PRECISION (* *PRECISION* (PRECISION))
	N 1123
	TERM 1
	NUMR 3528
	DENR N
	M 0)
  (LOOP
    (INCQ M)
    (SETQ N (+ N 21460)
	  TERM (* TERM (SUB1 (+ M M)) (- 1 (* 4 M)) (- (* 4 M) 3))
	  BITS (* M M M 24893568)
	  NUMR (* NUMR BITS)
	  DENR (+ (* DENR BITS) (* N TERM))
	  BITS (- (+ PRECISION (INTEGER-LENGTH N)) (INTEGER-LENGTH DENR)))
    (IF (MINUSP BITS)
	(SETQ TERM (SHIFT TERM BITS)
	      NUMR (SHIFT NUMR BITS)
	      DENR (SHIFT DENR BITS)) )
    ((ZEROP TERM)
      (PUT '*PI* 'PRECISION (PRECISION))
      (SETQ *PI* (ROUND/ NUMR DENR)) ) ) )


(DEFUN SINCOS-CALC (N M
; Returns the sine or cosine of an appropriately reduced angle.
; SIN (x=p/q) ~= x - x^3/3! + x^5/5! - ... +|- x^n/n! =
; ((...((2*3pq^2 - p^3)4*5q^2 + p^5)6*7q^2 - ...)(n-1)nq^2 +|- p^n) / (n! q^n)
; COS (x=p/q) ~= 1 - x^2/2! + x^3/3! - ... +|- x^n/n! =
; ((...((2q^2 - p^2)3*4q^2 + p^4)5*6q^2 - ...)(n-1)nq^2 +|- p^n) / (n! q^n)
    TERM P Q PRECISION BITS)
  (SETQ M (MOD M 8))
  ((> M 3)
    (- (SINCOS-CALC N (- M 4))) )
  (IF (ODDP M) (SETQ N (- 1 N)))
  (SETQ N (/ (* N (PI)) 4))
  (IF (<= 1 M 2)
      (SETQ M 0
	    NUMR 1
	    DENR 1)
      (SETQ M 1
	    NUMR (NUMERATOR N)
	    DENR (DENOMINATOR N)) )
  (SETQ PRECISION (+ (* *PRECISION* (PRECISION)) 14) ;14=accurate, 8=fast
	N (* N N)
	P (- (NUMERATOR N))
	Q (DENOMINATOR N)
	TERM NUMR)
  (LOOP
    (INCQ M 2)
    (SETQ N (* (SUB1 M) M)
	  TERM (* P TERM)
	  NUMR (+ TERM (* N Q NUMR))
	  DENR (* N Q DENR)
	  BITS (- PRECISION (INTEGER-LENGTH NUMR)))
    (IF (MINUSP BITS)
	(SETQ TERM (SHIFT TERM BITS)
	      NUMR (SHIFT NUMR BITS)
	      DENR (SHIFT DENR BITS)) )
    ((ZEROP TERM)
      (ROUND/ NUMR DENR) ) ) )


(DEFUN ATAN-CALC (N
; With v = x/(1+x^2) = r/s,  u = xv = p/q,  ATAN (x) ~=
; (1 + 2u/3 + 2 4u^2/(3 5) +...+ 2 4 ...(2n)u^n/(3 5 ...(2n+1))) v =
; ((...((3rq + 2rp)5q + 2 4rp^2)7q +...)(2n+1)q + 2 4 ...(2n)rp^n)
; / (3 5 ...(2n+1)sq^n)
    M NUMR DENR TERM P Q PRECISION BITS)
  ((MINUSP N)
    (- (ATAN-CALC (- N))) )
  ((> N 1)
    (- (/ (PI) 2) (ATAN-CALC (/ 1 N))) )
  (SETQ PRECISION (+ (* *PRECISION* (PRECISION)) 14)	;14=acc, 11=fast
	M (/ N (ADD1 (* N N)))
	N (* N M)
	P (NUMERATOR N)
	Q (DENOMINATOR N)
	NUMR (NUMERATOR M)
	DENR (DENOMINATOR M)
	TERM NUMR
	M 1)
  (LOOP
    (INCQ M 2)
    (SETQ TERM (* (SUB1 M) P TERM)
	  NUMR (+ TERM (* M Q NUMR))
	  DENR (* M Q DENR)
	  BITS (- PRECISION (INTEGER-LENGTH NUMR)))
    (IF (MINUSP BITS)
	(SETQ TERM (SHIFT TERM BITS)
	      NUMR (SHIFT NUMR BITS)
	      DENR (SHIFT DENR BITS)) )
    ((ZEROP TERM)
      (ROUND/ NUMR DENR) ) ) )
