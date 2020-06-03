using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerV1
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
            List<string> parsedData;
            int first, second;
            String result = "";
            String errorString = "Error!";            
            while (true)
            {
                Console.WriteLine("Очiкування клiєнта");
                Socket client = newSocket.Accept();
                IPEndPoint clientEp = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine("З'єднано з {0}, порт {1}", clientEp.Address, clientEp.Port);                
                data = new byte[256];
                recv = client.Receive(data);
                                
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                parsedData = parser(data);
                if (parsedData == null)
                {
                    result = errorString;
                }
                else
                {
                    first = int.Parse(parsedData[0]);
                    second = int.Parse(parsedData.Last());
                    switch (parsedData[1][0])
                    {
                        case '*': result = (first * second).ToString();
                            break;

                        case '/':
                            {
                                if (second == 0)
                                {
                                    result = errorString;
                                    break;
                                }
                                result = (first / second).ToString();
                            }
                            break;

                        case '+': result = (first + second).ToString();
                            break;

                        case '-': result = (first - second).ToString();
                            break;
                    }
                }

                client.Send(Encoding.UTF8.GetBytes(result), result.Length, SocketFlags.None);
                Console.WriteLine("Вiдєднуємося вiд клiєнта");
                client.Close();
            }
        }
        private static List<string> parser(byte[] data)
        {
            if (data == null)
                return null;
            if (data.Length < 3)//мається на увазі, що мінімальний вираз може бути типу 5*6
                return null;
            String digit = "dg", operation = "op";
            String state = digit;
            List<string> parsed = new List<string>();
            String str = Encoding.UTF8.GetString(data, 0, data.Length);
            str.Trim();
            String tmp = "";
            foreach (Char ch in str)
            {
                if (Char.IsDigit(ch))
                {
                    tmp += ch;
                    state = digit;
                }
                else if (ch == '*' || ch == '/' || ch == '+' || ch == '-')
                {
                    state = operation;
                    if (tmp.Length == 0)
                        return null;
                    parsed.Add(tmp);                    
                    tmp = "";
                    parsed.Add(ch.ToString());
                }
                else if (Char.IsSeparator(ch))
                    continue;
                    else if (ch == '\0')
                        break;
                else return null;
            }
            if (state == digit && tmp.Length > 0)
                parsed.Add(tmp);
            if (parsed.Count != 3)
                return null;
            return parsed;
        }
    }
}
