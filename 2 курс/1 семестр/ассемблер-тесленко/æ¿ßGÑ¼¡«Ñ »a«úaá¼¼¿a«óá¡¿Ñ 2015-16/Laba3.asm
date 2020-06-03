.386
Tabl1 struc
namex db 5 dup (1h)
field1 db ?
field2 dw ?
field8 db 'not error'
Tabl1 ENDS

data1 segment use16
I1 db 1
addr2 dd begin2
A1 Tabl1 6 dup (<>)

data1 ends

data2 segment use16
A2 Tabl1 6 dup (<>)
data2 ends

code1 segment use16
assume cs:code1,ds:data1
start:  
	
	mov ax,data1
    mov ds,ax
	mov I1,0
	xor ax,ax
	xor bx,bx
	xor cx,cx
	xor si,si
		
	loop1:	
	mov ax, type A1
	mul I1
	mov bp,ax
	
	mov si,5
	lea di, word ptr[bp+si]
	add si,di
	
	mov word ptr A1[bp+6],si
	xor bp,bp
	
	add I1,1
	cmp I1,5   
	jle SHORT loop1
	
    jmp addr2
code1 ENDS




code2 segment use16
assume cs:code2,es:data2
begin2: 
	mov ax, data2
	mov es, ax
	std
	xor ax,ax
	xor bx,bx
	xor si,si
	xor di,di
	lea di, A2
	lea si, A1
	loop2:
	add di,17
	add si,17
	mov cx,6
	rep movsb
	add di,6
	add si,6
	add bx,1
	cmp bx,5
	jle SHORT loop2
	mov ax,4c00h
    int 21h
code2 ENDS
end start 