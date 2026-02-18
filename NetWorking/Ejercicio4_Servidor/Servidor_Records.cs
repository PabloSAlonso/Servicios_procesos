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
        bool serverRunning = true;
        int Port = 31416;
        Socket socketServer;
        string rutaPalabras = $"{Environment.GetEnvironmentVariable("programdata")}\\palabras.txt";
        string rutaRecords = $"{Environment.GetEnvironmentVariable("programdata")}\\records.dat";

        public bool PuertoLibre(int puerto)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, puerto);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    s.Bind(iPEndPoint);
                    s.Listen(10);

                }
                catch (SocketException e)
                {
                    return false;
                }
                return true;
            }
        }

        public int GestionarPuertoOcupado(int puerto)
        {
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, puerto);
            bool puertoValido = false;
            while (!puertoValido && puerto <= IPEndPoint.MaxPort)
            {
                using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {

                    try
                    {
                        s.Bind(iP);
                        s.Listen(10);
                        puertoValido = true;
                    }
                    catch (SocketException e)
                    {
                        puerto++;
                    }
                    return puerto;
                }
            }
            return -1;
        }

        public void InitServer()
        {
            if (!PuertoLibre(Port))
            {
                GestionarPuertoOcupado(Port);
            }
            Console.WriteLine($"Puerto {Port}");
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socketServer.Bind(iPEndPoint);
                socketServer.Listen(1);
                Console.WriteLine("Usuario conectado");
                while (serverRunning)
                {
                    try
                    {
                        Socket client = socketServer.Accept();
                        Thread hiloClient = new Thread(() => ClientDispatcher(client));
                        hiloClient.Start();
                    }
                    catch (SocketException s) { }
                }
            }
        }
        public void ClientDispatcher(Socket sClient)
        {
            using (sClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, Port);
                using (NetworkStream network = new NetworkStream(sClient))
                using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Bienvenido al servidor de Ahorcado");
                    sw.WriteLine("Introduce nombre para guardar tus records");
                    string nombre = sr.ReadLine();
                    sw.WriteLine("Comandos disponibles: ( gw | sw 'palabra' | gr | sr 'record' | close 'clave' )");
                    string comando = sr.ReadLine();
                    string[] comandoEntero = new string[2];
                    if (comando != null)
                    {
                        comandoEntero = comando.Split(',');
                    }
                    switch (comandoEntero[0])
                    {
                        case "gw":

                            break;

                        case "sw":

                            break;
                        case "gr":

                            break;
                        case "sr":

                            break;
                        case "close":

                            break;
                    }
                }
            }
        }

        public void StopServer()
        {
            serverRunning = false;
            socketServer.Close();
        }

        public string[] PalabrasArchivo(string ruta)
        {
            using (StreamReader sr = new StreamReader(ruta))
            {
                return sr.ReadToEnd().Split(",");
            }
        }

        public List<Record>
    }
}
