using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Lab_1_4__2012_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        myThread t1, t2, t3, t4, t5;
        myThread2 t6, t7, t8, t9, t10;
        myThread2 t11, t12, t13, t14, t15;
        //myThread t16, t17, t18, t19, t20;
        //object  t1, t2, t3, t4, t5;
        //varib vari;
        static Random rand; 
        static Mutex mute = new Mutex(false);
        static Mutex mute2 = new Mutex(false);
        static int SCDelay;
        static int min_proc_speed;
        static int max_proc_speed;
        static myList List;
        static int processors_sum_speed_for_period; 
        static int processors_theory_sum_max_speed;
        static int processors_real_sum_max_speed;
        static int time_counter;
        static int chanse_of_new_task;
        static int steps;
        static double d1, d2;
        static Boolean bb;

       /* class varib
        {
            public varib(int n)
            {
                switch (n)
                {
                    case 1: { myThread this.t1, t2, t3, t4, t5; break; this.t1 = new(myThread); }
                    case 2: { break; }
                    case 3: { break; }
                    case 4: { break; }
                    default: { break; }
                }
            }

            public object t1, t2, t3, t4, t5;
        }*/

        class nodeL
        {
            public nodeL(int opr, int p1, int p2, int p3, int p4, int p5)
            {
                this.operations_need = opr;
                if (p1 == 1)
                    this.proc1 = true;
                else this.proc1 = false;
                if (p2 == 1)
                    this.proc2 = true;
                else this.proc2 = false;
                if (p3 == 1)
                    this.proc3 = true;
                else this.proc3 = false;
                if (p4 == 1)
                    this.proc4 = true;
                else this.proc4 = false;
                if (p5 == 1)
                    this.proc5 = true;
                else this.proc5 = false;
                if( (this.proc1 == false) && (this.proc2 == false) && (this.proc3 == false) &&
                     (this.proc4 == false) && (this.proc5 == false))
                {
                    switch (rand.Next(1, 5))
                    {
                        case 1:
                            {
                                this.proc1 = true;
                                break;
                            }
                        case 2:
                            {
                                this.proc2 = true;
                                break; 
                            }
                        case 3:
                            {
                                this.proc3 = true;
                                break; 
                            }
                        case 4:
                            {
                                this.proc4 = true;
                                break; 
                            }
                        case 5:
                            {
                                this.proc5 = true;
                                break; 
                            }
                    }
                }
                
            }
            public int number;
            public int operations_need;
            public Boolean proc1;
            public Boolean proc2;
            public Boolean proc3;
            public Boolean proc4;
            public Boolean proc5;
            public nodeL prev = null;
            public nodeL next = null;
        }

        class myList
        {
            Form1 f;

            public myList(object state)
            {
                f = (state as Form1);
                numb = 0;
                this.curr = null;
                this.first = null;
            }

            public myList(object state, int n)
            {
                f = (state as Form1);
                numb = 0;
                this.curr = null;
                this.first = null;
                this.add_new(n);
            }

            public myList(object state, nodeL nod)
            {
                f = (state as Form1);
                numb = 0;
                this.curr = null;
                this.first = null;
                this.add_new(nod);
            }

            public void add_new(int n)
            {
                int i;
                nodeL nod;
                if (n == 0)
                    return;
                if (this.curr == null)
                {
                    n--;
                    nod = new nodeL(rand.Next(min_proc_speed * 10, min_proc_speed * 200),
                        rand.Next(1), rand.Next(1), rand.Next(1), rand.Next(1), rand.Next(1));
                    nod.number = numb;
                    numb++;
                    this.curr = nod;
                    this.first = nod;
                    nod.next = nod;
                    nod.prev = nod;
                }
                for (i = 0; i < n; i++)
                {
                    nod = new nodeL(rand.Next(min_proc_speed * 10, min_proc_speed * 200),
                        rand.Next(0,2), rand.Next(0,2), rand.Next(0,2), rand.Next(0,2), rand.Next(0,2));
                    nod.number = numb;
                    numb++;
                    this.curr.next.prev = nod;
                    nod.next = this.curr.next;
                    nod.prev = this.curr;
                    this.curr.next = nod;
                    this.curr = nod;
                }
            }

            public void add_new(nodeL nod)
            {
                if (this.curr == null)
                {
                    nod.number = numb;
                    numb++;
                    this.curr = nod;
                    this.first = nod;
                    nod.next = nod;
                    nod.prev = nod;
                }
                else
                {
                    nod.number = numb;
                    numb++;
                    this.curr.next.prev = nod;
                    nod.next = this.curr.next;
                    nod.prev = this.curr;
                    this.curr.next = nod;
                    this.curr = nod;
                }
            }

            public void clear_List()
            {
            }

            private void exclude_Node(nodeL nod)
            {
                if (this.first.next == this.first)
                {
                    this.first = null;
                    this.curr = null;
                    return;
                }
                if (nod == this.first)
                    this.first = this.first.next;
                if (nod == this.curr)
                    this.curr = this.curr.prev;
                nod.prev.next = nod.next;
                nod.next.prev = nod.prev;
            }

            public nodeL get_Task(string st,int n)
            {
                nodeL nod = this.first;

                if (nod == null)
                    return null;

                char num = st[st.Length - 1];//Convert.ToInt32(st[st.Length - 1]);

                if (n == 1)
                {
                    switch (num)
                    {
                        case '1':
                            {
                                if (nod.proc1 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                /*  nod = nod.next;
                                  while (nod.proc1 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }*/
                                break;
                            }
                        case '2':
                            {
                                if (nod.proc2 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                /* nod = nod.next;
                                 while (nod.proc2 != true && nod != this.first)
                                 {
                                     nod = nod.next;
                                 }
                                 if (nod != this.first)
                                 {
                                     this.exclude_Node(nod);
                                     return nod;
                                 }*/
                                break;
                            }
                        case '3':
                            {
                                if (nod.proc3 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                /*  nod = nod.next;
                                  while (nod.proc3 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }*/
                                break;
                            }
                        case '4':
                            {
                                if (nod.proc4 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                /*  nod = nod.next;
                                  while (nod.proc4 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }*/
                                break;
                            }
                        case '5':
                            {
                                if (nod.proc5 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                /*nod = nod.next;
                                while (nod.proc5 != true && nod != this.first)
                                {
                                    nod = nod.next;
                                }
                                if (nod != this.first)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }*/
                                break;
                            }
                        default: { break; }
                    }
                }

                if (n == 2)
                {
                    switch (num)
                    {
                        case '1':
                            {
                                if (nod.proc1 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                  nod = nod.next;
                                  while (nod.proc1 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }
                                break;
                            }
                        case '2':
                            {
                                if (nod.proc2 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                 nod = nod.next;
                                 while (nod.proc2 != true && nod != this.first)
                                 {
                                     nod = nod.next;
                                 }
                                 if (nod != this.first)
                                 {
                                     this.exclude_Node(nod);
                                     return nod;
                                 }
                                break;
                            }
                        case '3':
                            {
                                if (nod.proc3 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                  nod = nod.next;
                                  while (nod.proc3 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }
                                break;
                            }
                        case '4':
                            {
                                if (nod.proc4 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                  nod = nod.next;
                                  while (nod.proc4 != true && nod != this.first)
                                  {
                                      nod = nod.next;
                                  }
                                  if (nod != this.first)
                                  {
                                      this.exclude_Node(nod);
                                      return nod;
                                  }
                                break;
                            }
                        case '5':
                            {
                                if (nod.proc5 == true)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                nod = nod.next;
                                while (nod.proc5 != true && nod != this.first)
                                {
                                    nod = nod.next;
                                }
                                if (nod != this.first)
                                {
                                    this.exclude_Node(nod);
                                    return nod;
                                }
                                break;
                            }
                        default: { break; }
                    }
                }

                return null;
            }

            public void print_out()
            {
                nodeL nod;
                nod = first;
                string st;
                st = nod.number.ToString() + " " + nod.operations_need.ToString() + " " +
                    nod.proc1.ToString() + " " + nod.proc2.ToString() + " " +
                    nod.proc3.ToString() + " " + nod.proc4.ToString() + " " +
                    nod.proc5.ToString() + "\n";
                f.SetTextSafe(st);
                nod = nod.next;
                while (nod != first)
                {
                    st = nod.number.ToString() + " " + nod.operations_need.ToString() + " " +
                        nod.proc1.ToString() + " " + nod.proc2.ToString() + " " +
                        nod.proc3.ToString() + " " + nod.proc4.ToString() + " " + 
                        nod.proc5.ToString() + "\n";
                    f.SetTextSafe(st);
                    nod = nod.next;
                }
            }
            
            private nodeL first;
            private nodeL curr;
            private int numb;            
        }

       private void SetTextSafe(string text)
       {
           Action chTxt = new Action(() =>
           {
               this.textBox11.AppendText(text);
           });

           if (InvokeRequired)
               this.BeginInvoke(chTxt);
           else chTxt();
       }

       private void OutKPD(string kpd1,string kpd2)
       {
           Action chKpd = new Action(() =>
           {
               this.textBox18.Text = (kpd1);
               this.textBox20.Text = (kpd2);
           });

           if (InvokeRequired)
               this.BeginInvoke(chKpd);
           else chKpd();
       }

              class myThread
              {
            Thread thread;
            Form1 f;
            private int n;

            public myThread(object state, int name, int num) //Конструктор получает имя функции и номер до кторого ведется счет
            {
                n = 1;
                f = (state as Form1);
                thread = new Thread(this.proc_funct);
                thread.Name = "Thread " + name.ToString();
                thread.Start(num);//передача параметра в поток
            }

            public void thr_stop()
            {
                thread.Abort();
                string st = thread.Name + " stoped\n";
                f.textBox11.AppendText(st);
            }

            private void proc_funct(object proc_op)
            {
                int process_operations = (int)proc_op;
                nodeL nod;
                //Boolean new_task;
               
                string st = " ";
                
                try
                {
                    mute.WaitOne();
                    st = "started " + thread.Name + "\n";
                    f.SetTextSafe(st);
                    mute.ReleaseMutex();
                }
                catch { };


                while(true)
                {
                  //  try
                   // {
                        mute.WaitOne();
                        //time_counter++;
                        if (rand.Next(0, 100) <= chanse_of_new_task)
                            List.add_new(1);
                        nod = List.get_Task(thread.Name,1);
                        if (nod != null)
                        {
                            st = thread.Name + " " + nod.number + "\n";
                        }
                        else st = thread.Name + " is waiting for tasks \n"; //st = thread.Name + " " + proc_op.ToString() + "\n";
                        f.SetTextSafe(st);
                        mute.ReleaseMutex();
                        n = SCDelay;
                  //  }
                 //   catch { };
                        if (nod != null)
                            while (nod.operations_need > 0)
                            {
                                nod.operations_need -= process_operations;
                                mute.WaitOne();
                                processors_theory_sum_max_speed += process_operations;
                                processors_real_sum_max_speed = processors_theory_sum_max_speed;
                                processors_sum_speed_for_period += process_operations;
                                //time_counter++;
                                st = thread.Name + " " + nod.number + "\n";
                                f.SetTextSafe(st);
                                if (time_counter >= 5000)
                                {
                                    time_counter = 0;
                                    d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                    d2 = (processors_sum_speed_for_period  / (double)processors_real_sum_max_speed)*100;
                                    f.OutKPD(((int)d1).ToString(),((int)d2).ToString());
                                    processors_sum_speed_for_period = 0;
                                    processors_theory_sum_max_speed = 0;
                                    processors_real_sum_max_speed = 0;
                                }
                                else time_counter++;
                                mute.ReleaseMutex();
                                Thread.Sleep(n);
                            }
                        else
                        {
                            mute.WaitOne();
                            processors_theory_sum_max_speed += process_operations;
                            processors_real_sum_max_speed = processors_theory_sum_max_speed;
                            //time_counter++;
                            if (time_counter >= 5000)
                            {
                                time_counter = 0;
                                d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                processors_sum_speed_for_period = 0;
                                processors_theory_sum_max_speed = 0;
                                processors_real_sum_max_speed = 0;
                            }
                            else time_counter++;
                            mute.ReleaseMutex();
                            Thread.Sleep(n);
                        }
                    //get task, sub operations from task's all operations
                }
            }
        }

              class myThread2
              {
                  Thread thread;
                  Form1 f;
                  private int n;

                  public myThread2(object state, int name, int num, int task_nmb) //Конструктор получает имя функции и номер до кторого ведется счет
                  {
                      n = 1;
                      f = (state as Form1);
                      if ((num == min_proc_speed) && (task_nmb == 2) && (bb == true))
                      {
                          bb = false;
                          thread = new Thread(this.proc_funct2);
                          thread.Name = "Thread " + name.ToString();
                          thread.Start(num);//передача параметра в поток                          
                      }
                      else
                      {
                          if ((num == max_proc_speed) && (task_nmb == 3) && (bb == true))
                          {
                              bb = false;
                              thread = new Thread(this.proc_funct3);
                              thread.Name = "Thread " + name.ToString();
                              thread.Start(num);//передача параметра в поток   
                          }
                          else
                          {
                              thread = new Thread(this.proc_funct);
                              thread.Name = "Thread " + name.ToString();
                              thread.Start(num);//передача параметра в поток
                          }                                                    
                      }
                  }

                  public void thr_stop()
                  {
                      thread.Abort();
                      string st = thread.Name + " stoped\n";
                      f.textBox11.AppendText(st);
                  }

                  private void proc_funct3(object proc_op)
                  {
                      int process_operations = (int)proc_op;
                      nodeL nod;
                      int step_cout = 0;
                      int step_amount = 0;
                      //Boolean new_task;

                      string st = " ";

                      try
                      {
                          mute.WaitOne();
                          step_amount = steps;
                          st = "started " + thread.Name + "\n";
                          f.SetTextSafe(st);
                          mute.ReleaseMutex();
                      }
                      catch { };


                      while (true)
                      {
                          //  try
                          // {
                          while (step_cout != step_amount)
                          {
                              step_cout++;
                              mute.WaitOne();
                              //time_counter++;
                              if (rand.Next(0, 100) <= chanse_of_new_task)
                                  List.add_new(1);
                              nod = List.get_Task(thread.Name,2);
                              if (nod != null)
                              {
                                  st = thread.Name + " " + nod.number + "\n";
                              }
                              else st = thread.Name + " is waiting for tasks \n"; //st = thread.Name + " " + proc_op.ToString() + "\n";
                              f.SetTextSafe(st);
                              mute.ReleaseMutex();
                              n = SCDelay;
                              //  }
                              //   catch { };
                              if (nod != null)
                                  while (nod.operations_need > 0)
                                  {
                                      nod.operations_need -= process_operations;
                                      mute.WaitOne();
                                      processors_theory_sum_max_speed += process_operations;
                                      processors_real_sum_max_speed += process_operations;
                                      processors_sum_speed_for_period += process_operations;
                                      //time_counter++;
                                      st = thread.Name + " " + nod.number + "\n";
                                      f.SetTextSafe(st);
                                      if (time_counter >= 5000)
                                      {
                                          time_counter = 0;
                                          d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                          d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                          f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                          processors_sum_speed_for_period = 0;
                                          processors_theory_sum_max_speed = 0;
                                          processors_real_sum_max_speed = 0;
                                      }
                                      else time_counter++;
                                      mute.ReleaseMutex();
                                      Thread.Sleep(n);
                                  }
                              else
                              {
                                  mute.WaitOne();
                                  processors_theory_sum_max_speed += process_operations;
                                  processors_real_sum_max_speed += process_operations;
                                  //time_counter++;
                                  if (time_counter >= 5000)
                                  {
                                      time_counter = 0;
                                      d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                      d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                      f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                      processors_sum_speed_for_period = 0;
                                      processors_theory_sum_max_speed = 0;
                                      processors_real_sum_max_speed = 0;
                                  }
                                  else time_counter++;
                                  mute.ReleaseMutex();
                                  Thread.Sleep(n);
                              }
                              step_cout = 0;
                              while (step_cout != 4)
                              {
                                  step_cout++;
                                  mute.WaitOne();
                                  processors_theory_sum_max_speed += process_operations;
                                  // processors_real_sum_max_speed = processors_theory_sum_max_speed;
                                  //time_counter++;
                                  if (rand.Next(0, 100) <= chanse_of_new_task)
                                      List.add_new(1);
                                  nod = List.get_Task(thread.Name, 2);
                                  st = thread.Name + " is giving tasks \n"; //st = thread.Name + " " + proc_op.ToString() + "\n";
                                  f.SetTextSafe(st);
                                  if (time_counter >= 5000)
                                  {
                                      time_counter = 0;
                                      d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                      d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                      f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                      processors_sum_speed_for_period = 0;
                                      processors_theory_sum_max_speed = 0;
                                      processors_real_sum_max_speed = 0;
                                  }
                                  else time_counter++;
                                  n = SCDelay;
                                  mute.ReleaseMutex();
                                  Thread.Sleep(n);
                              }
                          }
                          //get task, sub operations from task's all operations
                      }
                  }

                  private void proc_funct2(object proc_op)
                  {
                      int process_operations = (int)proc_op;
                      nodeL nod;
                      //Boolean new_task;

                      string st = " ";

                      try
                      {
                          mute.WaitOne();
                          st = "started " + thread.Name + "\n";
                          f.SetTextSafe(st);
                          mute.ReleaseMutex();
                      }
                      catch { };
                      while (true)
                      {
                          mute.WaitOne();
                          processors_theory_sum_max_speed += process_operations;
                          // processors_real_sum_max_speed = processors_theory_sum_max_speed;
                          //time_counter++;
                          if (rand.Next(0, 100) <= chanse_of_new_task)
                              List.add_new(1);
                          nod = List.get_Task(thread.Name, 2);
                          st = thread.Name + " is giving tasks \n"; //st = thread.Name + " " + proc_op.ToString() + "\n";
                          f.SetTextSafe(st);
                          if (time_counter >= 5000)
                          {
                              time_counter = 0;
                              d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                              d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                              f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                              processors_sum_speed_for_period = 0;
                              processors_theory_sum_max_speed = 0;
                              processors_real_sum_max_speed = 0;
                          }
                          else time_counter++;
                          n = SCDelay;
                          mute.ReleaseMutex();
                          Thread.Sleep(n);
                      }
                  }

                  private void proc_funct(object proc_op)
                  {
                      int process_operations = (int)proc_op;
                      nodeL nod;
                      //Boolean new_task;

                      string st = " ";

                      try
                      {
                          mute.WaitOne();
                          st = "started " + thread.Name + "\n";
                          f.SetTextSafe(st);
                          mute.ReleaseMutex();
                      }
                      catch { };


                      while (true)
                      {
                          //  try
                          // {
                          mute.WaitOne();
                          //time_counter++;
                          if (rand.Next(0, 100) <= chanse_of_new_task)
                              List.add_new(1);
                          nod = List.get_Task(thread.Name,2);
                          if (nod != null)
                          {
                              st = thread.Name + " " + nod.number + "\n";
                          }
                          else st = thread.Name + " is waiting for tasks \n"; //st = thread.Name + " " + proc_op.ToString() + "\n";
                          f.SetTextSafe(st);
                          mute.ReleaseMutex();
                          n = SCDelay;
                          //  }
                          //   catch { };
                          if (nod != null)
                              while (nod.operations_need > 0)
                              {
                                  nod.operations_need -= process_operations;
                                  mute.WaitOne();
                                  processors_theory_sum_max_speed += process_operations;
                                  processors_real_sum_max_speed += process_operations;
                                  processors_sum_speed_for_period += process_operations;
                                  //time_counter++;
                                  st = thread.Name + " " + nod.number + "\n";
                                  f.SetTextSafe(st);
                                  if (time_counter >= 5000)
                                  {
                                      time_counter = 0;
                                      d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                      d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                      f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                      processors_sum_speed_for_period = 0;
                                      processors_theory_sum_max_speed = 0;
                                      processors_real_sum_max_speed = 0;
                                  }
                                  else time_counter++;
                                  mute.ReleaseMutex();
                                  Thread.Sleep(n);
                              }
                          else
                          {
                              mute.WaitOne();
                              processors_theory_sum_max_speed += process_operations;
                              processors_real_sum_max_speed += process_operations;
                              //time_counter++;
                              if (time_counter >= 5000)
                              {
                                  time_counter = 0;
                                  d1 = (processors_sum_speed_for_period / (double)processors_theory_sum_max_speed) * 100;
                                  d2 = (processors_sum_speed_for_period / (double)processors_real_sum_max_speed) * 100;
                                  f.OutKPD(((int)d1).ToString(), ((int)d2).ToString());
                                  processors_sum_speed_for_period = 0;
                                  processors_theory_sum_max_speed = 0;
                                  processors_real_sum_max_speed = 0;
                              }
                              else time_counter++;
                              mute.ReleaseMutex();
                              Thread.Sleep(n);
                          }
                          //get task, sub operations from task's all operations
                      }
                  }
              }


      
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                bb = true;

                min_proc_speed = Convert.ToInt32(textBox1.Text);
                max_proc_speed = Convert.ToInt32(textBox1.Text);

                if (min_proc_speed > Convert.ToInt32(textBox2.Text))
                    min_proc_speed = Convert.ToInt32(textBox2.Text);
                else if(max_proc_speed < Convert.ToInt32(textBox2.Text))
                        max_proc_speed = Convert.ToInt32(textBox2.Text);
                if (min_proc_speed > Convert.ToInt32(textBox3.Text))
                    min_proc_speed = Convert.ToInt32(textBox3.Text);
                else if (max_proc_speed < Convert.ToInt32(textBox3.Text))
                    max_proc_speed = Convert.ToInt32(textBox3.Text);
                if (min_proc_speed > Convert.ToInt32(textBox4.Text))
                    min_proc_speed = Convert.ToInt32(textBox4.Text);
                else if (max_proc_speed < Convert.ToInt32(textBox4.Text))
                    max_proc_speed = Convert.ToInt32(textBox4.Text);
                if (min_proc_speed > Convert.ToInt32(textBox5.Text))
                    min_proc_speed = Convert.ToInt32(textBox5.Text);
                else if (max_proc_speed < Convert.ToInt32(textBox5.Text))
                    max_proc_speed = Convert.ToInt32(textBox5.Text);

/*                processors_theory_sum_max_speed = (Convert.ToInt32(textBox1.Text) +
                    Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) +
                    Convert.ToInt32(textBox4.Text) + Convert.ToInt32(textBox5.Text)) * 6;*/

                time_counter = 0;
                processors_sum_speed_for_period = 0;

                List = new myList(this, Convert.ToInt32(textBox14.Text));
                List.print_out();

                if (radioButton1.Checked == true)
                {
 //                   processors_real_sum_max_speed = processors_theory_sum_max_speed;
                    //myThread t1, t2, t3, t4, t5;
                    //vari = new varib(1);
                    t1 = new myThread(this, 1, Convert.ToInt32(textBox1.Text));
                    t2 = new myThread(this, 2, Convert.ToInt32(textBox2.Text));
                    t3 = new myThread(this, 3, Convert.ToInt32(textBox3.Text));
                    t4 = new myThread(this, 4, Convert.ToInt32(textBox4.Text));
                    t5 = new myThread(this, 5, Convert.ToInt32(textBox5.Text));
                }
                else if (radioButton2.Checked == true)
                {
                   // myThread2 t1, t2, t3, t4, t5;
                    t6 = new myThread2(this, 1, Convert.ToInt32(textBox1.Text),2);
                    t7 = new myThread2(this, 2, Convert.ToInt32(textBox2.Text),2);
                    t8 = new myThread2(this, 3, Convert.ToInt32(textBox3.Text),2);
                    t9 = new myThread2(this, 4, Convert.ToInt32(textBox4.Text),2);
                    t10 = new myThread2(this, 5, Convert.ToInt32(textBox5.Text),2);
                }
                else if ((radioButton3.Checked == true)) //|| (radioButton4.Checked == true))
                {
                    steps = Convert.ToInt32(textBox22.Text);
                    t11 = new myThread2(this, 1, Convert.ToInt32(textBox1.Text),3);
                    t12 = new myThread2(this, 2, Convert.ToInt32(textBox2.Text),3);
                    t13 = new myThread2(this, 3, Convert.ToInt32(textBox3.Text),3);
                    t14 = new myThread2(this, 4, Convert.ToInt32(textBox4.Text),3);
                    t15 = new myThread2(this, 5, Convert.ToInt32(textBox5.Text),3);
                }
                else
                {
                    /*t16 = new myThread2(this, 1, Convert.ToInt32(textBox1.Text));
                    t17 = new myThread2(this, 2, Convert.ToInt32(textBox2.Text));
                    t18 = new myThread2(this, 3, Convert.ToInt32(textBox3.Text));
                    t19 = new myThread2(this, 4, Convert.ToInt32(textBox4.Text));
                    t20 = new myThread2(this, 5, Convert.ToInt32(textBox5.Text));*/
                }
            }
            catch { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();

                bb = true;

                /*t6.thr_stop();
                t7.thr_stop();
                t8.thr_stop();
                t9.thr_stop();
                t10.thr_stop();*/

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

            }
            catch
            {
            }

            try
            {
                t6.thr_stop();
                t7.thr_stop();
                t8.thr_stop();
                t9.thr_stop();
                t10.thr_stop();

                bb = true;

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

            }
            catch
            {
            }
            
            try
            {
                t11.thr_stop();
                t12.thr_stop();
                t13.thr_stop();
                t14.thr_stop();
                t15.thr_stop();

                bb = true;

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

                /*t1.thr_stop();
                t2.thr_stop();
                t3.thr_stop();
                t4.thr_stop();
                t5.thr_stop();*/

            }
            catch
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rand = new Random();
            SCDelay = trackBar1.Value;
            textBox12.Text = "Delay: " + trackBar1.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SCDelay = trackBar1.Value;
            textBox12.Text = "Delay: " + trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nodeL nod = new nodeL(Convert.ToInt32(textBox16.Text), 
                Convert.ToInt32(checkBox1.Checked),
                Convert.ToInt32(checkBox2.Checked), 
                Convert.ToInt32(checkBox3.Checked),
                Convert.ToInt32(checkBox4.Checked),
                Convert.ToInt32(checkBox5.Checked));
            List.add_new(nod);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List.print_out();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
               // radioButton4.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
               // radioButton4.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
               // radioButton4.Checked = false;
            }
        }

      /*  private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
        }*/

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            chanse_of_new_task = trackBar2.Value;
            textBox23.Text = "Chanse: " + trackBar2.Value.ToString();
        }
    }
}
