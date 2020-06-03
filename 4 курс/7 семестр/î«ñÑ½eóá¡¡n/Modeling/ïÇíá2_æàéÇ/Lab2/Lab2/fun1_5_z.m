% function df = fun1_5_z(x, f)
% df = zeros(2, 1);
% df(2) = 2 * f(1)^2 + f(2);
% df(1) = exp(-(x^2) + (f(2)^2)) + 2 * x;
% end

function dy = fun1_5_z(x, y, z)
%df = zeros(2, 1);
dz = 2 * y^2 + z;
dy = exp(-(x^2) + (z^2)) + 2 * x;
end