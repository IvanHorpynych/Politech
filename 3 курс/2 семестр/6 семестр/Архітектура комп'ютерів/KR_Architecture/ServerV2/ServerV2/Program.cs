using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerV2
{
    class Program
    {
        static void Main(string[] args)
        {            
            byte[] data;
            /*IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);*/
            Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            UdpClient listener = new UdpClient();
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 9050);



            List<string> parsedData;            
            String result = "";
            String errorString = "Error!";
            while (true)
            {
                Console.WriteLine("Очiкування клiєнта");
                /************************************************************/
                try
                {
                    Console.WriteLine("Waiting for broadcast");
                    listener = new UdpClient(9050);
                    data = listener.Receive(ref groupEP);
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), Encoding.ASCII.GetString(data, 0, data.Length));

                    parsedData = parser(data);
                    if (parsedData == null)
                        result = errorString;
                    else
                        if (parsedData.Last() == "2" || parsedData.Last() == "1")
                        {
                            switch (parsedData.Last()[0])
                            {
                                case '1': result = parsedData[0] + parsedData[1];
                                    break;

                                case '2': result = parsedData[0].IndexOf(parsedData[1], StringComparison.CurrentCulture).ToString();
                                    break;
                            }
                        }
                        else
                            result = errorString;
                    listener.Send(Encoding.UTF8.GetBytes(result), result.Length, groupEP);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    listener.Close();
                }
                /************************************************************/
            }
        }

        private static List<string> parser(byte[] data)
        {
            if (data == null)
                return null;
            String input = Encoding.UTF8.GetString(data);
            String tmp = "";
            List<string> parsed = new List<string>();
            int counter = 0;
            foreach (Char ch in input)
            {
                if (ch == '\n')
                {
                    parsed.Add(tmp);
                    tmp = "";
                    counter++;
                    if (counter == 3)
                        return parsed;
                    continue;
                }
                tmp += ch;
            }
            return null;

        }
    }
}
