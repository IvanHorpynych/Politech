(defparameter R1 '(R 16))
(defparameter R2 '(R 110))
(defparameter R3 '(R 1))
(defparameter R4 '(R 110))
(defparameter R5 '(R 25))
(defparameter L1 '(L 120))
(defparameter C1 '(C 0.000012))

(defun res (elem fq)
	(cond
		((equalp (car elem) 'R)
			(cadr elem))
		((equalp (car elem) 'L)
			(* #c(0.0 1.0) fq (cadr elem)))
		((equalp (car elem) 'C)
			(/ 1 (* #c(0.0 1.0) fq (cadr elem))))))

(defun serial (res1 res2)
	(+ res1 res2))

(defun parallel (res1 res2)
	(/ (* res1 res2) (+ res1 res2)))

(defun chain (fq)
	(serial
		(parallel
			(res R4 fq)
			(res C1 fq))
		(parallel
			(res R5 fq)
			(serial
				(res R1 fq)
				(parallel
					(res R3 fq)
					(parallel
						(res R2 fq)
						(res L1 fq))))))) 