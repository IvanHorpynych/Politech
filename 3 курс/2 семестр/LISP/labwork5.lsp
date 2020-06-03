(defun deepest (lst &optional res)
	(cond
		((null lst)
			res)
		((atom (car lst))
			(deepest (cdr lst) res))
		(t
			(append-children lst res))))

(defun append-children (lst res)
	(when lst
		(dolist (x (car lst))
			(setq lst (append lst (list x))))
		(setq res (car lst))
		(deepest (cdr lst) res)))

(format t "~a~%" (deepest '(a b c d)))
(format t "~a~%" (deepest '(a (b) (c) d e (f))))
(format t "~a~%" (deepest '(a (b (c (d) e) (f)) (g h (i j)))))