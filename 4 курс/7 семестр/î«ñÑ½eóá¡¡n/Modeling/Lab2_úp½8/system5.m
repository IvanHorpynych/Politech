function df = system5(x, f)
    % f(1) is y'(x)
    % f(2) is y(x)
    df = zeros(2, 1);
    df(2) = -f(1) / x + f(2) / (x^2) + 1
    df(1) = f(1)
end
