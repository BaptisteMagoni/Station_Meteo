using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Station_Meteo
{

    class SocketClient
    {
        private NetworkStream stream;
        private TcpClient client;

        public SocketClient(TcpClient cl)
        {
            stream = null;
            client = cl;
        }

        public void send_data(string data)
        {
            stream = client.GetStream();
            byte[] message = Encoding.UTF8.GetBytes(data);
            stream.Write(message, 0, message.Length);
        }

        public string[] getData()
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = stream.Read(buffer, 0, client.ReceiveBufferSize);
            return Encoding.UTF8.GetString(buffer, 0, data).Split(':');
        }
    }
}