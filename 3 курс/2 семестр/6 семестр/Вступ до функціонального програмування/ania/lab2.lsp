( defun ret_lst_without_last ( lst )
    ( if ( NULL ( cdr lst ) )
	     nil
		 ( cons ( car lst ) ( ret_lst_without_last ( cdr lst )))))

( defun rotate ( lst num )

    ( if  ( eq num 0 )
     	lst
        ( cond
        ( ( > num 0 ) ( rotate ( append ( cdr lst ) (list ( car lst ))) ( - num 1 )))
        ( ( < num 0 ) ( rotate ( append ( last lst ) ( ret_lst_without_last lst )) ( + num 1 )))))) 

( print  "rotate" )		
( print ( rotate '( a b c d e f g h ) 2  ) )
( print ( rotate '( a b c d e f g h ) -3  ) )		

( defun mul_nums ( part_lst num )
    ( if ( eq num 1 )
	     part_lst
		 (append part_lst ( mul_nums part_lst ( - num 1 )))))

;( print ( mul_nums '( a ) 4 ))		 

( defun dups ( lst num )
    ( if ( NULL lst )
	      nil
		  ( append ( mul_nums ( list ( car lst )) num ) ( dups ( cdr lst ) num ))))

( print "dups")		  
( print ( dups '( a a b c d ) 2 ))		  

( defun insert-at ( el lst pos )
    ( if ( > pos ( length lst ))
         lst
         ( cond
         ( ( eq pos 1 ) ( cons el lst ))
         ( t ( cons ( car lst ) ( insert-at  el ( cdr lst ) ( - pos 1 )))))))

( print "insert-at")		 
( print ( insert-at 4 '( a b c d e ) 5 ))

( defun encode ( lst &optional prev ( num 1 ) )
    ( cond
	( ( NULL lst ) ( list ( list prev num ))  )
	( ( NULL prev ) ( encode ( cdr lst ) ( car lst ) 1 ))
	( ( equal ( car lst ) prev ) ( encode ( cdr lst ) prev ( + num 1 )))
	( t ( cons ( list prev num ) ( encode ( cdr lst ) ( car lst ) 1 )))))

( print "encode")	
( print ( encode '( 1 2 2 2 3 3 2 3 )))

( defun decode ( lst )
    ( cond 
    ( ( NULL lst ) nil )
	( t ( append ( mul_nums ( list ( caar lst )) ( cadar lst )) ( decode ( cdr lst ))))))
	
( print "decode")
( print ( decode '( ( 1 2 ) ( 2 1 ) ( 4 4 ))))	
		 

( defun _intersection ( lst1 lst2 )
    ( if ( NULL lst1 )
	     nil
		 ( cond
		 ( ( find ( car lst1 ) lst2 ) ( cons ( car lst1 ) ( intersection ( cdr lst1 ) lst2 )))
         ( t ( intersection ( cdr lst1 ) lst2 )))))		 
( print "intersection")
( print ( _intersection  '( 1 2 3 4 5 ) '( 2 3 6 7 )))		 

( defun _rest ( Xs Y Zs &optional ( mode 0 ))
	( if ( eq mode 0 )
	    ( cond
		( ( NULL Xs ) nil )
		( ( equal ( car Xs ) Y )  ( _rest ( cdr Xs ) Y Zs  1 ))
		( t ( _rest ( cdr Xs ) Y Zs )))
		
		( cond
		( ( and ( NULL Xs ) ( NULL Zs )) t )
		( ( equal ( car Xs ) ( car Zs )) ( and t ( _rest ( cdr Xs ) Y ( cdr Zs ) 1 )))
        ( t nil))))
		
( print "rest")
( print ( _rest '( 1 2 3 4 ) 3 '( 4  ) ))

( defun deepest_sublst ( lst )
    ( if ( NULL ( cdr lst ))
	     lst
		 (deepest_sublst ( car ( cdr lst )))))
		 
( print "deepest_sublst")		 
( print ( deepest_sublst  '( a ( v ( c ( d (5)))))))

			 





			 
		     
