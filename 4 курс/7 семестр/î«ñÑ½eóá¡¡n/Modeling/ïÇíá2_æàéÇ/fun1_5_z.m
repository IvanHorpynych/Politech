% function df = fun1_5_z(x, f)
% df = zeros(2, 1);
% df(2) = 2 * f(1)^2 + f(2);
% df(1) = exp(-(x^2) + (f(2)^2)) + 2 * x;
% end

function df = fun1_5_z (x , f )
 % f ( 1 ) i s y ( x )
 % f ( 2 ) i s z ( x )
 df = zeros( 2 , 1 );
 df(1) = exp(-(x^2 + f(2)^2 ) ) + 2 * x;
 df(2) = 2 * f(1)^2 + f(2);
 end