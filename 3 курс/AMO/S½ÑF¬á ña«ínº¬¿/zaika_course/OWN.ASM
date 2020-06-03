data segment
val1    db 'zaika'
val2    db 1010b
val3    dw 1234
val4    dd 056FDAH
val5    db -11b
val6    dw -32d
val7    dd -0abcdh
data ends

assume cs:code, ds:data
code segment
begin:
clc ;1

not val1[eax+edx*2];2
not val4[esi+edi*4];2
not cs:val1[eax+edx*2];2

jmp	begin
jmp fin

push ecx ;3


jbe fin

mov ebx, val7[eax+edx*2];4
mov ah, val5[esi+edi*4];4

cmp  val4[eax+edx*2], ebx;5
cmp  val2[esi+edi*4],ah ;5


jbe begin;6

fin:
code ends

code2 segment
assume CS:code2

clc;1

not val1[eax+edx*2];2
not val4[esi+edi*4];2
not cs:val1[eax+edx*2];2
jmp go
go:

push ecx;3

mov ebx, val7[eax+edx*2];4
mov ah, val5[esi+edi*4];4

cmp  val4[eax+edx*2], ebx;5
cmp  val2[esi+edi*4],ah ;5

code2 ends
end


