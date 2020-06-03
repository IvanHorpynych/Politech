#include "gauss_seidel_meth.h"

GausSeidelMeth::GausSeidelMeth():n(4)
{
	std::vector<double> tmp_vec;
	
	for (int i =0; i < n; i++) { 
		tmp_vec.push_back(0);	
		roots.push_back(0);		    //  roots null
		free_memb_vec.push_back(0); // free_memb null
	}
		
    for(int i =0; i < n; i++) 
		matrix.push_back(tmp_vec);  // main matrix null
   	

}

GausSeidelMeth::~GausSeidelMeth() 
{
	matrix.clear();
	roots.clear();
	free_memb_vec.clear();
}

void GausSeidelMeth::PrintRoots()
{
	std::cout<<"Roots: "<<std::endl;
    for (int i = 0; i < n; i++)
		std::cout<<roots[i]<<std::endl;
}

void GausSeidelMeth::GaussSeidel()
{
    double eps = 1E-14;
    std::cout<<"Gauss-Seidel method: "<<std::endl<<std::endl;
    PrintMatrix();
    std::cout<<"New matrix:"<<std::endl;
    for (int i = 0; i < n; i++)
    {
        double temp = matrix[3][i];
        matrix[3][i] = matrix[0][i];
        matrix[0][i] = temp - matrix[0][i];
    }
    
	double temp1 = free_memb_vec[3];
    free_memb_vec[3] = free_memb_vec[0];
    free_memb_vec[0] = temp1 - free_memb_vec[3];
    PrintMatrix();

    double d =0;
    double sum = 0, max = 0;


    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++){
            sum += abs(matrix[i][j]); 
        }
        if (sum > max)
            max = sum;
    }

    eps *= abs((1 - max) / max);
               
    do  {
            d = 0;
            for (int i = 0; i < n; i++)
            {
                double s = 0;
                for (int j = 0; j < n; j++)
                    if (i != j)
                        s += matrix[i][j] * roots[j];
                double temp = roots[i];
                roots[i] = (free_memb_vec[i] - s) / matrix[i][i];
                d = abs(temp - roots[i]);
            }

        } while (eps < d);

    PrintRoots();

}

void GausSeidelMeth::GaussJordan()
{
	std::cout<<"Gauss-Jordan method:"<<std::endl;
 
    for (int i = 0; i < n; i++) {
        double temp = matrix[i][i];
       free_memb_vec[i] /= temp;
                  
        for (int j = 0; j < n; j++) {
            matrix[i][j] /= temp;                      
        }

        for (int k = 0; k < n; k++) {
            if (k == i)
                continue;
            double temp1 = matrix[k][i];
           free_memb_vec[k] -=free_memb_vec[i] * temp1;
		   for (int z = 0; z < n; z++) {
                matrix[k][z] -= matrix[i][z] * temp1;
            }
        }

        PrintMatrix();
    }

}

void GausSeidelMeth::PrintMatrix()
{

    std::cout<<"Matrix and free members vector: "<<std::endl;
                
    for (int i = 0; i < n; i++) {                    
        for  (int j = 0; j < n; j++){
			std::cout<<std::setprecision(3)<<matrix[i][j]<<"\t";
        }
		std::setprecision(5);
        std::cout<<"|"<<free_memb_vec[i]<<"\t"<<std::endl;
    }	
} 
 
void GausSeidelMeth::SetMatrix(const double mat[4][4])
{
	for(int i =0; i < n; i++) {
		for (int j =0; j < n; j++) {
			matrix[i][j] = mat[i][j]; 
		}
	}
}

void GausSeidelMeth::SetFreeMemVec(const double vec[])
{
	for (int i =0; i < n; i++) {
		free_memb_vec[i] = vec[i];
	}
}