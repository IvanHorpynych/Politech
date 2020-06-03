#include "MySin.h"
#include <math.h>
#define _USE_MATH_DEFINES
#include <iostream>

using namespace std;

MySinus::~MySinus() {
	// TODO Auto-generated destructor stub
}

double MySinus::reduct_arg()
{
	reduct_x = x;
	if (reduct_x < 0){
		reduct_x = fabs(reduct_x);
		negativ = true;
	}
	else{
		negativ = false;
	}
	reduct_x = reduct_x - trunc(reduct_x / (2 * M_PI))*(2 * M_PI);
	if (reduct_x >= M_PI){
		reduct_x = reduct_x - M_PI;
		negativ = ! (negativ);
	}
	if (reduct_x >= M_PI_2){
		reduct_x = M_PI - reduct_x;
	}
	if (reduct_x > M_PI_4){
		rf = -1;
		remainder_term = 1;
		reduct_x = M_PI_2 - reduct_x;
	}
	else{
		rf = 1;
		remainder_term = reduct_x;
	}
	return this->reduct_x;
}

double MySinus::recurent_form(double prev_term)
{
	return -reduct_x*reduct_x / (2 * len_series*(2 * len_series + rf))*prev_term;
}

double MySinus::calc()
{
	reduct_arg();
	result = 0;
	len_series = 0;
	if (statusN){
		comput_const_N();
	}
	else{
		comput_float_N();
	}
	ifcomput = true;
	return result;
}
void MySinus::setParam(double _x, double _eps){
	eps = _eps;
	x = _x;
}
int MySinus::getN(){
	return len_series;
}
double MySinus::getRemaindTerm(){
	return fabs(remainder_term);
}
double MySinus::getAbsoluteError(){
	return fabs(sin(x) - result);
}
double MySinus::getResult(){
	return result;
}
double MySinus::increaseEps(double _eps){
	if ((_eps >= eps) || !(ifcomput) || statusN){
		return result;
	}
	eps = _eps;

	if (negativ) {
		result = -result;
	}
	return comput_float_N();
}
double MySinus::comput_float_N(){
	double term;
	term = remainder_term;
	do {
		result += term;
		len_series++;
		term = recurent_form(term);
	} while (fabs(term) >= eps);
	remainder_term = term;
	if (negativ) {
		result = -result;
	}
	return result;
}

double MySinus::comput_const_N(){
	double term;
	term = remainder_term;
	for (int i = 0; i < const_len_series; i++) {
		result += term;
		len_series++;
		term = recurent_form(term);
	}
	remainder_term = term;
	if (negativ) {
		result = -result;
	}
	return result;
}

bool MySinus::setStatusN(int _len_series){
	const_len_series = _len_series;
	return(statusN = true);
}
bool MySinus::setFloatN(){
	ifcomput = false;
	return(!(statusN = false));
}