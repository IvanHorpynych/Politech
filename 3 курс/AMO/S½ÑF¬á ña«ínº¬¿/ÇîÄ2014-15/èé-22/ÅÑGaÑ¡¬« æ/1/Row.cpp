#include "Row.h"

Row::Row()
{
    setlocale(LC_ALL, "Russian");
	E=2.7182818284590451;	
	int step=-3;     //крок
	double a=-6.2;
	double b=35.7;
	this->x=abs(b+a)/2;  
	double h=(b-a)/10;
	double eps;//=pow(10,-2);
	double el;    //поточний елемент
	double TZ=exp(abs(a));  //точне значення
	double rez;
  

	//std::cout<<"Точне значення: "<<TZ<<std::endl;
	printf("Точне значення: %.16f\n",TZ);
	printf("            eps             Довжина       Похибка        Залишковий член                 \n");
	
	for(int i=-2;i>=-14;i=i+step)
	{
	  eps=pow(10,i);
	  rez=this->EXP(abs(a),eps);
	  printf("%.16f\t%d\t%.16f\t%.20f\n",eps,this->n,abs(TZ-rez),this->R);//Rn(x,this->n));
	}
	
	printf("\n\n xi                         Похибка                  Залишковий член            \n");
	 
	for(int i=0; i<10; i++)
	{
	  this->x=abs(a)+h*i;
	  TZ=exp(this->x);
	  rez=this->EXP(this->x);
	 // rez=this->EXP(x,0.00000001);
	  printf("Точне значення: %.16f\n",TZ);
	  printf("%.16f\t%.16f\t%.30f\n",this->x,abs(TZ-rez),this->R);//Rn(x,this->n));
	}



}

double Row::EXP(double x,double eps)
{

	   double whp1;   //целая часть
	   double frap=modf(x,&whp1);  //дробная часть
	   int whp=int(whp1);
       whp1=1;
	   double U=1;
	   double prev=1;
	   int k=1;
	   double sum=1;
	   double eps2;

	  whp1=pow(this->E,whp);
	   eps2=eps/whp1;


	  while(true)
	      {
		   u2=U;
		   U=(frap*prev)/k;
		   prev=U;
		   if(U<eps)break;
		   k++;
		   sum=sum+U;
	      }
	  if(eps==1.0000000000000000e-008)
		  this->n2=k;
	  this->n=k;

	  this->R=U;
	  printf("sum : %.16f\n",sum*whp1);
	 //  std::cout<<k<<std::endl;
	 //  std::cout<<whp1*sum<<std::endl;
	 //  std::cout<<exp(x)<<std::endl;
	 //  std::cout<<Rn(x,k)<<std::endl;
	  // printf("ddd  %.20f",Rn(x,k));
	  // fprintf(FTabl1, "%.16f\t%.16f\t%.16f\n", x, (suma-TochnZn), chlen);

	return whp1*sum;
}


double Row::EXP(double x)
{

	   double whp1;   //целая часть
	   double frap=modf(x,&whp1);  //дробная часть
	   int whp=int(whp1);
       whp1=1;
	   double U=1;
	   double prev=1;
	   int k=1;
	   double sum=0;
	   double eps2;

	  whp1=pow(this->E,whp);
	   //eps2=eps/whp1;


	  for(int i=1; i<=this->n2; i++)
	     {
			 sum=sum+U;
		   U=(frap*U)/i;
           //prev=U;
		  // k++;
		   //sum=sum+U;
	     }
	  printf("sum : %.16f\n",sum*whp1);
	 //  std::cout<<k<<std::endl;
	 //  std::cout<<whp1*sum<<std::endl;
	 //  std::cout<<exp(x)<<std::endl;
	 //  std::cout<<Rn(x,k)<<std::endl;
	  // printf("ddd  %.20f",Rn(x,k));
	  // fprintf(FTabl1, "%.16f\t%.16f\t%.16f\n", x, (suma-TochnZn), chlen);
	  this->R=U;
	return whp1*sum;
}

double Row::Rn(double x,int n)
{
 /*    int k=n+1;
  double xx=abs(x);
  double firp =pow(xx,k)/factor(k);
  double secp =1/(1-((xx)/k+1));
  return firp*secp;*/
 
  double fraction = 1 - (double)abs(x)/ (n + 2);
  double invFraction = 1 / fraction;
  double numerator = (double)pow(abs(x), n + 1);
  double denomenator = (factor(n + 1));
  return numerator / denomenator * invFraction;
			
}

Row::~Row()
{

}

double Row::getel(double prev,int k,double x)
{
  return (x*prev)/k;
}

int Row::factor(int i)
{
	long long int n=1;
	for (long long int j=1;j<i;j++)
     	{
		 n=n+n*j;
	    }
	return n;
}