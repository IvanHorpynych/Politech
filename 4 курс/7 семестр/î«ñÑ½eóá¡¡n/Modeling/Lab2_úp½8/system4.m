function df = system4(x, f)
    % f(1) is y(x)
    % f(2) is z(x)
    df = zeros(2, 1);
    df(1) = exp(-(x^2 + f(2)^2)) + 2 * x
    df(2) = 2 * f(1)^2 + f(2)
end
