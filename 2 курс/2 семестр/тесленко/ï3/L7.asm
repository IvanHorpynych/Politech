.586p
descr	struct
 limit_1  dw	0
 base_1 dw	0
 base_2 db	0
 attrib db	0
 bt6  db	0
 base_3 db	0
descr	ends
;---------------------------------------------------------
dt_p	struc
 lim  dw	0
 adr  dd	0
dt_p	ends
;---------------------------------------------------------
S_cr0	equ	10011010b ;   Readable 0 level code segment
S_cr1	equ	10111010b ;   Readable 1 level code segment
S_c_0	equ	10011000b ; UnReadable 0 level code segment
S_dw0	equ	10010010b ;   Writable 0 level data segment
S_dw1	equ	10110010b ;   Writable 1 level data segment
S_d_0	equ	10010000b ; UnWritable 0 level data segment
S_d_1	equ	10110000b ; UnWritable 1 level data segment
S_sw0	equ	10010110b ;   Writable stack segment
;---------------------------------------------------------
_GDT	segment	para public 'data' use16
 descr_0	descr	<>
 descr_gdt  descr	<>
 descr_ds	descr	<>
 descr_es	descr	<>
 descr_ss	descr	<>
 descr_cs	descr	<>
 descr_i_code	descr	<>
 descr_t2 descr <>
 descr_t10  descr <>
 _gdt_size	equ	$-1
_GDT	ends
;---------------------------------------------------------
_IDT	segment para public 'data'
	vector = 0
	rept	256
	dw	Vector * proc_i_size
	dw	offset descr_i_code
	db	0
	db	10001111b
	dw	0
	vector = vector+1
	endm
_IDT	ends
;---------------------------------------------------------
_ST	segment	use16
  db	1000 dup (0)
 Top_stp  equ $
_ST	ends
;---------------------------------------------------------
_data	segment para public 'data' use16
 pgdt	dt_p	<>
 pidt	dt_p	<>
 old_st	dd	?
 mess	db	"We are in protected mode!"
 messl	equ	$-mess
 t1	db	?
 _data_size	equ	$-1
_data	ends
;---------------------------------------------------------
_t2    segment para public 'data' use16
t2_p:
v1      dw      0
_t2_size       equ     $-t2_p
_t2    ends
;---------------------------------------------------------
_code	segment para public 'code' use16
	assume	cs:_code
_begin:
	mov	eax, cr0
	test	al, 1
	jz	cont

	mov	ax, 4C00h
	int	21h

cont:
	mov	ax, 3
	int	10h

	in	al, 92h
	or	al, 2
	out	92h, al

	assume	ds:_GDT
	mov	ax, _GDT
	mov	ds, ax

	; _GDT segment descr
	mov	descr_gdt.limit_1, _gdt_size
	xor	eax, eax
	mov	ax, _GDT
	shl	eax, 4
	mov	dword ptr descr_gdt.base_1, eax
	mov	descr_gdt.attrib, S_dw0

	;_data segment descr
	mov	descr_ds.limit_1, _data_size
	xor	eax, eax
	mov	ax, _data
	shl	eax, 4
	mov	dword ptr descr_ds.base_1, eax
	mov	descr_ds.attrib, S_dw0

	; _ST segment descr
	mov	descr_ss.limit_1, 10
	xor	eax, eax
	mov	ax, _ST
	shl	eax, 4
	mov	dword ptr descr_ss.base_1, eax
	mov	descr_ss.attrib, S_sw0

	; Video segment descr (es)
	mov	descr_es.limit_1, 0FFFFh
	mov	dword ptr descr_es.base_1, 0B8000h
	mov	descr_es.attrib, S_dw0

	; _code segment descr
	mov	descr_cs.limit_1, _code_size
	xor	eax, eax
	mov	ax, _code
	shl	eax, 4
	mov	dword ptr descr_cs.base_1, eax
	mov	descr_cs.attrib, S_c_0

	; _i_code segment descr
	mov	descr_i_code.limit_1, _i_code_size
	xor	eax, eax
	mov	ax, _i_code
	shl	eax, 4
	mov	dword ptr descr_i_code.base_1, eax
	mov	descr_i_code.attrib, S_cr0

; _t2 segment descr
  mov descr_t2.limit_1, _t2_size
  xor eax, eax
  mov ax, _t2
  shl eax, 4
  mov dword ptr descr_t2.base_1, eax
  mov descr_t2.attrib, S_d_0

