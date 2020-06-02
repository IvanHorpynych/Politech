unit GLOBAL;

interface {Описання доступної інформації для їнших модулів}

const
  p = 1;
  m = 10;
  n = 10;

type
  arr = array[1..p, 1..m, 1..n] of integer; {Задання користувацького типу "масив"}
  vector=array [1..n*m] of integer; {Задання користувацького типу "вектор"}
var A:arr; {Змінна типу масив} 
	V:arr; {Змінна типу масив} 
    C:vector; {Змінна типу вектор} 

procedure ShowArray(const a: arr); {Процедура виведення масиву на екран}
procedure SortArray(var a: arr); {Процедура впорядкованного заповнення масиву}
procedure UnSortArray(var a: arr); {Процедура невпорядкованного заповнення масиву}
procedure UnSortCopy(var a: arr); {Процедура копіюванння рандомного масиву}
procedure BackSortArray(var a: arr); {Процедура заповнення обернено впорядкованного масиву}
procedure SortVect(var C: vector); {Процедура впорядкованного заповнення вектора}
procedure UnSortVect(var C: vector); {Процедура невпорядкованного заповнення вектора}
procedure BackSortVect(var C: vector); {Процедура заповнення обернено впорядкованного вектора}
implementation

procedure ShowArray(const a: arr); {Процедура виведення масиву на екран}
var
  i, j, k: word;
begin
  for k := 1 to p do {Лічильники проходу по координатам масиву}
  begin
    for i := 1 to m do
    begin
      for j := 1 to n do
        write(a[k, i, j]:8); {Виведення елементу на екран}
      writeln;
    end;
    writeln;
  end;
end;

procedure UnSortArray(var a: arr); {Процедура невпорядкованного заповнення масиву}
var
  i, j, k: word; {Змінні кординат}
begin
  randomize;
  for k := 1 to p do {Лічильники проходу по координатам масиву}
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := random(p * m * n); {Присвоєння комірці масиву рандомного значення}
end;

procedure UnSortCopy(var a: arr); {Процедура копіюванння рандомного масиву}
  var
  i, j, k: word; {Змінні кординат}
begin
  for k := 1 to p do {Лічильники проходу по координатам масиву}
    for i := 1 to m do
      for j := 1 to n do
        a[k, i, j] := V[k,i,j]; {Присвоєння запам'ятованих рандомних занчень}
end;

procedure SortArray(var a: arr); {Процедура впорядкованного заповнення масиву}
var
  i, j, k: word; {Змінні кординат}
  l: integer; {Змінна, за допомогою якої буде заповнюватися масив}
begin
  l := 1;
  for k := 1 to p do {Лічильники проходу по координатам масиву}
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l; {Присвоєння комірці масиву значення l}
        inc(l); {При кожному проході лічильника, змінна l буде збільшуватися на 1}
      end;
end;

procedure BackSortArray(var a: arr); {Процедура заповнення обернено впорядкованного масиву}
var
  i, j, k: word; {Змінні кординат}
  l: integer; {Змінна, за допомогою якої буде заповнюватися масив}
begin
  l := p * m * n; {Змінній l присвоюється значення кількості елементів масиву}
  for k := 1 to p do {Лічильники проходу по координатам масиву}
    for i := 1 to m do
      for j := 1 to n do
      begin
        a[k, i, j] := l; {Присвоєння комірці масиву значення l}
        dec(l); {При кожному проході лічильника, змінна l буде зменьшуватися на 1}
      end;  
end;

procedure SortVect(var C: vector); {Процедура впорядкованного заповнення вектора}
var
  i,j:integer;
    begin
      for i:=1 to (n*m) do {Лічильник проходу по вектору}
      C[i]:=i; {Заповнення вектора впорядкованими числами}
    end; 

procedure UnSortVect(var C: vector); {Процедура невпорядкованного заповнення вектора}
var
  i,j:integer;
    begin
      j:=n*m;
      for i:=1 to (j) do {Лічильник проходу по вектору}
        C[i]:=random(j); {Присвоєння рандомного значення комірці вектора}
    end;

procedure BackSortVect(var C: vector); {Процедура заповнення обернено впорядкованного вектора}
var
  i,j,g:integer;
    begin
      j:=n*m; g:=j;
      for i:=1 to j do {Лічильник проходу по вектору}
      begin
        C[i]:=g; {Заповнення вектора обернено впорядкованими числами}
        dec(g);
      end;
    end;
end.