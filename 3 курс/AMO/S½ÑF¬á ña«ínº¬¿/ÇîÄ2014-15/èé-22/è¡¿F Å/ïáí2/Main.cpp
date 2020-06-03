#include "Calc.h"

using namespace std;

int main(){
	Calculation lab2;

	//1st root
	lab2.RunIteration(-1, -0.8, 4.5, 7.5);

	lab2.RunChord(-1, -0.8);

	//2nd root
	lab2.RunIteration(0.3, 0.4, 2, 3.5);

	lab2.RunChord(0, 0.4);

}