; x:=a+b-c

data segment
a	db	5
b	db	4
c	db	2
x	db	?
data	ends

code	segment
	assume  cs:code,ds:data

     begin:
	     mov ax, data
             mov ds, ax
             mov al, a    ;(al):=a
             add al, b    ;(al)+b-->al
             sub al, c    ;(al)-c-->al
             mov x,  al   ;x:=(al)

             mov ax, 4c00h
             int 21h
     code ends
          end  begin