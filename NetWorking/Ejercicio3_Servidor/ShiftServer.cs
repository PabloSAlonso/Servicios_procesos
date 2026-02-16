using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_Servidor
{
    internal class ShiftServer
    {
        public string[] users;
        public List<string> waitQueue;
        public int Port = 31416;
        private Socket socketServer;

        bool servidorEjecutandose = true;

        public void ReadNames(String ruta)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    users = sr.ReadToEnd().Split(";");
                }
            }
            catch (IOException io)
            {
                Console.WriteLine("Error de archivo");
            }
        }

        public int ReadPin(String ruta)
        {
            String pin = "";
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    string contenido = sr.ReadToEnd().Trim();
                    for (int i = 0; i < 4; i++)
                    {
                        pin += contenido[i];
                    }
                }
            }
            catch (Exception e) when (e is IOException || e is ArgumentNullException)
            {
                Console.WriteLine("Error de archivos o Pin");
                return -1;
            }
            return int.Parse(pin);
        }

        public int GestionarPuerto(int puertoInicial)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, puertoInicial);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool libre = false;
                do
                {
                    try
                    {
                        socketServer.Bind(iPEndPoint);
                        socketServer.Listen(1);
                        libre = true;
                    }
                    catch (SocketException se) when (se.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        Console.WriteLine($"Puerto {puertoInicial} ocupado");
                        puertoInicial++;
                    }
                }
                while (libre && puertoInicial < IPEndPoint.MaxPort);
                return puertoInicial;
            }
        }

        public bool PuertoLibre(int puerto)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, puerto);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    s.Bind(iPEndPoint);
                    s.Listen(1);
                }
                catch (SocketException)
                {
                    return false;
                }
                return true;
            }
        }

        public void InitServer()
        {
            if (!PuertoLibre(Port))
            {
                GestionarPuerto(Port);
            }
            Console.WriteLine($"Puerto:{Port}");
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {

                    socketServer.Bind(ie);
                    Console.WriteLine($"Servidor iniciado. " +
                                      $"Escuchando en {ie.Address}:{ie.Port}");
                    socketServer.Listen(1);
                    while (servidorEjecutandose)
                    {
                        Socket cliente = socketServer.Accept();
                        Thread hiloCliente = new Thread(() => ProtocoloCliente(cliente));
                        hiloCliente.Start();
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Fin de servidor");
                }
            }
            ReadNames($"{Environment.GetEnvironmentVariable("userprofile")}\\usuarios.txt");
        }

        public void StopServer()
        {
            servidorEjecutandose = false;
            socketServer.Close();
        }

        public void ProtocoloCliente(Socket socketClient)
        {
            using (socketClient)
            {
                IPEndPoint ip = (IPEndPoint)socketClient.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(socketClient))
                using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Bienvenido");
                    sw.WriteLine("Introduce tu nombre");
                    try
                    {
                        string nombreUsuario = sr.ReadLine();
                        if (usuarioEnLista(users, nombreUsuario) || nombreUsuario == "admin")
                        {
                            if (nombreUsuario == "admin")
                            {
                                sw.Write("Introduce un PIN:");
                                int pin = int.Parse(sr.ReadLine());
                                int pinArchivo = 0;
                                try
                                {
                                    pinArchivo = ReadPin($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt");
                                }
                                catch (ArgumentException)
                                {
                                    pinArchivo = 1234;
                                }
                                if (pin == pinArchivo)
                                {
                                    string comando = "";
                                    string[] comandoCompleto;
                                    do
                                    {
                                        sw.WriteLine("Introduce un comando (del pos | chpin pin | exit | shutdown):");
                                        comando = sr.ReadLine();
                                        if (comando != null)
                                        {
                                            comandoCompleto = comando.Split(' ');
                                            switch (comandoCompleto[0])
                                            {
                                                case "del":
                                                    break;
                                                case "chpin":
                                                    break;
                                                case "exit":


                                                    break;

                                                case "shutdown":

                                                    break;
                                                default:
                                                    sw.WriteLine("Comandos válidos: (del pos | chpin pin | exit | shutdown)");
                                                    break;
                                            }
                                        }
                                    } while (comando != "list" && comando != "add");
                                    socketClient.Close();
                                }
                                else
                                {
                                    sw.WriteLine("PIN incorrecto");
                                    sr.ReadLine();
                                    socketClient.Close();
                                }
                            }
                            else
                            {
                                string comando = "";
                                do
                                {
                                    sw.WriteLine("Introduce un comando (list | add):");
                                    comando = sr.ReadLine();
                                    if (comando != null)
                                    {

                                        switch (comando)
                                        {
                                            case "list":
                                                foreach (string user in waitQueue)
                                                {
                                                    sw.WriteLine($"{user}");
                                                }

                                                break;

                                            case "add":
                                                if (!usuarioEnLista(waitQueue.ToArray(), nombreUsuario))
                                                {
                                                    waitQueue.Add($"{nombreUsuario}-{DateTime.Now.ToString("yyyy-MM-dd  HH/mm/ss")}");
                                                    sw.WriteLine("OK");
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Este usuario esta ya en la cola");
                                                }
                                                break;
                                            default:
                                                sw.WriteLine("Comandos válidos: list | add");
                                                break;
                                        }
                                    }
                                } while (comando != "list" && comando != "add");
                                socketClient.Close();
                            }
                        }
                        else
                        {
                            socketClient.Close();
                        }
                    }
                    catch (IOException e)
                    {

                    }
                }

            }
        }
        public bool usuarioEnLista(string[] nombres, string nombreBuscar)
        {
            foreach (String nombre in nombres)
            {
                if (nombre == nombreBuscar)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
