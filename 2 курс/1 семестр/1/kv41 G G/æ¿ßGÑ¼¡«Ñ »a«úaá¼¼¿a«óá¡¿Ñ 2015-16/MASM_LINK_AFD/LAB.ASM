data          SEGMENT  byte
 val1         db       3
 val2         db       2
 result       db       ?
data          ENDS

code          SEGMENT
              ASSUME   cs:code,ds:data
               ; assume информирует ассемблер о СОДЕРЖИМОМ
               ; сегментных регистров
begin:
              mov      ax,data        ; адрес сегмента данных
              mov      ds,ax          ; засылается в регистр DS
     
              mov      al,val1        ; загрузка регистра al содержимым
                                      ; ячейки ппамяти по адресу val1

              add      al,val2        ; сложение содержимого регистра al
                                      ; c содержимым ячейки памяти по аадресу
                                      ; val2, результат засылается в регистр al
 
              mov      result,al      ; пересылка результата из регистра al 
                                      ; в ячейку памяти по адресу result
mov si, offset val2
push si

push bp
mov bp,sp
lds bx,[bp+2]
mov ax,data
mov ds,ax
lea bx, [bp+2]
mov  ax, [bx]
mov word ptr [bx],7
              nop                     ; команда "нет операции"
; место останова для отладчика
              nop         
              nop
              mov      ax,4c00h       ; выход в MS-DOS
              int      21h

code          ENDS
              end      begin

