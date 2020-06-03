#include "Seidel.h"
#include "MainElem.h"

void main()
{
	//int matrix[4][5] = {11,14,5,8,98,3,38,18,16,108,18,4,32,9,185,12,9,13,4,111};
	int matrix[4][5] = {20,20,20,0,180,16,37,14,6,163,18,6,33,8,218,16,9,14,13,142};

	std::cout<< "Start Matrix" << std::endl;
	for(int i = 0; i < 4; i++)
	{
		for(int j = 0; j < 5; j++)
			std::cout << matrix[i][j] << ' ';
		std::cout << std::endl;
	}

	//Task 1
	std::cout<< "Start Task1" << std::endl;
	MainElem Task1 = MainElem();
	Task1.Method(matrix);
	std::cout<< "End Task1" << std::endl;
	std::cout << std::endl;

	//Task2
	std::cout<< "Start Task2" << std::endl;
	Seidel Task2;
	std::cout<< "End Task2" << std::endl;

	std::system("pause");
}