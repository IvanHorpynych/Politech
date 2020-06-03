; ��������, ������������, �������� �� x � y ������� �������� �������
;(defun coprime-test (a b) (= 1 (gcd a b)))
(defun coprime (x y)
	(if (or (= x 1) (= y 1) (= x -1) (= y -1)) T			;; 1, -1 ������� ������� � ���������� � ����� ����� ������
		(if (or (= 0 x) (= 0 y)) nil 						;; 0 ������� ����� � ���������� � ����� ����� ������, ����� 1 � -1
			(do ((i 2 (+ i 1)) (top (min (abs x) (abs y))))
				((> i top) T)								;; ���������� ������, ���� �������� �� ��� ������
				(if (and (= 0 (mod x i)) (= 0 (mod y i))) 	;; ���� ������� ����� ��������
					(return nil))))))						;; ������� � ���������� ������ �������

; ��������, ������������, �������� �� ����� �������
(defun prime (a)
	(if (< a 2) nil											;; ������ ����������� ����� �������� ��������
		(if (= a 2) T										;; 2 ���������� ������� �����
			(do ((i 2 (+ i 1)))
				((= i a) T)									;; ���������� ������, ���� �������� �� ��� ������
				(if (= 0 (mod a i))							;; ���� ������� ��������
					(return nil))))))						;; ������� � ���������� ������ �������

; ������� ���������� ������� �������� a ����� n									
(defun factor-counter (n a count)
	(if (= 1 a)												;; ��� ����� ����� ������� �� ����������
		nil
		(if (not (= 0 (mod n a)))							;; ���� ����� �� ������� ������ �� a
			count											;; ���������� �������� ��������
			(factor-counter (/ n a) a (+ 1 count)))))		;; ��� ���������� �������� �������

; ���������� ������ �� �� ������� ��������� � ������� ���������
(defun prime-factors-mult (n)
	(let ((prime-list (list)))								;; ������� ������ ������
	(do ((i n (- i 1)))										;; �������� �������� � n-1
		((< i 2) prime-list) 								;; ����������� �� 2
		(if (prime i)										;; �������� �� ��������
			(if (= 0 (mod n i))								;; � ���������
				;; ������� ��������� �� ���� ��������� � ��������� � �������� ������
				(setf prime-list (cons (list i (factor-counter n i 0)) prime-list)))))))

; ������� ������
(defun phi (m)
	(if (< m 1)												;; m - ������������� �����
		nil
		(if (= m 1)
			1												;; ��� 1-�� ������� ������ phi(1) = 1
			(let ((count 1))
			(do ((i 2 (+ i 1)))								;; min(phi(m)) = 1
				((= i m) count)
				(if (coprime i m)							;; �������� �� �������� ��������
					(setf count (+ count 1))))))))			;; ���������� ����� ��������

; �������� ������������ ��������� �������
(defun list-mult (a b)
	(let ((result 0))
	(do ((i 0 (+ i 1)))
		((or (not (nth i a)) (not (nth i b))) result)		;; ����� ����� �� ����� ������ �� ������� ���������� �����
		(setf result (+ result (* (nth i a) (nth i b)))))))	;; ����� ���������� ������������ ��������������� ��������

; �������� ���������
; (A)
(defun goldbach (N)
	(if (or (< N 2) (not (= 0 (mod N 2))))					;; ��������� ������������ �����
		nil
		(do ((i 2 (+ i 1)))
			((> i n) nil)
			(if (and (prime i) (prime (- N i)))				;; ���� ������� ������� �����, ������ � ����� N
				(return (list i (- N i)))))))				;; ���������� �� � ���� ������
; (B)
(defun goldbach-list (lower upper)
	(do ((i (cond 	((< lower 4) 4)							;; �� ������������� ����� ������ 4
					((= 0 (mod lower 2)) lower)				;; �������� �� ��������
					(t (+ lower 1))) (+ i 2)))
		((> i upper))
		(progn 	(setq small-prime (first (goldbach i)))
				(setq big-prime (second (goldbach i)))
															;; ��������� �����
				(format t "~a~a~a~a~a~%" i " = " small-prime " + " big-prime))))
; (C)
(defun goldbach-list-limited (lower upper limit)
	(do ((i (cond 	((< lower 4) 4)							;; �� ������������� ����� ������ 4
					((= 0 (mod lower 2)) lower)				;; �������� �� ��������
					(t (+ lower 1))) (+ i 2)))
		((> i upper))
		(progn 	(setq small-prime (first (goldbach i)))
				(setq big-prime (second (goldbach i)))
				(if (> small-prime limit)
															;; ��������� �����
					(format t "~a~a~a~a~a~%" i " = " small-prime " + " big-prime)))))

; ������� ������ (������ �� �������)
(defun phi-lightning (m)
	(setq factors-list (prime-factors-mult m))				;; ������ ������� ���������
	(setq tmp-factor (first factors-list))					;; ������� ��������
	(if (< m 1)
		nil
		(let ((phi-value 1))
		(do ((i 1 (+ i 1)))
			((not tmp-factor) phi-value)					;; ���� �� �������� ����� ������
			(progn
															;; ����������� �� �������
				(setf phi-value (* phi-value (- (first tmp-factor) 1) (expt (first tmp-factor) (- (second tmp-factor) 1))))
				(setf tmp-factor (nth i factors-list)))))))	;; � ������ ����� ������� ������ ���������