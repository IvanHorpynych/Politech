   unit usermode;
interface 
const K: set of char=['b','c','d','f','g','h','j','k','l','m','n','p','q','r','s','t','v','x','z'];
type mnojyna=set of byte;
var  a,b,c,d,e,x,h:mnojyna;
     S: string;
     Y: set of char;
procedure formyla_1(a,b,c,d,e: mnojyna;var x:mnojyna); 
procedure formyla_2(a,b,c,d,e: mnojyna;var x:mnojyna);
procedure vivod_mnojyny(f:mnojyna);
procedure findsimbols(r:string; var P:set of char); 

implementation 
   procedure formyla_1(a,b,c,d,e:mnojyna; var x:mnojyna); 
     begin
     x:=(a-(b*c*d*e))+((b*c*d*e)-a);
     end; 
   procedure formyla_2(a,b,c,d,e:mnojyna;var x:mnojyna); 
    begin
     x:=(a*b*c)-d-e;
    end; 
   procedure vivod_mnojyny(f :mnojyna);
   var i:integer;
   begin
    for i:=0 to 255 do
     if i in x then write(i:3);
    end; 
   procedure findsimbols(r:string; var P:set of char);
   var i: byte;
   begin
   i:=1;
   while (r[i]<>'.') and (i<>length(r)) do
    begin
    if r[i] in P then
    P:=P-[r[i]];
    i:=i+1;
    end; 
   end;
end. 
       		
