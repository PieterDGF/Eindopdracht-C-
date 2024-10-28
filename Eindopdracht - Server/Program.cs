// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

public class Server()
{
    private static ArrayList socketList;
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

        string formattedData = "[" + string.Join(",", testData) + "]";

        socket.Send(asen.GetBytes("chathistorie |"+formattedData)); 

        socketList.Add(socket);

        while (true)
        {
            b = new byte[100];
            k = socket.Receive(b);
            String message = "message |" + name + ": " + System.Text.Encoding.ASCII.GetString(b);
            sendMessageToAll(socket, message);
            Console.WriteLine("bericht gestuurd"+message);
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
}


