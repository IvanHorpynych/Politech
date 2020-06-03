#ifndef CTRAINTREE_H
#define CTRAINTREE_H

#include <string>
#include <exception>
#include <vector>
#include <fstream>
#include <sstream>
#include <iostream>
#include <iomanip>

class TrainDuplicate: public std::exception{
	virtual const char* what() const throw(){
		return "Train duplication error!";
	}
};

typedef struct{
	int year;
	int month;
	int day;
	int hour;
	int minute;
} time_s;

class train_s{
public:
	int m_TrainNumber;
	std::string m_Destination;
	time_s m_DepartureTime;

	train_s& operator= (train_s&);
};

class CTrain{
public:
	CTrain();
	CTrain(train_s& new_train);
	~CTrain();

	train_s data;
	CTrain *left, *right;
};

class CTrainTree{
public:
	CTrainTree();
	~CTrainTree();

	void newTrain(train_s&);
	CTrain* searchByTrainNum(int);
	std::vector<CTrain*> searchByDestination(std::string);
	void delTrain(CTrain*);
	void print();
	void parseCSV(std::string);

private:
	CTrain *head;

	CTrain* newTrain_helper(train_s&, CTrain*);
	CTrain* searchByTrainNum_helper(int, CTrain*);
	void searchByDest_helper(std::string&, std::vector<CTrain*>&, CTrain*);
	CTrain* delTrain_helper(CTrain*, CTrain*);
	CTrain* findMin(CTrain*);
	void print_helper(CTrain*);
};

#endif //CTRAINTREE_H