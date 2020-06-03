#include "CTrainTree.h"

train_s& train_s::operator= (train_s &right){
	m_TrainNumber = right.m_TrainNumber;
	m_Destination = right.m_Destination;
	m_DepartureTime.year = right.m_DepartureTime.year;
	m_DepartureTime.month = right.m_DepartureTime.month;
	m_DepartureTime.day = right.m_DepartureTime.day;
	m_DepartureTime.hour = right.m_DepartureTime.hour;
	m_DepartureTime.minute = right.m_DepartureTime.minute;
	return *this;
}

CTrain::CTrain(){
	left = right = nullptr;
	data.m_TrainNumber = -1;
}

CTrain::CTrain(train_s &new_train){
	left = right = nullptr;
	data = new_train;
}

CTrain::~CTrain(){
	delete left;
	delete right;
}

CTrainTree::CTrainTree(){
	head = nullptr;
}

CTrainTree::~CTrainTree(){
	delete head;
}

void CTrainTree::newTrain(train_s &new_train){
	head = newTrain_helper(new_train, head);
}

CTrain* CTrainTree::newTrain_helper(train_s &new_train, CTrain *node){
	if (node == nullptr)
		return new CTrain(new_train);
	else if (node->data.m_TrainNumber > new_train.m_TrainNumber)
		node->left = newTrain_helper(new_train, node->left);
	else if (node->data.m_TrainNumber < new_train.m_TrainNumber)
		node->right = newTrain_helper(new_train, node->right);
	else
		throw TrainDuplicate();
	return node;
}

CTrain* CTrainTree::searchByTrainNum(int TrainNumber){
	return searchByTrainNum_helper(TrainNumber, head);
}

CTrain* CTrainTree::searchByTrainNum_helper(int TrainNumber, CTrain *node){
	if (node == nullptr)
		return nullptr;
	else if (node->data.m_TrainNumber == TrainNumber)
		return node;
	else if (node->data.m_TrainNumber > TrainNumber)
		return searchByTrainNum_helper(TrainNumber, node->left);
	else
		return searchByTrainNum_helper(TrainNumber, node->right);
}

std::vector<CTrain*> CTrainTree::searchByDestination(std::string dest){
	std::vector<CTrain*> res;
	searchByDest_helper(dest, res, head);
	return res;
}

void CTrainTree::searchByDest_helper(std::string &dest, std::vector<CTrain*> &res, CTrain *node){
	if (node == nullptr)
		return;
	if (node->data.m_Destination == dest)
		res.push_back(node);
	searchByDest_helper(dest, res, node->left);
	searchByDest_helper(dest, res, node->right);
}

CTrain* CTrainTree::findMin(CTrain* node){
	if (node == nullptr)
		return nullptr;
	if (node->left == nullptr)
		return node;
	return findMin(node->left);
}

void CTrainTree::delTrain(CTrain* train_to_del){
	head = delTrain_helper(train_to_del, head);
}

CTrain* CTrainTree::delTrain_helper(CTrain *train_to_del, CTrain *node){
	if (train_to_del == nullptr)
		return nullptr;
	if (node == train_to_del){
		if ((node->left != nullptr) && (node->right != nullptr)){
			CTrain *tmp = findMin(node->right);
			node->data = tmp->data;
			delTrain(tmp);
		} else if (node->right != nullptr){
			CTrain *tmp = node->right;
			node->right = nullptr;
			delete node;
			return tmp;
		} else if (node->left != nullptr){
			CTrain *tmp = node->left;
			node->left = nullptr;
			delete node;
			return tmp;
		} else {
			delete node;
			return nullptr;
		}
	} else if (node->data.m_TrainNumber > train_to_del->data.m_TrainNumber)
		node->left = delTrain_helper(train_to_del, node->left);
	else
		node->right = delTrain_helper(train_to_del, node->right);
	return node;
}

void CTrainTree::print(){
	print_helper(head);
}

void CTrainTree::print_helper(CTrain *node){
	if (node == nullptr)
		return;
	print_helper(node->left);
	std::cout << "Train #" << node->data.m_TrainNumber << std::endl;
	std::cout << "Destination: " << node->data.m_Destination << std::endl;
	std::cout << "Departure time: ";
	std::cout << std::setw(2) << std::setfill('0') << node->data.m_DepartureTime.day << ".";
	std::cout << std::setw(2) << std::setfill('0') << node->data.m_DepartureTime.month << ".";
	std::cout << std::setw(2) << std::setfill('0') << node->data.m_DepartureTime.year << " ";
	std::cout << std::setw(2) << std::setfill('0') << node->data.m_DepartureTime.hour << ":";
	std::cout << std::setw(2) << std::setfill('0') << node->data.m_DepartureTime.minute << std::endl << std::endl;
	print_helper(node->right);
}

void CTrainTree::parseCSV(std::string name){
	train_s tmp;
	std::ifstream file(name);
	std::string line;
	while (std::getline(file, line) && file){
    	std::stringstream lineStream(line);
    	std::string cell;

	    std::getline(lineStream, cell, ';');
	    tmp.m_TrainNumber = stoi(cell);

	    std::getline(lineStream, cell, ';');
	    tmp.m_Destination = cell;

	    std::getline(lineStream, cell, '.');
	    tmp.m_DepartureTime.day = stoi(cell);

	    std::getline(lineStream, cell, '.');
	    tmp.m_DepartureTime.month = stoi(cell);

	    std::getline(lineStream, cell, ' ');
	    tmp.m_DepartureTime.year = stoi(cell);

	    std::getline(lineStream, cell, ':');
	    tmp.m_DepartureTime.hour = stoi(cell);

	    std::getline(lineStream, cell, ' ');
	    tmp.m_DepartureTime.minute = stoi(cell);

	   	newTrain(tmp);
	}
}