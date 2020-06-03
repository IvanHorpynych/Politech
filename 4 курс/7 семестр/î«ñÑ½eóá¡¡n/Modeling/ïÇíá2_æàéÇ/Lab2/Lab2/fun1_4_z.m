function dz = fun1_4_z(x, y, z)
dy = cos(y + 2 * z) + 2;
dz = 2 / (x + 2 * pow(y, 2)) + x + 1;
end