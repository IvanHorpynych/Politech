#include "Lab2.h"


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

}

Root::~Root()
{
}

double Root::_eps() // похибка
{
 return	this->eps=eps/1000;
}

double Root::_der(double a) // знаходить першу похідну f'(x)
{
  return 4*(a*a*a)+6*(a*a)+cos(a); 
}

double Root::_Q(double a, double b) // узагальненій спосіб зведення рівняння для добору початкового наближеного х0  
{                                   // з проміжка [a,b]
	double al = _der(a) / _der(b);
	if (1 < al){
		return 1 - 1 / al;
	}
	return 1 - al;
}

double Root::_AlphCalc(double a,double b) // знаходить Альфа на заданому інтервалі
{
    double ZnA = _der(a);
	double ZnB = _der(b);
	if (ZnA > ZnB){
		return 1 / ZnA;
	}
	return 1 / ZnB;
}

double Root::_FuncCalc(double x) // знаходить значення функції f(x)
{
  return (x*x*x*x)+2*(x*x*x)+sin(x)+0.5; 
}

double Root::_ApproxCalc(double x,double a,double b)  // наближений розрахунок
{
 	double alpha = _AlphCalc(a, b);
	double f = _FuncCalc(x);
	return x - alpha*f;
}

double Root::_ConstForEnd(double q) // для забезпечення точності необхідно продовжувати виконувати наближення
{                                   // доки не буде справджуватись нерівність
   return (1-q)*eps/q;              // (константа умови завершення) 
}

double Root::_der2(double a)  // для методу дотичних обчислюємо
{                             // другу похідну f"(x)
  return 12*(a*a)+12*(a)-sin(a);
}

double Root::_m1(double a)
{
	return abs(_der(a));

}

double Root::_xn_B(double prev) // розрахунок для бісекції
{
	double f=_FuncCalc(prev);
	double fs=_der(prev);
	double r1= f/fs;
	return prev-r1;
}

double* Root::root_App(double a,double b) // метод послідовних наближень (метод ітерації) 
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

double* Root::root_New(double a,double b) // метод дотичних (метод Ньютона)
{
	double SecDe_A=_der2(a)*_der(a);
		double SecDe_B=_der2(b)*_der(b);
		double St;
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
   printf( "   Eps\t\t\tEquation root\t\tEvaluation of the accuracy\n\n");
	for( int i=1; i <= 4; i++)
	   {      
		 _eps();
		 for (int j = 0; j < 2; j++)
			 {
				 double* R =root_App(arr_v[j].a,arr_v[j].b);
				 double xi=R[0];
				 double M_a =_ConstForEnd(_Q(arr_v[j].a,arr_v[j].b));
				 printf( "%.16f\t%.16f\t%.16f\n", eps, xi, M_a);
			 }
	   }
 eps=10;
}

void Root::MofNewton()
{
	printf("\n\n");
     printf( "   Eps\t\t\tEquation root\t\tEvaluation of the accuracy\n\n");
    

for (int j = 0; j < 4; j++)
  {

	  _eps();
	for (int i = 0; i < 2; i++)
	{

		double*res = root_New(arr_v[i].a,arr_v[i].b);
		
	  printf( "%.16f\t%.16f\t%.16f\n", eps, res[0], res[2]);

	} 
  }
}

void Root::Comp()
{
	double A = 0.01;
		eps=10;
	double B = 0.1;
printf("\n\n");
	printf("   Eps\t\t\tIterative Method\tNewton-Raphson method\n\n");

	for (int i = 1; i <= 4; i++)
	   {
		_eps();
		double* res_I=root_App(A, B);
		double* res_N = root_New(A, B);
			printf( "%.16f\t%.16f\t%.16f\n", eps, res_I[1], res_N[1]);
	   }
}