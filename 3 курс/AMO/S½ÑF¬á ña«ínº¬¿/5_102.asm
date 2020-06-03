.386
code segment use16
assume cs:code
begin:
	mov ah, 0 					;����� ������� ��������� ����������� 
	mov al, 3 					;����� ����������� (��������� 80�25) 
	int 10h 					;����� ���������� ������������ 
	
	mov ax, 0					;������������� ����
	int 33h
	
	mov ax, 1					;��������� ����
	int 33h

	mov ax, 0Ch					;������� ����������� �������
	mov cx, 0000000000001010B			;������� - ������� ������ ��� ����� ������� ����
	push es
	push cs
	pop es
	
	;mov bx, seg MOUSE	
	;mov es, bx					;����� �������h� �����������
	lea dx, MOUSE					;�������� �����������	
	
	
	int 33h	
	pop es
	
	mov ah,1 					; �������� �������
	int 21h 
	
	xor cx,cx		
	mov	ax,0ch		
	int	33h	
	
	mov ax,4c00h 					;����� ������� ���������� ��������� 
	int 21h 					;���������� ��������� 	
	
Mouse proc far
	push es			  			; c��������� ���������� �������� es � ���
	pusha
							;�������� ����������� �������� es
	
	
	;mov ax, 0b800h
	;mov es, ax
	
	push 0b800h 					;���������� ������ �����������
	pop	es	
							; �������� ���� ���������:
	shr cx,3					; ����� �������
	shr dx,3					; ����� ������

	mov al, dl 					; � al ���������� Y
	mov ah, 0 					; �������� ������� ����� ax ���� ���� �� ������
	imul ax,(80*2)				 	; ����� ������� ������� ����������
	add ax,cx 					; � ����� ����
	add ax,cx 					; � ��� ����� �������-��	
	
	
	mov di, ax					;��������
	
	mov ax, 0ch
	
	cmp bx, 010b					;������ ����� �������
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
	;stosw 						; ����� �� ��� � ax � es:[di]
	;mov ax, 0000h					; ������ ������
	;mov es:[di],ax					; ������� ������
	
	jmp @el

@p:							
	;mov ax, 0141h
	;cld					
	;sub di,4
	;add di,4
	;mov ax, 0141h					; ��������� ������
	;mov es:[di],ax
	;stosw 						; ����� �� ��� � ax � es:[di]
	

	mov ax, es:[di]
	mov al, 41h
	mov es:[di], al
	inc di
	mov ah, 11h
	mov es:[di], al
	
  
@el:	

	popa						;�������������� ���������� �������� es � ���
	pop es									
	ret						;������� ���������� �� ���������
								
Mouse endp
	
code ends
	end begin	