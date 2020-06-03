#include <iostream>
#include <string>
#include <fstream>
#include <list>
#include <iomanip>
#include <windows.h>

using namespace std;

int const quantum = 2;

int ttime = 0;
int q1_time = 0;
int q2_time = 0;

struct process {
	string id;
	int starttime;
	int runtime;
	int timeleft;
};

struct executed_process {
	string id;
	int starttime;
	int runtime;
	int endtime;
	int delaytime;
};

list<process> queue1, queue2;
list<process> q1, q2;
list<executed_process> executed;

void load_from_file(list<process> &q, string path);
void task_manager();
void show_results();
void change_time();
void fon();
void init();

int main() {
	load_from_file(queue1, "tasks1.txt");
	load_from_file(queue2, "tasks2.txt");
	task_manager();
	show_results();
	return 0;
}

void load_from_file(list<process> &q, string path) {
	fstream in(path.c_str());
	process tmp;		
	string s;
	in >> s;
	in >> s;
	in >> s;

	cout << path << endl;
	cout << "  id starttime runtime" << endl;
	while (!in.eof()) {
		q.push_back(tmp);
		in >> tmp.id;
		in >> tmp.starttime;
		in >> tmp.runtime;
		if (in.eof()) {
			break;
		}
		cout << setw(4) << tmp.id << " ";
		cout << setw(9) << tmp.starttime << " ";
		cout << setw(7) << tmp.runtime << " ";
		cout << endl;
		tmp.timeleft = tmp.runtime;
	}
	cout << endl;
	in.close();
}

void fon() {
	
	list<process>::iterator it, min;
	it = min = q2.begin();

	while (it != q2.end()) {
		if (it->runtime < min->runtime) {
			min = it;
		}
		it++;
	}
	
	process p = *min;
	
	executed_process current;
	current.id = p.id;
	current.starttime = p.starttime;
	current.runtime = p.runtime;
	current.delaytime = ttime - p.starttime;
	current.endtime = ttime + p.runtime;
	
	while (current.endtime != ttime) {
		cout << setw(4) << ttime << " ";
		cout << setw(6) << current.id << endl;
		change_time();
		q2_time++;
	}


	executed.push_back(current);
	
	q2.erase(min);
}

void task_manager() {
	init();
	list<executed_process> current_list;
	executed_process tmp_executed;

	cout << "time" << "    task" << endl;
	
	int i;
	while (1) {
		if (q1.empty() || q1_time > ttime * 0.8) {
			if (q2.empty()) {
				if (ttime == 50) {
					break;
				} else {
					cout << setw(4) << ttime << " ";
					cout << setw(6) << "free" << endl;
					change_time();
					continue;
				}
			} else {
				fon();
				continue;
			}
		}
		
		i = 0;
		process p = q1.front();
		while (i < quantum) {
			cout << setw(4) << ttime << " ";
			cout << setw(6) << p.id << endl;
			
			if (p.timeleft <= 1) {
				process tmp = q1.front();
				tmp_executed.id = p.id;
				tmp_executed.starttime = p.starttime;
				tmp_executed.runtime = p.runtime;
				tmp_executed.delaytime = ttime - p.starttime - p.runtime + 1;
				tmp_executed.endtime = ttime;
				executed.push_back(tmp_executed);
				break;
			}
			
			p.timeleft--;
			change_time();
			q1_time++;
			i++;
		}
		q1.pop_front();
		if (i == quantum) {
			q1.push_back(p);	
		}
	}
}

void show_results() {
	
	cout << endl;

	if (executed.empty()) {
		cout << "There are no executed processes" << endl;
		return;
	}
	
	cout << "  id starttime runtime endtime delaytime" << endl;
	
	double delay = 0;
	int size = executed.size();

	while (!executed.empty()) {
		cout << setw(4) << executed.front().id << " ";
		cout << setw(9) << executed.front().starttime << " ";
		cout << setw(7) << executed.front().runtime << " ";
		cout << setw(7) << executed.front().endtime << " ";
		cout << setw(9) << executed.front().delaytime << " ";
		cout << endl;
		delay += executed.front().delaytime;
		executed.pop_front();
	}
	delay /= size;
	cout << "delay time = " << delay << endl; 
}

void init() {
	list<process>::iterator it;
	
	for (it = queue1.begin(); it != queue1.end(); it++) {
		if (it->starttime == 0) {
			q1.push_back(*it);
		}
	}
	
	for (it = queue2.begin(); it != queue2.end(); it++) {
		if (it->starttime == 0) {
			q2.push_back(*it);
		}
	}	

}

void change_time() {
		
	//Sleep(100);
	ttime++;	
	
	list<process>::iterator it;
	
	for (it = queue1.begin(); it != queue1.end(); it++) {
		if (it->starttime == ttime) {
			q1.push_back(*it);
		}
	}
	
	for (it = queue2.begin(); it != queue2.end(); it++) {
		if (it->starttime == ttime) {
			q2.push_back(*it);
		}
	}
}