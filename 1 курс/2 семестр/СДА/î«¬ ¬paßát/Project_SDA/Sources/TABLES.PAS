unit tables;
interface
	procedure DrawTable;
	procedure DrawLTable;
	procedure DrawTest;
	procedure DrawLTest;
	procedure SetSizes(newP, newM, newN, newLen:word);
	procedure SetAlgo(newAlgo:word);
	procedure SetSortingType(newType:word);
	procedure DrawMenuBar;

    var algoTypes:array [1..3] of string [24];
implementation
	uses crt, algos;
	type ALGOFUNC=function:longint;
		FORMPROC=procedure;
	var linId, id, testId, lTestId, M, N, P, Len, sorting, algo:word;
		algFuncs:array [1..3] of ALGOFUNC;
		formProcs:array[1..3] of FORMPROC;
		busy:boolean;
		menuStrs:array [1..5, 1..4] of string [24];
                var sortTypes:array [1..3] of string [16];
		
	function Bigger(x, y:longint):longint;
	begin
		if x > y then Bigger:=x
		else Bigger:=y;
	end;

	procedure SetSizes(newP, newM, newN, newLen:word);
	begin
		M:=newM;
		N:=newN;
		P:=newP;
		Len:=newLen;
		SetDimensions(P, M, N, Len);
		DrawMenuBar;
	end;
	
	procedure DrawTest;
   var tm:longint;
	begin
		busy:=true;
		DrawMenuBar;
		testId:=testId+1;
		formProcs[sorting];
      writeln('   Тест №', testId,  'для масива A[P, M, N]; P=', P, ' M=', M, ' N=', N, '.');
      writeln('   Алгоритм обходу: ', algoTypes[algo], '. Тип упорядкованстi:', sortTypes[sorting], '.');
      write('Час сортування : ', algFuncs[algo], '.');
		writeln;
		busy:=false;
    algFuncs[algo];
		DrawMenuBar;
	end;

	procedure SetAlgo(newAlgo:word);
	begin
		algo:=newAlgo;
		DrawMenuBar;
	end;
	
	procedure SetSortingType(newType:word);
	begin
		sorting:=newType;
		DrawMenuBar;
	end;
	
	procedure DrawLTest;
	var tm:longint;
	begin
		busy:=true;
		DrawMenuBar;
		lTestId:=lTestId+1;
		formProcs[sorting];
		tm:=SortLinear;
      writeln('   Тест №', lTestId, ' для вектора C[N];  N=', Len, '.');
      writeln('   Алгоритм обходу: ', algoTypes[algo], '. Тип упорядкованстi: ', sortTypes[sorting], '.');
      write('Час сортування: ', tm, '.');
		writeln;
		busy:=false;
    SortLinear;
		DrawMenuBar;
	end;
		
	procedure DrawTable;
	var cellStrs:array [1..4, 1..4] of string [32];
		colWids:array [1..4] of word;
		max, tabWid:word;
		i, j, k:word;
	begin
		busy:=true;
		DrawMenuBar;
		id:=id+1;
      cellStrs[1, 1]:=''; cellStrs[1, 2]:='Вiдсорт.';
      cellStrs[1, 3]:='Невiдсорт.'; cellStrs[1, 4]:='Оберн. вiдсорт.';
      cellStrs[2, 1]:='Дод. масив';
      cellStrs[3, 1]:='Перетв. коорд.';
      cellStrs[4, 1]:='Прямий обхiд';
		for j:=1 to 3 do
			for i:=1 to 3 do begin
            formProcs[j];
            str(longint(algFuncs[i]), cellStrs[i+1, j+1]);
      end;
		tabWid:=0;
		for j:=1 to 4 do begin
			max:=0;
			for i:=1 to 4 do
				if length(cellStrs[i, j]) > max then
					max:=length(cellStrs[i, j]);
			colWids[j]:=max+2;
			tabWid:=tabWid+colWids[j];
		end;
      writeln('   Таблиця №', id, ' для масива A[P, M, N]; P=', P, ' M=', M, ' N=', N, '.');
		tabWid:=tabWid+5;
		for i:=1 to 4 do begin
			for j:=1 to tabWid do write('-');
			writeln; write('|');
			for j:=1 to 4 do begin
				for k:=1 to (colWids[j]-length(cellStrs[i, j])-1) do
					write(' ');
				write(cellStrs[i, j]);
				write(' |');
			end;
			writeln;
		end;
		for i:=1 to tabWid do write('-');
		writeln;
		busy:=false;
		DrawMenuBar;
	end;

	procedure DrawLTable;
	var cellStrs:array [1..2, 1..3] of string [32];
		colWids:array [1..3] of word;
		max, tabWid:word;
		i, j, k:word;
	begin
		busy:=true;
		DrawMenuBar;
		linId:=linId+1;
      cellStrs[1, 1]:='Вiдсорт.';
      cellStrs[1, 3]:='Оберн. вiдсорт.';
      cellStrs[1, 2]:='Невiдсорт.';
		for i:=1 to 3 do begin
			formProcs[i];
			str(SortLinear, cellStrs[2, i]);
		end;
      writeln('   Таблиця №', linId, ' для вектора C[N]; N=', Len, '.');
		tabWid:=0;
		for i:=1 to 3 do begin
			colWids[i]:=Bigger(length(cellStrs[1, i]), length(cellStrs[2, i]))+2;
			tabWid:=tabWid+colWids[i];
		end;
		tabWid:=tabWid+4;
		for i:=1 to 2 do begin
			for j:=1 to tabWid do write('-');
			writeln; write('|');
			for j:=1 to 3 do begin
				for k:=1 to (colWids[j]-length(cellStrs[i, j])-1) do
					write(' ');
				write(cellStrs[i, j], ' |');
			end;
			writeln;
		end;
		for i:=1 to tabWid do write('-');
		writeln;
		busy:=false;
		DrawMenuBar;
	end;

	procedure DrawMenuBar;
	var i, j, k, tabWid, max:word;
		colWids:array [1..4] of word;
    x, y:word;
   begin
      x:=WhereX; y:=WhereY;
      if y < 8 then y:=8;
      for i:=1 to 8 do begin
          GotoXY(1, i);
          for j:=1 to 80 do write(' ');
      end;
      GotoXY(1, 1);
      str(P, menuStrs[1, 3]); menuStrs[1, 3]:='P='+menuStrs[1, 3];
		str(M, menuStrs[2, 3]); menuStrs[2, 3]:='M='+menuStrs[2, 3];
		str(N, menuStrs[3, 3]); menuStrs[3, 3]:='N='+menuStrs[3, 3];
		menuStrs[4, 3]:='';
		str(Len, menuStrs[5, 3]); menuStrs[5, 3]:='Len='+menuStrs[5, 3];
      menuStrs[1, 4]:='Упорядк.: '+sortTypes[sorting];
      menuStrs[2, 4]:='Обхiд: '+algoTypes[algo];
		menuStrs[3, 4]:='';
      if busy = true then menuStrs[4, 4]:='Працюю...'
      else menuStrs[4, 4]:='Вiльний';
      menuStrs[4, 4]:='Статус: '+menuStrs[4, 4];
		menuStrs[5, 4]:='';
                tabWid:=0;
		for j:=1 to 4 do begin
			max:=0;
			for i:=1 to 5 do
				if length(menuStrs[i, j]) > max then
					max:=length(menuStrs[i, j]);
			colWids[j]:=max+2;
			tabWid:=tabWid+colWids[j];
		end;
		tabWid:=tabWid+3;
		for i:=1 to tabWid do write('=');
		writeln;
		for i:=1 to 5 do begin
			write('| ');
			for j:= 1 to 4 do begin
				write(menuStrs[i, j]);
				for k:=1 to (colWids[j]-length(menuStrs[i, j])-1) do
					write(' ');
				if (j=2) or (j=4) then write('|');
				write(' ');
			end;
			writeln;
		end;
		for i:=1 to tabWid do write('=');
    GotoXY(x, y);
	end;

	begin
		algFuncs[1]:=Algo_1;
		algFuncs[2]:=Algo_2;
		algFuncs[3]:=Algo_3;

		formProcs[1]:=FormStraightSorted;
		formProcs[2]:=FormRandom;
		formProcs[3]:=FormBackSorted;
		
      menuStrs[1, 1]:='1-Розмiри';
      menuStrs[2, 1]:='2-Алг. обходу';
      menuStrs[3, 1]:='3-Вiдсорт.';
      menuStrs[4, 1]:='4-Невiдсорт.';
      menuStrs[5, 1]:='5-Оберн. вiдсорт.';
      menuStrs[1, 2]:='6-Тест. масив';
      menuStrs[2, 2]:='7-Тест. вектор';
      menuStrs[3, 2]:='8-Пак. режим (масив)';
      menuStrs[4, 2]:='9-Пак. режим (вектор)';
      menuStrs[5, 2]:='0-Вихiд';
      sortTypes[1]:='Вiдсорт.';
      sortTypes[2]:='Невiдсорт.';
      sortTypes[3]:='Оберн.';
      algoTypes[1]:='Дод. масив';
      algoTypes[2]:='Перет. Коорд.';
      algoTypes[3]:='Прямий обхiд';
		
		id:=0;
		linId:=0;
		testId:=0;
		lTestId:=0;
        busy:=false;
        algo:=1;
        sorting:=2;
        SetSizes(1, 1, 1, 1);
	end.
