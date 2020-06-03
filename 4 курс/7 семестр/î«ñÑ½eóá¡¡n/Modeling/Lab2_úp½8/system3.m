function df = system3(x, f)
    % f(1) is y(x)
    % f(2) is z(x)
    df = zeros(2, 1);
    df(1) = cos(f(1) + 2 * f(2)) + 2
    df(2) = 2 / (x + 2 * f(1)^2) + x + 1
end
