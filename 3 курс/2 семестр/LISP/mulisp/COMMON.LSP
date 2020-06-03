; File: COMMON.LSP		06/02/89		Soft Warehouse, Inc.


;		    The Common LISP Compatibility File

; This source file defines the library functions and macros described in
; Chapter 5 of the muLISP Reference Manual.


; Section 5.1:	Predicate Primitives

(MOVD 'SYMBOLP 'STRINGP)
(MOVD 'NUMBERP 'RATIONALP)
(MOVD 'EQUAL 'TREE-EQUAL)


; Section 5.2:	List Primitives

(MOVD 'LENGTH 'LIST-LENGTH)
(MOVD 'REVERSE 'REVAPPEND)
(MOVD 'NREVERSE 'NRECONC)

(DEFUN NUNION (LST1 LST2 TEST)
; Returns the union of LST1 and LST2 by destructively modifying LST1.
  ((ATOM LST1)	LST2)
  ((MEMBER (CAR LST1) LST2 TEST)
    (NUNION (CDR LST1) LST2 TEST) )
  (RPLACD LST1 (NUNION (CDR LST1) LST2 TEST)) )

(DEFUN NINTERSECTION (LST1 LST2 TEST)
; Returns the intersection of LST1 and LST2 by destructively modifying LST1.
  ((ATOM LST1)	NIL)
  ((MEMBER (CAR LST1) LST2 TEST)
    (RPLACD LST1 (NINTERSECTION (CDR LST1) LST2 TEST)) )
  (NINTERSECTION (CDR LST1) LST2 TEST) )

(DEFUN NSET-DIFFERENCE (LST1 LST2 TEST)
; Returns the difference of LST1 minus LST2 by destructively modifying LST1.
  ((ATOM LST1)	NIL)
  ((MEMBER (CAR LST1) LST2 TEST)
    (NSET-DIFFERENCE (CDR LST1) LST2 TEST) )
  (RPLACD LST1 (NSET-DIFFERENCE (CDR LST1) LST2 TEST)) )

(DEFUN NSET-EXCLUSIVE-OR (LST1 LST2 TEST
; Returns the exclusive or of LST1 and LST2 by destructively modifying
; both lists.
    TMP)
  ((ATOM LST1)	LST2)
  ((MEMBER (CAR LST1) LST2 TEST)
    (SETQ TMP (CAR LST1))
    (NSET-EXCLUSIVE-OR (DELETE TMP LST1 TEST) (DELETE TMP LST2 TEST) TEST) )
  (RPLACD LST1 (NSET-EXCLUSIVE-OR (CDR LST1) LST2 TEST)) )

(MOVD 'SORT 'STABLE-SORT)


; Section 5.3:	Symbol Primitives

(DEFUN SYMBOL-VALUE (SYM)
; If SYM is a a symbol, returns the value of SYM.
  ((SYMBOLP SYM)
    (CAR SYM) ) )

