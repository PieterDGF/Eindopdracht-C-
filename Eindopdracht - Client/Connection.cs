using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht___Client
{
    internal class Connection
    {
        private TcpClient TcpClient;
        private Stream Stream;

        public async Task Connect(string serverIp, int port)
        {
            TcpClient = new TcpClient();
            await TcpClient.ConnectAsync(serverIp, port);
            Stream = TcpClient.GetStream();
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

        public async Task<string> ReceiveMessageAsync()
        {
            if (Stream == null)
            {
                throw new InvalidOperationException("Niet verbonden met de server.");
            }

            byte[] buffer = new byte[1024];
            int bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }

        public void Disconnect()
        {
            Stream.Close();
            TcpClient.Close();
        }

    }
}
