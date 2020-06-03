function df = fun1_2_z(x, f)
df = zeros(2, 1);
df(1) = f(2) / x;
df(2) = (2 * (f(2) ^ 2))/(x * (f(1) - 1)) + f(2) / x;
end