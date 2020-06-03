function dz = fun1_2_z(x, y, z)
dy = z / x;
dz = (2 * pow(z, 2))/(x * (y - 1)) + dy;
end