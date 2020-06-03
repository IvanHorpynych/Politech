
.386
data segment use16
sax		dw	0
scx		dw  0		 
data ends

assume ds:data, cs:code

code segment use16
prmaus 	proc 		far

	push		di    
	push 		es
	push 		ds
	pusha

	push		0b800h 
	pop 		es
	push 		data
	pop 		ds


	
	mov sax, ax
	mov scx, cx 
	

	mov cl, 0
	mov di, 16
	@aloop:
	mov ax, sax 
	shr ax, cl	
	and ax, 1111b 
	
	
		cmp ax, 9 
		jle @d 
		add ax, 55 
		jmp @out
		@d:
		add ax, 48 
		
	@out:
	mov es:[di], al 
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @aloop
	

	mov cl, 0
	mov di, (16+160)
	@bloop:
	mov ax, bx
	shr ax, cl
	and ax, 1111b
	
	
		cmp ax, 9 
		jle @bd 
		add ax, 55 
		jmp @bout
		@bd:
		add ax, 48 
		
	@bout:
	mov es:[di], al 
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @bloop
	

	mov cl, 0
	mov di, (16+160*2)
	@cloop:
	mov ax, scx
	shr ax, cl
	and ax, 1111b 
	
	
		cmp ax, 9 
		jle @cd 
		add ax, 55
		jmp @cout
		@cd:
		add ax, 48 
		
	@cout:
	mov es:[di], al 
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @cloop


	mov cl, 0
	mov di, (16+160*3)
	@dloop:
	mov ax, dx
	shr ax, cl
	and ax, 1111b 
	

		cmp ax, 9 
		jle @dd 
		add ax, 55 
		jmp @dout
		@dd:
		add ax, 48
		
	@dout:
	mov es:[di], al 
	sub di, 2
	add cl, 4
	cmp cl, 16
	jne @dloop
	

	popa
	pop 		ds
	pop 		es
	pop 		di
	ret
prmaus	endp

begin:
	mov ax, data
	mov ds, ax


	xor 	ax, ax
	int 	33h


	mov 	ax, 0ch	
	mov		cx, 11111b 
	push 	es	
	push 	cs
	pop 	es		
					
					
	lea 	dx, prmaus
						
	int 	33h		
	pop 	es



	mov ax, 0b800h
	mov es, ax

	mov dx, 15

	xor ax, ax
	mov	di,ax 
	

	mov	    ax, 3
	int 	10h


	mov dl, 'a'
	mov cx, 4
	@regloop:
		mov es:[di], dl
		inc dl
		add di, (80*2)
		dec cx
		jnz  @regloop
		
	 mov	di,ax 
	 mov cx, 4
	 @xloop:
		 mov byte ptr es:[di+2], 'x'
		 mov byte ptr es:[di+4], ':'
		 add di, (80*2)
		 dec cx
		 jnz  @xloop


	mov 	ah, 01h		
	int		21h		

	xor 	cx,cx
	mov		ax,0ch

	int		33h		
					

code ends

end begin
