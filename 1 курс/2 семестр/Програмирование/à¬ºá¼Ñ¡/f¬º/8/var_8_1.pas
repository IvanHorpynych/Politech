program Var_8_1;

type
	rec = record
		name : string;
		surname : string;
		work : string;
		HouseTel : integer;
		WorkTel : integer;	
	end;
	a = array[1..3] of rec;
const 
	b : a =((name:'Andrey';surname:'Chernysh';work:'Student';HouseTel:57911;WorkTel:0663586109),
			(name:'Alexander';surname:'Grek';work:'Programmer';HouseTel:32455;WorkTel:0638532450),
			(name:'Julius';surname:'';work:'Cesar';HouseTel:666;WorkTel:0674960344));
			
var 
	i : integer;
	name:string;
	surname:string;
begin
	writeln('Welcome to our DATABASE':20);
	write('Enter name:');
	readln(name);
	writeln('Enter surname');
	readln(surname);
	for i := 1 to 3 do begin
		if (b[i].name = name) and (b[i].surname = surname) then  begin
			writeln('HouseTel : ',b[i].HouseTel);
			writeln('WorkTel : ',b[i].WorkTel);
		end;
	end;
readln;
end.
