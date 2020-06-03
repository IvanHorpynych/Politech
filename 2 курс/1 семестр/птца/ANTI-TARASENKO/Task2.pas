var
  num: integer := 31921;

begin
  write('Your "#19**" :');
  readln(num);
  writeln('G = ', ((num mod 3) + 2) mod 5);
  if (((num mod 3) + 2) mod 5) = 2 then 
    writeln('F(2) = ', ((num mod 3) + 4) mod 7);
  if (((num mod 3) + 2) mod 5) = 3 then   
    writeln('F(3) = ', ((num mod 5) + 3) mod 8);
  if (((num mod 3) + 2) mod 5) = 4 then 
    writeln('F(4) = ', ((num mod 7) + 2) mod 9);
  writeln('H = ', num mod 5);
end.