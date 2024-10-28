// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server()
{
    private static ArrayList socketList;
    private static ASCIIEncoding asen = new ASCIIEncoding();

    public static void Main(string[] args)
    {
        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 8001);
            tcpListener.Start();

            socketList = new ArrayList();


            while (true)
            {
                Socket socket = tcpListener.AcceptSocket();
                Thread socketThread = new Thread(new ParameterizedThreadStart(newSocket));
                socketThread.Start(socket);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void newSocket(Object object1)
    {
        Socket socket = object1 as Socket;

        byte[] b = new byte[100];
        int k = socket.Receive(b);
        String name = getUsername(System.Text.Encoding.ASCII.GetString(b));

        socket.Send(asen.GetBytes("chat history")); //TODO moet nog een chat history worden

        socketList.Add(socket);

        while (true)
        {
            b = new byte[100];
            k = socket.Receive(b);
            String message = "message |" + name + ": " + System.Text.Encoding.ASCII.GetString(b);
            sendMessageToAll(socket, message);
        }
    }

    public static void sendMessageToAll(Socket socket, String message)
    {
        foreach (Socket socket1 in socketList)
        {
            if (socket1 != socket)
            {
                socket1.Send(asen.GetBytes(message));
            }
        }
    }

    public static String getUsername(String message)
    {
        String[] strings = message.Split('|');
        return strings[1];
}


