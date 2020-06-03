.386
;======================================================
;		������ ��������������ί �������
;======================================================
  max_prg		equ		10	;����������� ������� "���������"
                                              		; �����������  �����
  time_slice		equ		65535; ������� ����������, �������� �� ����
 						; ����� ���� (����������� �������� 65535)

_ST		SEGMENT		WORD STACK 'stack' use16
       		dw      	32000 dup (?)
  		top 		label		word
   		dw		100 dup (?);������ ��� �������
       				 ; ���� ������������ �����
_ST		ENDS

_DATA         SEGMENT    WORD PUBLIC 'DATA' use16

@ms_dos_busy	dd	  (?) ; ������ ������ ������ ��������� MS-DOS
  	int8set       	db        0   			;������ ������������ ����������� �� �������
 	int9set       	db        0  			;������ ������������ ����������� �� ���������
	fon		equ		max_prg		; ������ ������ ������;
	fonsp       	label  		word		;������ ���������� SP ������ ������
	sssp          	dd       	top  		;������ ������ ����� ������ ������
  ; ����� ������� SP ��� �����, (��� ����� ����� ������ �������� 1000 ���)
  ;������ �������� �������� 
  	stp   	      dw        	 1000,2000,3000,4000
          	      dw         	 5000,6000,7000,8000
         	      dw        	 9000,10000,11000,12000
          	      dw        	 13000,14000,15000,16000

	nprg        	dw	 	0     		;����� ������� ������ (�� 0 �� 
							;max_prg-1)
							; ��� ������ ������ ������ (fon)
  ; ����� ����� �����
  	init  	   	db     	16 dup (0)

  ; ����� ����������� ����� ������ �����
  	clock  	db   	16 dup (1)

  ; ����� ��������� ������ ����� 
  	clockt  db    	16 dup (0)

  	screen_addr 	dw  	16 dup (0) 		; ������ (������� �� ������� �����������)
 ; ������ �������� �� ����� ������� ������

  ; ����� ���� �����
  	names   label      word
          	db         '0T1T2T3T4T5T6T7T8T9TATBTCTDTETFT'
	clk     dw       0    				;�������� ���������� �� ������� 

_DATA         ENDS

_TEXT	 SEGMENT	 BYTE PUBLIC 'CODE' use16
 ASSUME		 CS:_TEXT,DS:_DATA

;------------------------------------------------------------
; ��������� "������������" ����������� �� ������� (int8)
;------------------------------------------------------------
  setint8        PROC

;------------------------------------------------------------
                mov	al,int8set
                or		al,al    	    	; �������� "������������" �����������
                jnz	zero_8   ;
                MOV	AH,35H          		; �������� ������ �����������
                MOV  	AL,8     	    		; ����������� �� ������� (8)
                INT    	21H                 		; �������� �� �����������:
                                    		    	; es:bx - ������ ������ �������� ���������
                                    	               	; ������� ����������� �� �������

                mov     cs:int8ptr,bx    		; �������� ������ ������ ��������
                mov    	cs:int8ptr+2,es  		; ��������� � ������� ����

                mov     dx,offset userint8		;���������� � ds:dx ������
                push    ds				; ������ ��������� �����������
                push    cs				; ��� ������� ���������� �� �������
                pop     ds

                MOV  	AH,25H    			; ���������� ������
                MOV  	AL,8         			; ����������� �� �������
                INT    	21H           		; ds:dx - �������� �� �������������
; ��������� �����. ����������� �� ;�������


           mov	     	ax,time_slice       	; ���������� ������ �������� ������ ����
           out 	     	40h,al                  	; 40h - ������ 8-���������� ����� �������, 
 						; ����� ���� ������� ����� ������� 
; �������� �������� ����,
; � ���� �������

           jmp  		$+2              		; ����������� ����� ���������� ����������
                                           		; ��������� � ���� �������� ���������
                                           		; ��������. ����������, ��
                                           		; "���������" ������� jmp ����� �����
                                           		; ���������� ������ ������ �, ��� �����,
                                           		; ��������� ������ ���������. ��� �����
                                           		; ��������� ������� ���� ������� 
       						;�������� ��������� ����
       	nop

          	mov   	al,ah            		; (������� ����)
       	out     	40h,al

           pop        	ds

           mov     	int8set,0ffh 		; �������� ��������� ��������
