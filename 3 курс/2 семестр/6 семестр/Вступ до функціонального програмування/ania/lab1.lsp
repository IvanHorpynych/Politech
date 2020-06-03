;additional functions

( defun factorial ( n )
    ( if ( < n 2 )
	     1
		 ( * n ( factorial ( - n 1 )))))

( defun check ( n )
    ( if ( and ( > n  0 ) ( eq ( mod ( + 1 ( factorial ( - n 1 ))) n ) 0 ))
	     T
		 nil ))
		 
( defun pairtwins ( x )
    ( if ( NULL x )
	     nil
    ( cond
    (( eq x 0 ) '( 1 3 ))
    (t ( list ( - ( * 6 x ) 1 ) ( + ( * 6 x ) 1))))))

( defun _exp ( x y )
    ( if ( = y 0 )
         1
         ( * x ( _exp x ( - y 1 )))))		 


;creates list of simple numbers with help of 		 
( defun make_pairs ( len &optional ( x 0 ) ( prev 1 ))
    ( if ( > x len )
         nil 
         ( let (( simp ( pairtwins x )))
            ( cond
               (( and ( check ( car simp )) ( check ( cadr simp ))) ( cons ( car simp ) ( cons ( cadr simp ) ( make_pairs len ( 1+ x ) ( cadr simp )))))
               (( check ( car simp )) ( cons ( car simp ) ( make_pairs len ( 1+ x ) ( car simp ))))
               (( check ( cadr simp )) ( cons ( cadr simp ) ( make_pairs len ( 1+ x ) ( cadr simp ))))
			   ( t ( make_pairs len ( 1+ x ) prev))))))		
;-------
; 1 task	   
( defun mygcd ( lst )
    ( if (NULL ( cdr lst ))
	    ( car lst )
		( gcd ( car lst ) ( mygcd ( cdr lst )))))

( defun pred ( lst )
    ( if ( > ( mygcd lst ) 1 )
          nil
          t ))		  
		
( print "1 task" )
( print ( pred '(  3 5 7 9  )))		
	
;2 task
;--------
( defun prime-factors-mult ( n &optional ( lst ( cdr ( make_pairs ( + 1 ( round ( / n 12 ) ))))))
		( if ( NULL lst )
		    nil
		   ( let (( buf n ) ( count 0) )
		     ( loop while ( = ( mod buf ( car lst ) ) 0 ) do 
		        ( setq buf ( / buf ( car lst) )) 
			    ( setq count ( + count 1 )))
		     ( cond
                 ( ( > count 0 ) ( cons ( list ( car lst ) count ) ( prime-factors-mult n ( cdr lst))))
                 ( t ( prime-factors-mult n ( cdr lst)))))))		
	
( print "2 task" )
;( print ( prime-factors-mult 9009 ))		

;3 task	
( defun phi ( num &optional ( lst (make_pairs ( + 1 ( round ( / num 6 ))))))
	( if ( > ( car lst ) num )     
		 0
		 ( + 1 ( phi num ( cdr lst )))))
 
 ( print "function phi")
 ( print ( phi 10 ))

 ;4 task 
( defun fnd_mul ( l1 l2 )
    ( if ( or ( NULL l1 ) ( NULL l2 ) )
	      0
		  ( + ( * ( car l1 ) ( car l2 )) ( fnd_mul ( cdr l1 ) ( cdr l2 )))))
		  	
( print "4 task")
( print ( fnd_mul '( 1 2 3 ) '( 4 5 6 7)))	 

;5 task		 
( defun goldbach ( n &optional ( lst ( cdr (make_pairs ( + 1 ( round ( / n 6 )))))))
	( if ( NULL lst )
	    nil
		(progn
		     ( if ( check ( - n ( car lst )))
			    ( list ( car lst ) ( - n ( car lst )))
				( goldbach n ( cdr lst ))))))
	    		
	( print "goldbach")			
    ( print (goldbach 124 ))  
	
( defun goldbach-list ( lower upper )
    ( let ( ( it lower ) )
	     ( loop while ( <= it upper ) do
		    ( if ( oddp it )
			    ( setq it ( + it 1 ))
			    (progn 
				    ( print ( list it ( goldbach it )))
				    ( setq it ( + it 2 )))))))					
	
    ( print "goldbach-list")	
	( goldbach-list 1 20 )
	
( defun goldbach-list–limited  ( lower upper limit )
     ( let ( ( it lower ) ( lst ( cdr (make_pairs ( + 1 ( round ( / upper 6 )) )))))
	     ( loop while ( <= it upper ) do
		    ( if ( oddp it )
			    ( setq it ( + it 1 ))
			    (let (( buf ( goldbach it lst )))				
				    ( if ( and ( not ( NULL buf )) ( > ( car buf ) limit ))
					     ( print ( list it buf )))
				    ( setq it ( + it 2 )))))))	
	
( print "goldbach-list-limited")
( goldbach-list–limited 1 2000 50 )
	
( defun phi-lightning ( n &optional ( lst (prime-factors-mult n )))
 	( if ( NULL lst )
	     1
		 ( * ( - ( caar lst ) 1 ) ( _exp ( caar lst ) ( - ( cadar lst ) 1 )) ( phi-lightning n ( cdr lst )))))
	
;6 task
( print "phi-lightning")	
( print ( phi-lightning 10 ))       			 
			 

      			 
        

	 
	     
	    
  
		 
		
 
 
 