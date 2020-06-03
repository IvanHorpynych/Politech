function df = fun1_3_z(x, f)
df=zeros(2, 1);
df(1) = (f(2) + f(1)) * x;
df(2) = (f(2) - f(1)) * x;
end