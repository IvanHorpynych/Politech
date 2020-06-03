data          SEGMENT  
 msg         db       13, 10, "Hello, students", 13,10
 data          ENDS

code          SEGMENT
              ASSUME   cs:code,ds:data
               ; assume информирует ассемблер о СОДЕРЖИМОМ
               ; сегментных регистров
begin:
              mov      ax,data        ; адрес сегмента данных
              mov      ds,ax          ; засылается в регистр DS
     
              mov      ah, 9        	; 
              mov dx, offset msg   ; 
              int 21h     		; 
                                      
              nop                     ; команда "нет операции"
; место останова для отладчика
              nop         
              nop
              mov      ax,4c00h       ; выход в MS-DOS
              int      21h

code          ENDS
              end      begin

