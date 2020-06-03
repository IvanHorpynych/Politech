#include "Result.h"

Result::Result(Result& source) 
{
	x = source.x;
	clarRoot = source.clarRoot;
	countIter = source.countIter;
}

Result& Result::operator=(Result& source) 
{
	if (this == &source)
		return source;
	x = source.x;
	clarRoot = source.clarRoot;
	countIter = source.countIter;
	return source;
}

Result::~Result(void) {}

Result::Result(void) {}