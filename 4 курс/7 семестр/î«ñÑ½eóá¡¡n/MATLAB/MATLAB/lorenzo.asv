%Програма обчислення траекторії атрактора Лоренца 
%довжиною iter через інтервали часу dt
function [x y z]=lorenzo(iter,dt);
x(1)=2.9; y(1)=-1.3; z(1)=25.9;
for i=2:iter
    [x(i) y(i) z(i)]=nextiter(x(i-1),y(i-1),z(i-1),dt);
end
%Власне обчислення наступної точки через похідну.
% Похідна       X' = X(i) - X(i-2)
function [XO YO ZO]=nextiter(X,Y,Z,dt)
    r=28; b=8/3; s=10;
    x1=X+s*(Y-X)*dt/2;   %x1=X(i-1) ; X=X(i-2) 
	y1=Y+(X*(r-Z)-Y)*dt/2;
	z1=Z+(X*Y-b*Z)*dt/2;
	XO=X+s*(y1-x1)*dt;   %XO=X(i)
	YO=Y+(x1*(r-z1)-y1)*dt;
	ZO=Z+(x1*y1-b*z1)*dt;