unit menu;
interface
	procedure MenuProc;
implementation
	uses crt, tables, algos;
	
	procedure MenuProc;
	var i:word;
		maxes, sizes:array [1..4] of word;
		names:array [1..4] of string [4];
		sel:char;
	begin
		clrscr;
		DrawMenuBar;
		GotoXY(1, 9);
		maxes[1]:=MAXP; maxes[2]:=MAXM;
		maxes[3]:=MAXN; maxes[4]:=MAXLEN;
		names[1]:='P'; names[2]:='M';
		names[3]:='N'; names[4]:='Len';
		sel:='0';
		repeat
			if (sel >= '3') and (sel <= '9') then begin
				writeln;
				DrawMenuBar;
			end;
         write('Ваш вибiр: ');
			sel:=readkey;
			writeln(sel); DrawMenuBar;
			case sel of
			'1': begin
               writeln('  ***** Налаштування розмiрiв *****'); DrawMenuBar;
					for i:=1 to 4 do repeat
						write(names[i], '='); read(sizes[i]);
						DrawMenuBar;
						if sizes[i] > maxes[i] then begin
                     writeln('Занадто велике значення!');
							DrawMenuBar;
						end;
					until sizes[i] <= maxes[i];
					writeln;
					SetSizes(sizes[1], sizes[2], sizes[3], sizes[4]);
				end;
			'2': begin
               writeln('  ***** Вибiр алгоритму обходу  *****'); DrawMenuBar;
					for i:=1 to 3 do writeln(i, '-', algoTypes[i]);
					DrawMenuBar;
					repeat
						sel:=readkey;
						if not ((sel >= '1') and (sel <= '3')) then begin
                     writeln('Неправильне значення!');
							DrawMenuBar;
						end;
					until ((sel >= '1') and (sel <= '3'));
					SetAlgo(ord(sel)-ord('0'));
          sel:='3';
				end;
			'3': SetSortingType(1);
			'4': SetSortingType(2);
			'5': SetSortingType(3);
			'6': DrawTest;
			'7': DrawLTest;
			'8': DrawTable;
			'9': DrawLTable;
			'0': begin
					repeat
                  write('Вийти? (y,n) '); sel:=readkey;
						writeln(sel); DrawMenuBar;
						if (sel <> 'y') and (sel <> 'n') then begin
                     writeln('Лише ''y'' або ''n''!');
							DrawMenuBar;
						end;
					until not ((sel <> 'y') and (sel <> 'n'));
					writeln; DrawMenuBar;
				end;
			else begin
          writeln('Неправильне значення!');
          sel:='3';
					DrawMenuBar;
				end;
			end;
		until sel='y';
	end;

	begin 	end.
