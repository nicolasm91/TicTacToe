using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TTTserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciado servidor || Tic-Tac-Toe { ACTIVATED }");

            TcpListener serverSocket = new TcpListener(8000);
            

            serverSocket.Start();
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                HandlerCliente client = new HandlerCliente();
                client.startClient(clientSocket);

            }
        }
    }
}
