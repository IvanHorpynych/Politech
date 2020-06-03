.386
code segment use16
assume cs:code
begin:
	mov ah, 0 					;номер функции установки видеорежима 
	mov al, 3 					;номер видеорежима (текстовый 80х25) 
	int 10h 					;вызов прерывания видеосервиса 
	
	mov ax, 0					;инициализация мыши
	int 33h
	
	mov ax, 1					;видимость мыши
	int 33h

	mov ax, 0Ch					;задание обработчика событий
	mov cx, 0000000000001010B			;событие - нажатие правой или левой клавиши мыши
	push es
	push cs
	pop es
	
	;mov bx, seg MOUSE	
	;mov es, bx					;адрес сегментhа обработчика
	lea dx, MOUSE					;смещение обработчика	
	
	
	int 33h	
	pop es
	
	mov ah,1 					; ожидание символа
	int 21h 
	
	xor cx,cx		
	mov	ax,0ch		
	int	33h	
	
	mov ax,4c00h 					;номер функции завершения программы 
	int 21h 					;завершение программы 	
	
Mouse proc far
	push es			  			; cохранение содержания регистра es и РЗП
	pusha
							;загрузка сегментного регистра es
	
	
	;mov ax, 0b800h
	;mov es, ax
	
	push 0b800h 					;сегментный адресс видеобуфера
	pop	es	
							; основное тело процедуры:
	shr cx,3					; номер столбца
	shr dx,3					; номер строки

	mov al, dl 					; в al координету Y
	mov ah, 0 					; обунулим старшую часть ax дабы ничё не мешало
	imul ax,(80*2)				 	; кароч формула находим координату
	add ax,cx 					; и опять ищем
	add ax,cx 					; и вот нашли наконец-то	
	
	
	mov di, ax					;смещение
	
	mov ax, 0ch
	
	cmp bx, 010b					;нажата левая клавиша
	je @p

	cmp bx,001b
	je @pp
	
	
	jmp @el
	
@pp:
	

	mov ax, es:[di]
	mov al, 0h
	mov es:[di], al
	inc di
	mov ah, 0h
	mov es:[di], al
	
	;mov ax, 0000h
	;cld 						
	;sub di,4
	;add di,4
	;stosw 						; пишем то что в ax в es:[di]
	;mov ax, 0000h					; пустой символ
	;mov es:[di],ax					; вывести символ
	
	jmp @el

@p:							
	;mov ax, 0141h
	;cld					
	;sub di,4
	;add di,4
	;mov ax, 0141h					; выводимый символ
	;mov es:[di],ax
	;stosw 						; пишем то что в ax в es:[di]
	

	mov ax, es:[di]
	mov al, 41h
	mov es:[di], al
	inc di
	mov ah, 11h
	mov es:[di], al
	
  
@el:	

	popa						;восстановление содержания регистра es и РЗП
	pop es									
	ret						;возврат управления из процедуры
								
Mouse endp
	
code ends
	end begin	