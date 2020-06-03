SELECT  t.acc6, t.naim naim6, t.ACC5, (SELECT naim  FROM nsiaccount5 a  WHERE a.acc5=t.acc5) as naim5,
        substr(t.ACC6,1,4) as acc4, (SELECT naim    FROM nsiaccount4 b  WHERE b.acc4=substr(t.ACC6,1,4)) as naim4,
        substr(t.ACC6,1,3) as acc3, (SELECT naim    FROM nsiaccount3 c  WHERE c.acc3=substr(t.ACC6,1,3)) as naim3,
        substr(t.ACC6,1,2) as acc2, (SELECT naim    FROM nsiaccount2 d  WHERE d.acc2=substr(t.ACC6,1,2)) as naim2, 
        (CASE (SELECT prz   FROM nsiaccount2    d WHERE d.acc2=substr(t.ACC6,1,2))
             WHEN 'АП' THEN 'Коментар для АП'
             WHEN 'А' THEN 'Коментар для А'
        END) as prz,
        substr(t.ACC6,1,1) as acc1, (SELECT naim    FROM nsiaccount1 e  WHERE e.acc1=substr(t.ACC6,1,1)) as naim1 
        FROM nsiaccount6 t WHERE t.ACC6=361401       
