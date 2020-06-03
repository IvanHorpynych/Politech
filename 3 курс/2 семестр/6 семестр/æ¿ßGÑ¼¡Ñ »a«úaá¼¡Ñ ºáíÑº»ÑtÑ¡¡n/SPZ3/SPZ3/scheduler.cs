using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;

namespace SPZ3
{
    class scheduler
    {
        public thread head;
        public thread tail;
        public int min_lim;
        public int max_lim;
        public int qu_len;
        private int cons_qu;
        public Timer Consumer;
        public Timer Producer;

        static List<thread> Current = new System.Collections.Generic.List<thread>();
        static List<thread> Thrown = new System.Collections.Generic.List<thread>();
        static List<thread> Performed = new System.Collections.Generic.List<thread>();
        public scheduler()
        {
            head = null;
            tail = null;
            min_lim = 8;
            max_lim = 20;
            cons_qu = 1;
            qu_len = 0;
        }

        public void push_back(thread ptr)
        {
            thread buf = new thread(ptr);
            buf.q_len = qu_len;
            if (head == null)
            {
                head = buf;
                tail = head;
                cons_qu = ptr.PerfQaunt;
                return;
            }
            tail.Next = buf;
            tail = buf;
            tail.Next = null;
            return;
        }

        public void pop_front()
        {
            thread ptr = head;

            if (qu_len == 0)
                return;

            head = head.Next;
            ptr.Next = null;
            qu_len--;
            if (head == null)
            {
                tail = null;
                return;
            }
            cons_qu = head.PerfQaunt;
            return;
        }

        public void GenerateQueue ()
        {
            for (int i = 0; i < Thrown.ToArray().Length; i++)
                Current.Add(Thrown.ToArray()[i]);
            Thrown.Clear();
            Random rnd = new Random();
            int cnt = rnd.Next(1, 4);
            for (int i = 0; i < cnt; i++)
            {
                thread ptr = new thread();
                ptr.PerfQaunt = rnd.Next(1,4);
                ptr.dt = DateTime.Now;
                Current.Add(ptr);
            }
            
        }

        public void ThrowPackage(int mode)
        {
            List<thread> buf = new System.Collections.Generic.List<thread>();
            for (int i = 1; i < Current.ToArray().Length; i++)
                buf.Add(Current.ToArray()[i]);
            thread ptr = Current.First();
            if (mode == 1)
                Thrown.Add(ptr);
            Current.Clear();
            for (int i = 0; i < buf.ToArray().Length; i++)
                Current.Add(buf.ToArray()[i]);
        }

        public void SchStart()
        {
            try
            {    
                Producer = new Timer();
                Producer.Elapsed += new ElapsedEventHandler(TimerProducer);
                Producer.Enabled = true;
                Producer.Interval = 1000;
                Producer.Start();

                Consumer = new Timer();
                Consumer.Elapsed += new ElapsedEventHandler(TimerConsumer);
                Consumer.Interval = 1000;
                Consumer.Start();

                while (Performed.ToArray().Length < 20 )
                {
                    //do nothing
                }
                Producer.Stop();
                Consumer.Stop();
                Console.Write("\n");
                StreamWriter fl_w = new StreamWriter(new FileStream("spz3.txt", FileMode.Create));
                for (int i = 0; i < Performed.ToArray().Length; i++)
                {
                    string str = Performed.ToArray()[i].q_len.ToString() + " " + Performed.ToArray()[i].ts.TotalMilliseconds.ToString();
                    fl_w.WriteLine(str);
                    Console.WriteLine("{0}  packages length {1}", Performed.ToArray()[i].ts.TotalMilliseconds, Performed.ToArray()[i].q_len);
                }
                fl_w.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PropAdd()
        {
            Random rnd = new Random();

            double val;
            if (qu_len <= min_lim)
                val = rnd.Next(0, min_lim);
            else
                val = rnd.Next(qu_len, max_lim);
            val = val - min_lim;

            if (val < 0)
                val = 0;

            double prop = val / (max_lim - min_lim);
            Console.Write("propability of adding process is {0:F4} - ", prop);
            if (prop > 0.6)
            {
                Console.Write("packet wasn't added. Current queue length {0}\n", qu_len);
                ThrowPackage(1);
                return;
            }

            thread ptr = new thread (Current.First());
            push_back(ptr);
            qu_len++;
            ThrowPackage(0);
            Console.WriteLine(" added new process  {0} with perf_quant {1}  ", qu_len,ptr.PerfQaunt); 
        }

        public void TimerProducer(object source, ElapsedEventArgs e)
        {
           
            Producer.Stop();

            GenerateQueue();
            for (int i = 0; i < Current.ToArray().Length; i++)
                PropAdd();
            Producer.Start();
            return;
        }

        public void TimerConsumer(object source, ElapsedEventArgs e)
        {
            Consumer.Stop();
            if (cons_qu > 0)
                cons_qu--;
            else
            {
                thread ptr = new thread(head);
                ptr.ts = DateTime.Now - ptr.dt;
                Performed.Add(ptr);
                pop_front();
                Console.WriteLine("router took process to perform, current queue length {0}", qu_len);
            }
            Consumer.Start();
        }

        static void Main(string[] args)
        {
            scheduler sch = new scheduler();
            sch.SchStart();
        }

    }
}
