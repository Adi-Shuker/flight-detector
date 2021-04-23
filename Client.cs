using FlightDetector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightDetector
{
    class Client: IClient
    {
        TcpClient client;

        public void connect(string ip, int port)
        {
            client = new TcpClient("127.0.0.1", 5400);
        }

        public void disconnect()
        {
            client.Close();
        }

        public void write(string line)
        {
            line = line + "\n";
            byte[] bytes = Encoding.ASCII.GetBytes(line);
            client.Client.Send(bytes, bytes.Length, SocketFlags.Partial);
        }
    }
}
