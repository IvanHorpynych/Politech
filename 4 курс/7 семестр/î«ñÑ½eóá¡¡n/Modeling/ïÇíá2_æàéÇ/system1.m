function df = system1 (x , f )
% f ( 1 ) i s y ( x )
% f ( 2 ) i s z ( x )
df = zeros( 2 , 1 ) ;
df ( 1 ) = f ( 2 ) / x ;
df ( 2 ) = 2 * ( f (2) )^2 / ( x * ( f ( 1 ) - 1 ) ) + f ( 2 ) / x ;
end