; _t10 segment descr
  mov descr_t10.limit_1, _code_size
  xor eax, eax
  mov ax, _code
  shl eax, 4
  mov dword ptr descr_t10.base_1, eax
  mov descr_t10.attrib, S_cr1

	assume	ds:_data
	mov	ax, _data
	mov	ds, ax
	xor	eax, eax
	mov	ax, _GDT
	shl	eax, 4
	mov	pgdt.adr, eax
	mov	ax, _gdt_size
	mov	pgdt.lim, ax
	lgdt	pgdt

	cli
	mov	word ptr old_st, sp
	mov	word ptr old_st+2, ss
	mov	eax, cr0
	or	al, 1
	mov	cr0, eax

	db	0eah
	dw	offset protect
	dw	offset descr_cs
protect:
	mov	ax, offset descr_ss
	mov	ss, ax
	mov	sp, Top_stp
	mov	ax, offset descr_ds
	mov	ds, ax
	mov	ax, offset descr_es
	mov	es, ax

	xor	eax, eax
	mov	ax, _IDT
	shl	eax, 4
	mov	pidt.adr, eax
	mov	pidt.lim, 8*256
	lidt	pidt

	mov	cx, messl
	mov	si, offset mess
	mov	di, 1000
	mov	ah, 07h
outmes:
	mov	al, [si]
	mov	es:[di], ax
	inc	si
	add	di, 2
	loop	outmes

;--- Tests ---

; Test 2
;	mov ax, offset descr_t2
;	mov ds, ax
;	mov bx, 0
;	mov byte ptr [bx], al

; Test 4
;	mov cs:[0], al

; Test 10
;	db  0eah
;	dw  offset _begin
;	dw  offset descr_t10

; Test 15
;	assume  ds:_GDT
;	mov	ax, offset descr_t2
;	mov	gs, ax
;	mov	ax, offset descr_GDT
;	mov	ds, ax
;	mov	descr_t2.attrib, S_dw0
;	mov	byte ptr gs:v1, 0

;--- End of Tests ---

return_dos:
	assume	ds:_GDT
	cli	
	mov	ax, offset descr_gdt
	mov	ds, ax
	mov	descr_cs.limit_1, 0FFFFh
	mov	descr_ds.limit_1, 0FFFFh
	mov	descr_ss.limit_1, 0FFFFh
	mov	descr_ss.attrib, S_dw0

	assume	ds:_data
	mov	ax, offset descr_ds
	mov	ds, ax
	mov	gs, ax
  mov fs, ax
	mov	ax, offset descr_es
	mov	es, ax
	mov	ax, offset descr_ss
	mov	ss, ax
	db	0EAh
	dw	offset jumpt
	dw	offset descr_cs
jumpt:
	xor	eax, eax
	mov	pidt.adr, eax
	mov	pidt.lim, 3FFh
	lidt	pidt

	mov	eax, cr0
	and	al, 0FEh
	mov	cr0, eax
	db	0EAh
	dw	offset r_mode
	dw	_code

r_mode:
	assume	ds:_data
	mov	ax, _data
	mov	ds, ax
	lss	sp, old_st
	sti
	mov	ah, 01h
	int	21h
	mov	ax, 3
	int	10h
	mov	ax, 4C00h
	int	21h

	_code_size	equ	$-1
_code	ends
;---------------------------------------------------------
_i_code	segment	para public 'code' use16
	assume	cs:_i_code
	vector = 0
i_beg:
  rept  255

	pusha
	mov	ax, vector
	jmp	common_int
  vector = vector + 1

  endm
i_end:
	proc_i_size = 7

mes_int	db		'INTERUPT N'

common_int:
  mov cl, 10
	div cl
	or  ah, 30h
	mov bh, ah
	xor ah, ah
	div cl
	or  ax, 3030h
	mov dx, ax

	push  offset descr_es
	pop es
	mov si, offset Mes_int
	mov cx, 10
	mov di, 2620
	mov ah, 07h
outstr:
	mov al,CS:[si]
	stosw
	inc si
	loop  outstr
	mov al, ' '
	stosw
	mov al, dl
	stosw
	mov al, dh
	stosw
	mov al, bh
	stosw

	pushf
	push  offset descr_cs
	push  offset return_dos
	iret
common_end:
	_i_code_size = $-1
_i_code	ends
 	end _begin
