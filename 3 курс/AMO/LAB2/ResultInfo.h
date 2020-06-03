#pragma once

struct ResultInfo{
	double x;
	double clarRoot;
	int countIter;

	ResultInfo(void);
	ResultInfo(ResultInfo& src);
	ResultInfo& operator=(ResultInfo& src);
	~ResultInfo(void);
};