program ksjdb;
 type 
   person=object
    name,sername:string;
    procedure Inp;
    procedure Outp;
   end; 
   
   procedure person.Inp;
   begin
    Write('Name: ');
    Readln(name);
    Write('Sername: ');
    Readln(sername);
   end;
   
   procedure person.Outp;
   begin
    Writeln(name);    
    Writeln(sername);
   end;
   
 var p:person;
     f:file of object;
     
     
begin
 person.inp;
end.    