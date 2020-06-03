using System;

namespace OS_1
{
    public class queve
    {
        thread head;
        thread tail;

        public queve()
        {
            head = null;
            tail = null;
        }

        void push_back(thread ptr)
        {
            if (head == tail)
            {
                head = ptr;
                tail = head;
                return;
            }
            tail.Next = ptr;
            tail = ptr;
            return;
        }

        void pop_front(queve qu)
        {
            thread ptr = head;
            if ( head.Next != null )
                head = head.Next;
        }
    }
}