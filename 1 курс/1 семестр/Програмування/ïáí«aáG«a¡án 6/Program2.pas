program LABO6;
type mas= array [1..255] of byte;
var
  A:string;
  B: mas;
  i,j:byte;
procedure findSimbols(var B:mas);
begin
  j:=1; i:=1; b[1]:=0;
  while  i<=length(A) do 
  begin
    if A[i]=' ' then
      begin
      j:=j+1;
      B[j]:=i;
      j:=j+1;
      B[j]:=i;
      while (i<length(A)) and (a[i+1]=' ') do 
        begin
        i:=i+1;
        b[j]:=i; 
        end;
      end;
      i:=i+1;
  end;
B[j+1]:=length(A)+1;  
end;

procedure Edit(var A:string);
var i,j,k:integer;
begin   
for i:=1 to (127) do  
  for j:=(B[2*i-1]+1) to (B[2*i]-1) do
    if A[j]='+' then
      for k:=j+1 to (B[2*i]-1) do
        if (ord(A[k])>=48) and (ord(A[k])<=57) then
          A[k]:='-';
end;

begin
write('Vvedite text:');
readln(A);
writeln;
findSimbols(B);
Edit(A);
write('Izmen.  text:');
write(A);
readln;
end.