zero_8:
                ret
  int8ptr	 dw 	   2 dup (?)
  setint8       ENDP


;--------------------------------------------------------------------------
;  ��������� ���������� ������� ����������� �� �������
;--------------------------------------------------------------------------
  retint8       PROC
;--------------------------------------------------------------------------
                push 	     ds
                push 	     dx

                mov 	     al,0ffh           	; �������� ��������� ������
                out  	     40h,al            	; ���������� �������
                jmp	     $+2
                nop
                out   	     40h,al
                mov   	     dx,cs:int8ptr
                mov   	     ds,cs:int8ptr+2

                MOV 	     AH,25H        	; �������� ���������� ������
                MOV 	     AL,8             	; ����������� �� �������
                INT    	     21H              	; ds:dx - �������� (������ ������) 
;�� ��������� (��������) ���������
                                                      ;	 �����. ����������� �� �������
                pop   	     dx
                pop   	     ds
                mov  	     int8set,0h      	; ����� ��������� "�����������"
                ret
  retint8       ENDP



;------------------------------------------------------------
 setint9        PROC
;-----------------------------------------------------------
;  ��������� "������������" ����������� �� ��������� (int9)
;------------------------------------------------------------
                mov	     al,int9set
                or        	     al,al
                jnz       	     zero_9
                MOV  	     AH,35H         	; �������� ������ �����������
                MOV	     AL,9              	; ����������� �� ��������� (9)
                INT             21H             	;�������� �� �����������:
                                                        	; es:bx - �������� �� �������� ���������
                                                        	; ������� ����������� �� ���������

                mov       	cs:int9ptr,bx    	; �������� � ������� ���� �������� 
                mov      	cs:int9ptr+2,es 	; �� �������� ���������

                mov       	dx,offset userint9
                push     	ds
                push     	cs			; ds:dx - �������� �� ��������� �����������
                pop       	ds			; �����. ����������� �� ���������

                MOV   	AH,25H              	; ���������� ������ "������������"
                MOV   	AL,9                   	; ����������� �� ��������� (9)
                INT      	21H                     	; 
      pop     	ds

                mov     	int9set,0ffh        	; �������� ��������� ��������

   zero_9:
                     ret
  int9ptr       dw         2 dup (?)
  setint9        ENDP


;--------------------------------------------------------------------------
;  ��������� ���������� ������������ (����������)
;  ������� ����������� �� ���������
;--------------------------------------------------------------------------
  retint9       PROC
                push 	ds
                push     	dx
                mov     	dx,cs:int9ptr	; ds:dx - �������� �� ��������� (��������)
                mov     	ds,cs:int9ptr+2	; ��������� ������� ����������� ��
						; ���������

                MOV   	AH,25H             	; ���������� ������ �������� ���������
                MOV     	AL,9                  	; ������� ����������� �� ���������
                INT      	21H                   	; 
                                                            	; 
      pop       	dx
                pop       	ds
                mov      	int9set,0h         	; ����� ��������� "�����������"
                ret
  retint9       ENDP


;-----------------------------------------------------------------------------------------------
  ; ��������� ������� ���������� �� ���������,
  ; ����������� ��� ������ ���������� ��� ���������� ����� ���������,
  ; ������� ���������� � MS-DOS ���� ���������� ������ Esc
;------------------------------------------------------------------------------------------------
  userint9  	 proc        far
;----------------------------------------------------------------------------
  esc_key    	equ   	       01h            	; ����-��� ������ esc
                pusha
                push   	es
                in        	al,60h           	; ������ ����-��� - ������� 0-6
                mov     	ah,al			; 7-�� ������ ������� 0 ��� ����������
                and     	al,7fh     		;������, 1- ��� ����������

                push    	ax                		; al - "������" ����-��� (��� ������ 
						; ���������� - ����������) 
                push   	2600
                call       	show			; ����������� ����-���� �� ������

                cmp    	al,esc_key
                je        	ui9010

; (������ 2)
                pop    	es
                popa
                jmp    	dword ptr cs:int9ptr 	; ������� �� ��������
                                                                       	;��������� �������
                                                                       	;���������� �� ���������, ���
                                                                       	;������ �� ��������� 䳿, ���������
                                                                       	;���������� � ��������� ��������


