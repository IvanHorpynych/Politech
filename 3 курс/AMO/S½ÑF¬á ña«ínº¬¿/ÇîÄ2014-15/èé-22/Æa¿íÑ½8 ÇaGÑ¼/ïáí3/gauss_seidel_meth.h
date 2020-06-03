#include <cstdio>
#include <cstdint>
#include <cmath>
#include <vector>
#include <ios>
#include <iostream>
#include <iomanip>

class GausSeidelMeth {
public:
	int n;
	std::vector<std::vector<double>> matrix;
	std::vector<double> free_memb_vec;
	std::vector<double> roots;
	

public:
	GausSeidelMeth();
	~GausSeidelMeth();
	void PrintRoots();
	void GaussSeidel();
	void GaussJordan();
    void PrintMatrix();
	
	void SetMatrix(const double mat[4][4]);
	void SetFreeMemVec(const double vec[]);

};