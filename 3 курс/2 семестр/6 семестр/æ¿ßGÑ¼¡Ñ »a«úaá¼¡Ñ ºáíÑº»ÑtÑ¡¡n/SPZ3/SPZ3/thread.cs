using System;

namespace SPZ3
{
    public class thread
    {
        public int PerfQaunt; 
        public thread Next;
        public DateTime dt;
        public TimeSpan ts;
        public int q_len;

        public thread()
        {
            PerfQaunt = 0;
            Next = null;
            dt = new DateTime();
            ts = new TimeSpan();
            q_len = 0;
        }

        public thread(thread obj)
        {
            Next = null;
            PerfQaunt = obj.PerfQaunt;
            dt = obj.dt;
            ts = obj.ts;
            q_len = obj.q_len;
        }

    }
}