using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ClientV3
{
    class Program
    {
        static void Main(string[] args)
        {

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server;
            byte[] data, receivedData;
            int recv;
            String address;
            int sizeOfFile = 0;
            while (true)
            {
                Console.WriteLine("Введiть адресу файлу:");
                address = Console.ReadLine();
                if (address.Length == 0)
                    continue;
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipep);
                data = new byte[128];
                data = Encoding.UTF8.GetBytes(address);
                server.Send(data);
                data = new byte[16];
                server.Receive(data);
                string status = Encoding.UTF8.GetString(data);
                status = status.Substring(0, status.IndexOf('\0'));
                if (status == "Error!")
                {
                    Console.WriteLine(status);
                    continue;
                }
                sizeOfFile = int.Parse(Encoding.UTF8.GetString(data));
                receivedData = new byte[sizeOfFile];
                server.Send(Encoding.UTF8.GetBytes("YES"));
                recv = 0;
                while (recv < sizeOfFile)
                {
                    recv = server.Receive(receivedData);
                    if (recv == 0)
                        break;
                }
                Console.WriteLine("Вiдповiдь iз сервера:");
                Console.WriteLine(Encoding.UTF8.GetString(receivedData, 0, receivedData.Length));
                server.Close();
            }
        }
    }
}
