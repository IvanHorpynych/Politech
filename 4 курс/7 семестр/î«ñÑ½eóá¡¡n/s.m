function df = s(t, f)
    % beta < gamma < 0.1
    beta = 0.05;
    gamma = 0.05;
    % 0 < k < 3.
    k = 1;
    % -3 < omega < 3.
    omega = 1;
    % 0 < alpha < 1.5
    alpha = 1.0;
    % 0 < beta < 0.03.

     f(1) is x(t).
     f(2) is y(t).
     f(3) is z(t).
    df = zeros(3, 1);
    df(1) = k + 1 / 2 * f(1) * f(3) + (omega - 1 / 2 * alpha * f(3)) * f(2);
    df(2) = -(omega - 1 / 2 * alpha * f(3)) * f(1) + 1 / 2 * (f(2)) ^ 2;
    df(3) = -2 * gamma * f(3) - (1 + 2 * beta * f(3)) * ((f(1)) ^ 2 + (f(2)) ^ 2 - 1);
end
