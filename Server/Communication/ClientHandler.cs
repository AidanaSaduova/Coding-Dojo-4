using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CD4_Server.Communication
{
    internal class ClientHandler
    {
        
        private Action<string, Socket> action;
        private byte[] buffer = new byte[512];
        private Thread clientReceiveThread;
        const string endMessage = "@quit";

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.Clientsocket = socket;
            this.action = action;
            //start receiving in a new thread
            clientReceiveThread = new Thread(Receive);
            clientReceiveThread.Start();
        }

        private void Receive()
        {
            string message = "";
            while (!message.Equals(endMessage))
            {
                int length = Clientsocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //set name prop if not already done
                if (Name==null && message.Contains(":"))
                {
                    Name = message.Split(':')[0];
                }
                //inform GUI via delegate 
                action(message, Clientsocket);
            }
            Close();
        }

        public string Name { get; private set; }
        public Socket Clientsocket { get; private set; }

        internal void Close()
        {
            Send(endMessage);//sends end message to client
            Clientsocket.Close(1);//disconection
            clientReceiveThread.Abort();//abort client threads(остановка потока)
        }

        internal void Send(string message)
        {
            Clientsocket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}