; (������ 1)
; �����Ҳ��� ��в����� ������� � ���²������

ui9010:
                mov    	bx,ax
                in        	al,61h       	;�� 7 ����� 61h ����������� ��� ��������
;				; �������������� �������� � ��������� ����.
                                                  	; ��������� ��������� ���� �� ������
                                                  	; ������������� �������
                                                  	;
                mov     	ah,al
                or        	al,80h	;					 |
                out        	61h,al        	; ��������� �� ���������	L---�     
                jmp    	$+2		;						  |
                mov    	al,ah								  |
                out      	61h,al         	; �������������� ��������	-----

                mov    	al,20h       	; ������������ � ��������� �����������
                                                	; ����������� ������ �� ����������� 
;��������� �� ������� ����� ���������,
                out       	20h,al    	; �� ����������� ��������� ���������� 
;����������� �� ���������

                mov     	ax,bx
                cmp    	ah,al         	; �������� ��䳿 ����������� - �� ����������
                                                 	; �� �� ���������� ������ ���������
                je       	ui9040
                                              	;���������� ������

ui9020:
                push	es
                les		bx, @ms_dos_busy	; es:bx - ������ ������ 
;��������� MS-DOS
                mov	al,es:[bx]			; ax - ������ ��������� MS-DOS
                pop	es
                or       	al,al		; ��������
; ���� ���� ��������� ������ MS-DOS
;� "��������" ������
                jnz      	ui9040      	; �� �� ����� �� �� ��������
                                              	; ��������� ���� �������
					; (� ���������� ������� MS-DOS 
					; �� ��������� �������� ���������)

                call     	retint8
                call    	retint9
                mov   	ax,4c00h
                int      	21h 		; ��ʲ����� ������
; ��������������ί ����˲
ui9040:
                pop      	es		; �������� ���� ��������� ��������
                popa
                iret			; �������� ������� �����������
userint9        	endp
;------------------------------------------------------------
; ��������� ������� ����������� �� �������
;         (�������� ������)
;     ���� ����� ����� (���������������� � ����� init)
ready     	equ        0   	; ������ ����������� � ������ �
                                        	; ������ �� ����������� �������
                                        	; ������ �������������� ���� ���������� ������
execute     	equ        1   	; ������ ����������
hesitation 	equ        2    ; ������ ����������� � ���� �� �����
close          	equ        4    ; ��������� ������ ���������
stop           	equ        8  	 	; ������ ��������  
                                       		; ������ �������������� � ���������
                                        	 	; ���� ��������� ������
absent        	equ       16  		; ������ ������� 


;------------------------------------------------------------
  userint8      PROC       far
;------------------------------------------------------------
                pushad                   	;���������� ��� � ����� ��������� ������
                push     ds

; (������ 3)
                pushf                     	;��������� ������� ���������� �����������
;²�̲���� - ������ ������� �� ����������� (if) ���������� ������� � 0.

call       	cs:dword ptr int8ptr
	;������ �������� ��������� ������� ����������� int8,
	;���, �� �����, �������� 8-�� ����������� � ��������� ���������� 
	;��� �������� ����������� �� ������, ������� if=0

                           
                mov     	ax,_data 	;� ���������� ������� ���� ����������� �������
                mov     	ds,ax		;ds � ���������� ������� ���� ���� �����

                inc        	clk            	; ���������� �������� ���������� �� �������
                push     	clk           	; ���� ���� �������� ��� �������� �����
                push     	2440
                call      	show		; ��������� �� ����� �������� ���������

                xor      	esi,esi
                mov    	si,nprg
                cmp    	si,fon         	; ��������� ������ ������ ?
                je        	disp005

                                              	; ��������� ������ �� ������
                cmp    	clockt[si],1 	; � �� �� ����������� ������ ?
                jc        	disp010

                dec   	clockt[si]  	; �������� �������� ������
                pop    	 ds
                popad                	; ���������� ��������� ��������� ������
                iret

disp005:                             	 ; ��������� ������ ������
                mov     	fonsp,sp
                mov     	nprg,max_prg-1    	; ����������� �������� ����� � 0-��
                mov     	cx,max_prg            	; max_prg - max ������� �����
                jmp     	disp015

