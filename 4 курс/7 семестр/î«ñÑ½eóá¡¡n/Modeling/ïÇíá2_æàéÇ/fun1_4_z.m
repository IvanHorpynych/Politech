function df = fun1_4_z(x, f)
df = zeros(2,1);
df(1) = cos(f(1) + 2 * f(2)) + 2;
df(2) = 2 / (x + 2 * f(1)^2) + x + 1;
end