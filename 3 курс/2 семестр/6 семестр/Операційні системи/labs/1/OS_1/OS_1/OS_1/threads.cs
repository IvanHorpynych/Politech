using System;

namespace OS_1
{
    public class queve
    {
        public thread head;
        public thread tail;

        public queve()
        {
            head = null;
            tail = null;
        }

       public void push_back(thread ptr)
        {
            

            if (head == null)
            {
                head = ptr;
                tail = head;
                return;
            }
            tail.Next = ptr;
            tail = ptr;
            tail.Next = null;
            return;
        }

        public void pop_front(queve qu)
        {
            thread ptr = head;

            if (head == null)
                return;

            
                head = head.Next;
                if (head == null)
                    tail = null;
            

            ptr.Next = null;
            qu.push_back(ptr);
            return;
        }

        public void pop ( int num )
        {
            thread prev = head, ptr=prev.Next;
            if (num == 0)
            {
                pop_front(this);
                return;
            }

            for (int i = 1; i < num && ptr != null; i++)
            {
                prev = prev.Next;
                ptr = prev.Next;
            }

            if ( ptr == null )
                return;
            prev.Next = ptr.Next;
            ptr.Next = null;
            ptr = null;
            return;
        }
    }
}