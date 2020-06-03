using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace SClient
{
    public class SClient
    {
        public static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            //Соединяемся с удаленным устройством
            try
            {
                //Устанавливаем удаленную конечную точку для сокета
                IPHostEntry ipHost = Dns.Resolve("127.0.01");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //Соединяем сокет с удаленной конечной точкой
                sender.Connect(ipEndPoint);
                Console.WriteLine("Connection... {0}",
                    sender.RemoteEndPoint.ToString());
                //otpravka soobshcnenia
                string theMessage = null;
                do
                {
                    theMessage = Console.ReadLine();
                    byte[] msg = Encoding.ASCII.GetBytes(theMessage);
                    //отправляем данные через сокет
                    int bytesSent = sender.Send(msg);
                    //Получаем ответ от удаленного устройства
                    int bytesRec = sender.Receive(bytes);

                    Console.WriteLine("Server says : {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                } while (theMessage != "exit");
                //Освобождаем сокет
                //zakruvaem soedininenie
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}