using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eindopdracht___Client
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        Connection Connection;
        MainWindow mainWindow;
        public loginWindow()
        {
            Connection = new Connection();
            Connection.Connect(IPAddress.Loopback.ToString(), 8001);

            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            string message = UserName.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                Connection.SendMessageAsync("username |" + message).Wait();
                mainWindow = new MainWindow(Connection);
                mainWindow.Show();
                this.Close();
            }


        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
