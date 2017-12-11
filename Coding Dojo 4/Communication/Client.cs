using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CD4_Client.Communication
{
    class Client
    {
        byte[] buffer = new byte[512];
        Socket clientSocket;
        Action<string> MessageInformer;
        Action AbortInformer;

        public Client(string ip, int port,Action<string> messageInformer, Action abortInformer)
        {
            try
            {
                this.AbortInformer = abortInformer;
                this.MessageInformer = messageInformer;
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                clientSocket = client.Client;
                StartReceiving();
            }
            catch (Exception)
            {
                messageInformer("Server not ready");
                AbortInformer();//reset Client Communication
            }
            
        }

        public void StartReceiving()
        {
            Task.Factory.StartNew(Receive);
            //start receiving in new Thread
        }

        private void Receive()
        {
            string message = "";
            while (!message.Equals("@quit"))
            {
                int length = clientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //inform GUI via delegate
                MessageInformer(message);
            }
            Close();
        }

        public void Send(string message)
        {
            if (clientSocket!=null)//check if clientsocket connected
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }
        private void Close()
        {
            clientSocket.Close();
            AbortInformer();
        }
    }
}
