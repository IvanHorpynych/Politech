(setf db nil)

(defun print_db ( _db )
    ( if ( NULL _db )
	    nil
	   ( prog1 
        ( format t "~A: ~A ~%" (caar _db ) ( cadar _db ))
		(print_db (cdr _db )))))
		
(defun check ( _key &optional _db )					;checking whether element is in db
    ( or
        (equal _key ( caar _db ))
		( if ( NULL _db )
		     nil
            (check _key ( cdr _db )))))		

;( setf db '( ( "hello" "world" ) ( "btr" "jnui")))
;(print ( check "btr" db ))	
	
(defun add_to_db ( _key _value )
    ( if ( NULL ( check _key db ))
        ( setf db ( cons ( list _key _value ) db ))))
		
( defun del_node ( _name &optional (_db db))
 	( cond
	(( NULL _db ) nil )
    ((equal _name  ( caar _db )) (del_node _name ( cdr _db )))
	( t ( cons ( car _db ) (del_node _name ( cdr _db ))))))

(defun fnd_by ( val fun &optional ( _db db ))
    ( cond
	( ( NULL _db ) (print nil) )
	( (equal val ( funcall fun _db )) (print ( car _db )))
	( t (fnd_by val fun ( cdr _db )))))
	
(defun template_name ( tmp _name )										;compares template with usual string
    (cond
	( ( and ( NULL tmp ) ( NULL _name )) t)
    ( ( or ( NULL tmp ) ( NULL _name )) nil )
	( ( equal ( car tmp ) ( car _name )) (template_name ( cdr tmp ) ( cdr _name )))
	( ( eq ( car tmp ) #\* ) ( if ( equal ( cadr tmp ) ( cadr _name ))
                                   	(template_name ( cdr tmp ) ( cdr _name )) 
	                                (template_name tmp ( cdr _name ))))
	( t nil )))

;( print (template_name ( coerce "hel" 'list ) ( coerce "hello" 'list )))	

(defun print_by_name ( _name &optional ( _db db ))
	( cond
	( ( NULL _db ) nil)
	( (template_name ( coerce _name 'list) (coerce ( caar _db ) 'list)) ( print ( car _db )) ( print_by_name _name ( cdr _db )))
	( t ( print_by_name _name ( cdr _db )))))
	
	;( print_by_name "he*")

(defun sep_seq ( line &optional ( mode 0 ) ( a '(nil) ) ( b '(nil) ) )				;seperate sequence from file: name_phone 
;(print line)
    ( cond
	(( or (NULL line) ( equal ( car line ) #\Return )) ( list ( coerce a 'string) (coerce b 'string)))
	( ( equal ( car line ) #\_ ) ( sep_seq ( cddr line ) 1 a ( list ( cadr line ) )))
    ( ( and ( eq mode 0 ) ( not ( equal ( car line ) #\_ )))  ( sep_seq ( cdr line ) mode ( append a ( list ( car line ))) ))
  	( ( eq mode 1 )  ( sep_seq ( cdr line ) mode a ( append b ( list ( car line ))) ))
	))
	
(defun fill_db ()
(let ((in (open "c:/1/1.txt" :if-does-not-exist nil)))
  (when in
    (loop for line = (read-line in nil)
         while line do ( setf db ( cons  (sep_seq ( cdr ( coerce line 'list)) 0 ( list ( car ( coerce line 'list)))) db )))
    (close in))))
 	
( defun save_db ()
( let ( ( _db db ) (out (open "c:/1/1.txt" :direction :output :if-exists :supersede)))
    ( loop while ( not ( NULL _db )) do
	    ( let (( lst (coerce ( caar _db ) 'list )))
		    ( setq lst ( append lst '(#\_) ))
			( setq lst ( append lst (coerce (cadar _db ) 'list )))
			(write-line ( coerce lst 'string ) out )
			(setq _db ( cdr _db ))
		 ))
	(close out)))

(defun seperate_line ( line &optional ( el nil ) ( lst nil ))
    (cond
	(( NULL line ) (append lst (list (coerce el 'string) )))
	((equal (car line) #\Space ) (seperate_line (cdr line) nil (append lst (list (coerce el 'string)))))
	(t (seperate_line (cdr line) (append el (list (car line))) lst))))
	
(defun shell ()
	(let (( str nil ))
	    (loop while ( not ( equal str "exit")) do
		( let* (( _cmd (seperate_line ( coerce (read-line) 'list))) ( str ( car _cmd)))
            (cond
			((equal str "exit") (return-from shell))
			((equal str "load") (fill_db))
			((equal str "save") (save_db))
			((equal str "add" ) ( add_to_db (cadr _cmd) ( caddr _cmd))(print_db db)) ;add key value
			((equal str "delete") (setf db ( del_node (cadr _cmd ))) (print_db db))             ;delete key 
			((and (equal str "list") (equal ( cadr _cmd ) "by")) (print ( print_by_name (caddr _cmd ))))  ;list by *templlate*
			((and (equal str "find") (equal (caddr _cmd) "name")) (fnd_by (cadddr _cmd) #'caar ))   ;find by name _name
			((and (equal str "find") (equal (caddr _cmd) "phone")) (fnd_by (cadddr _cmd) #'cadar )) ;find by phone _phone
			((equal str "list") ( print_db db))
			(t (print "unknown command" )))))))
(setf db nil)
(shell)	 

  