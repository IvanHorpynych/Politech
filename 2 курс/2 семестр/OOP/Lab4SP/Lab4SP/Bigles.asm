.686
.model flat, C

public Big2sSub
.code
Big2sSub proc
@M1	equ	[ebp + 8]
@M2	equ	[ebp + 12]
@M3	equ	[ebp + 16]
@len	equ	[ebp + 20]

	push	ebp
	mov	ebp, esp

	mov	ecx, @len
	shr	ecx, 2
	mov	ebx, 0
	clc

	mov	esi, @M2
	mov	edi, @M3
	mov	edx, @M1

	jecxz	@len_b_4
@1:
	mov	eax, [esi + ebx*4]
	sbb	eax, [edi + ebx*4]
	mov	[edx + ebx*4], eax

	inc	ebx
	loop	@1
@len_b_4:
	lahf
	mov	ecx, @len
	and	ecx, 11b
	jnz	@2
	
@2:
	shl	ebx, 2
	sahf
;@3:
;	mov	al, [esi + ebx]
;	sbb	al, [edi + ebx]
;	mov	[edx + ebx], al
;	inc	ebx
;	loop	@3
	pop	ebp
	ret
Big2sSub	endp
	end