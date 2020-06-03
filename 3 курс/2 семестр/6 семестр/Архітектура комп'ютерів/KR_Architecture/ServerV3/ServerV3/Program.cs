using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerV3
{
    class Program
    {
        static void Main(string[] args)
        {
            int recv;
            byte[] data;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newSocket.Bind(ipep);
            newSocket.Listen(10);
            String errorString = "Error!";
            while (true)
            {
                Console.WriteLine("Очiкування клiєнта");
                Socket client = newSocket.Accept();
                IPEndPoint clientEp = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine("З'єднано з {0}, порт {1}", clientEp.Address, clientEp.Port);
                data = new byte[256];
                recv = client.Receive(data);
                String address = Encoding.UTF8.GetString(data);
                String newstr = address.Substring(0, address.IndexOf('\0'));
                Console.WriteLine(newstr);
                try
                {
                    data = System.IO.File.ReadAllBytes(newstr);
                }
                catch (Exception e)
                {
                    client.Send(Encoding.UTF8.GetBytes(errorString));
                    Console.WriteLine(e.ToString());
                    continue;
                }
                //відправка розміру
                client.Send(Encoding.UTF8.GetBytes(data.Length.ToString()));
                //отривання звіту про доставку
                byte[] report = new byte[16];
                client.Receive(report);
                if (Encoding.UTF8.GetString(report).Substring(0, 3) == "YES")
                {
                    client.Send(data, data.Length, SocketFlags.None);
                }
                Console.WriteLine("Вiдєднуємося вiд клiєнта");
                client.Close();
            }
        }
    }
}
