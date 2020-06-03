% function df = diffr(x, f, K, Alpha, Epsilon)
% df = zeros(3, 1);
% df(1) = K + f(3) * (x - Alpha * f(2));
% df(2) = f(3) * (Alpha * x - Epsilon * f(2));
% df(3) = 1 - pow(x, 2) - pow(f(2), 2);
% end

function df = diffr(x, f)
df = zeros(3, 1);
df(1) = 5 + f(3) * (x - 3 * f(2));
df(2) = f(3) * (3 * x - 2 * f(2));
df(3) = 1 - x ^ 2 - f(2)^2;
end