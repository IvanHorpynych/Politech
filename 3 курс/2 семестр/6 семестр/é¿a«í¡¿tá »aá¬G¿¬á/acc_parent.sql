SELECT t.acc, t.naim, x.idtabl, NVL(x.short, x.naim) as tabl_name/*, y.idtabl as idtable_parent/*, /*y.naim as name_table_parent/*, t.table_parent*/ FROM
  (SELECT '' as acc, 'План рахунків' as naim, '' as tabl, '' as table_parent FROM DUAL d1
         UNION 
  SELECT a.acc1, a.naim, 'NSIACCOUNT1' as tabl, '' as acc_parent FROM nsiaccount1 a
         UNION
  SELECT b.acc2, b.naim, 'NSIACCOUNT2' as tabl, b.acc1 as acc_parent FROM nsiaccount2 b
         UNION 
  SELECT c.acc3, c.naim, 'NSIACCOUNT3' as tabl, c.acc2 as acc_parent FROM nsiaccount3 c
         UNION 
  SELECT d.acc4, d.naim, 'NSIACCOUNT4' as tabl, d.acc3 as acc_parent FROM nsiaccount4 d
         UNION 
  SELECT e.acc5, e.naim, 'NSIACCOUNT5' as tabl, e.acc4 as acc_parent FROM nsiaccount5 e
         UNION 
  SELECT f.acc6, f.naim, 'NSIACCOUNT6' as tabl, f.acc5 as acc_parent FROM nsiaccount6 f) t
  LEFT JOIN (SELECT idtabl, naim, short, tabl FROM list_table_ac) x ON x.tabl=t.tabl
--  LEFT JOIN (SELECT idtabl, naim, short, tabl FROM list_table_ac) y ON y.tabl=t.tabl
