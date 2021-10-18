using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;
using System.Text.Encodings;
namespace ArduinoServerGit
{
    public class Server
    {
        const int port = 5000;// ethernet port;
        IPHostEntry hostEntry;//host for constent adress;
        IPAddress IP;//IP
        TcpClient client; //Client
        TcpListener listen;//Listener for clients
        NetworkStream nwStream;
        Thread R;
        string DataToSend;
        bool isListening = true;
        int bufferSize;

        public Server()
        {

            hostEntry = Dns.GetHostEntry("Your host name");
            IP = hostEntry.AddressList[1];
            Console.WriteLine(IP);
            listen = new TcpListener(IP, port);
            client = new TcpClient();
            Run();

        }
        public void Run()
        {
            listen.Start();
            while (isListening == true)
            {
                while (listen.Pending())
                {
                    client = listen.AcceptTcpClient();
                }
                if (client.Connected)
                {
                    try
                    {
                        byte[] data;
                        nwStream = client.GetStream();
                        DataToSend = Console.ReadLine();
                        data = Encoding.ASCII.GetBytes(DataToSend);
                        nwStream.Write(data, 0, data.Length);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
    }
}
