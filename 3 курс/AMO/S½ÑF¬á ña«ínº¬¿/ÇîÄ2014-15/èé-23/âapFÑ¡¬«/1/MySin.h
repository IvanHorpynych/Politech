#ifndef SINUS_H_
#define SINUS_H_

class MySinus {
public:
	MySinus(double _x = 0, double _eps = 1) :
		x(_x), eps(_eps), ifcomput(false), len_series(0), reduct_x(_x), result(0), remainder_term(_x),
		negativ(false), rf(1), statusN(false), const_len_series(0) {
	};
	~MySinus();

	void setParam(double _x, double _eps);
	double increaseEps(double _eps);

	double calc();

	double getResult();
	int getN();
	double getRemaindTerm();
	double getAbsoluteError();
	bool setStatusN(int _len_series);
	bool setFloatN();

private:
	double x;
	double eps;
	double reduct_x;
	double result;
	double remainder_term;
	int len_series;
	bool ifcomput;
	bool negativ;
	bool statusN;
	int rf;
	int const_len_series;

	double reduct_arg();
	double recurent_form(double prev_term);
	double comput_float_N();
	double comput_const_N();
};
#endif /* SINUS_H_ */