using System;

namespace OS_1
{
    public class thread
    {
        public int TimeExp;
        public int TimePerf;
        public int TimeDelay;
        public int TimeFin;
        public thread Next;

        public thread()
        {
            TimeExp = 0;    //час надходження
            TimePerf = 0;  //час виконання
            TimeDelay = 0;  //час затримки
            TimeFin = 0;    //час завершення
            Next = null;
        }

        public thread(int exp, int perf)
        {
            TimeExp = exp;
            TimePerf = perf;
            TimeDelay = 0;
            TimeFin = 0;
            Next = null;
        }

        public thread(thread obj)
        {
            Next = null;
            TimeExp = obj.TimeExp;
            TimePerf = obj.TimePerf;
            TimeDelay = obj.TimeDelay;
            TimeFin = obj.TimeFin;
        }

        void addtime(int _time)
        {
            TimeDelay += _time;
        }

        public void PrintRes()
        {
            Console.WriteLine("{0}      {1}     {2}     {3}", TimeExp, TimePerf, TimeFin,TimeDelay);
        }
    }
}