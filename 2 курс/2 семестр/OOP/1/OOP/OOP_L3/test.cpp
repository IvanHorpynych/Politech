#include "CTrainTree.h"

int main(){
	CTrainTree tree1;

	tree1.parseCSV("test.csv");
	std::cout << "Create new tree" << std::endl;
	tree1.print();

	std::cout << "Delete train with num = 2" << std::endl;
	tree1.delTrain(tree1.searchByTrainNum(2));
	tree1.print();	

	std::cout << "Show trains with destination \"MMM\"" << std::endl;
	std::vector<CTrain*> res = tree1.searchByDestination("MMM");
	for (size_t i = 0; i < res.size(); i++)
		std::cout << res[i]->data.m_TrainNumber << std::endl;

	return 0;
}