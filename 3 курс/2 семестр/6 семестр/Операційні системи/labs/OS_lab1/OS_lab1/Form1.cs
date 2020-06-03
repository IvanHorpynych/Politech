using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace OS_lab1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            info.Clear();
            logger.Clear();
            
            List<Descriptor> initial = new List<Descriptor>();
            Queue<int> Q0 = new Queue<int>();
            Queue<int> Q1 = new Queue<int>();
            Queue<int> Q2 = new Queue<int>();

            int tiks_counter = 0;
            int process_count = (int)numericUpDown1.Value;

            // filling up initial
            int mod = 0;
            Descriptor ts_tmp;
            for (int i = 0; i < process_count; i++)
            {
                ts_tmp = new Descriptor(i, mod);
                initial.Add(ts_tmp);
                mod = ts_tmp.begin;
            }



            int new_proc = 0;
            int kvant0 = 50;
            int kvant1 = 100;
            int tmp;

            string output = "";

            while (tiks_counter <= 100000)
            { 
                // --------- scheduler emulation here ----------------------------------------------------------------------
                
                // 1) Adding new process to Q0 if it is time for it  
                if ((new_proc < process_count) && (initial[new_proc].begin == tiks_counter))
                {
                    output += "P" + initial[new_proc].id.ToString() + " START at " + tiks_counter.ToString() + "\n";
                    Q0.Enqueue(initial[new_proc].id);
                    new_proc++;
                }

                //Q0 handling with quantum = 50
                if (Q0.Count != 0)
                    if (kvant0 != 0)
                    {
                        //there is kvant time. so first process gains -1 work todo, all other gain +1 delay

                        initial[Q0.Peek()].todo--;
                        output += "Q0: P" + initial[Q0.Peek()].id.ToString() + " -1  TODO:" + initial[Q0.Peek()].todo.ToString() + "  k0:" + kvant0.ToString() + "  d:" + initial[Q0.Peek()].delay.ToString() + "\n";

                        foreach (int i in Q0) initial[i].delay++;
                        initial[Q0.Peek()].delay--;

                        foreach (int i in Q1) initial[i].delay++;
                        foreach (int i in Q2) initial[i].delay++;

                        kvant0--;

                        //but if first process has already done his work we delete it from q0 and set kvant0 to 50.
                        if (initial[Q0.Peek()].todo == 0)
                        {
                            output += "Q0: P" + initial[Q0.Peek()].id.ToString() + " FINISH at " + tiks_counter.ToString() + "\n";
                            initial[Q0.Peek()].end = tiks_counter;
                            Q0.Dequeue();
                            kvant0 = 50;
                        }
                    }
                    else
                    {
                        //kvant = 0. so the first process in Q0 hadn't done his work in kvant, moving it to Q1
                        output += "P" + initial[Q0.Peek()].id.ToString() + " Q0 --> Q1\n";
                        Q1.Enqueue(Q0.Dequeue());
                        tiks_counter--; //considering that this operation doesn't take any time.
                        kvant0 = 50;
                    }
                else // if q0 is empty we go here
                {
                    if (Q1.Count != 0)
                        if (kvant1 != 0)
                        {
                            //there is kvant time. so first process gains -1 work todo, all other gain +1 delay

                            initial[Q1.Peek()].todo--;
                            output += "Q1: P" + initial[Q1.Peek()].id.ToString() + " -1  TODO:" + initial[Q1.Peek()].todo.ToString() + "  k1:" + kvant1.ToString() + "  d:" + initial[Q1.Peek()].delay.ToString() + "\n";

                            foreach (int i in Q1) initial[i].delay++;
                            initial[Q1.Peek()].delay--;

                            foreach (int i in Q2) initial[i].delay++;

                            kvant1--;

                            //but if first process has already done his work we delete it from q1 and set kvant1 to 100.
                            if (initial[Q1.Peek()].todo == 0)
                            {
                                output += "Q1: P" + initial[Q1.Peek()].id.ToString() + " FINISH at " + tiks_counter.ToString() + "\n";
                                initial[Q1.Peek()].end = tiks_counter;
                                Q1.Dequeue();
                                kvant1 = 100;
                            }
                        }
                        else
                        {
                            //kvant = 0. so the first process in Q1 hadn't done his work in kvant, moving it to Q2
                            output += "P" + initial[Q1.Peek()].id.ToString() + " Q1 --> Q2\n";
                            Q2.Enqueue(Q1.Dequeue());
                            tiks_counter--; //considering that this operation doesn't take any time.
                            kvant1 = 100;
                        }
                    else // and now, all queues 0 and 1 are empty, we can execute the 2 queue
                    {
                        if (Q2.Count != 0)
                        {
                            initial[Q2.Peek()].todo--;
                            output += "Q2: P" + initial[Q2.Peek()].id.ToString() + " -1  TODO:" + initial[Q2.Peek()].todo.ToString() + "  d:" + initial[Q2.Peek()].delay.ToString() + "\n";

                            //foreach (int i in Q1) initial[Q1.Peek()].delay++;

                            //foreach (int i in Q0) initial[Q0.Peek()].delay++;
                            foreach (int i in Q2) initial[i].delay++;
                            initial[Q2.Peek()].delay--;

                            //but if first process has already done his work we delete it from q1 and set kvant1 to 100.
                            if (initial[Q2.Peek()].todo == 0)
                            {
                                output += "Q2: P" + initial[Q2.Peek()].id.ToString() + " FINISH at " + tiks_counter.ToString() + "\n";
                                initial[Q2.Peek()].end = tiks_counter;
                                Q2.Dequeue();
                            }
                        }
                    }
                }
                tiks_counter++;
            }

            //debug output + correct formatting
            double sum = 0;
            foreach (Descriptor t in initial)
            {
                info.Text += string.Format(" {0,2} {1,5}   {2,5}   {3,5}   {4,5}\n", t.id, t.begin.ToString(), t.length.ToString(), t.end.ToString(), t.delay.ToString());
                sum += t.delay;
            }
            sum = sum / initial.Count;

            logger.Text = output;
            info.Text += "Середній час очікування: " + sum.ToString();
        }

    }
    //============================================  R A N D O M 
    static class rnd
    {
        public static Random r = new Random();
    }
    //============================================  T H R E A D   S T A T S
    class Descriptor
    {
        public Descriptor(int i, int modifier)
        {
            id = i;

            begin = rnd.r.Next(150) + modifier+1;
            length = rnd.r.Next(200)+ 1;
            todo = length;

        }

        public int id;
        public int begin;
        public int length;
        public int todo;
        public int end = 0;
        public int delay = 0;

    }

}
