function df = fun1_6(x, f)
df = zeros(2, 1);
df(1) = f(1);
df(2) = - f(1)/x + f(2)/(x*x) + 1;
end