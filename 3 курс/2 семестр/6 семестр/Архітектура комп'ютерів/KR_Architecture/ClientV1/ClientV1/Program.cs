using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientV1
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server;
            byte[] data, receivedData;
            int recv;
            String expression;
            while (true)
            {
                Console.WriteLine("Введiть вираз:");
                expression = Console.ReadLine();
                if (expression.Length == 0)
                    continue;
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipep);
                data = new byte [128];
                receivedData = new byte[128];
                data = Encoding.UTF8.GetBytes(expression);
                server.Send(data);               
                recv = server.Receive(receivedData);
                Console.WriteLine("Вiдповiдь iз сервера:");
                Console.WriteLine(Encoding.UTF8.GetString(receivedData, 0, receivedData.Length));
                server.Close();
            }
        }
    }
}
