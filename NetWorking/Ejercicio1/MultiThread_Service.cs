using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio1
{
    internal class MultiThread_Service
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { get; set; } = 31416;

        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(100);
                Console.WriteLine($"Servidor iniciado. " +
$"Escuchando en {ie.Address}:{ie.Port}");
                Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
                try
                {
                    while (ServerRunning)
                    {
                        Socket client = s.Accept();
                        Thread hilo = new Thread(() => ClientDispatcher(client));
                        hilo.Start();
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Fin de servidor");
                }
            }
        }
        string pass = "";
        private void ClientDispatcher(Socket sClient)
        {
            using (sClient)
            {
                IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado:{ieClient.Address} " +
                $"en puerto {ieClient.Port}");
                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamReader sr = new StreamReader(ns, codificacion))
                using (StreamWriter sw = new StreamWriter(ns, codificacion))
                {
                    sw.AutoFlush = true;
                    string welcome = "Bienvenido al Servicio de Fecha y Hora :)\n Comandos: time, date, all, close password";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    try
                    {
                        msg = sr.ReadLine();
                        if (msg != null)
                        {
                            if (msg != "time" || msg != "date" || msg != "all" || msg != "close")
                            {
                                Console.WriteLine($"No se ha reconocido el comando {msg} en la lista de comandos disponibles");
                                msg = null;
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "time":

                                        break;
                                    case "date":

                                        break;
                                    case "all":

                                        break;
                                    case "close":

                                        break;
                                }
                            }
                            Console.WriteLine($"El cliente dice {msg}");
                            Thread.Sleep(3000);
                            sw.WriteLine($"El servidor dice {msg}");
                        }
                    }
                    catch (IOException)
                    {
                        msg = null;
                    }
                    Console.WriteLine("Cliente desconectado.\nConexión cerrada");
                }
            }
        }
        string ProgramData = Environment.GetEnvironmentVariable("ProgramData");
        string Pass = "";
        public bool GestionarPassword(string pass)
        {
            DirectoryInfo d;
            StreamReader sr;
            Directory.SetCurrentDirectory(ProgramData);
            d = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (FileInfo file in d.GetFiles())
            {
                if (file.Name == "password")
                {
                    using (sr = new StreamReader(file.Name))
                    {
                        Pass = sr.ReadToEnd();
                    }
                }
            }
            if (Pass == pass)
            {
                return true;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
