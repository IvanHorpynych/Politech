using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace OS_1
{
    
    class Program
    {
        queve mainq,rest, _all; //mainq - потоки, які стоять в черзі, _rest - виконані потоки
        Timer NewReq, PerformRequ; //NewReq - посилаэ в чергу новий потік, PerformRequ - Виконання потоку
        int quant = 1000, perf_proc = 0, new_proc = 1;
        DateTime[] StartTime;
        DateTime[] FinTime;

        void Fill( int num, int[] exp, int[] perf )
        {
            _all = new queve();
            mainq = new queve();
            rest = new queve();
            StartTime = new DateTime[num];
            FinTime = new DateTime[num];

            thread ptr = new thread(exp[0], perf[0]);
            _all.push_back(ptr);
            Console.WriteLine("First process with performing time {0}", _all.head.TimePerf);

            for (int i = 1; i < num; i++)
            {
                ptr = new thread(exp[i]-exp[i-1], perf[i]);
                _all.push_back(ptr);
            }

           // for (ptr = _all.head; ptr != null; ptr = ptr.Next)
           //     Console.WriteLine("coming time {0}, Perform time {1}", ptr.TimeExp, ptr.TimePerf);
           // Console.WriteLine("head {0} tail {1}", _all.head.TimeExp, _all.tail.TimeExp);
        }

       
        public void scheduler () 
        {
            try
            {

                _all.pop_front(mainq);
                //StartTime[0] = DateTime.Now;
                mainq.head.TimeFin = mainq.head.TimePerf;

                PerformRequ = new Timer();
                PerformRequ.Elapsed += new ElapsedEventHandler(Perfreq);
                PerformRequ.Interval = mainq.head.TimePerf;
                PerformRequ.Enabled = true;
                PerformRequ.Start();

                NewReq = new Timer();
                NewReq.Elapsed += new ElapsedEventHandler(GetNewReq);
                NewReq.Interval = _all.head.TimeExp;
                NewReq.Enabled = true;
                NewReq.Start();

                while (mainq.head != null)
                {
                    // do nothing...
                }


                Console.WriteLine("Incoming     Performing  Finish  Delay");

                for (thread ptr = rest.head; ptr != null; ptr = ptr.Next)
                    ptr.PrintRes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        static void Main(string[] args)
        {
            Random rnd = new Random();
            int num_threads = 10;
            int[] exp = new int[num_threads];
            int[] perf = new int[num_threads];

            for (int i = 0; i < num_threads; i++)
            {
                exp[i] = rnd.Next(100, 3000);
                perf[i] = rnd.Next(100, 3000);
            }
            exp[0] = 0;
            Array.Sort(exp);
            

           
            Program obj = new Program();
            obj.Fill(num_threads, exp, perf);
            obj.scheduler();
        }

        public void GetNewReq (object source, ElapsedEventArgs e)
        {
            NewReq.Stop();

            if (_all.head == null)
            {
                Console.WriteLine("No more processes");
                return;
            }

            thread ptr = new thread(_all.head);
            Console.WriteLine("added new process, upcoming time {0}", ptr.TimeExp);
            //mainq.push_back(ptr);
           // StartTime[new_proc] = DateTime.Now;
            _all.pop_front(mainq);
            
            new_proc++;

            NewReq.Start();
        }

        public void Perfreq(object source, ElapsedEventArgs e)
        {
            PerformRequ.Stop();

            if (mainq.head == null && _all.head != null)
            {
                PerformRequ.Interval = quant;
                Console.WriteLine("waiting for process");
                PerformRequ.Start();
                return;
            }

            if (mainq.head == null)       
                return;
           
            if (mainq.head != mainq.tail)
            {
                mainq.head.Next.TimeFin = mainq.head.TimeFin + mainq.head.Next.TimePerf;
                mainq.head.Next.TimeExp += mainq.head.TimeExp;
                mainq.head.Next.TimeDelay = mainq.head.TimeFin - mainq.head.Next.TimeExp;
            }


            mainq.pop_front(rest);
            PerformRequ.Interval = mainq.head.TimePerf;
            Console.WriteLine("Perfoming process with performing time {0}", mainq.head.TimePerf);
            perf_proc++;

            PerformRequ.Start();
        }
    }
}
