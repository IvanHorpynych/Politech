; предикат, определяющий, являются ли x и y взаимно простыми числами
;(defun coprime-test (a b) (= 1 (gcd a b)))
(defun coprime (x y)
	(if (or (= x 1) (= y 1) (= x -1) (= y -1)) T			;; 1, -1 взаимно простые в комбинации с любым целым числом
		(if (or (= 0 x) (= 0 y)) nil 						;; 0 взаимно прост в комбинации с любым целым числом, кроме 1 и -1
			(do ((i 2 (+ i 1)) (top (min (abs x) (abs y))))
				((> i top) T)								;; возвращаем истину, если делитель не был найден
				(if (and (= 0 (mod x i)) (= 0 (mod y i))) 	;; если находим общий делитель
					(return nil))))))						;; выходим и возвращаем ложный признак

; предикат, определяющий, является ли число простым
(defun prime (a)
	(if (< a 2) nil											;; только натуральные числа являются простыми
		(if (= a 2) T										;; 2 наименьшее простое число
			(do ((i 2 (+ i 1)))
				((= i a) T)									;; возвращаем истину, если делитель не был найден
				(if (= 0 (mod a i))							;; если находим делитель
					(return nil))))))						;; выходим и возвращаем ложный признак

; функция возвращает степень делителя a числа n									
(defun factor-counter (n a count)
	(if (= 1 a)												;; для любое число делится на бесконечно
		nil
		(if (not (= 0 (mod n a)))							;; если число не делится нацело на a
			count											;; возвращаем значение счетчика
			(factor-counter (/ n a) a (+ 1 count)))))		;; или рекурсивно вызываем функцию

; составляем список из из простых делителей и степени делимости
(defun prime-factors-mult (n)
	(let ((prime-list (list)))								;; создаем пустой список
	(do ((i n (- i 1)))										;; начинаем проверку с n-1
		((< i 2) prime-list) 								;; заканчиваем на 2
		(if (prime i)										;; проверка на простоту
			(if (= 0 (mod n i))								;; и делимость
				;; создаем подсписок из двух элементов и добавляем в исходный список
				(setf prime-list (cons (list i (factor-counter n i 0)) prime-list)))))))

; функция Эйлера
(defun phi (m)
	(if (< m 1)												;; m - положительное число
		nil
		(if (= m 1)
			1												;; две 1-цы взаимно просты phi(1) = 1
			(let ((count 1))
			(do ((i 2 (+ i 1)))								;; min(phi(m)) = 1
				((= i m) count)
				(if (coprime i m)							;; проверка на взаимную простоту
					(setf count (+ count 1))))))))			;; записываем новое значение

; попарное произведение элементов списков
(defun list-mult (a b)
	(let ((result 0))
	(do ((i 0 (+ i 1)))
		((or (not (nth i a)) (not (nth i b))) result)		;; когда дошли до конца одного из списков возвращаем сумму
		(setf result (+ result (* (nth i a) (nth i b)))))))	;; иначе прибавляем произведение ооответствующих элеметов

; гипотеза Гольдбаха
; (A)
(defun goldbach (N)
	(if (or (< N 2) (not (= 0 (mod N 2))))					;; проверяем корректность ввода
		nil
		(do ((i 2 (+ i 1)))
			((> i n) nil)
			(if (and (prime i) (prime (- N i)))				;; если находим простые числа, дающие в сумме N
				(return (list i (- N i)))))))				;; возвращаем их в виде списка
; (B)
(defun goldbach-list (lower upper)
	(do ((i (cond 	((< lower 4) 4)							;; не рассматриваем числа меньше 4
					((= 0 (mod lower 2)) lower)				;; проверка на четность
					(t (+ lower 1))) (+ i 2)))
		((> i upper))
		(progn 	(setq small-prime (first (goldbach i)))
				(setq big-prime (second (goldbach i)))
															;; форматный вывод
				(format t "~a~a~a~a~a~%" i " = " small-prime " + " big-prime))))
; (C)
(defun goldbach-list-limited (lower upper limit)
	(do ((i (cond 	((< lower 4) 4)							;; не рассматриваем числа меньше 4
					((= 0 (mod lower 2)) lower)				;; проверка на четность
					(t (+ lower 1))) (+ i 2)))
		((> i upper))
		(progn 	(setq small-prime (first (goldbach i)))
				(setq big-prime (second (goldbach i)))
				(if (> small-prime limit)
															;; форматный вывод
					(format t "~a~a~a~a~a~%" i " = " small-prime " + " big-prime)))))

; функция Эйлера (расчет по формуле)
(defun phi-lightning (m)
	(setq factors-list (prime-factors-mult m))				;; список простых делителей
	(setq tmp-factor (first factors-list))					;; текущий делитель
	(if (< m 1)
		nil
		(let ((phi-value 1))
		(do ((i 1 (+ i 1)))
			((not tmp-factor) phi-value)					;; пока не достигли конца списка
			(progn
															;; расчитываем по формуле
				(setf phi-value (* phi-value (- (first tmp-factor) 1) (expt (first tmp-factor) (- (second tmp-factor) 1))))
				(setf tmp-factor (nth i factors-list)))))))	;; и читаем новый элемент списка делителей