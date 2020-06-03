; File: HELP.LSP  (C)		05/25/89	Soft Warehouse, Inc.

;			muLISP Help Facility

; If this file is loaded and <name> is the name of a function,
; special form, macro, or variable that is defined in the muLISP
; machine language kernel or in a muLISP library file, the command

;		(HELP 'name)

; displays <name>'s arguments or initial value and the page it is
; documented on in the muLISP Reference Manual.

(DEFUN HELP (NAME)
  (LOOP
    (FRESH-LINE T)
    ((GET NAME 'HELP-POINTER)
      ((OR (INPUT-FILE-P *HELP-FILE*) (OPEN-INPUT-FILE *HELP-FILE*))
	(FILE-READ-POSITION (GET NAME 'HELP-POINTER) *HELP-FILE*)
	(WRITE-LINE (READ-LINE *HELP-FILE*) T)
	T )
      (WRITE-LINE (PACK* *HELP-FILE* "not found") T)
      NIL )
    (WRITE-STRING "Enter primitive name: " T)
    (CLEAR-INPUT T)
    (SETQ NAME (STRING-UPCASE (STRING-TRIM '" " (READ-LINE T))))
    ((EQ NAME '||)) ) )

(MOVD 'HELP '?)

(SETQ *HELP-FILE* (CAR (INPUT-FILES)))

((LAMBDA (*TAB-EXPAND*)
(SETQ *TAB-EXPAND* T)
(LOOP
  ((NOT (LISTEN *HELP-FILE*))
    (TERPRI 2 T)
    (WRITE-LINE
      "For help on the muLISP primitive <name>, enter:	(HELP 'name)" T)
    (SETQ *INPUT-FILE*) )
  ((LAMBDA (NUM LINE)
      ((EQ LINE ||))
      (PUT (STRING-RIGHT-TRIM |: | (SUBSTRING LINE 0 (FINDSTRING '| | LINE)))
	   'HELP-POINTER NUM))
    (FILE-READ-POSITION NIL *HELP-FILE*)
    (READ-LINE *HELP-FILE*)) ) ))

ABS [n] 				   Numerical	  Function     135
ABSTRAP [object]			   Break	  Function     274
ACONS [key, object, a-list]		   A-list	  Function     91
ACOS [n]				   IRRATNAL.LSP   Function     288
ACOSH [n]				   IRRATNAL.LSP   Function     290
ACOT [n, m]				   IRRATNAL.LSP   Function     288
ACOTH [n]				   IRRATNAL.LSP   Function     290
ACSC [n]				   IRRATNAL.LSP   Function     288
ACSCH [n]				   IRRATNAL.LSP   Function     290
ADD1 [n]				   Numerical	  Function     130
ADJOIN [object, list, test]		   Constructor	  Function     58
ALLOCATE [n]				   Memory	  Function     201
ALPHA-CHAR-P [symbol]			   Character	  Function     149
ALPHANUMERICP [symbol]			   Character	  Function     149
AND [form1, ..., formn] 		   Logical	  Special form 48
APPEND [list1, ..., listn]		   Constructor	  Function     60
APPLY [function, arg1, ..., argn, arglist] Application	  Function     187
AREF [array, subscript1, ...]		   ARRAY.LSP	  Macro        280
ARRAY-DIMENSION [array, axis]		   ARRAY.LSP	  Function     281
ARRAY-DIMENSIONS [array]		   ARRAY.LSP	  Function     281
ARRAY-IN-BOUNDS [array, subscript1, ...]   ARRAY.LSP	  Function     282
ARRAY-RANK [array]			   ARRAY.LSP	  Function     281
ARRAY-TOTAL-SIZE [array]		   ARRAY.LSP	  Function     281
ARRAYP [object] 			   ARRAY.LSP	  Function     282
ASCII [atom]				   Character	  Function     153
ASEC [n]				   IRRATNAL.LSP   Function     288
ASECH [n]				   IRRATNAL.LSP   Function     290
ASH [n, count]				   Numerical	  Function     143
ASIN [n]				   IRRATNAL.LSP   Function     288
ASINH [n]				   IRRATNAL.LSP   Function     290
ASSOC [key, a-list, test]		   A-list	  Function     92
ASSOC-IF [test, a-list] 		   A-list	  Function     92
ATAN [n, m]				   IRRATNAL.LSP   Function     288
ATANH [n]				   IRRATNAL.LSP   Function     290
ATOM [object]				   Recognizer	  Function     39
*AUTO-NEWLINE*: T			   Output	  Control var. 222

BACK [distance] 			   GRAPHICS.LSP   Function     308
BACKGROUND-COLOR [n]			   Screen	  Function     231
BENCH-MARK [form]			   Hardware	  Macro        235
BINARY-LOAD [file, offset]		   Hardware	  Function     244
*BLANK-COMPRESS*: NIL			   Output	  Control var. 222
BORDER-COLOR [n]			   Screen	  Function     231
BREAK [object, message] 		   Break	  Function     270
BREAK					   Break	  Variable     270
BREAK-CHARS [list, flag]		   Input	  Function     205
BUTLAST [list, n]			   Constructor	  Function     63
BUTTON-PRESSES [button] 		   MOUSE.LSP	  Function     312
BUTTON-RELEASES [button]		   MOUSE.LSP	  Function     312

CAAAAR [object] 			   Selector	  Function     51
CAAADR [object] 			   Selector	  Function     51
CAAAR [object]				   Selector	  Function     51
CAADAR [object] 			   Selector	  Function     51
CAADDR [object] 			   Selector	  Function     51
CAADR [object]				   Selector	  Function     51
CAAR [object]				   Selector	  Function     51
CADAAR [object] 			   Selector	  Function     51
CADADR [object] 			   Selector	  Function     51
CADAR [object]				   Selector	  Function     51
CADDAR [object] 			   Selector	  Function     51
CADDDR [object] 			   Selector	  Function     51
CADDR [object]				   Selector	  Function     51
CADR [object]				   Selector	  Function     51
CAR [object]				   Selector	  Function     50
CASE [keyform, case1, ..., casen]	   Control	  Macro        172
CATCH [tag, form1, ..., formn]		   Control	  Special form 176
CDAAAR [object] 			   Selector	  Function     51
CDAADR [object] 			   Selector	  Function     51
CDAAR [object]				   Selector	  Function     51
CDADAR [object] 			   Selector	  Function     51
CDADDR [object] 			   Selector	  Function     51
CDADR [object]				   Selector	  Function     51
CDAR [object]				   Selector	  Function     51
CDDAAR [object] 			   Selector	  Function     51
CDDADR [object] 			   Selector	  Function     51
CDDAR [object]				   Selector	  Function     51
CDDDAR [object] 			   Selector	  Function     51
CDDDDR [object] 			   Selector	  Function     51
CDDDR [object]				   Selector	  Function     51
CDDR [object]				   Selector	  Function     51
CDR [object]				   Selector	  Function     50
CEILING [n, m]				   Numerical	  Function     137
CHANGE-WINDOW [ ]			   WINDOWS.LSP	  Function     339
CHAR [atom, n]				   String	  Function     157
CHAR-CODE [symbol]			   Character	  Function     153
CHAR-DOWNCASE [symbol]			   Character	  Function     154
CHAR-EQUAL [sym1, ..., symn]		   Character	  Function     152
CHAR-GREATERP [sym1, ..., symn] 	   Character	  Function     152
CHAR-LESSP [sym1, ..., symn]		   Character	  Function     152
CHAR-NOT-EQUAL [sym1, ..., symn]	   Character	  Function     152
CHAR-NOT-GREATERP [sym1, ..., symn]	   Character	  Function     152
CHAR-NOT-LESSP [sym1, ..., symn]	   Character	  Function     152
CHAR-UPCASE [symbol]			   Character	  Function     154
CHAR=  [symbol1, ..., symboln]		   Character	  Function     151
CHAR<  [symbol1, ..., symboln]		   Character	  Function     151
CHAR>  [symbol1, ..., symboln]		   Character	  Function     151
CHAR<=	[symbol1, ..., symboln] 	   Character	  Function     151
CHAR>=	[symbol1, ..., symboln] 	   Character	  Function     151
CHAR/=	[symbol1, ..., symboln] 	   Character	  Function     151
CLEAR-INPUT [source]			   Input	  Function     208
CLEAR-SCREEN [ ]			   Screen	  Function     229
CLOSE-INPUT-FILE [file] 		   Input file	  Function     248
CLOSE-OUTPUT-FILE [file]		   Output file	  Function     254
CLOSE-WINDOW				   WINDOWS.LSP	  Keyword      338
CLOSURE [varlist, function]		   CLOSURE.LSP	  Function     301
CLOSURE-ALIST [closure] 		   CLOSURE.LSP	  Function     305
CLOSURE-FUNCTION [closure]		   CLOSURE.LSP	  Function     305
CLOSURE-VARIABLES [closure]		   CLOSURE.LSP	  Function     305
CLOSUREP [closure]			   CLOSURE.LSP	  Function     303
CLR [ ] 				   GRAPHICS.LSP   Function     307
CODE-CHAR [n]				   Character	  Function     153
*COLOR* 				   GRAPHICS.LSP   Control var. 306
COLUMN [ ]				   Screen	  Function     228
COMMENT [comments]			   Evaluation	  Macro        185
COMPILE [name]				   COMPILE.LSP	  Function     344
COMPILE-FILE [compile-files, load-files]   COMPILE.LSP	  Function     346
:CONC-NAME [prefix]			   STRUCTUR.LSP   Option       292
COND [cond1, ..., condn]		   Control	  Special form 171
*CONDENSE*: NIL 			   Definition	  Control var. 117
CONS [obj1, obj2]			   Constructor	  Function     57
CONSOLE-INTERRUPT [ ]			   Break	  Function     273
CONSP [object]				   Recognizer	  Function     40
CONSTANTP [object]			   Evaluation	  Function     184
:CONSTRUCTOR [name, arglist]		   STRUCTUR.LSP   Option       292
CONTINUE-PROMPT [ ]			   WINDOWS.LSP	  Function     339
:COPIER [name]				   STRUCTUR.LSP   Option       292
COPY-ALIST [a-list]			   A-list	  Function     91
COPY-CHAR-TYPE [char1, char2, flag]	   Input	  Function     211
COPY-CLOSURE [closure]			   CLOSURE.LSP	  Function     303
COPY-LIST [list]			   Constructor	  Function     61
COPY-TREE [object]			   Constructor	  Function     61
COS [n] 				   IRRATNAL.LSP   Function     287
COS-DEG [angle] 			   GRAPHICS.LSP   Function     306
COSH [n]				   IRRATNAL.LSP   Function     289
COT [n] 				   IRRATNAL.LSP   Function     287
COTH [n]				   IRRATNAL.LSP   Function     289
COUNT [object, list, test]		   Search	  Function     79
COUNT-IF [test, list]			   Search	  Function     79
CREATE-WINDOW				   WINDOWS.LSP	  Keyword      338
CSC [n] 				   IRRATNAL.LSP   Function     287
CSCH [n]				   IRRATNAL.LSP   Function     289
CSMEMORY [offset, value, flag]		   Hardware	  Function     240
CURSOR-LINES [start-line, end-line]	   Screen	  Function     233

DECF [place, n] 			   Numerical	  Macro        132
DECQ [symbol, n]			   Numerical	  Special form 131
DEFAULT-DRIVE [drive]			   Directory	  Function     261
DEFAULT-PATH [drive, path]		   Directory	  Function     261
DEFFLAVOR [name, vars, flavors, opt1, ...] FLAVORS.LSP	  Macro        295
DEFMACRO [sym, arglist, form1, ...]	   Definition	  Special form 116
DEFMETHOD [(flv type meth), args, form1, ...] FLAVORS.LSP Macro        297
DEFSTRUCT [(name opt1 ...), slot1, ...]    STRUCTUR.LSP   Macro        291
DEFUN [symbol, arglist, form1, ..., formn] Definition	  Special form 115
DEFUN [symbol, (ftype arglist form1 ... formn)] Definition Special form 115
DEFUN [symbol, address] 		   Definition	  Special form 115
DEFWHOPPER [(flavor meth), args, form1, ...] FLAVORS.LSP  Macro        298
DELETE [item, list, test]		   Deletion	  Function     74
DELETE-COMPILER [ ]			   COMPILE.LSP	  Macro        345
DELETE-DUPLICATES [list, test]		   Deletion	  Function     76
DELETE-FILE [file]			   File 	  Function     259
DELETE-IF [test, list]			   Deletion	  Function     75
DELETE-LINES [n]			   Screen	  Function     229
DELETE-NTH [list, n]			   Modifier	  Function     72
DENOMINATOR [n] 			   Numerical	  Function     136
DENTRAP [object]			   Break	  Function     274
DIGIT-CHAR-P [symbol, radix]		   Character	  Function     150
DIRECTORY [path]			   Directory	  Function     261
DISPLAY-PAGE [n, active-only]		   Screen	  Function     232
DIVIDE [n, m]				   Numerical	  Function     140
DO [letlist, form1, ..., formn] 	   Control	  Macro        174
DO* [letlist, form1, ..., formn]	   Control	  Macro        174
DOLIST [(var list result), form1, ..., formn]		  Control      Macro
					   175
DOS [command]				   Control	  Function     180
DOT [x, y]				   GRAPHICS.LSP   Function     306
DOTIMES [(var count result), form1, ..., formn] 	  Control      Macro
					   175
DRIVER [file]				   Driver	  Function     268
DRIVER: 'DRIVER 			   Driver	  Control var. 267
DSMEMORY [offset, value, flag]		   Hardware	  Function     240

EIGHTH [list]				   Selector	  Function     53
ENCODE-UNIVERSAL-TIME [s, m, h, d, mo, y, z]   Hardware   Function     236
ENDP [object]				   Recognizer	  Function     41
EQ [obj1, obj2] 			   Comparator	  Function     42
EQL [obj1, obj2]			   Comparator	  Function     43
EQUAL [obj1, obj2, test]		   Comparator	  Function     44
ERROR-BEEP [ ]				   WINDOWS.LSP	  Function     339
EVAL [form]				   Evaluation	  Function     181
EVAL-FUNCTION-P [symbol]		   Definition	  Function     111
EVENP [object]				   Numerical	  Function     123
EVERY [test, list1, ..., listn] 	   Mapping	  Function     192
*EXACT-MODE*: NIL			   Numerical	  Control var. 119
EXACT-MODEP [ ] 			   IRRATNAL.LSP   Function     283
EXECUTE [program, command]		   Control	  Function     180
EXECUTE-OPTION [prompt, option-tree]	   WINDOWS.LSP	  Function     339
EXP [n] 				   IRRATNAL.LSP   Function     283
EXPT [n, m]				   IRRATNAL.LSP   Function     284

FBOUNDP [symbol]			   Definition	  Function     111
FIFTH [list]				   Selector	  Function     53
FILE-DATE [file]			   File 	  Function     260
FILE-LENGTH [file]			   File 	  Function     260
FILE-LINE-LENGTH [n, file]		   Output file	  Function     258
FILE-READ-POSITION [n, file]		   Input file	  Function     251
FILE-WRITE-POSITION [n, file]		   Output file	  Function     257
FILL [list, object]			   Modifier	  Function     68
FIND [object, list, test]		   Search	  Function     77
FIND-IF [test, list]			   Search	  Function     77
FINDSTRING [atom1, atom2, n]		   String	  Function     163
FIRST [list]				   Selector	  Function     53
FIRSTN [n, list]			   Constructor	  Function     63
FLAG [symbol, flag]			   Flag 	  Function     108
FLAGP [symbol, flag]			   Flag 	  Function     108
FLOOR [n, m]				   Numerical	  Function     137
FMAKUNBOUND [symbol]			   Definition	  Function     114
FOREGROUND-COLOR [n]			   Screen	  Function     231
FORWARD [distance]			   GRAPHICS.LSP   Function     308
FOURTH [list]				   Selector	  Function     53
*FREE-LIST*: NIL			   Memory	  Variable     200
FRESH-LINE [sink]			   Output	  Function     220
FUNCALL [function, arg1, ..., argn]	   Application	  Function     186
FUNCTIONP [object]			   Evaluation	  Function     184

GCD [n1, ..., nm]			   Numerical	  Function     133
GCDTRAP [obj1, obj2]			   Break	  Function     274
*GCHOOK*: NIL				   Memory	  Control var. 200
*GCSTAT*: NIL				   Memory	  Control var. 198
GENSYM [atom]				   String	  Function     165
*GENSYM-COUNT*: 0			   String	  Control var. 165
*GENSYM-PREFIX*: G			   String	  Control var. 165
GET [symbol, key]			   Property	  Function     106
GET-DECODED-TIME [ ]			   Hardware	  Function     236
GET-UNIVERSAL-TIME [ ]			   Hardware	  Function     236
GET-MACRO-CHAR [char, flag]		   Input	  Function     212
GETD [symbol, flag]			   Definition	  Function     109
GETSET [parameter]			   Directory	  Function     262
:GETTABLE-INSTANCE-VARIABLES		   FLAVORS.LSP	  Option       295
GO-DOS [ ]				   WINDOWS.LSP	  Function     340

HIDE-MOUSE [ ]				   MOUSE.LSP	  Function     311
*HISTORY-LENGTH*: 25			   DEBUG.LSP	  Control var. 336

IDENTITY [object]			   Evaluation	  Function     183
IF [testform, thenform, elseform]	   Control	  Special form 170
*IGNORE-CASE*: NIL			   Input	  Control var. 216
INCF [place, n] 			   Numerical	  Macro        132
:INCLUDE [structure]			   STRUCTUR.LSP   Option       293
:INCLUDED-FLAVORS [flavor1, ..., flavorn]  FLAVORS.LSP	  Option       296
INCQ [symbol, n]			   Numerical	  Special form 131
:INITABLE-INSTANCE-VARIABLES		   FLAVORS.LSP	  Option       295
:INITIAL-CONTENTS expression		   ARRAY.LSP	  Option       279
:INITIAL-ELEMENT expression		   ARRAY.LSP	  Option       279
*INIT-WINDOW*				   WINDOWS.LSP	  Control var. 337
*INPUT-ECHO*: NIL			   Input	  Control var. 215
INPUT-FILE [file]			   Input file	  Function     249
*INPUT-FILE*: NIL			   Input file	  Control var. 250
INPUT-FILE-P [file]			   Input file	  Function     249
INPUT-FILES [ ] 			   Input file	  Function     249
INSERT-LINES [n]			   Screen	  Function     229
*INSERT-MODE*: T			   Line editor	  Control var. 277
INSERT-NTH [item, list, n]		   Modifier	  Function     72
INTEGER-LENGTH [n]			   Numerical	  Function     144
INTEGERP [object]			   Recognizer	  Function     38
INTERRUPT [n]				   Hardware	  Function     243
*INTERRUPT-HOOK*: 'CONSOLE-INTERRUPT   Break		  Control var. 273
INTERSECTION [list1, list2, test]	   Set		  Function     87
ISQRT [n]				   IRRATNAL.LSP   Function     284

LAMBDA					   Definition	  Keyword      109
LAST [list]				   Selector	  Function     54
LCM [n1, ..., nm]			   Numerical	  Function     134
LCMTRAP [obj1, obj2]			   Break	  Function     274
LCONC [dotted-pair, list]		   Modifier	  Function     73
LDIFF [list, tail]			   Constructor	  Function     64
LEFT [angle]				   GRAPHICS.LSP   Function     307
*LEFT-BUTTON*				   MOUSE.LSP	  Variable     312
LENGTH [object] 			   Selector	  Function     56
LET [letlist, form1, ..., formn]	   Control	  Macro        169
LET* [letlist, form1, ..., formn]	   Control	  Macro        169
LET-CLOSED [letlist, function]		   CLOSURE.LSP	  Macro        302
LINE [x1, y1, x2, y2]			   GRAPHICS.LSP   Function     306
*LINE-COLUMN*				   Line editor	  Variable     276
LINE-EDIT [text, point, col, width]	   Line editor	  Function     276
*LINE-POINT*				   Line editor	  Variable     276
*LINE-TERMINATOR*			   Line editor	  Variable     276
LIST [obj1, ..., objn]			   Constructor	  Function     59
LIST* [obj1, ..., objn] 		   Constructor	  Function     59
LIST-LENGTH [object]			   Selector	  Function     56
LISTEN [source] 			   Input	  Function     208
LISTP [object]				   Recognizer	  Function     40
LN [n]					   IRRATNAL.LSP   Function     285
LOAD [file, display]			   Memory image   Function     265
*LOAD-VERBOSE*: T			   Memory image   Control var. 265
LOCATION [object]			   Hardware	  Function     242
LOG [n, base]				   IRRATNAL.LSP   Function     285
LOGAND [n1, ..., nm]			   Numerical	  Function     142
LOGBITP [index, n]			   Numerical	  Function     141
LOGEQV [n1, ..., nm]			   Numerical	  Function     142
LOGIOR [n1, ..., nm]			   Numerical	  Function     142
LOGNOT [n]				   Numerical	  Function     141
LOGTEST [n, m]				   Numerical	  Function     141
LOGXOR [n1, ..., nm]			   Numerical	  Function     142
LOOP [form1, ..., formn]		   Control	  Special form 173
LOWER-CASE-P [symbol]			   Character	  Function     148

MACRO					   Definition	  Keyword      109
MACRO-FUNCTION [symbol] 		   Macro	  Function     194
MACRO-P [symbol]			   Definition	  Function     111
MACROEXPAND [form]			   Macro	  Function     195
MACROEXPAND: T				   Macro	  Control var. 196
MACROEXPAND-1 [form]			   Macro	  Function     195
MAKE-ARRAY [dimensions, opt1, ..., optn]   ARRAY.LSP	  Function     279
MAKE-INSTANCE [flavor, vars]		   FLAVORS.LSP	  Macro        299
MAKE-LIST [n, object, list]		   Constructor	  Function     61
MAKE-RANDOM-STATE [state]		   Numerical	  Function     146
MAKE-WINDOW [row, column, rows, cols]	   Screen	  Function     227
MAPC [function, list1, ..., listn]	   Mapping	  Function     188
MAPCAN [function, list1, ..., listn]	   Mapping	  Function     190
MAPCAR [function, list1, ..., listn]	   Mapping	  Function     189
MAPCON [function, list1, ..., listn]	   Mapping	  Function     190
MAPL [function, list1, ..., listn]	   Mapping	  Function     188
MAPLIST [function, list1, ..., listn]	   Mapping	  Function     189
MAX [n1, ..., nm]			   Numerical	  Function     127
MAXTRAP [obj1, obj2]			   Break	  Function     274
MEMBER [object, list, test]		   Search	  Function     81
MEMBER-IF [test, list]			   Search	  Function     81
MEMORY [address, value, flag]		   Hardware	  Function     239
MERGE [list1, list2, test]		   Sorting	  Function     96
:METHOD-COMBINATION			   FLAVORS.LSP	  Option       296
MIN [n1, ..., nm]			   Numerical	  Function     127
MINTRAP [obj1, obj2]			   Break	  Function     274
MINUSP [object] 			   Numerical	  Function     122
MISMATCH [list1, list2, test]		   Search	  Function     80
MOD [n, m]				   Numerical	  Function     139
MOUSE-BOX [x1, y1, x2, y2]		   MOUSE.LSP	  Function     313
MOUSE-POSITION [x, y]			   MOUSE.LSP	  Function     311
MOUSE-RESET [ ] 			   MOUSE.LSP	  Function     310
MOUSE-STATUS [ ]			   MOUSE.LSP	  Function     312
*MOUSE-X*				   MOUSE.LSP	  Function     311
*MOUSE-Y*				   MOUSE.LSP	  Function     311
MOVD [symbol1, symbol2] 		   Definition	  Function     113

:NAMED					   STRUCTUR.LSP   Option       293
NBUTLAST [list, n]			   Modifier	  Function     71
NCONC [list1, ..., listn]		   Modifier	  Function     69
NEQ [obj1, obj2]			   Comparator	  Function     42
NEQL [obj1, obj2]			   Comparator	  Function     43
NEW-CODE-SPACE [ ]			   Memory	  Function     202
NIL					   Evaluation	  Constant     41
NINTERSECTION [list1, list2, test]	   Set		  Function     87
NINTH [list]				   Selector	  Function     53
NLAMBDA 				   Definition	  Keyword      109
NO-EVAL-FUNCTION-P [symbol]		   Definition	  Function     111
:NO-VANILLA-FLAVOR			   FLAVORS.LSP	  Option       296
NOT [object]				   Logical	  Function     47
NOTANY [test, list1, ..., listn]	   Mapping	  Function     191
NOTEVERY [test, list1, ..., listn]	   Mapping	  Function     192
NRECONC [list, object]			   Modifier	  Function     70
NREVERSE [list, object] 		   Modifier	  Function     70
NSET-DIFFERENCE [list1, list2, test]	   Set		  Function     88
NSET-EXCLUSIVE-OR [list1, list2, test]	   Set		  Function     89
NSUBLIS [a-list, object, test]		   A-list	  Function     94
NSUBST [new, old, object, test] 	   Substitution   Function     84
NSUBST-IF [new, test, object]		   Substitution   Function     85
NSUBSTITUTE [new, old, list, test]	   Substitution   Function     82
NSUBSTITUTE-IF [new, test, list]	   Substitution   Function     83
NTH [n, list]				   Selector	  Function     55
NTHCDR [n, list]			   Selector	  Function     55
NULL [object]				   Recognizer	  Function     41
NUMBERP [object]			   Recognizer	  Function     38
NUMERATOR [n]				   Numerical	  Function     136
NUMERIC-CHAR-P [symbol] 		   Character	  Function     149
NUMTRAP [object]			   Break	  Function     274
NUNION [list1, list2, test]		   Set		  Function     86

OBLIST [ ]				   Constructor	  Function     66
ODDP [object]				   Numerical	  Function     123
OPEN-INPUT-FILE [file]			   Input file	  Function     247
OPEN-OUTPUT-FILE [file, overwrite]	   Output file	  Function     252
OR [form1, ..., formn]			   Logical	  Special form 49
ORDERP [obj1, obj2]			   Comparator	  Function     46
OTHERWISE				   Control	  Keyword      172
*OUTPUT-ECHO*: NIL			   Output	  Control var. 222
OUTPUT-FILE [file]			   Output file	  Function     255
*OUTPUT-FILE*: NIL			   Output file	  Control var. 256
OUTPUT-FILE-P [file]			   Output file	  Function     255
OUTPUT-FILES [ ]			   Output file	  Function     255

PACK [list]				   String	  Function     156
PACK* [atom1, ..., atomn]		   String	  Function     156
PAIRLIS [keys, objects, a-list] 	   A-list	  Function     91
PALETTE-COLOR [n]			   Screen	  Function     231
PARSE-INTEGER [string, radix]		   Input	  Function     210
PEEK-CHAR [type, source, eof-p, eof-v]	   Input	  Function     207
PENDOWN [ ]				   GRAPHICS.LSP   Function     307
PENUP [ ]				   GRAPHICS.LSP   Function     307
PI [ ]					   IRRATNAL.LSP   Function     286
PLOT-CIRCLE [x, y, r, color]		   Screen	  Function     234
PLOT-DOT [x, y, color]			   Screen	  Function     234
PLOT-LINE [x1, y1, x2, y2, color]	   Screen	  Function     234
PLUSP [object]				   Numerical	  Function     122
POP [symbol]				   Symbol	  Special form 103
PORTIO [port, value, flag]		   Hardware	  Function     238
POSITION [object, list, test]		   Search	  Function     78
POSITION-IF [test, list]		   Search	  Function     78
PRECISION [n]				   Numerical	  Function     119
:PREDICATE [name]			   STRUCTUR.LSP   Option       292
PRIN1 [object, sink]			   Output	  Function     218
PRINC [object, sink]			   Output	  Function     218
PRINT [object, sink]			   Output	  Function     218
*PRINT-BASE*: 10			   Output	  Control var. 224
*PRINT-DOWNCASE*: NIL			   Output	  Control var. 223
*PRINT-ESCAPE*: T			   Output	  Control var. 223
PRINT-LENGTH [atom]			   String	  Function     164
*PRINT-POINT*: 7			   Output	  Control var. 225
*PRINT-TRACE*: NIL			   DEBUG.LSP	  Control var. 336
*PRINTER-ECHO*: NIL			   Output	  Control var. 222
PROBE-FILE [file]			   File 	  Function     259
PROG1 [form1, ..., formn]		   Control	  Special form 168
PROG2 [form1, ..., formn]		   Control	  Macro        168
PROGN [form1, ..., formn]		   Control	  Special form 167
PROMPT-YN [prompt]			   WINDOWS.LSP	  Function     339
PSETQ [symbol1, form1, ..., symboln, formn]  Symbol	  Special form 101
PUSH [form, symbol]			   Symbol	  Special form 104
PUSHNEW [form, symbol, test]		   Symbol	  Macro        104
PUT [symbol, key, object]		   Property	  Function     105
PUTD [symbol, definition]		   Definition	  Function     112

QUIT-PROGRAM [ ]			   WINDOWS.LSP	  Function     340
QUOTE [object]				   Evaluation	  Special form 183

RANDOM [n, state]			   Numerical	  Function     145
*RANDOM-STATE*				   Numerical	  Control var. 146
RANDOM-STATE-P [object] 		   Numerical	  Function     147
RASSOC [key, a-list, test]		   A-list	  Function     93
RASSOC-IF [test, a-list]		   A-list	  Function     93
RATIONAL				   IRRATNAL.LSP   Keyword      283
RATIONALP [object]			   Recognizer	  Function     38
READ [source, eof-p, eof-v]		   Input	  Function     204
READ-ATOM [source, eof-p, eof-v]	   Input	  Function     205
*READ-BASE*: 10 			   Input	  Control var. 215
READ-BYTE [source, eof-p, eof-v]	   Input	  Function     206
READ-CHAR [source, eof-p, eof-v]	   Input	  Function     206
READ-DOT [x, y] 			   Screen	  Function     234
READ-FROM-STRING [string, eof-p, eof-v]    Input	  Function     209
READ-LINE [source, eof-p, eof-v]	   Input	  Function     206
READ-RECORD [source, eof-p, eof-v]	   Input	  Function     206
*READ-STRINGS*				   Input	  Variable     209
*READ-UPCASE*: T			   Input	  Control var. 215
RECLAIM [ ]				   Memory	  Function     197
REDUCE [function, list, initial]	   Mapping	  Function     193
REGISTER [n, m] 			   Hardware	  Function     243
REM [n, m]				   Numerical	  Function     139
REMD [symbol]				   Definition	  Function     114
REMFLAG [symbol, flag]			   Flag 	  Function     108
REMOVE [item, list, test]		   Deletion	  Function     74
REMOVE-DUPLICATES [list, test]		   Deletion	  Function     76
REMOVE-IF [test, list]			   Deletion	  Function     75
REMPROP [symbol, key]			   Property	  Function     107
RENAME-FILE [oldname, newname]		   File 	  Function     259
REPLACE [list1, list2]			   Modifier	  Function     68
REPLACE-NTH [item, list, n]		   Modifier	  Function     72
:REQUIRED-FLAVORS [flavor1, ..., flavorn]  FLAVORS.LSP	  Option       295
REST [list]				   Selector	  Function     53
RESTART [ ]				   Control	  Function     179
RETURN [form]				   Control	  Special form 178
REVAPPEND [list, object]		   Constructor	  Function     65
REVERSE [list, object]			   Constructor	  Function     65
RIGHT [angle]				   GRAPHICS.LSP   Function     307
*RIGHT-BUTTON*				   MOUSE.LSP	  Variable     312
ROUND [n, m]				   Numerical	  Function     138
ROW [ ] 				   Screen	  Function     228
RPLACA [obj1, obj2]			   Modifier	  Function     67
RPLACD [obj1, obj2]			   Modifier	  Function     67
RUN-WINDOW				   WINDOWS.LSP	  Keyword      338

SAVE [file]				   Memory image   Function     263
SEC [n] 				   IRRATNAL.LSP   Function     287
SECH [n]				   IRRATNAL.LSP   Function     289
SECOND [list]				   Selector	  Function     53
SELF					   FLAVORS.LSP	  Variable     300
SEND [object, operation, arg1, ..., argn]  FLAVORS.LSP	  Macro        300
SET [symbol, object]			   Symbol	  Function     99
SET-CURSOR [row, column]		   Screen	  Function     228
SET-DIFFERENCE [list1, list2, test]	   Set		  Function     88
SET-DISPLAY [ ] 			   WINDOWS.LSP	  Function     340
SET-EXCLUSIVE-OR [list1, list2, test]	   Set		  Function     89
SET-IN-CLOSURE [closure, symbol, object]   CLOSURE.LSP	  Function     304
SET-INSERT [ ]				   WINDOWS.LSP	  Function     340
SET-MACRO-CHAR [char, defn, flag]	   Input	  Function     212
SET-MENU-COLOR [ ]			   WINDOWS.LSP	  Function     340
SET-MUTE [ ]				   WINDOWS.LSP	  Function     340
SET-WORK-COLOR [ ]			   WINDOWS.LSP	  Function     340
SETCOLOR [angle]			   GRAPHICS.LSP   Function     307
SETF [place1, form1, ..., placen, formn]   Symbol	  Macro        102
SETHEADING [angle]			   GRAPHICS.LSP   Function     307
SETPOS [x, y]				   GRAPHICS.LSP   Function     307
SETQ [symbol1, form1, ..., symboln, formn] Symbol	  Special form 100
:SETTABLE-INSTANCE-VARIABLES		   FLAVORS.LSP	  Option       295
SEVENTH [list]				   Selector	  Function     53
SHIFT [n, count]			   Numerical	  Function     143
*SHOW-LOAD*: T				   DEBUG.LSP	  Control var. 333
SHOW-MOUSE [ ]				   MOUSE.LSP	  Function     311
SHOW-PROMPT [prompt]			   WINDOWS.LSP	  Function     340
SIGNUM [n]				   Numerical	  Function     135
SIN [n] 				   IRRATNAL.LSP   Function     287
SIN-DEG [angle] 			   GRAPHICS.LSP   Function     306
SINH [n]				   IRRATNAL.LSP   Function     289
SIXTH [list]				   Selector	  Function     53
SLEEP [seconds] 			   Hardware	  Function     237
SNAPSHOT [address, atom]		   Hardware	  Function     245
SOME [test, list1, ..., listn]		   Mapping	  Function     191
SORT [list, test]			   Sorting	  Function     97
SPACES [n, sink]			   Output	  Function     220
SPECIAL 				   Definition	  Keyword      109
SPECIAL-FORM-P [symbol] 		   Definition	  Function     111
SPLIT [list]				   Sorting	  Function     95
SQRT [n]				   IRRATNAL.LSP   Function     284
STABLE-SORT [list, test]		   Sorting	  Function     97
STACK-LIST [ ]				   Constructor	  Function     66
STRING-CAPITALIZE [atom]		   String	  Function     162
STRING-DOWNCASE [atom]			   String	  Function     162
STRING-EQUAL [atom1, atom2]		   String	  Macro        158
STRING-GREATERP [atom1, atom2]		   String	  Macro        160
*STRING-INDEX*				   Input	  Variable     209
STRING-LEFT-TRIM [chars, atom]		   String	  Function     161
STRING-LESSP [atom1, atom2]		   String	  Macro        160
STRING-NOT-EQUAL [atom1, atom2] 	   String	  Macro        160
STRING-NOT-GREATERP [atom1, atom2]	   String	  Macro        160
STRING-NOT-LESSP [atom1, atom2] 	   String	  Macro        160
STRING-RIGHT-TRIM [chars, atom] 	   String	  Function     161
STRING-TRIM [chars, atom]		   String	  Function     161
STRING-UPCASE [atom]			   String	  Function     162
STRING=  [atom1, atom2, flag]		   String	  Function     158
STRING<  [atom1, atom2, flag]		   String	  Function     159
STRING>  [atom1, atom2, flag]		   String	  Function     159
STRING<=  [atom1, atom2, flag]		   String	  Function     159
STRING>=  [atom1, atom2, flag]		   String	  Function     159
STRING/=  [atom1, atom2, flag]		   String	  Function     159
STRINGP [object]			   Recognizer	  Function     37
SUB1 [n]				   Numerical	  Function     130
SUBLIS [a-list, object, test]		   A-list	  Function     94
SUBLIST [list, n, m]			   Constructor	  Function     62
SUBSETP [list1, list2, test]		   Set		  Function     90
SUBST [new, old, object, test]		   Substitution   Function     84
SUBST-IF [new, test, object]		   Substitution   Function     85
SUBSTITUTE [new, old, list, test]	   Substitution   Function     82
SUBSTITUTE-IF [new, test, list] 	   Substitution   Function     83
SUBSTRING [atom, n, m]			   String	  Function     157
SYMBOL-FUNCTION [symbol]		   Definition	  Function     110
SYMBOL-PLIST [symbol]			   Property	  Function     106
SYMBOL-VALUE [symbol]			   Symbol	  Function     98
SYMBOLP [object]			   Recognizer	  Function     37
SYMEVAL-IN-CLOSURE [closures, symbol]	   CLOSURE.LSP	  Function     304
SYSTEM [n]				   Control	  Function     179

*TAB-EXPAND*: NIL			   Input	  Control var. 215
TAILP [list1, list2]			   Comparator	  Function     45
TAN [n] 				   IRRATNAL.LSP   Function     287
TANH [n]				   IRRATNAL.LSP   Function     289
TCONC [dotted-pair, object]		   Modifier	  Function     73
TENTH [list]				   Selector	  Function     53
TERPRI [n, sink]			   Output	  Function     220
THIRD [list]				   Selector	  Function     53
THROW [tag, form]			   Control	  Special form 178
*THROW-TAG*				   Control	  Variable     178
TIME [reset]				   Hardware	  Function     235
*TIME-ZONE*: 0				   Hardware	  Control var. 236
TONE [frequency, milliseconds]		   Hardware	  Function     237
TREE-EQUAL [obj1, obj2, test]		   Comparator	  Function     44
TRUNCATE [n, m] 			   Numerical	  Function     138
TURTLE [ ]				   GRAPHICS.LSP   Function     309
TURTLE-WINDOW [ ]			   GRAPHICS.LSP   Function     309
:TYPE LIST				   STRUCTUR.LSP   Option       293

*UNDEFINED-HOOK*: 'UNDEFINED-FUNCTION	   Break	  Control var. 273
UNDEFINED-FUNCTION [form]		   Break	  Function     273
UNDEFMETHOD [(flavor type method)]	   FLAVORS.LSP	  Macro        297
UNDERFLOW [n]				   Numerical	  Function     121
UNION [list1, list2, test]		   Set		  Function     86
UNLESS [testform, form1, ..., formn]	   Control	  Macro        170
UNPACK [atom]				   String	  Function     155
UNREAD-CHAR [source]			   Input	  Function     207
UNWIND-PROTECT [form1, form2, ..., formn]  Control	  Special form 177
UPDATE-STATE [arglist]			   WINDOWS.LSP	  Function     341
UPDATE-WINDOW				   WINDOWS.LSP	  Keyword      338
UPPER-CASE-P [symbol]			   Character	  Function     148

VECTOR [obj1, ..., objn]		   ARRAY.LSP	  Function     279
VIDEO-MODE [n]				   Screen	  Function     230

*WINDOW-TYPES*				   WINDOWS.LSP	  Control var. 337
WINDOWS [command-line]			   WINDOWS.LSP	  Function     341
WHEN [testform, form1, ..., formn]	   Control	  Macro        170
WRITE [object, sink]			   Output	  Function     217
WRITE-BYTE [n, m, sink] 		   Output	  Function     221
WRITE-LINE [symbol, sink]		   Output	  Function     219
WRITE-STRING [symbol, sink]		   Output	  Function     219

Y-OR-N-P [message]			   Query	  Function     275
YES-OR-NO-P [message]			   Query	  Function     275

ZEROP [object]				   Numerical	  Function     122

+  [n1, ..., nm]			   Numerical	  Function     128
-  [n1, ..., nm]			   Numerical	  Function     128
*  [n1, ..., nm]			   Numerical	  Function     129
/  [n1, ..., nm]			   Numerical	  Function     129

+TRAP [obj1, obj2]			   Break	  Function     274
-TRAP [obj1, obj2]			   Break	  Function     274
*TRAP [obj1, obj2]			   Break	  Function     274
/TRAP [obj1, obj2]			   Break	  Function     274

=  [n1, ..., nm]			   Numerical	  Function     124
/=  [n1, ..., nm]			   Numerical	  Function     124
<  [n1, ..., nm]			   Numerical	  Function     125
>  [n1, ..., nm]			   Numerical	  Function     125
<=  [n1, ..., nm]			   Numerical	  Function     126
>=  [n1, ..., nm]			   Numerical	  Function     126

\  (back slash) 			   Input	  Escape char. 204
|  (vertical bar)			   Input	  Escape char. 204

(  (left parenthesis)			   Input	  Macro char.  213
)  (right parenthesis)			   Input	  Macro char.  213
]  (right square bracket)		   Input	  Macro char.  213
,  (comma)				   Input	  Macro char.  213
'  (single quote)			   Input	  Macro char.  213
;  (semicolon)				   Input	  Macro char.  213
"  (double quote)			   Input	  Macro char.  214
`  (backquote)				   Input	  Macro char.  214

+					   Driver	  Variable     268
++					   Driver	  Variable     268
+++					   Driver	  Variable     268
*					   Driver	  Variable     268
**					   Driver	  Variable     268
***					   Driver	  Variable     268
-					   Driver	  Variable     268
