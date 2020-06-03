using System;

namespace OS_1
{
    public class thread
    {
        int TimeExp;
        int TimePerf;
        thread Next;

        public thread()
        {
            TimeExp = 0;
            TimePerf = 0;
            Next = null;
        }

        void addtime(int _time)
        {
            TimeExp += _time;
        }

        void PrintRes()
        {
            Console.Write("Expecting time {0} Performing time {1} \n", TimeExp, TimePerf);
        }
    }
}