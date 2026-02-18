using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4_Servidor
{
    internal class Servidor_Records
    {
        bool serverRunning = false;
        int Port = -1;
        Socket socketServer;
        string rutaPalabras = $"{Environment.GetEnvironmentVariable("programdata")}\\palabras.txt";
        string rutaRecords = $"{Environment.GetEnvironmentVariable("programdata")}\\records.dat";

        public void InitServer()
        {
            Console.WriteLine($"Puerto {Port}");
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socketServer.Bind(iPEndPoint);
                socketServer.Listen(1);
                Console.WriteLine("Usuario conectado");


            }
        }

        public void ClientDispatcher(Socket sClient)
        {

        }
    }
}
