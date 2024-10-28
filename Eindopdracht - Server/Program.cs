// See https://aka.ms/new-console-template for more information

using Eindopdracht___Server;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server()
{
    private static List<Socket> socketList;
    private static List<String> ChatHistory;
    private static JSONHandler JSONHandler;
    private static ASCIIEncoding asen = new ASCIIEncoding();

    public static void Main(string[] args)
    {
        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 8001);
            tcpListener.Start();

            socketList = new List<Socket>();
            ChatHistory = new List<String>();
            JSONHandler = new JSONHandler();

            List<String> list = new List<String>();
            list.Add("lol1");
            list.Add("lol2");
            list.Add("lol3");
            list.Add("lol4");
            list.Add("lol5");
            list.Add("lol6");
            JSONHandler.saveList(list);
            List<String> test = JSONHandler.readList();
            Console.WriteLine(test.Count);
            Console.WriteLine("[" + string.Join(",", test.ToArray()) + "]");


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

        socket.Send(asen.GetBytes(getHistory()));

        socketList.Add(socket);

        while (true)
        {
            b = new byte[100];
            k = socket.Receive(b);
            String message = name + ": " + System.Text.Encoding.ASCII.GetString(b);

            ChatHistory.Add(message);
            sendMessageToAll(socket, "message |" + message);
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

    public static String getHistory()
    {
        List<String> history = JSONHandler.readList();
        history.AddRange(ChatHistory);

        String result = "[" + string.Join(",", history.ToArray()) + "]";

        Console.WriteLine(result);

        return result;
    }
}


