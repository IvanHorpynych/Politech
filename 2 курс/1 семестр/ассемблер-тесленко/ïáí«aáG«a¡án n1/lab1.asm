.286 
				data segment 
				 base   db  38h 
			     result db  0 
				data ends 
				code segment 
				     assume cs:code, ds:data 
				begin: 
		 mov dx,data 
		 mov ds,dx 
				  
		 mov dh, result 
		 mov dl, base 
				   
	 and dl,00000010b 
		 shl dl,1 
		 or dh,dl 
		 shr dl,2 
		 or dh,dl
		 
		 mov dl, base
		 
	 and dl,00000100b
		 shr dl,2 
		 or dh,dl		  
	     shl dl,5
		 or dh,dl
	     shl dl,2
		 or dh,dl
		
		 mov dl, base
		 
     and dl,00010000b   
		 or dh,dl 
		 shr dl,1 
	     or dh,dl
				  
		 mov dl, base
	 and dl,01000000b
		 or dh,dl

		 mov result,dh 
				 
			 mov dx, 4c00h 
			 int 21h 
				code ends 
				 end begin 

