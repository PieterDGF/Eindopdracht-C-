// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
try
{
    TcpListener tcpListener = new TcpListener(IPAddress.Any, 8001);
    tcpListener.Start();

    while (true)
    {
        Socket socket = tcpListener.AcceptSocket();
        Thread socketThread = new Thread(new ParameterizedThreadStart(newSocket));
        socketThread.Start();
    }
} catch (Exception e) 
{ 
    Console.WriteLine(e.ToString());
}

void newSocket(Object object1)
{
    //socket logt in
}



