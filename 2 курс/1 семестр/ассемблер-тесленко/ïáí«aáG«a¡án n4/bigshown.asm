.386     
.model flat,C    ; модель пам`яті та передача параметрів за правилами С 
  
.code      ; розділ коду програми 

FBig2sSub proc

M1      EQU [EBP+8]
M2      EQU [EBP+12]
M3      EQU [EBP+16]
len     EQU [EBP+20]

push    EBP
mov     EBP, ESP        ; базова адреса фактичних параметрів 

		mov esi, [M3]
		mov edi, [M1] 
		xor ecx, ecx
		mov cx, [len] 
		shr ecx, 2
 
		cld
		clc

		test ecx, ecx
		jz @loop_1_end

	@loop_1_continue:
		lodsd 
		sbb [edi], eax
		lea edi, [edi + 4]
	  loop @loop_1_continue
	@loop_1_end:

		pushf
		mov cx, [len]
		and ecx, 03h
		jz @loop_2_end_popf
		popf

	@loop_2_continue:
		lodsb
		sbb [edi], al
		inc edi
	  loop @loop_2_continue
	  jmp @loop_2_end
	@loop_2_end_popf:
		popf
	@loop_2_end:
		
POP		EBP
		ret  
FBig2sSub endp

end
