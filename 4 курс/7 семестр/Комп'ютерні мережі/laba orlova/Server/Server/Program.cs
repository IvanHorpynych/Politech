using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            //Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.Resolve("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
            //Создаем сокет TCP\IP
            Socket sListener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            //Назначаем сокет локальной конечной точку
            // и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                //Начинаем слущать соединения 

                while (true)
                {
                    Console.WriteLine("Waiting for connections... ", ipEndPoint);
                    //программа приостанавливается,ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;
                    do
                      {

                      data = null;
                        //дождались клиента,пытающегося с нами соединиться  
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        //выводим данные на консоль
                        Console.WriteLine("Сlient Message : {0}", data);
                        
                        string theReply = "Received " + data + "\n";
                        theReply += "got message " + data + "\n";
                        byte[] msg = Encoding.ASCII.GetBytes(theReply);
                        handler.Send(msg);
                    } while (data != "exit");
                    Console.WriteLine("Disconected\n");
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