disp010:                                               		; ��������� ������ �� ������
                mov     	stp[esi*2],sp       		
                mov     	init[si],hesitation    	; ����������� ������� ������
                mov    	 cx,max_prg


disp015:
	; ���������� ������, ��� ��������� �������� ���������
                mov   	 di,max_prg+1
                sub     	di,cx
                add    	di,nprg
                cmp    	di,max_prg
                jc       	disp018
                sub    	di,max_prg
disp018:
                xor      	ebx,ebx
                mov    	bx,di
                ;push   	bx
                ;push   	3220
                ;call    	show
                                    
                                  	; �� ������ �������� max_prg,max_prg-1,...,2,1
                                  	; bx ������ ��������  nprg+1,nprg+2,...,max_prg- 
;1,0,...,nprg
;
                cmp   	init[bx],ready
                je        	disp100                  	; ������� �� ���������� ������ ������

                cmp   	init[bx],hesitation
                je         	disp020                  	; ������� �� ���������� ������
                                                              	; �������� ������
                loop    	disp015

                                                             		; �������  ������, �� ����� ���������
                                                             		; (�������������), ���� 
							; 
                mov    	sp,fonsp			; ������������ ���� ������ ������
                mov    	nprg,fon
                pop     	ds			; �� ����� ������ ������ ����������
                popad					; ���� �������
                iret                       		         ; ���������� � ������ ������
 

disp020:
                                                           		; ���������� ������ �������� ������
                ;push    	bx
                ;push   	2480
                ;call    	show
                mov   	nprg,bx
                mov   	sp,stp[ebx*2]
                mov   	al,clock[bx]
                mov   	clockt[bx],al        		; ���������� ���������
                                                          		; ������� ������
                mov   	init[bx],execute		; ���� ������ - ������ ����������

                pop     	ds
                popad
                iret

disp100:
                                                             		; ��������������� ������ ������
                mov     	nprg,bx
                mov     	sp,stp[ebx*2]
                mov     	al,clock[bx]
                mov     	clockt[bx],al             	; ���������� ���������
                                                              	; ������� ������
                mov     	init[bx],execute

                push	names[ebx*2]		; ��'� ������
                push    	screen_addr[ebx*2]  	; ������ "����" ��� ������ �� ������ 
                push    	22                               	; ����������� ���������
                call       	Vcount                       	; ������


                xor    	esi,esi
                mov   	si,nprg                        	; �� ax - ����� ������, ���
                                                                  	; ��������� ���� ������ � �����
                                                                  	; ��������� ������ ����
                mov   	init[si],close
                mov    	sp,fonsp
                mov    	nprg,fon
                pop     	ds
                popad
                iret                 		                 ; ���������� � ������ ������

  userint8   	   ENDP
;-
; Vcount - ��������� ��� ����������� ���������� ����� 
; ������ ���������:
;	1-� - ��'� ������ (��� �������) [bp+8]
;	2-� - ������� � ����������� "����" ������ [bp+6]
;	3-� - ������� �������� ������� ��������� [bp+4]
; ���������� 䳿:
;    ��� �������:
;  - �������� �����������
;  - ������� � ����� 10-������ ������� ��� ��������� �����
;  - ������ � �� ������� �� ����� [bp-2] ������ �� ������
;               3-�� ��������� �� 32 (�������� ����������� ��������� -
;               ������������� �� ������� � �������� ����������)
;   - ������ � �� ������� �� ������ [bp-6] ����� � ������
;               ������� � �������� ������� ����� ��������� 
;               ���������� ���������
;  - ������ � ���� � 4-� ���� ��� �������� �� ������ [bp-10]

;   � ���������� � ����:
;           - �������� ��������� ��������� �� �����
;           - ������ �������� ��������� �� 1
;  ���������� ������ ���� �������� ��������� 
;  � ����� "�� �������" � ���� �� 0

