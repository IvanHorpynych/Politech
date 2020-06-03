.386
data1 segment
   VAL1   db    11010b
   VAL2   dw    15
   VAL3   dd    0abcdh
   STR1    db    'рядок'
   SMTH   equ   [ebx
   NINE   =     178
data1 ends

data2 segment
   VAL4   db    4h
   VAL5   dw    0b9h
data2 ends

code1 segment
assume cs:code1, ds:data1
start:
   sti
   jz    M1
   jmp M1
   push  ecx
   div   dword ptr SMTH+edi]
   xor   dword ptr es:[esp+ebp], 0ah
   adc   eax, ecx
   mov   ecx, ds:[bx+di]
   sub   eax, ds:[bp+ di]
   xor   ds:val1, NINE
   jz    start
   M1:
   jmp   start
   jmp  far ptr start2
code1 ends

code2 segment
assume cs:code2, ds:data1, es:data2
start2:
   push  ecx
   adc   eax, ecx
   NINE = 50
   xor   ds:val1, NINE
jmp far ptr start

code2 ends
end start
