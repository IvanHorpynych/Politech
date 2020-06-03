#include <iostream>
#include <string>
#include <fstream>
#include <list>
#include <iomanip>
#include <algorithm>
#include <windows.h>
#include <stdio.h>

using namespace std;

int const quantum = 3; 

int ttime = 0;
int q1_time = 0;
int q2_time = 0;

struct process {
	string id;
	int starttime;
	int runtime;
	int timeleft;

	process(const char* _id, int start, int run)
		: id(_id)
		, starttime(start)
		, runtime(run)
		, timeleft(runtime)
	{};
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

void task_manager();
void show_results();
void change_time();
void fon();
void init(int _time);
void add_task(list<process> &q, const char* id, int start, int run);

int main() {
	// first queue
	add_task(queue1, "0", 0, 5);
	add_task(queue1, "1", 2, 5);
	add_task(queue1, "2", 1, 5);
	add_task(queue1, "3", 4, 6);
	// second queue
	add_task(queue2, "4", 0, 5);
	add_task(queue2, "5", 2, 5);
	add_task(queue2, "6", 1, 5);
	add_task(queue2, "7", 4, 6);

	task_manager();
	show_results();
	system("pause");
	return 0;
}

void add_task(list<process> &q, const char* id, int start, int run) {
	q.push_back(process(id, start, run));
}

void fon() {
	process p = q2.front(); 
	q2.pop_front();   
	
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
}

void task_manager() {
	init(0);
	list<executed_process> current_list;
	executed_process tmp_executed;
	list<process>::iterator c_it = q1.begin(), tmp; 

	cout << "ttime" << "  process" << endl;
	
	int i;
	while (1) {
		if (q1.empty() || q1_time > ttime * 0.8) {  
			if (q2.empty()) {              
				if (ttime == 50) {
					break;
				} else {
					cout << setw(4) << ttime << " ";
					cout << setw(6) << "empty" << endl;
					change_time();
					if (!q1.empty()) {
						c_it = q1.begin();
					}
					continue;
				}
			} else {
				fon();
				continue;
			}
		}
		
		i = 0;
		while (i < quantum) {
			cout << setw(4) << ttime << " ";
			cout << setw(6) << c_it->id << endl;
			
			if (c_it->timeleft <= 1) {        
				tmp_executed.id = c_it->id;
				tmp_executed.starttime = c_it->starttime;
				tmp_executed.runtime = c_it->runtime;
				tmp_executed.delaytime = ttime - c_it->starttime - c_it->runtime + 1;
				tmp_executed.endtime = ttime;
				executed.push_back(tmp_executed); 
				
				tmp = c_it;
				c_it++;   
				q1.erase(tmp); 
				break;
			}
			
			c_it->timeleft--;
			change_time();
			q1_time++; 
			i++;
		}
		
		if (i == quantum) {
			c_it++;

		}
		
		if (c_it == q1.end()) {
			c_it = q1.begin(); 
			
		}

	}
	
}

void show_results() {	
	cout << endl << "Results:" << endl;
	if (executed.empty()) {
		cout << "There are no executed processes" << endl;
		return;
	}	
	cout << "  id starttime runtime endtime delaytime" << endl;	
	double delay = 0;
	int size = executed.size();
	for_each(executed.begin(), executed.end(), [&](const executed_process& p) { 
		cout << setw(4) << p.id << " ";
		cout << setw(9) << p.starttime << " ";
		cout << setw(7) << p.runtime << " ";
		cout << setw(7) << p.endtime << " ";
		cout << setw(9) << p.delaytime << " ";
		cout << endl;
		delay += p.delaytime;
	});
	delay /= size;
	cout << "delay ttime " << delay << endl; 
}

void init(int _time) {
	auto get_tasks = [&](list<process>& from, list<process>& to) { 
		int t = _time;
		for_each(from.begin(), from.end(), [&](const process& p) {
			if (p.starttime == t) {
				to.push_back(p); 
			}
		});
	};
	get_tasks(queue1, q1);
	get_tasks(queue2, q2);
}

void change_time() {		
	::Sleep(100);
	ttime++;	
	init(ttime);
}