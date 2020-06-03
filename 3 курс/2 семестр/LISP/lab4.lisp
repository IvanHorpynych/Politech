(defun func(lst)
(cond ((null lst) nil)
		(t (cons (car lst) (func (cdddr lst))))
))

(func '(a b c d e f g))