#include <iostream>
#include <math.h>
#include "lab2header.h"

#define MAX_IT 1000
#define MAX_EPS 0.00000000000000001
using namespace std;

double f(double x)
{
	return (x*x - sin(x))/(cos(x) - 2) + 1;
}

double f_1(double x)
{
	return (x*x*sin(x) + 2*(x + 1)*cos(x) - 4*x - 1)/((cos(x) - 2)*(cos(x) - 2));
}

double f_2(double x)
{
return ( ((x*x + 2)*cos(x) - 2*sin(x) - 4)/( (cos(x) - 2)*(cos(x) - 2)) ) + (2*sin(x)*f_1(x))/(cos(x) - 2);
}

int main()
{
	double a,b,a1,b1,x1,x2,eps;
	int st,num_of_iterations;
	int it_X1_i[4],it_X2_i[4],it_X1_c[4],it_X2_c[4];

	a = -1.0;
	b = -0.5;
	double x1_root = iteration(f,f_1,a,b,MAX_EPS,MAX_IT,&num_of_iterations,&st);
	a1 = 1.0;
	b1 = 2.0;
	double x2_root = iteration(f,f_1,a1,b1,MAX_EPS,MAX_IT,&num_of_iterations,&st);
	cout<<"MIHEDKO ANDREY KV-21 VAR # 12  \n";
	cout<<"\n";

	cout<<"                         METHOD OF SUCCESSIVE APPROXIMATIONS"<<endl;
	cout<<endl;
	cout<<"...............................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<" :";
	cout.width(30);
	cout<<"EQUATION ROOTS      "<<" :";
	cout.width(30);
	cout<<"ABSOLUTE ERROR      "<<" :"<<endl;
	cout<<":    EPS     .................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<":";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X2     "<<" :";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X2     "<<" :"<<endl;
	
    cout<<"...............................................................................  \n";

	eps = 0.01;

	for (int i=0;i<4;++i)
	{
	x1 = iteration(f,f_1,a,b,eps,MAX_IT,&num_of_iterations,&st);
	it_X1_i[i] = num_of_iterations;

	x2 = iteration(f,f_1,a1,b1,eps,MAX_IT,&num_of_iterations,&st);
	it_X2_i[i] = num_of_iterations;

	cout<<":";
	cout.width(12);
	cout<<eps<<" :";
	cout.width(14);
	cout.precision(11);
	cout<<x1<<" :";
	cout.width(14);
	cout<<x2<<" :";
	cout.width(14);
	cout.precision(8);
	cout<<fabs(x1_root - x1)<<" :";
	cout.width(14);
	cout<<fabs(x2_root - x2)<<" :"<<endl;

	eps = eps*0.001;
	}

	cout<<"...............................................................................  \n";

	cout<<endl;

	cout<<"                              SECANT (CHORD) METHOD"<<endl;
	cout<<endl;
	cout<<"...............................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<" :";
	cout.width(30);
	cout<<"EQUATION ROOTS      "<<" :";
	cout.width(30);
	cout<<"ABSOLUTE ERROR      "<<" :"<<endl;
    cout<<":    EPS      .................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<":";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X2     "<<" :"<<endl;
	cout<<"...............................................................................  \n";

	eps = 0.01;

	for (int i=0;i<4;++i)
	{
	x1 = chord(f,f_1,f_2,a,b,eps,MAX_IT,&num_of_iterations,&st);
	it_X1_c[i] = num_of_iterations;

	x2 = chord(f,f_1,f_2,a1,b1,eps,MAX_IT,&num_of_iterations,&st);
	it_X2_c[i] = num_of_iterations;

	cout<<":";
	cout.width(12);
	cout<<eps<<" :";
	cout.width(14);
	cout.precision(11);
	cout<<x1<<" :";
	cout.width(14);
	cout<<x2<<" :";
	cout.width(14);
	cout.precision(8);
	cout<<fabs(x1_root - x1)<<" :";
	cout.width(14);
	cout<<fabs(x2_root - x2)<<" :"<<endl;

	eps = eps*0.001;
	}
	cout<<"...............................................................................  \n";

	cout<<endl;

	cout<<"                               TABLE OF ITERATIONS"<<endl;
	cout<<endl;
	cout<<"...............................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<" :";
	cout.width(30);
	cout<<"ITERATION METHOD    "<<" :";
	cout.width(30);
	cout<<"CHORD METHOD        "<<" :"<<endl;
	cout<<":    EPS      .................................................................  \n";
	cout<<":";
	cout.width(14);
	cout<<":";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X2     "<<" :";
	cout.width(14);
	cout<<"X1     "<<" :";
	cout.width(14);
	cout<<"X2     "<<" :"<<endl;
	cout<<"...............................................................................  \n";

	eps = 0.01;

	for (int i=0;i<4;++i)
	{
	cout<<":";
	cout.width(12);
	cout<<eps<<" :";
	cout.width(14);
	cout<<it_X1_i[i]<<" :";
	cout.width(14);
	cout<<it_X2_i[i]<<" :";
	cout.width(14);
	cout<<it_X1_c[i]<<" :";
	cout.width(14);
	cout<<it_X2_c[i]<<" :"<<endl;

	eps = eps*0.001;
	}
	cout<<"...............................................................................  \n";

	return 0;
	system("Pause");
}
