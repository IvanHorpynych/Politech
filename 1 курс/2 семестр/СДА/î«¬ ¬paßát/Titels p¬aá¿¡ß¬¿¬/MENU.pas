Unit MENU;

interface {Описання доступної інформації для їнших модулів}

procedure Menu_1;

implementation {Описання прихованої інформації від інших модулів}

uses crt,GLOBAL,WORKAR,TIMER,TEST;

procedure Menu_1;
var ch, ch1:char; {Змінні, необхідні для роботи процедури Menu_1}
    T:integer;  {Змінна, яка передає час виконання обраного метода обходу}
begin
 repeat {Цикл повернення головного меню на початкову сторінку}
  clrscr;
  gotoxy(35,12);
  Writeln('Pressed:');
  gotoxy(35,13);
  Writeln('1 - Sort array');
  gotoxy(35,14);
  Writeln('2 - Unsort array');
  gotoxy(35,15);
  Writeln('3 - Backsort array');
  gotoxy(35,16);
  Writeln('4 - Packed-mode launch');
  gotoxy(35,17);
  Writeln('5 - Average packed-mode launch');
  gotoxy(35,18);
  Writeln('Esc - Exit');
  ch:=readkey;
  case ch of {Вибір методу заповнення масиву}
   '1': SortArray(A);
   '2': UnSortArray(A);
   '3': BackSortArray(A);
   '4': begin{Вибір методу пакетного запуску}
		Main_2(A,V,C);{Прохід по алгоритмах по одному разу}
		exit;
		end;
    '5': begin
    Main_1(A,V,C);{Прохід по алгоритмах з пошуком середнього значення}
    exit;
    end;
   #27: halt; {При натисканні клавіші Esc программа завершиться}
   end;

 if (ch='1')or(ch='2')or(ch='3') then {Вибір методу обходу}
  clrscr;
  gotoxy(35,12);
  Writeln('Pressed:');
  gotoxy(35,13);
  Writeln('1 - Bypass 1 (With additional array)'); {Обхід з використанням додаткового масиву}
  gotoxy(35,14);
  Writeln('2 - Bypass 2 (With indexes imaginary vector)'); {Обхід з використанням індексів уявного масиву}
  gotoxy(35,15);
  Writeln('3 - Bypass 3 (Around it elements of array)'); {Обхід безпосередньо по елементах масиву}
  gotoxy(35,16);
  Writeln('Esc - Back');
  ch:=readkey;
  case ch of
    '1': begin {Безпосердній виклик виконання обраного обходу}
          clrscr;
          Writeln('Press Space if you want to show array before sorting'); 
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід первозданного заданого масиву(на вибір)}
           begin
            ShowArray(A); {Безпосередній вивід первозданного заданого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1711; {Змінній процедурного типу присвоюється значення процедури обраного методу обходу; означення чисел: 17_ _ : номер алгоритму за методичними вказівками; _ 1 _ номер задачі за методичними вказівками; _ _ 1: номер метода обходу за методичними вказівками}
          Writeln('Please wait...');
          T:=Time(pr); {Присвоєння часу виконання алгоритму за заданим видом оходу}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід відсортованого масиву(на вибір)}
           begin
            ShowArray(A);{Безпосередній вивід відсортованого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '2':  begin {Безпосердній виклик виконання обраного обходу}
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід первозданного заданого масиву(на вибір)}
           begin
            ShowArray(A); {Безпосередній вивід первозданного заданого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1712; {Змінній процедурного типу присвоюється значення процедури обраного методу обходу; означення чисел: 17_ _ : номер алгоритму за методичними вказівками; _ 1 _ номер задачі за методичними вказівками; _ _ 1: номер метода обходу за методичними вказівками}
          Writeln('Please wait...');
          T:=Time(pr); {Присвоєння часу виконання алгоритму за заданим видом оходу}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід відсортованого масиву(на вибір)}
           begin
            ShowArray(A); {Безпосередній вивід відсортованого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    '3': begin {Безпосердній виклик виконання обраного обходу}
          clrscr;
          Writeln('Press Space if you want to show array before sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід первозданного заданого масиву(на вибір)}
           begin
            ShowArray(A); {Безпосередній вивід первозданного заданого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;

          pr:=WORKAR1713; {Змінній процедурного типу присвоюється значення процедури обраного методу обходу; означення чисел: 17_ _ : номер алгоритму за методичними вказівками; _ 1 _ номер задачі за методичними вказівками; _ _ 1: номер метода обходу за методичними вказівками}
          Writeln('Please wait...');
          T:=Time(pr); {Присвоєння часу виконання алгоритму за заданим видом оходу}
          clrscr;
          Writeln('Press Space if you want to show array after sorting');
          Writeln('Press Enter for continue without showing an array');
          ch1:=readkey;
          if ch1=#32 then {Вивід відсортованого масиву(на вибір)}
           begin
            ShowArray(A); {Безпосередній вивід відсортованого масиву}
            Writeln('Press Enter for continue');
            readkey;
           end;
         end;
    #27: exit; {Вихід з процедури Menu_1}
   end;{case}
   if ch<>#27 then
    begin
     clrscr;
     gotoxy(40,12);
     Writeln('Algorithm time - ',T); {Вивід часу виконання заданого алгоритму}
     gotoxy(40,13);
     Writeln('Press Enter for continue');
     readkey;
    end;
 until (ch=#27); {Цикл повернення головного меню на початкову сторінку, доки не буде натиснута Esc}
end;
end.