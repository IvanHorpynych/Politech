using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OS3
{
   class MemCell
    {
        public int num_req;
        public int addr;
        public int cash_addr;
        public int size;
        public int val;
        public bool modified;
        
        public MemCell()
        {
            cash_addr = -1;
            addr = -1;
            modified = false;
            val = -1;
        }

        public MemCell(MemCell mc)
        {
            this.addr = mc.addr;
            this.cash_addr = mc.cash_addr;
            this.val = mc.val;
            this.modified = mc.modified;
        }
    }

    class Mems
    {
        int mem_size;
        int cash_size;
        int capacity;
        int amount;
        MemCell[] main_memory;
        MemCell[] cash_memory;

        public Mems()
        {
            capacity = 16;
            mem_size = 2048;
            main_memory = new MemCell[mem_size / capacity];
            Console.WriteLine("number is {0}", mem_size / capacity);
            for (int i = 0; i < main_memory.Length; i++)
            {
                main_memory[i] = new MemCell();
                main_memory[i].addr = i * capacity;
                main_memory[i].val = i;
            }

            cash_size = 256;
            cash_memory = new MemCell[cash_size / capacity];
            Console.WriteLine("cash number is {0}", cash_size / capacity);
            for (int i = 0; i < cash_memory.Length; i++)
            {
                cash_memory[i] = new MemCell();
                cash_memory[i].addr = -1;
                cash_memory[i].cash_addr = i * capacity;
            }

            amount = mem_size / cash_size;
        }


        public void PushToCash(MemCell cell)       //input data to cash memory
        {
            int idx = cell.addr / (capacity*amount);
            if (cash_memory[idx].addr > 0 && cash_memory[idx].modified ==  true )
            {
                cash_memory[idx].modified = false;
                main_memory[cash_memory[idx].addr / capacity].val = cash_memory[idx].val;
            }

            cash_memory[idx].addr = cell.addr;
            cash_memory[idx].val = cell.val;
        }


        public bool DCHangeInCash(int value, int addr)
        {
            int idx = addr / (capacity*amount);

            if (cash_memory[idx].addr == addr)
            {
                cash_memory[idx].val = value;
                cash_memory[idx].addr = addr;
                cash_memory[idx].modified = true;
                return true;
            }
            return false;
        }

        public int ReadFromCash(int addr)
        {
            int idx = addr / (capacity * amount);

            if (cash_memory[idx].addr == addr)
                return cash_memory[idx].val;
            
            return -1;
        }

        public void ReadData(int addr)
        {
            int num;
            if ( ( num = ReadFromCash(addr)) > 0)
            {
                Console.WriteLine("Кеш попадання\nRead from cash {0}", num);
                return;
            }
            int idx = addr / capacity;
            Console.WriteLine("Кеш промах\nRead data from memory {0}", main_memory[idx].val);
            PushToCash(main_memory[idx]);
            Console.WriteLine("added new record to cash\n");
        }

        public void ChangeData(int value, int addr)
        {
            if (DCHangeInCash(value, addr))
            {
                Console.WriteLine("Кеш попадання");
                return;
            }
            Console.WriteLine("Кеш промах");
            int idx = addr / capacity;
            PushToCash(main_memory[idx]);
            Console.WriteLine("added new record to cash\n");
        }

        public void PrintCash()
        {
            for (int i = 0; i < cash_size / capacity; i++)
                Console.WriteLine("cash addr {0}  addr {1} val  {2}  modified {3}", cash_memory[i].cash_addr, cash_memory[i].addr, cash_memory[i].val, cash_memory[i].modified);
        }

        public void PrintMem()
        {
            for (int i = 0; i < mem_size / capacity; i++)
                Console.WriteLine("addr {0}  val  {1}  ", main_memory[i].addr, main_memory[i].val);
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Mems memory = new Mems();
            memory.PrintMem();
            memory.ReadData(128);
            memory.ReadData(544);
            memory.ReadData(32);
            memory.PrintCash();
            memory.ChangeData(678, 128);
            memory.ReadData(128);
            memory.ReadData(144);
            memory.PrintCash();
            memory.PrintMem();
        }
    }
}
