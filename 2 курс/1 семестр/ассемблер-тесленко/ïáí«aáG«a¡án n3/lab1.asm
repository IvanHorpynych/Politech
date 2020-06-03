.286

Tabl1		struc
namex		db		4 dup(?)
field1		dw		4 dup(?)
Tabl1		ENDS

Data1 segment
I_struc			db	?
I_namex			db	?
A1		Tabl1 6 dup(<>)
Data1 ends

Data2 segment
A2		Tabl1 13 dup(<>)
Data2 ENDs


code1 segment
Assume cs:code1, ds:data1

code2ptr	dd	begin2

begin:
mov		ax, data1
mov		ds, ax

xor 	ax, ax

mov I_struc,0
cmp		I_struc, 6
jnl	@end_1
@rep_1:
	mov		I_namex,0
	cmp		I_namex,4
	jnl @end_2
	@rep_2:
		mov		ax, type A1
		mul		I_struc
		mov 	bp, offset A1
		add 	bp, ax
		
		mov		si, word ptr I_namex
		shl		si, 1
		mov		di, 2
		add		di,si
		shr 	si, 1
		
		lea		ax, word ptr [bp+si]
		mov		word ptr A1[bp+di], ax
		
		
		inc		I_namex
	cmp I_namex,4
	jl @rep_2
	@end_2:
	
inc I_struc

cmp I_struc,6
jl @rep_1

@end_1:


jmp dword ptr code2ptr

code1 ends


code2 	segment
ASSUME 	cs:code2, ds:data1, es:data2
begin2:
	mov		ax, data1
	mov		ds, ax
	mov		ax, data2
	mov		es, ax
	
	mov		ax, type A1
	mov 	dx, 5
	mul		dx
	mov		bp, ax
	
	mov 	cx, 36
	lea		si, A1
	lea		di, A2[bp]
	cld
	rep movsw

mov 	ax, 4c00h
int 	21h
code2 ENDS

end  begin