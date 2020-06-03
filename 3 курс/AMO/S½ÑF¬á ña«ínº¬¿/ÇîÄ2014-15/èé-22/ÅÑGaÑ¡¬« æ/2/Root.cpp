#include "Root.h"


Root::Root()
{
  this->eps=10;

  inte s;

  s.a=-2.05;
  s.b=-2.0;
	this->arr_v.push_back(s);
  s.a=-0.5;
  s.b=-0.3;
	this->arr_v.push_back(s);

  s.a=-2.05;
  s.b=_FuncCalc(s.a);           //a f(a)
	this->arr_v2.push_back(s);

  s.a=-2.0;
  s.b=_FuncCalc(s.a);             //b f(b)  
	this->arr_v2.push_back(s);

  s.a=-0.5;
  s.b=_FuncCalc(s.a);             //a2 f(a2);
	this->arr_v2.push_back(s);
  
  s.a=-0.3;
  s.b=_FuncCalc(s.a);            //b2 f(b2)
	this->arr_v2.push_back(s);

 /* s.a=0.01;
  s.b=0.1;
	this->arr_v.push_back(s);
  s.a=0.3;
  s.b=0.4;
	this->arr_v.push_back(s);
  s.a=3.3;
  s.b=3.4;
	this->arr_v.push_back(s);*/
}

Root::~Root()
{
}

double Root::_eps()
{
 return	this->eps=eps/1000;
}

double Root::_der(double a)
{
  // return exp(a) + 1 / a - 10;	
  return 4*(a*a*a)+6*(a*a)+cos(a);
}

double Root::_Q(double a, double b)
{
	double al = _der(a) / _der(b);
	if (1 < al){
		return 1 - 1 / al;
	}
	return 1 - al;
}

double Root::_AlphCalc(double a,double b)
{
    double ZnA = _der(a);
	double ZnB = _der(b);
	if (ZnA > ZnB){
		return 1 / ZnA;
	}
	return 1 / ZnB;
}

double Root::_FuncCalc(double x)
{
  return (x*x*x*x)+2*(x*x*x)+sin(x)+0.5;
	//return exp(x) + log(x) - 10 * x + 3;

}

double Root::_ApproxCalc(double x,double a,double b)
{
 	double alpha = _AlphCalc(a, b);
	double f = _FuncCalc(x);
	return x - alpha*f;
}

double Root::_ConstForEnd(double q)
{
   return (1-q)*eps/q;
}

double Root::_der2(double a)
{
  return 12*(a*a)+12*(a)-sin(a);
}

double Root::_m1(double a)
{
	return abs(_der(a));
	//double mi2=_der(b);

}

double Root::_xn_B(double prev)
{
	double f=_FuncCalc(prev);
	double fs=_der(prev);
	double r1= f/fs;
	return prev-r1;
}

double* Root::root_App(double a,double b)
{
    double X_n = (a+b)/2;     //значення X на ітерації (K+1)
	double X_p;               //значення X на ітерації K
	double q = _Q(a, b);      //правильний дріб
	double CFE = _ConstForEnd(q);//константа умови завершення
	int i = 0;                     //кількість ітерацій поточна
	do{
		X_p = X_n;
		X_n = _ApproxCalc(X_n, a, b);
		i++;
	} while (abs(X_n - X_p) > CFE);
	double* forR = new double[2];
	forR[0] = X_n;
	forR[1] =i;
	return forR;
}

double* Root::root_New(double a,double b)
{
	double SecDe_A=_der2(a)*_der(a);
		double SecDe_B=_der2(b)*_der(b);
		double St;
		double Fun;
		double aa=a,bb;
		double xi;

		if(SecDe_A >0)
		  {
			St=SecDe_A;
			xi=a;
			aa=xi;
		  }
		else
		  {
			St=SecDe_B;
			// xi=arr_v2[i+1].a;
			 xi=b;
			 bb=xi;
		  }

		double xn;


	double forer;
	 int i=0;
	 do{
		 i++;
		 xn=xi;
		 xi=_xn_B(xi);
		 //xi=xn;
		 forer=abs(_FuncCalc(xn))/_m1(a);
		 if((a<xi)&&(bb>xi))
			 bb=xi;
		 if(forer<=eps )
			 break;
	   } while (true);
	 double *ret= new double[3];

	 ret[0]=xi;
	 ret[1]=i;
	 ret[2]=forer;
	 return ret;
}

void Root::MofApproxim()
{
   FILE* f = fopen("iter.txt", "wt");		
   fprintf(f, "eps\t\t\tзначення\t\tоцінка точності\n");
 //   _eps();
	for( int i=1; i <= 4; i++)
	   {      
		 _eps();
		//double* R =root_App(arr_v[1].a,arr_v[1].b);
		 for (int j = 0; j < 2; j++)
			 {
				 double* R =root_App(arr_v[j].a,arr_v[j].b);
				 double xi=R[0];
				 double M_a =_ConstForEnd(_Q(arr_v[j].a,arr_v[j].b));
				 fprintf(f, "%.16f\t%.16f\t%.16f\n", eps, xi, M_a);
			 }
	   }
 eps=10;
}

void Root::MofNewton()
{
	double xi;

	FILE* f2 = fopen("Neot.txt", "wt");		
     fprintf(f2, "eps\t\t\tзначення\t\tоцінка точності\n");
    

for (int j = 0; j < 4; j++)
  {

	  _eps();
	for (int i = 0; i < 2; i++)
	{
		//double SecDe_A=_der2(arr_v2[i].a)*_der(arr_v2[i].a);
		//double SecDe_B=_der2(arr_v2[i+1].a)*_der(arr_v2[i+1].a);

		double*res = root_New(arr_v[i].a,arr_v[i].b);
		
	  fprintf(f2, "%.16f\t%.16f\t%.16f\n", eps, res[0], res[2]);
	} 
  }
}

void Root::Comp()
{
	double A = 0.01;
		eps=10;
	double B = 0.1;
	FILE* f = fopen("Comp.txt", "wt");
	fprintf(f,"    eps\t\tметод І\t\tметод Б\n");
	for (int i = 1; i <= 4; i++)
	   {
		_eps();
		double* res_I=root_App(A, B);
		double* res_N = root_New(A, B);
			fprintf(f, "%.16f\t%.16f\t%.16f\n", eps, res_I[1], res_N[1]);
			//xi = ObchKoren_bis(MasKoren[j][0], MasKoren[j][1], eps);
			//OcToch*/
	   }
}