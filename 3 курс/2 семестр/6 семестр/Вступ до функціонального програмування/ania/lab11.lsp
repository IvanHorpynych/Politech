;1111111111111111111111
; Euclidean algorithm
(defun gcd-ea (a b) 
	(
		if (= b 0) 
		nil ; zero arg 
		(gcd b (rem a b))
	)
)
	
; comprime test
(defun comprime-test (a b)
	(= 1 (gcd-ea a b))
)

;222222222222222222222222
(defun ifactor (n i)
(	
	if (= i 1)
	nil
	(
		if(= (rem n i) 0)
		(
			cons i (ifactor n (- i 1)) 
		)
		(
			ifactor n (- i 1)
		)
	)
)
)

(defun isfactor (n i s)
(	
	if (= i s)
	nil
	(
		if(= (rem n i) 0)
		(
			cons i (isfactor (/ n i) i s) 
		)
		(
			isfactor n (+ i 1) s
		)
	)
)
)

(defun factor (n)
(
	ifactor n (isqrt n)
	;+ n (isqrt n)
	;isqrt n
)
)

(defun sfactor (n)
(
	isfactor n 2 (+ (/ n 2) 1)
)
)

(defun in (el ls)
(
	if (equal ls nil)
	nil
	(
		if (= el (car ls))
		T
		(
			in el (cdr ls)
		)
	)
)
)


(defun getun (un all)
(
	if (equal all nil)
	un
	(
		if (in (car all) un)
		(
			getun un (cdr all)
		)
		(
			getun (cons (car all) un) (cdr all)
		)
	)
)
)

(defun count2 (item ls) ; replacement for count
(
	if (equal ls nil)
	0
	(
		+ 
		(
			if (eq item (car ls)) 1 0
		)
		(
			count2 item (cdr ls)
		)
	)
)
)

(defun un-count (unls ls)
(
	if(equal unls nil)
	nil
	(
		cons
		(
			if(in (car unls) ls)
			(
				cons (car unls) (list (count2 (car unls) ls))
			)
			nil
		)
		(
			un-count (cdr unls) ls
		)
	)
)
)

;(defun reverse2 (ls) ; replacemet for reverse
;(
;	cons (car ls) (cdr ls)
;)
;)

(defun prime-factors-mult (n)
(
	reverse( un-count (getun nil (sfactor n)) (sfactor n))
)
)
;33333333333333333333333333333333333333

(defun euler-helper (pa)
(
	if(equal pa nil)
	1
	(
		* (- 1 (/ 1 (car pa))) (euler-helper (cdr pa))
	)
)
)

(defun euler-phi (n)
(
	* n (euler-helper (getun nil (sfactor n)))
)
)

; 4444444444444444444444444444444
(defun pol (i vec)
(
	if(equal vec nil)
	0
	(
		+ (* i (car vec)) (pol i (cdr vec))
	)
)
)

(defun soppov (v1 v2)
(
	if(equal v1 nil)
	0
	(
		+ (pol (car v1) v2) (soppov (cdr v1) v2) 
	)
)
)

; 5555555555555555555555555555555555
(defun prime-test (n i)
(
	if(eq i 1)
	T
	(
		if(eq (rem n i) 0)
		nil
		(
			prime-test n (- i 1)
		)
	)
)
)

(defun gen-prime (n)
(
	if(eq n 1)
	nil
	(
		if(prime-test n (isqrt n))
		(
			cons n (gen-prime (- n 1))
		)
		(
			gen-prime (- n 1)
		)
	)
)
)

(defun gen-fg-prime (i)
(
	if(prime-test i (isqrt i))
	i
	(
		gen-fg-prime (+ i 1)
	)
)
)


(defun goldbach-helper (n i)
(
	if(>= i (/ n 2))
	nil
	(
	let ((b (- n i)))
	(
		if(prime-test b (isqrt b))
		(
			list i b
		)
		(
			goldbach-helper n (gen-fg-prime (+ i 1))
		)
	)
	)
)
)

;(defun even (n) (if(eq (rem n 2) 0) n (+ n 1) ) )

(defun goldbach-list (st en)
(
	let* (	(s (if(evenp st) st (+ st 1)))
			(e (if(evenp en) en (- en 1))) )
		(	
			if(> s e)
			nil
			(
				let ((gh-ls (goldbach-helper s 2)))
				(
					progn
					(
						if(eq s 2)
						nil
						(format t "~S=~S+~S~%"  s (car gh-ls) (car (cdr gh-ls)) ); st (car)
					)
					(goldbach-list (+ s 2) e)
				)
			)
		)
)
)

(defun goldbach-list-limited (lower upper limit)
(
	let* (	(s (if(evenp lower) lower (+ lower 1)))
			(e (if(evenp upper) upper (- upper 1))) )
		(	
			if(> s e)
			nil
			(
				let ((gh-ls (goldbach-helper s (gen-fg-prime limit))))
				(
					progn
					(
						if(or (eq s 2) (equal (car gh-ls) nil))
						nil
						(format t "~S=~S+~S~%"  s (car gh-ls) (car (cdr gh-ls)) ); st (car)
					)
					(goldbach-list-limited (+ s 2) e limit)
				)
			)
		)

)
)

;66 66 66 66 66 66 66 66 66 66 66 66 66 66 66 66 
(defun euler-phi2-helper (pfm)
(
	if(equal pfm nil)
	1
	(
		let ( (p_i (car (car pfm))) 
			  (a_i (car (cdr (car pfm)))) )
		(
			* (- p_i 1) (expt p_i (- a_i 1) ) (euler-phi2-helper (cdr pfm))
		)
	)
)
)

(defun euler-phi2 (n)
(
	if(eq n 1)
	1
	(
		euler-phi2-helper (prime-factors-mult n)
	)
)
)