Vcount proc    near
         	push  	bp
         	mov  	bp,sp
         	sub   	sp,10         			;���������� � ����� ������ ���
                                            		;���������� �����
         	sti

         push  	es
         mov  	ax,0b800h
         mov  	es,ax

 	mov  	ax,[bp+4]          		;ax = ������� ������� ���������
        	and   	ax,31                  		;ax=ax mod 32 (��� �������������)
        	mov  	[bp-2],ax           		;�� [bp-2] ������� ����. ���������  
                                             			;<32
         	mov  	cx,ax
         	mov  	eax,001b
         	shl     	eax,cl
         	dec   	eax                    		; eax - ����� � ������ 1 �����
                                               	  	; ������� ������� ���������
         mov  	[bp-6],eax

         mov   	dword ptr [bp-10],0   	; �������� ���������

         mov   	di,[bp+6]                      	; ���� ����� ������
         mov  	dx,[bp+8]
         mov  	ah,1011b
         mov  	al,dh
         cld
         stosw
         mov  	al,dl
         stosw

         std                                       	;��������� �� ������ ���������
         add   	di,cx                         	;��������� � �������� �������
         add   	di,cx
         mov  	bx,di
         xor    	edx,edx

l20:                                                  	;���� ��������� ���������  � �������� 
                                                          	;������
         mov  	di,bx
         mov  	cx,[bp-2]
         mov  	ah,1010b                        	; 1010b ������� �������, ������� ���� - 0 
                                                             	; (������)
l40:
         mov  	al,'0'
         shr   	edx,1
         jnc   	l60
         mov  	al,'1'
l60:
         stosw
         loop  	l40

         inc    	dword ptr [bp-10]           	; +1 � ��������
         mov  	edx,dword ptr [bp-10]
         and   	edx,[bp-6]                          	; �������� �� 0
         jnz      l20

         pop    	es
         add    	sp,10
         mov  	ax,[bp+8]
         and   	ax,0fh
         cli
         pop   	bp
         ret    	6
   Vcount endp

;=====
show	proc	near
         push 	 bp
         mov  	bp,sp
         pusha
         push  	es
         mov  	ax,0b800h
         mov   	es,ax

         std
ls20:
         mov  	di,[bp+4]
         mov  	bx,[bp+6]
         mov  	cx,4
         mov  	ah,0ah
ls40:
         mov  	al,bl
         and   	al,00001111b
         cmp  	al,10
         jl      	ls100
         add   	al,7
ls100:
         add   	al,30h
         stosw
         shr    	bx,4
         loop  	ls40

         pop   	es
         popa
         pop   	bp
         ret    	4
  show  endp
;------------------------------------------------------------
;------------------------------------------------------------
;------------------------------------------------------------
begin:
                	mov 	ax,_data
                	mov 	ds,ax

                	mov 	ax,3                         	; ������ ��������� ����� 80 �� 25
                	int   	10h

                	mov  	ah,10h                     	; ��������� ����� ��������
                	mov  	al,3
                	mov  	bl,0
                	int    	10h

                	mov  	cx,max_prg
                	xor   	esi,esi
                	mov  	bx,4
b10:
 		mov  	screen_addr[esi*2],bx       	; ���������� �������
               	                                              		; ����� ������ ��� �����
 	 	mov 	init[si],ready 		; �������������� ����������
                 	                                            		; ������� ����� �����
 		add  	bx,80
                	inc   	si
                	loop 	b10
;SETINT
	cli					; �������� ����������
	
	mov		ah,34h
	int		21h		;es:bx - ������ ������ ��������� MS-DOS
	mov		word ptr @ms_dos_busy,bx
	mov		word ptr @ms_dos_busy+2,es

	call 		setint8		;"������������" int8
	call		setint9		;"������������" int9

	lss    		sp,sssp		; ���� ������ ������
	mov 		nprg,fon
	push		'FN'
      	push		1800
      	push		30
          call  		Vcount    	; ������ ������ ������
					; � �������� Vcount �������������� �����
					;�� ����������� � ��� �������� ������������
					; �� ������� �������� ������ (userint8) 
					; ���� ��������� ���� ������
						;
; ��������� � �� ����� ���� �������� �� ������ RET �� ��������� ������ ; ������, � �� ������� ���� ���� ���������� ����� �����

          	call   		retint8		; ���������� ��������� ������� 
      	call   		retint9
	sti
      	mov 		ax,4c00h
     	int  		21h
 _TEXT         ENDS

                end  begin
