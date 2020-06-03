using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientV2
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server;
            byte[] data, receivedData;
            int recv;
            String string1, string2, type;
            UdpClient client = new UdpClient(5656);
            while (true)
            {
                    Console.WriteLine("\nВведiть перший рядок");
                string1 = Console.ReadLine();
                if (string1.Length == 0)
                    continue;
                Console.WriteLine("Введiть другий рядок");
                string2 = Console.ReadLine();
                if (string2.Length == 0)
                    continue;
                Console.WriteLine("Введiть типу операцii\n1 - конкантенацiя\n2 - пошук");
                type = Console.ReadLine();
                if (type.Length == 0)
                    continue;
                if (type == "1" || type == "2")
                {
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    server.Connect(ipep);
                    data = new byte[1024];
                    receivedData = new byte[128];
                    data = Encoding.UTF8.GetBytes(string1 + "\n" + string2 + "\n" + type + "\n");
                    server.SendTo(data, ipep);
                    recv = server.Receive(receivedData);
                    Console.WriteLine("Вiдповiдь iз сервера:");
                    Console.WriteLine(Encoding.UTF8.GetString(receivedData, 0, receivedData.Length));
                    server.Close();
                }
            }
        }
    }   
}
