(defun simple-numbers (n)
	(when (>= n 2)
		(let ((res (simple-numbers (1- n))))
			(if (member 0 (mapcar (lambda (x) (mod n x)) res))
				res
				(append res (list n))))))

(defun factors-list (n simple res)
	(cond
		((= 1 n)
			res)
		((= 0 (mod n (car simple)))
				(factors-list (/ n (car simple)) simple (append res (list (car simple)))))
		(t
			(factors-list n (cdr simple) res))))

(defun number-of-entry (n lst)
	(cond
		((null lst)
			0)
		((equal n (car lst))
			(1+ (number-of-entry n (cdr lst))))
		(t
			(number-of-entry n (cdr lst)))))

(defun prime-factors-mult (n)
	(let* ((res) (simple (simple-numbers n)) (factors (factors-list n simple nil)))
		(dolist (x simple)
			(when (/= 0 (number-of-entry x factors))
				(setq res (append res (list (list x (number-of-entry x factors)))))))
		res))
