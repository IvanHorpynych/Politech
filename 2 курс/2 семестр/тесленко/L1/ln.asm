;Забезпечити виведення на екран в шестадцятирічному
;форматі вмісту регістрів ax, bx, cx і dx, одержуваних процедурою 
;користувача для обробки подій від мишки. Умовою виклику процедури 
;прийняти будь-яке переміщення мишки. 
;Основна програма реалізується згідно з Прикладом 1
.386
data segment use16
sax		dw	0
scx		dw  0		 
data ends

assume ds:data, cs:code

code segment use16
prmaus 	proc 		far
; збереження вмісту регістрів ds, es та  РЗП
	push		di    
	push 		es
	push 		ds
	pusha
;завантаження  сегментних регістрів ds та  es
	push		0b800h ; сегментна адреса відеобуфера
	pop 		es
	push 		data
	pop 		ds

;основне тіло процедури:
	
	mov sax, ax
	mov scx, cx ;сохраним регистр сх, так как сх будет использоваться как счетчик в цикле
	
;Виведення AX
	mov cl, 0
	mov di, 16
	@aloop:
	mov ax, sax ;загрузим в ax изначальное содержимое ах
	shr ax, cl	;его вправо, чтобы выделить следующие 4 бита
	and ax, 1111b ;возьмем 4 бита исходного числа
	
	;найдем 16-ричную цифру, которая соответствует этим 4 битам
		cmp ax, 9 
		jle @d ; если число <= 9, то это десятичная цифра
		add ax, 55 ; иначе - буква от a до f. Код a в ASCII - 55
		jmp @out
		@d:
		add ax, 48 ; десятичные цифры начинаются с 48 в ASCII
		
	@out:
	mov es:[di], al ;выведем эту цифру
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @aloop
	
;Виведення BX
	mov cl, 0
	mov di, (16+160)
	@bloop:
	mov ax, bx
	shr ax, cl
	and ax, 1111b ;возьмем 4 бита исходного числа
	
	;найдем 16-ричную цифру, которая соответствует этим 4 битам
		cmp ax, 9 
		jle @bd ; если число <= 9, то это десятичная цифра
		add ax, 55 ; иначе - буква от a до f. Код a в ASCII - 55
		jmp @bout
		@bd:
		add ax, 48 ; десятичные цифры начинаются с 48 в ASCII
		
	@bout:
	mov es:[di], al ;выведем эту цифру
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @bloop
	
;Виведення CX
	mov cl, 0
	mov di, (16+160*2)
	@cloop:
	mov ax, scx
	shr ax, cl
	and ax, 1111b ;возьмем 4 бита исходного числа
	
	;найдем 16-ричную цифру, которая соответствует этим 4 битам
		cmp ax, 9 
		jle @cd ; если число <= 9, то это десятичная цифра
		add ax, 55 ; иначе - буква от a до f. Код a в ASCII - 55
		jmp @cout
		@cd:
		add ax, 48 ; десятичные цифры начинаются с 48 в ASCII
		
	@cout:
	mov es:[di], al ;выведем эту цифру
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @cloop

;Виведення DX
	mov cl, 0
	mov di, (16+160*3)
	@dloop:
	mov ax, dx
	shr ax, cl
	and ax, 1111b ;возьмем 4 бита исходного числа
	
	;найдем 16-ричную цифру, которая соответствует этим 4 битам
		cmp ax, 9 
		jle @dd ; если число <= 9, то это десятичная цифра
		add ax, 55 ; иначе - буква от a до f. Код a в ASCII - 55
		jmp @dout
		@dd:
		add ax, 48 ; десятичные цифры начинаются с 48 в ASCII
		
	@dout:
	mov ah, 01001111b
	mov es:[di], ax ;выведем эту цифру
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @dloop
	
	;відновлення регістрів
	popa
	pop 		ds
	pop 		es
	pop 		di
	ret
prmaus	endp

begin:
	mov ax, data
	mov ds, ax

;ініціалізація миші
	xor 	ax, ax
	int 	33h


	mov 	ax, 0ch	; встановити режим обробки подій від мишки
	mov		cx, 11111b ;любые события. Возможно, надо 0001b
	push 	es	;зберегти вміст сегментного регістра 
	push 	cs
	pop 	es		; вважаєм, що процедура користувача
					; для обробки подій від мишки знаходиться 
					;в поточному сегменті кодів
	lea 	dx, prmaus; встановити зміщення процедури
						;обробки подій від мишки в сегменті кодів
	int 	33h		; регістрація адреси та умов виклику
	pop 	es


;WRITE
	mov ax, 0b800h
	mov es, ax

	mov dx, 15

	xor ax, ax
	mov	di,ax ;1st string
	
; увімкнути графічний режим
	mov	    ax, 3
	int 	10h

;виведення маски "ax: " / "bx: / "cx: "...
	mov dl, 'a'
        mov dh, 'x'
	mov cx, 4
	@regloop:
		mov es:[di], dl
		mov es:[di+2], dh
		inc dl
		add di, (80*2)
		dec cx
		jnz  @regloop
		
	 mov	di,ax ;1st string
	 mov cx, 4
	 @xloop:
		 mov byte ptr es:[di+2], 'x'
		 mov byte ptr es:[di+4], ':'
		 add di, (80*2)
		 dec cx
		 jnz  @xloop


	mov 	ah, 01h	; ввести символ з клавіатури ПЕОМ	
	int		21h		; виклик функції DOS 

	xor 	cx,cx
	mov		ax,0ch
	;вважаємо що регістри es:dx містять логічну адресу процедури prmaus
	int		33h		;процедура prmaus
					;далі викликатись не буде

code ends

end begin
