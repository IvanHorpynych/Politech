function df = system2(x, f)
    % f(1) is y(x)
    % f(2) is z(x)
	df = zeros(2, 1);
    df(1) = (f(2) - f(1)) * x
    df(2) = (f(2) + f(1)) * x
end
