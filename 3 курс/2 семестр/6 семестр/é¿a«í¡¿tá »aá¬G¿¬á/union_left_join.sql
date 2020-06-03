SELECT t.acc, t.naim, x.idtabl, NVL(x.short, x.naim) as tabl_name, y.idtabl as idtable_parent, t.table_parent FROM
  (SELECT ' ' as acc, 'План рахунків' as naim, ' ' as tabl, ' ' as table_parent FROM DUAL d1
         UNION 
  SELECT a.acc1, a.naim, 'NSIACCOUNT1' as tabl, ' ' as table_parent FROM nsiaccount1 a
         UNION
  SELECT b.acc2, b.naim, 'NSIACCOUNT2' as tabl, 'NSIACCOUNT1' as table_parent FROM nsiaccount2 b
         UNION 
  SELECT c.acc3, c.naim, 'NSIACCOUNT3' as tabl, 'NSIACCOUNT2' as table_parent FROM nsiaccount3 c
         UNION 
  SELECT d.acc4, d.naim, 'NSIACCOUNT4' as tabl, 'NSIACCOUNT3' as table_parent FROM nsiaccount4 d
         UNION 
  SELECT e.acc5, e.naim, 'NSIACCOUNT5' as tabl, 'NSIACCOUNT4' as table_parent FROM nsiaccount5 e
         UNION 
  SELECT f.acc6, f.naim, 'NSIACCOUNT6' as tabl, 'NSIACCOUNT5' as table_parent FROM nsiaccount6 f) t
  LEFT JOIN (SELECT idtabl, naim, short, tabl FROM list_table_ac) x ON x.tabl=t.tabl
  LEFT JOIN (SELECT idtabl, tabl FROM list_table_ac) y ON y.tabl=t.table_parent
