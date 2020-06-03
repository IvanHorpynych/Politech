WITH temptable AS (SELECT t.acc, t.naim, x.idtabl, NVL(x.short, x.naim) as table_name, t.acc_parent, x.naim as table_naim FROM
  (SELECT '' as acc, 'План рахунків' as naim, '' as tabl, '' as acc_parent FROM DUAL d1
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
  LEFT JOIN (SELECT idtabl, naim, short, tabl FROM list_table_ac) x ON x.tabl=t.tabl)
  
SELECT t.acc, t.naim, t.idtabl, t.table_name, tt.idtabl as parent_table_id, tt.table_naim as parent_table_name FROM temptable t
  LEFT JOIN (SELECT idtabl, acc, table_naim FROM temptable) tt ON tt.acc=t.acc_parent