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

        updateTimer = new System.Timers.Timer(1);
        updateTimer.Elapsed += OnTimedEvent;
        updateTimer.AutoReset = true;
        updateTimer.Enabled = true;



        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 8001);
            tcpListener.Start();

            Console.WriteLine("Ik ben aan het luisteren");

            socketList = new List<Socket>();
            ChatHistory = new List<String>();
            JSONHandler = new JSONHandler();

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
        //saveHistory();
    }

    public static void newSocket(Object object1)
    {
        Socket socket = object1 as Socket;
        byte[] b = new byte[100000];

        try
        {
            int k = socket.Receive(b);
            String name = getUsername(System.Text.Encoding.ASCII.GetString(b));
            Console.WriteLine(name);

            socket.Send(asen.GetBytes(getHistory()));
            socketList.Add(socket);

            while (true)
            {
                b = new byte[1024];
                k = socket.Receive(b);

                if (k == 0)
                {
                    Console.WriteLine("Client heeft de verbinding verbroken.");
                    break;
                }

                String message = (name + System.Text.Encoding.ASCII.GetString(b));
                ChatHistory.Add(message);
                sendMessageToAll(socket, "message |" + message);
                Console.WriteLine("bericht gestuurd: " + message);
            }
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException: " + se.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        finally
        {
            socketList.Remove(socket);
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            socket.Close();
            Console.WriteLine("Verbinding met client gesloten.");
        }
    }


    public static void sendMessageToAll(Socket socket, String message)
    {
        Console.WriteLine("deze message ga ik doorsturen:" + message);
        foreach (Socket socket1 in socketList)
        {
            if (socket1 != socket)
            {

                socket1.Send(asen.GetBytes(message));
            }
        }
        saveHistory();
    }

    public static void saveHistory()
    {
        Console.WriteLine("History gesaved");
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
        String Result;

        if (history.Count > 0)
        {
            Result = "chathistory |[" + string.Join(",", history.ToArray()) + "]";
            
            Console.WriteLine(Result);

        }
        else
        {
            Result = "chathistory |[]";

        }
        return Result;

    }
}