(DEFMACRO SETF LST
; If PLACE is a valid place form, (SETF PLACE FORM) stores the result of
; evaluating FORM in PLACE.
  ((NULL LST) NIL)
  ((NULL (CDDR LST))
    ((ATOM (CAR LST))
      (CONS 'SETQ LST) )
    (MAKE-SET-FORM (CAR LST) (CADR LST)) )
  (CONS 'PROGN (SETF-AUX LST)) )

(DEFUN SETF-AUX (LST
; Returns a list of SETF forms.
    TMP)
  ((NULL LST) NIL)
  ((ATOM (CAR LST))
    ((AND (SETQ TMP (SETF-AUX (CDDR LST))) (EQ (CAAR TMP) 'SETQ))
      (CONS (LIST* 'SETQ (CAR LST) (CADR LST) (CDAR TMP)) (CDR TMP)) )
    (CONS (LIST 'SETQ (CAR LST) (CADR LST)) TMP) )
  (CONS (MAKE-SET-FORM (CAR LST) (CADR LST)) (SETF-AUX (CDDR LST))) )

(DEFUN MAKE-SET-FORM (PLACE VALUE)
; Returns a form that changes the value stored at PLACE to VALUE.
  ((EQ (CAR PLACE) 'CAR)
    (LIST 'CAR (LIST 'RPLACA (CADR PLACE) VALUE)) )
  ((OR (EQ (CAR PLACE) 'CDR) (EQ (CAR PLACE) 'REST))
    (LIST 'CDR (LIST 'RPLACD (CADR PLACE) VALUE)) )
  ((EQ (CAR PLACE) 'NTH)
    (LIST 'CAR (LIST 'RPLACA (CONS 'NTHCDR (CDR PLACE)) VALUE)) )
  ((EQ (CAR PLACE) 'GET)
    (LIST 'PUT (CADR PLACE) (CADDR PLACE) VALUE) )
  ((EQ (CAR PLACE) 'SYMBOL-FUNCTION)
    (LIST 'PUTD (CADR PLACE) VALUE) )
  ((GET (CAR PLACE) 'CAR)  (LIST 'CAR
      (LIST 'RPLACA (LIST (GET (CAR PLACE) 'CAR) (CADR PLACE)) VALUE)) )
  ((GET (CAR PLACE) 'CDR)  (LIST 'CDR
      (LIST 'RPLACD (LIST (GET (CAR PLACE) 'CDR) (CADR PLACE)) VALUE)) )
  ((GET (CAR PLACE) 'NTH)
    (LIST 'CAR (LIST 'RPLACA
		     (LIST 'NTHCDR (GET (CAR PLACE) 'NTH) (CADR PLACE))
		     VALUE)) )
  ((MACRO-P (CAR PLACE))
    (MAKE-SET-FORM (MACROEXPAND PLACE) VALUE) )
  (BREAK (LIST 'SETF PLACE VALUE) "Invalid Place Form") )

(MAPC '(LAMBDA (PAIR) (PUT (CAR PAIR) 'CAR (CDR PAIR)))
  '((CAAR . CAR) (CADR . CDR)
    (CAAAR . CAAR) (CAADR . CADR) (CADAR . CDAR) (CADDR . CDDR)
    (CAAAAR . CAAAR) (CAAADR . CAADR) (CAADAR . CADAR) (CAADDR . CADDR)
    (CADAAR . CDAAR) (CADADR . CDADR) (CADDAR . CDDAR) (CADDDR . CDDDR)) )

(MAPC '(LAMBDA (PAIR) (PUT (CAR PAIR) 'CDR (CDR PAIR)))
  '((CDAR . CAR) (CDDR . CDR)
    (CDAAR . CAAR) (CDADR . CADR) (CDDAR . CDAR) (CDDDR . CDDR)
    (CDAAAR . CAAAR) (CDAADR . CAADR) (CDADAR . CADAR) (CDADDR . CADDR)
    (CDDAAR . CDAAR) (CDDADR . CDADR) (CDDDAR . CDDAR) (CDDDDR . CDDDR)) )

(MAPC '(LAMBDA (PAIR) (PUT (CAR PAIR) 'NTH (CDR PAIR)))
  '((FIRST . 0) (SECOND . 1) (THIRD . 2) (FOURTH . 3) (FIFTH . 4)
    (SIXTH . 5) (SEVENTH . 6) (EIGHTH . 7) (NINTH . 8) (TENTH . 9)) )


(DEFMACRO PUSHNEW (OBJ SYM TEST)
; If OBJ is not a member of the stack SYM, pushes OBJ on the stack.
  (LIST 'SETQ SYM (LIST 'ADJOIN OBJ SYM TEST)) )

(DEFUN SYMBOL-PLIST (SYM)
; If SYM is a symbol, returns SYM's property list.
  ((SYMBOLP SYM)
    (CDR SYM) ) )

(DEFUN SYMBOL-FUNCTION (SYM)
; If SYM is a symbol, returns SYM's function definition.
  ((SYMBOLP SYM)
    ((FBOUNDP SYM)
      (GETD SYM) )
    (BREAK (LIST 'SYMBOL-FUNCTION SYM) "Undefined Function") ) )

(DEFUN EVAL-FUNCTION-P (SYM)
; If SYM is an eval function, it returns T, otherwise it returns NIL.
  (EQ (GETD SYM T) 'LAMBDA) )

(DEFUN NO-EVAL-FUNCTION-P (SYM)
; If SYM is a no eval function, it returns T, otherwise it returns NIL.
  (EQ (GETD SYM T) 'NLAMBDA) )

(DEFUN MACRO-P (SYM)
; If SYM is a macro, it returns T, otherwise it returns NIL.
  (EQ (GETD SYM T) 'MACRO) )

(DEFUN SPECIAL-FORM-P (SYM)
; If SYM is a special form, it returns T, otherwise it returns NIL.
  (EQ (GETD SYM T) 'SPECIAL) )

(DEFUN FBOUNDP (SYM)
; If SYM is a function, macro, or special form, it returns T, otherwise it
; returns NIL.
  (GETD SYM T) )

(MOVD 'REMD 'FMAKUNBOUND)


; Section 5.4:	Number Primitives

(DEFMACRO INCF (PLACE N)
; If PLACE is a valid place form, INCF increments the value in PLACE by N.
; N defaults to 1.
  ((NULL N)
    ((ATOM PLACE)
      (LIST 'INCQ PLACE) )
    (MAKE-SET-FORM PLACE (LIST 'ADD1 PLACE)) )
  ((ATOM PLACE)
    (LIST 'INCQ PLACE N) )
  (MAKE-SET-FORM PLACE (LIST '+ PLACE N)) )

(DEFMACRO DECF (PLACE N)
; If PLACE is a valid place form, INCF decrements the value in PLACE by N.
; N defaults to 1.
  ((NULL N)
    ((ATOM PLACE)
      (LIST 'DECQ PLACE) )
    (MAKE-SET-FORM PLACE (LIST 'SUB1 PLACE)) )
  ((ATOM PLACE)
    (LIST 'DECQ PLACE N) )
  (MAKE-SET-FORM PLACE (LIST '- PLACE N)) )

(DEFUN LOGTEST (N M)
; If any corresponding bits in N and M are both 1, it returns T, otherwise it
; returns NIL.
  (NOT (ZEROP (LOGAND N M))) )

(DEFUN LOGBITP (INDEX N)
; If the INDEXth bit in N is 1, it returns T, otherwise it returns NIL.
  (LOGTEST (SHIFT 1 INDEX) N) )

(DEFUN LOGEQV LST
; Returns the bit-wise logical equivalence of its arguments.
  (REDUCE '(LAMBDA (INT1 INT2) (LOGNOT (LOGXOR INT1 INT2)) )
	  LST -1) )

(DEFUN ASH (N COUNT)
; Shifts N by COUNT bits as if negative integers are stored in "half-infinite"
; two's-complement notation.
  ((OR (>= N 0) (>= COUNT 0))
    (SHIFT N COUNT) )
  (ASH (SHIFT (SUB1 N) -1) (ADD1 COUNT)) )


(DEFUN RANDOM (N STATE
; If N is a positive integer, it returns a random integer between 0 (inclusive)
; and N (exclusive).  If N is negative and/or a ratio, it returns a random
; rational number between 0 (inclusive) and | N | (exclusive).
    BL COUNT ANS)
  (IF (RANDOM-STATE-P STATE)
      (SETQ *RANDOM-STATE* STATE) )
  (SETQ *RANDOM-STATE*
		(LOGAND (ADD1 (* 3141592653 *RANDOM-STATE*)) 4294967295))
  ((AND (INTEGERP N) (PLUSP N))
    (SETQ ANS *RANDOM-STATE*
	  BL (- -10 (INTEGER-LENGTH N))
	  COUNT 0)
    (LOOP			; concatenate 32-bit random integers
      ((> BL (DECQ COUNT 32))
	(SHIFT (* N ANS) COUNT) )
      (SETQ *RANDOM-STATE*
	      (LOGAND (ADD1 (* 3141592653 *RANDOM-STATE*)) 4294967295)
	    ANS (+ (SHIFT ANS 32) *RANDOM-STATE*)) ) )
  ((NUMBERP N)
    (SETQ ANS (SHIFT 1 (- (* 16 (ADD1 (MAX (PRECISION) 1))) 10)))
    (/ (* (ABS N) (RANDOM ANS)) ANS) ) )

(DEFUN MAKE-RANDOM-STATE (STATE)
; (MAKE-RANDOM-STATE) returns *RANDOM-STATE*.  (MAKE-RANDOM-STATE T) returns
; a random-number state based on the system clock.
  ((NULL STATE) *RANDOM-STATE*)
  ((EQ STATE 'T)
    (TIME) )
  ((RANDOM-STATE-P STATE) STATE) )

(SETQ *RANDOM-STATE* (MAKE-RANDOM-STATE 'T))

(DEFUN RANDOM-STATE-P (OBJ)
; If OBJ is a valid random-number state, returns T, otherwise it returns NIL.
  (AND (INTEGERP OBJ) (>= OBJ 0)) )


; Section 5.5:	Print Name Primitives

(DEFUN CHAR-CODE (SYM)
; If SYM is a symbol, it returns the ASCII code for SYM.
  ((SYMBOLP SYM)
    (ASCII SYM) ) )

(DEFUN CODE-CHAR (N)
; If N is an integer between 0 and 255 inclusive, it returns the symbol whose
; ASCII code is N.
  ((AND (INTEGERP N) (<= 0 N 255))
    (ASCII N) ) )


; The following predicates perform lexicographically comparisons of the
; print names of their arguments without regard to case.

(DEFMACRO STRING-EQUAL (ATM1 ATM2)
  (LIST 'STRING= ATM1 ATM2 T) )

(DEFMACRO STRING-LESSP (ATM1 ATM2)
  (LIST 'STRING< ATM1 ATM2 T) )

(DEFMACRO STRING-GREATERP (ATM1 ATM2)
  (LIST 'STRING> ATM1 ATM2 T) )

(DEFMACRO STRING-NOT-GREATERP (ATM1 ATM2)
  (LIST 'STRING<= ATM1 ATM2 T) )

(DEFMACRO STRING-NOT-LESSP (ATM1 ATM2)
  (LIST 'STRING>= ATM1 ATM2 T) )

(DEFMACRO STRING-NOT-EQUAL (ATM1 ATM2)
  (LIST 'STRING/= ATM1 ATM2 T) )


(SETQ *GENSYM-PREFIX* 'G)
(SETQ *GENSYM-COUNT* 0)

(DEFUN GENSYM (ATM)
; (GENSYM) returns the symbol whose print name consists of *GENSYM-PREFIX*
; concatenated to *GENSYM-COUNT* and increments *GENSYM-COUNT*.
; (GENSYM symbol) sets *GENSYM-PREFIX* to <symbol>.
; (GENSYM integer) sets *GENSYM-COUNT* to <integer>.
  ( ((NULL ATM))
    ((SYMBOLP ATM)
      (SETQ *GENSYM-PREFIX* ATM) )
    ((AND (INTEGERP ATM) (>= ATM 0))
      (SETQ *GENSYM-COUNT* ATM) ) )
  (PROG1 (PACK* *GENSYM-PREFIX* *GENSYM-COUNT*)
	 (INCQ *GENSYM-COUNT*)) )


; Section 5.6:	Control Constructs

(DEFMACRO PROG2 (FORM1 . FORMS)
; Evaluates FORM1 and the forms in FORMS, and returns the value of the first
; form in FORMS.
  (LIST 'PROGN FORM1 (CONS 'PROG1 FORMS)) )

(DEFMACRO LET (LETLIST . BODY)
; Binds the variables in LETLIST in parallel, and evaluates the BODY.
   (CONS (LIST* 'LAMBDA
		(MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR))) LETLIST)
		BODY)
	 (MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) NIL (CADR VAR))) LETLIST)) )

(DEFMACRO LET* (LETLIST . BODY)
; Binds the variables in LETLIST sequentially, and evaluates the BODY.
   (CONS (LIST* 'LAMBDA
		(MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR))) LETLIST)
		(CONS 'SETQ
		      (MAPCAN '(LAMBDA (VAR)
				(IF (ATOM VAR)
				    (LIST VAR NIL)
				    (LIST (CAR VAR) (CADR VAR))) )
			      LETLIST))
		BODY)
	 (MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR))) LETLIST)) )

(DEFMACRO WHEN (TEST . BODY)
; If the value of TEST is nonNIL, BODY is evaluated.
  (LIST 'IF TEST (CONS 'PROGN BODY)) )

(DEFMACRO UNLESS (TEST . BODY)
; If the value of TEST is NIL, BODY is evaluated.
  (LIST 'IF TEST NIL (CONS 'PROGN BODY)) )

(DEFMACRO CASE (KEYFORM . CASES)
; If the value of KEYFORM matches the car of a case in CASES, the cdr of the
; case is evaluated using an implicit PROGN.
  (LIST (LIST 'LAMBDA
	      '(KEY)
	      (CONS 'COND
		    (MAPCAR '(LAMBDA (CASE) (CONS
			       (IF (MEMBER (CAR CASE) '(T OTHERWISE))
				   T
				   (LIST (IF (ATOM (CAR CASE)) 'EQL 'MEMBER)
					 KEY
					 (LIST 'QUOTE (CAR CASE))) )
			       (OR (CDR CASE) '(NIL))) )
			     CASES)))
	KEYFORM) )

(DEFMACRO DO (LETLIST . BODY)
; Repeatedly binds the variables in LETLIST in parallel and evaluates BODY,
; until a nonNIL implicit COND is encountered in BODY.
  (CONS (LIST 'LAMBDA
	      (MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR))) LETLIST)
	      (CONS 'LOOP
		    (APPEND BODY (LIST (CONS 'PSETQ
			(MAPCAN '(LAMBDA (VAR)
				   ((ATOM VAR) NIL)
				   ((CDDR VAR)
				     (LIST (CAR VAR) (CADDR VAR)) ) )
				LETLIST))))))
	(MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) NIL (CADR VAR))) LETLIST)) )

(DEFMACRO DO* (LETLIST . BODY)
; Repeatedly binds the variables in LETLIST sequentially and evaluates BODY,
; until a nonNIL implicit COND is encountered in BODY.
  (CONS (LIST 'LAMBDA
	      (MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR)))
		      LETLIST)
	      (CONS 'SETQ
		    (MAPCAN '(LAMBDA (VAR)
				(IF (ATOM VAR)
				   (LIST VAR NIL)
				   (LIST (CAR VAR) (CADR VAR))) )
			    LETLIST))
	      (CONS 'LOOP
		    (APPEND BODY (LIST (CONS 'SETQ
			(MAPCAN '(LAMBDA (VAR)
				   ((ATOM VAR) NIL)
				   ((CDDR VAR)
				     (LIST (CAR VAR) (CADDR VAR)) ) )
				LETLIST))))))
	 (MAPCAR '(LAMBDA (VAR) (IF (ATOM VAR) VAR (CAR VAR))) LETLIST)) )

(DEFMACRO DOLIST ((VAR LISTFORM RSLTFORM) . BODY)
; Sets VAR to each element of the value of LISTFORM and evaluates BODY.
  (LET ((DOLIST (GENSYM)))
    (LIST 'LET
	  (LIST (LIST DOLIST LISTFORM))
	  (LIST* 'LOOP
		 (LIST (LIST 'ATOM DOLIST) RSLTFORM)
		 (LIST 'SETQ VAR (LIST 'POP DOLIST))
		 BODY)) ) )

(DEFMACRO DOTIMES ((VAR COUNTFORM RSLTFORM) . BODY)
; Binds VAR to 0 through the value of COUNTFORM and evaluates BODY.
  (LET ((DOTIMES (GENSYM)))
    (LIST 'LET
	  (LIST (LIST VAR 0)
		(LIST DOTIMES COUNTFORM))
	  (LIST* 'LOOP
		 (LIST (LIST '>= VAR DOTIMES) RSLTFORM)
		 (APPEND BODY (LIST (LIST 'INCQ VAR))))) ) )


(DEFUN PROG (NLAMBDA LST$
; PROG (var-list, expn1, expn2, ..., expnm)  sets the local variables
; in <var-list> to NIL and sequentially evaluates <expn1> through <expnm>,
; unless the functions GO or RETURN are encountered.  We recommend that
; functions using PROG be translated into equivalent muLISP functions
; using the LOOP construct, since the result will be more efficient.
  (EVAL (LIST (CONS 'LAMBDA (LIST (CAR LST$) (LIST 'PROG-AUX
      '(CDR LST$)))))) ))

(DEFUN PROG-AUX (BDY$
    LST$ GO-LABEL$ ANS$)
  (SETQ LST$ BDY$)
  (LOOP
    ((NULL LST$) ANS$)
    (SETQ ANS$ (EVAL (POP LST$)))
    ( ((NULL GO-LABEL$))
      (SETQ LST$ (CDR (MEMBER GO-LABEL$ BDY$))
	    GO-LABEL$) ) ) )

(DEFUN GO (NLAMBDA (LABEL$)
; GO (label)  if within a PROG, transfers control to the expression in
; the PROG body immediately following <label>.
  (SETQ GO-LABEL$ LABEL$) ))


(DEFUN DOS (COMMAND)
; Executes the MS-DOS <command>, and returns the exit code.
  (EXECUTE (GETSET 'COMSPEC) (IF COMMAND (PACK* "/C " COMMAND))) )


; Section 5.7:	Interpreter Primitives

(DEFUN FUNCTIONP (OBJ)
; If OBJ is suitable for use as a functional argument, it returns T, otherwise
; it returns NIL.
  ((SYMBOLP OBJ))
  ((ATOM OBJ) NIL)
  (EQ (CAR OBJ) 'LAMBDA) )

(DEFMACRO COMMENT LST
; Returns NIL without evaluating its arguments.
; It can be used to include comments in function definitions.
  NIL )

(DEFUN MACRO-FUNCTION (SYM)
; If SYM is a macro, it returns SYM's macro definition.
  ((MACRO-P SYM)
    (GETD SYM) ) )


; Section 5.8.1:  Input Functions

(DEFUN READ-CHAR-NO-HANG (SOURCE EOF-VALUE-P EOF-VALUE)
; Read a console character from SOURCE without hanging.
  ((OR (LISTEN SOURCE) (INPUT-FILE-P SOURCE))
    (ASCII (READ-BYTE SOURCE EOF-VALUE-P EOF-VALUE)) ) )

(DEFUN PARSE-INTEGER (SYM RADIX
; Parses SYM and returns the corresponding integer, otherwise it returns NIL.
    CHAR SIGN N )
  (SETQ *STRING-INDEX* 0)
  (LOOP
    ((NULL (SETQ CHAR (CHAR SYM *STRING-INDEX*))) NIL)
    ((NEQ CHAR '| |)
      (SETQ SIGN 1)
      ( ((EQ CHAR '+)
	  (INCQ *STRING-INDEX*) )
	((EQ CHAR '-)
	  (SETQ SIGN -1)
	  (INCQ *STRING-INDEX*) ) )
      ((SETQ CHAR (CHAR SYM *STRING-INDEX*))
	((SETQ N (DIGIT-CHAR-P CHAR RADIX))
	  (IF (NULL RADIX) (SETQ RADIX 10))
	  (LOOP
	    (INCQ *STRING-INDEX*)
	    ((NULL (SETQ CHAR (CHAR SYM *STRING-INDEX*))))
	    ((NOT (SETQ CHAR (DIGIT-CHAR-P CHAR RADIX))))
	    (SETQ N (+ (* N RADIX) CHAR)) )
	  (* SIGN N) ) ) )
    (INCQ *STRING-INDEX*) ) )


; Section 5.8.2:  Read Macro Characters

(SETQ *BACKQUOTE-CHAR* '\`)	;Determines the backquote macro character

(SETQ *LPAR* '\()
(SETQ *RPAR* '\))

(DEFUN READ-BACKQUOTE (PAREN-DEPTH
; This backquote facility conforms to the Common LISP standard as
; described in Section 22.1.3 of Common LISP by Steele [1984].
; It was contributed by Fujimasa Kouno of Tokushima, Japan.
    OBJ CHAR FORM-LIST)
  (SETQ CHAR (PEEK-CHAR T))

  ((EQ CHAR *BACKQUOTE-CHAR*)
    ((ZEROP PAREN-DEPTH)
      (READ) )
    (LIST 'LIST (READ)) )

  ((EQ CHAR '\')
    (READ-CHAR)
    ((ZEROP PAREN-DEPTH)
      (LIST 'QUOTE (LIST 'QUOTE (EVAL (READ-BACKQUOTE PAREN-DEPTH)))) )
    (LIST 'LIST (LIST* 'LIST ''QUOTE (CDR (READ-BACKQUOTE PAREN-DEPTH)))) )

  ((EQ CHAR '\.)
    ((ZEROP PAREN-DEPTH)
      (BREAK CHAR "Syntax Error") )
    (READ-CHAR)
    (SETQ CHAR (PEEK-CHAR T))
    ((EQ CHAR '\,)
      (READ-CHAR)
      (SETQ CHAR (PEEK-CHAR))
      ((OR (EQ CHAR '@) (EQ CHAR '\.))
	(BREAK CHAR "Syntax Error") )
      (SETQ OBJ (READ)
	    CHAR (PEEK-CHAR T) )
      ((EQ CHAR *RPAR*) OBJ)
      (BREAK CHAR "Syntax Error") )
    (SETQ OBJ (READ-BACKQUOTE 0)
	  CHAR (PEEK-CHAR T) )
    ((EQ CHAR *RPAR*) OBJ)
    (BREAK CHAR "Syntax Error") )

  ((EQ CHAR '\,)
    (READ-CHAR)
    (SETQ CHAR (PEEK-CHAR))
    ((AND (EQ CHAR '\@) (NEQ PAREN-DEPTH 0))
      (READ-CHAR)
      (SETQ OBJ (READ))
      ((EQ (PEEK-CHAR T) *RPAR*) OBJ)
      (LIST 'COPY-LIST OBJ) )
    ((AND (EQ CHAR '\.) (NEQ PAREN-DEPTH 0))
      (READ-CHAR)
      (READ) )
    ((OR (EQ CHAR '\@) (EQ CHAR '\.))
      (BREAK CHAR "Syntax Error") )
    ((ZEROP PAREN-DEPTH)
      (READ) )
    (LIST 'LIST (READ)) )

  ((EQ CHAR *RPAR*)
    (READ-CHAR)
    NIL)

  ((EQ CHAR *LPAR*)
    (READ-CHAR)
    (LOOP
      ((NULL (SETQ OBJ (READ-BACKQUOTE (ADD1 PAREN-DEPTH))))
	((ZEROP PAREN-DEPTH)
	  (CONS 'NCONC (NREVERSE FORM-LIST)) )
	(LIST 'LIST (CONS 'NCONC (NREVERSE FORM-LIST))) )
      (PUSH OBJ FORM-LIST) ) )

  ((ZEROP PAREN-DEPTH)
    (LIST 'QUOTE (READ)) )
  (LIST 'LIST (LIST 'QUOTE (READ))) )

(SET-MACRO-CHAR *BACKQUOTE-CHAR* '(LAMBDA ()
  (READ-BACKQUOTE 0) ))


; Section 5.8.4:  Output Functions

(DEFUN PPRINT (OBJECT FILE-NAME)
; Sends a pretty-printed representation of OBJECT to FILE-NAME.
  (MAPC '(LAMBDA (LINE) (WRITE-LINE LINE FILE-NAME))
	(FORMAT-EXPN OBJECT) )
  NIL )

(DEFUN FORMAT-EXPN (EXPN
; Returns a pretty-printed representation of EXPN as a list of strings.
    *LINE* *TEXT* )
  (FORMAT-TASK EXPN 0)
  (TERMINATE-LINE)
  (MAPCAR 'PACK (NREVERSE *TEXT*)) )

(DEFUN FORMAT-TASK (EXPN INDENT)
  (INCQ INDENT 2)
  ((ATOM EXPN)
    (PUSH (ATOM-TO-PRINT-NAME EXPN) *LINE*) )
  ((ATOM (CAR EXPN))
    ((AND (EQ (CAR EXPN) 'QUOTE)
	  (CDR EXPN)
	  (NULL (CDDR EXPN)))
      (PUSH '\' *LINE*)
      (FORMAT-TASK (CADR EXPN) INDENT) )
    (PUSH '\( *LINE*)
    ((AND (MEMBER (CAR EXPN) '(LAMBDA NLAMBDA MACRO CATCH))
	  (CDR EXPN))
      (PUSH (ATOM-TO-PRINT-NAME (POP EXPN)) *LINE*)
      (PUSH '| | *LINE*)
      (FORMAT-TASK (POP EXPN) INDENT)
      (FORMAT-BODY EXPN INDENT)
      (PUSH '\) *LINE*) )
    ((MEMBER (CAR EXPN) '(LOOP COND PROGN PROG1 PROG2 UNWIND-PROTECT))
      (PUSH (ATOM-TO-PRINT-NAME (POP EXPN)) *LINE*)
      (FORMAT-BODY EXPN INDENT)
      (PUSH '\) *LINE*) )
    ((AND (MEMBER (CAR EXPN) '(DEFUN DEFMACRO))
	  (CDDR EXPN)
	  (ATOM (CADR EXPN)))
      (PUSH (ATOM-TO-PRINT-NAME (POP EXPN)) *LINE*)
      (PUSH '| | *LINE*)
      (PUSH (ATOM-TO-PRINT-NAME (POP EXPN)) *LINE*)
      (PUSH '| | *LINE*)
      (FORMAT-TASK (POP EXPN) INDENT)
      (FORMAT-BODY EXPN INDENT)
      (PUSH '\) *LINE*) )
    ((AND (MEMBER (CAR EXPN) '(SETQ PSETQ SETF)) (CDR EXPN))
      (PUSH (ATOM-TO-PRINT-NAME (CAR EXPN)) *LINE*)
      (PUSH '| | *LINE*)
      (INCQ INDENT (LENGTH (ATOM-TO-PRINT-NAME (POP EXPN))))
      (LOOP
	(FORMAT-TASK (POP EXPN) INDENT)
	((ATOM EXPN))
	(PUSH '| | *LINE*)
	(FORMAT-TASK (POP EXPN) INDENT)
	((ATOM EXPN))
	(TERMINATE-LINE) )
      (PUSH '\) *LINE*) )
    ((AND (MEMBER (CAR EXPN) *NARY-FUNCTIONS*) (> (LENGTH (CDR EXPN)) 2))
      (PUSH (ATOM-TO-PRINT-NAME (CAR EXPN)) *LINE*)
      (PUSH '| | *LINE*)
      (INCQ INDENT (LENGTH (ATOM-TO-PRINT-NAME (POP EXPN))))
      (LOOP
	(FORMAT-TASK (POP EXPN) INDENT)
	((ATOM EXPN))
	(TERMINATE-LINE) )
      (PUSH '\) *LINE*) )
    (LOOP
      (FORMAT-TASK (POP EXPN) INDENT)
      ((ATOM EXPN)
	((NULL EXPN))
	(PUSH '| | *LINE*)
	(PUSH '\. *LINE*)
	(PUSH '| | *LINE*)
	(PUSH (ATOM-TO-PRINT-NAME EXPN) *LINE) )
      (PUSH '| | *LINE*) )
    (PUSH '\) *LINE*) )
  ((ATOM (CAAR EXPN))
    (PUSH '\( *LINE*)
    (FORMAT-TASK (POP EXPN) (SUB1 INDENT))
    ((AND EXPN
	  (ATOM (CAR EXPN))
	  (NULL (CDR EXPN)))
      (PUSH '| | *LINE*)
      (PUSH (ATOM-TO-PRINT-NAME (POP EXPN)) *LINE*)
      (PUSH '\) *LINE*) )
    (FORMAT-BODY EXPN INDENT)
    (PUSH '\) *LINE*) )
  (PUSH '\( *LINE*)
  (PUSH '| | *LINE*)
  (FORMAT-BODY1 EXPN INDENT)
  (PUSH '\) *LINE*) )

(DEFUN FORMAT-BODY (BODY INDENT)
  ((NULL BODY))
  (TERMINATE-LINE)
  (FORMAT-BODY1 BODY INDENT) )

(DEFUN FORMAT-BODY1 (BODY INDENT)
  (LOOP
    (FORMAT-TASK (POP BODY) INDENT)
    ((NULL BODY)
      (PUSH '| | *LINE*) )
    (TERMINATE-LINE) ) )

(DEFUN TERMINATE-LINE ()
  (PUSH (NREVERSE *LINE*) *TEXT*)
  (SETQ *LINE* (MAKE-LIST INDENT '| |)) )

(DEFUN ATOM-TO-PRINT-NAME (EXPN
    *PRINT-ESCAPE* )
  ((AND EXPN (SYMBOLP EXPN))
    (SETQ *PRINT-ESCAPE* T)
    ((EQ (LENGTH EXPN) (PRINT-LENGTH EXPN))
      EXPN )
    ((AND (EQ (LENGTH EXPN) 1) (NEQ EXPN '| |))
      (PACK* '\\ EXPN) )
    (PACK* '\| EXPN '\|) )
  EXPN )

(SETQ *NARY-FUNCTIONS* '(AND OR IF UNLESS WHEN
	APPEND GCD LCM LIST LIST* MAX MIN NCONC))


; Section 5.8.7:  Hardware Interface Primitives

(DEFMACRO BENCHMARK (FORM)
; Returns the result of evaluating FORM after displaying the time required.
  (LIST 'PROGN '(TIME T) (LIST 'UNWIND-PROTECT
			       FORM
			       '(PRIN1 (/ (TIME) 100))
			       '(WRITE-LINE " seconds"))) )


(SETQ *TIME-ZONE* 0)	;The number of hours behind GMT (Greenich Mean Time).

(DEFUN GET-UNIVERSAL-TIME ()
; Returns the number of seconds since midnight, January 1, 1900, GMT.
  (APPLY 'ENCODE-UNIVERSAL-TIME (NREVERSE (GET-DECODED-TIME))) )

(DEFUN ENCODE-UNIVERSAL-TIME (SECOND MINUTE HOUR DAY MONTH YEAR ZONE
; Returns the number of seconds from midnight, January 1, 1900, GMT till
; the specified time.
    BASE DAYS)
  (SETQ BASE 1900)
  ((< YEAR BASE)  NIL)					;If before 1900!
  (SETQ DAYS (* 365 (- YEAR BASE)))			;365 days/year
  (LOOP
    ((>= BASE YEAR))
    (IF (LEAP-YEAR BASE) (INCQ DAYS))			;+1 day/leap year
    (INCQ BASE 4) )
  (INCQ DAYS (NTH (SUB1 MONTH) '(0 31 59 90 120 151 181 212 243 273 304 334)))
  (INCQ DAYS (SUB1 DAY))
  (IF (AND (LEAP-YEAR YEAR) (> MONTH 2)) (INCQ DAYS))
  (+ (* 60 (+ (* 60 (+ (* 24 DAYS) HOUR (OR ZONE *TIME-ZONE*))) MINUTE))
     SECOND) )

(DEFUN LEAP-YEAR (YEAR)
; Return T if YEAR is a leap year, otherwise NIL.
  ((ZEROP (REM YEAR 4))
    ((ZEROP (REM YEAR 100))
      (ZEROP (REM YEAR 400)) )
    T ) )


(DEFUN SLEEP (N)
; Ceases program execution for approximately N seconds, and then returns NIL.
  (TONE NIL (TRUNCATE N 1/1000))
  NIL )


; Section 5.10:  User Interface Primitives

(DEFUN Y-OR-N-P (MSG
; Displays MSG, and returns T if the user presses Y; it returns NIL if the
; users presses N.
    CHAR )
  ( ((NULL MSG))
    (FRESH-LINE T)
    (WRITE-STRING (PACK* MSG " (Y/N) ") T) )
  (CLEAR-INPUT T)
  (LOOP
    (SETQ CHAR (CHAR-UPCASE (ASCII (READ-BYTE T))))
    ((EQ CHAR 'Y)
      (WRITE-STRING CHAR T)
      T )
    ((EQ CHAR 'N)
      (WRITE-STRING CHAR T)
      NIL )
    (WRITE-BYTE 7 NIL T) ) )

(DEFUN YES-OR-NO-P (MSG
; Displays MSG, and returns T if the user enters Yes; it returns NIL if the
; users enters No.
    LINE )
  (CLEAR-INPUT T)
  (LOOP
    ( ((NULL MSG))
      (FRESH-LINE T)
      (WRITE-STRING (PACK* MSG " (Yes or No) ") T) )
    (WRITE-BYTE 7 NIL T)
    (SETQ LINE (STRING-UPCASE (READ-LINE T)))
    ((EQ LINE 'YES))
    ((EQ LINE 'NO)  NIL) ) )
