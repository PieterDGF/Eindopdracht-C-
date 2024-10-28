// See https://aka.ms/new-console-template for more information

using Eindopdracht___Server;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

public class Server()
{
    private static List<Socket> socketList;
    private static List<String> ChatHistory;
    private static JSONHandler JSONHandler;
    private static ASCIIEncoding asen = new ASCIIEncoding();
    private static List<string> testData;
    private static System.Timers.Timer updateTimer;


    public static void Main(string[] args)
    {

        updateTimer = new System.Timers.Timer(1000);
        updateTimer.Elapsed += OnTimedEvent;
        updateTimer.AutoReset = true;
        updateTimer.Enabled = true;



        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 8001);
            tcpListener.Start();

<<<<<<< HEAD
            Console.WriteLine("Ik ben aan het luisteren");

            socketList = new ArrayList();
            testData = new List<string>();
            testData.Add("hoi");
            testData.Add("doei");
            testData.Add("hoi");
            testData.Add("doei");
            testData.Add("hoi");
            testData.Add("doei");
            testData.Add("hoi");
            testData.Add("doei");


=======
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
>>>>>>> origin/FileIO


            while (true)
            {
                Socket socket = tcpListener.AcceptSocket();
                Thread socketThread = new Thread(new ParameterizedThreadStart(newSocket));
                socketThread.Start(socket); 
                Console.WriteLine("Ik heb een nieuwe verbinding");

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine("Timer event triggered at {0}", e.SignalTime);
    }

    public static void newSocket(Object object1)
    {
        Socket socket = object1 as Socket;

        byte[] b = new byte[100];
        int k = socket.Receive(b);
        String name = getUsername(System.Text.Encoding.ASCII.GetString(b));
        Console.WriteLine(name);

<<<<<<< HEAD
        string formattedData = "[" + string.Join(",", testData) + "]";

        socket.Send(asen.GetBytes("chathistorie |"+formattedData)); 
=======
        socket.Send(asen.GetBytes(getHistory()));
>>>>>>> origin/FileIO

        socketList.Add(socket);

        while (true)
        {
            b = new byte[100];
            k = socket.Receive(b);
<<<<<<< HEAD
            String message = "message |" + name + ": " + System.Text.Encoding.ASCII.GetString(b);
            sendMessageToAll(socket, message);
            Console.WriteLine("bericht gestuurd"+message);
=======
            String message = name + ": " + System.Text.Encoding.ASCII.GetString(b);

            ChatHistory.Add(message);
            sendMessageToAll(socket, "message |" + message);
>>>>>>> origin/FileIO
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

    public static void saveHistory()
    {
        JSONHandler.saveList(ChatHistory);
        ChatHistory = new List<string>();
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


