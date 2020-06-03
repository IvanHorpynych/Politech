#include "gauss_seidel_meth.h"

using namespace std;

int main(int argc, char* argv[]) 
{
	const double matrix[4][4] = 
	{
		{11,5,1,20},
		{19,57,19,18},
		{6,10,33,16},
		{15,14,5,14}
	};

	const double free_memb_vec[4] = {75, 437, 117, 149};
	

	GausSeidelMeth obj;
	 obj.SetMatrix(matrix);
	 obj.SetFreeMemVec(free_memb_vec);
	 obj.GaussSeidel();

	 cout<<endl;

	GausSeidelMeth obj1;
	 obj1.SetMatrix(matrix);
	 obj1.SetFreeMemVec(free_memb_vec);
	 obj1.GaussJordan();
  return 0;	
}