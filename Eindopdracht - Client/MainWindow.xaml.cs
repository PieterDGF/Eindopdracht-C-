using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace Eindopdracht___Client
{

    public partial class MainWindow : Window
    {

        private DispatcherTimer updateTimer;
        private List<string> messages;

        static String Message;
        Connection Connection;
        public MainWindow(Connection connection)
        {
            InitializeComponent();
            Connection = connection;
          
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(0.5);
            updateTimer.Tick += Update_Tick;
            updateTimer.Start();
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            //messages = Connection.getMessages();
            //foreach (string item in messages)
            //{
            //    SetMessage(item);
            //}
            //messages.Clear();

        }



        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                ChatBox.Text += "You: " + message + "\n";
                MessageTextBox.Clear();

                SendMessageToServer(message);
            }
        }

        private void UserList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void SendMessageToServer(string message)
        {
            //Connection.SendMessageAsync(message).Wait();
        }

        public void SetMessage(string message)
        {
            ChatBox.Text += message + "\n";
        }


    }
}