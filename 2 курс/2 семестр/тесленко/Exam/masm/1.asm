.386
Data		Segment use16
v1		db 0,2,4h,8h,10h,20h,40h

Data		Ends
		Assume 	ds:Data,cs:Code
Code		Segment use16
Begin:
	Mov		ax,Data
		Mov		ds,ax
		Mov		ecx,6
		Mov		bx,0
		@10:
		mov al,v1[ecx]
		shr al,cl
		add dl,al
		
		loop @10
		;@10:
		;mov al, v1[ecx]
		;xor dx,[bx]
		;add dx,ax
		;shl	dx, cl
		;loop @10
		
Code		ends
		End		begin
