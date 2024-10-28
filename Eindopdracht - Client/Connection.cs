using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht___Client
{
    public  class Connection
    {
        private TcpClient TcpClient;
        private Stream Stream;
        private bool isListening;
        private List<string> messages;

        public async Task Connect(string serverIp, int port)
        {
            messages = new List<string>();
            //TcpClient = new TcpClient();
            //await TcpClient.ConnectAsync(serverIp, port);
            //Stream = TcpClient.GetStream();
            //isListening = true;
            //StartListening();


            addMessage("message |Gerrit: Hoi");
            addMessage("message |pieter: Hoi");
            addMessage("message |daan: Hoi");
            addMessage("message |erik: Hoi");
            
        }

        public async Task SendMessageAsync(string message)
        {
            if (Stream == null)
            {
                throw new InvalidOperationException("Niet verbonden met de server.");
            }

            byte[] data = Encoding.ASCII.GetBytes(message);
            await Stream.WriteAsync(data, 0, data.Length);
        }

        private async void StartListening()
        {
            byte[] buffer = new byte[1024];

            while (isListening)
            {
                try
                {
                    int bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if (message.StartsWith("message"))
                        {
                        }
                        else
                        {
                            OnMessageReceived(message); 
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during listening: {ex.Message}");
                    break;
                }
            }
        }

        public void Disconnect()
        {
            isListening = false; 
            Stream.Close();
            TcpClient.Close();
        }

        protected virtual void OnMessageReceived(string message)
        {
            Console.WriteLine($"Bericht ontvangen: {message}");
        }


        private void addMessage(string message) 
        {
            string[] parts = message.Split("|");


            messages.Add(parts[1]);
        }

        public List<string> getMessages()
        {
            return messages;
            messages.Clear();
        }
    }
}
