using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ejercicio1
{
    internal class MultiThread_Service  //Puerto ocupado arreglau
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { set; get; } = 0;

        int[] OpcionesPuerto = { 135, 31416, 135 };
        public int GestionarPuerto()
        {
            int i = 0;
            bool PuertoLibre = false;
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                do
                {
                    try
                    {
                        IPEndPoint ie = new IPEndPoint(IPAddress.Any, OpcionesPuerto[i]);
                        s.Bind(ie);
                        Console.WriteLine($"Servidor iniciado. " +
        $"Escuchando en {ie.Address}:{ie.Port}");
                        s.Listen(1);
                        PuertoLibre = true;
                    }
                    catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        Console.WriteLine($"Puerto {OpcionesPuerto[i]} en uso");
                        i++;
                    }
                } while (!PuertoLibre && i <= OpcionesPuerto.Length - 1);
            }
            return OpcionesPuerto[i];
        }
        public void InitServer()
        {
            Port = GestionarPuerto();
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    s.Bind(ie);
                    Console.WriteLine($"Servidor iniciado. " +
    $"Escuchando en {ie.Address}:{ie.Port}");
                    s.Listen(1);
                    while (ServerRunning)
                    {
                        Socket client = s.Accept();
                        Thread hilo = new Thread(() => ClientDispatcher(client));
                        hilo.IsBackground = true;
                        hilo.Start();
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Fin de servidor");
                }
            }
        }
        private Socket s;
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
                    string welcome = "Bienvenido al Servicio de Fecha y Hora, Comandos: time | date | all | close *****";
                    string pass = GestionarPassword("password.txt");
                    sw.WriteLine(welcome);
                    string? msg = "";
                    string comando;
                    DateTime ahora;
                    try
                    {
                        msg = sr.ReadLine();
                        if (msg != null)
                        {
                            comando = msg.Split(' ')[0];
                            switch (comando)
                            {
                                case "time":
                                    ahora = DateTime.Now;
                                    sw.WriteLine(ahora.ToString("HH:mm:ss"));
                                    break;
                                case "date":
                                    ahora = DateTime.Now;
                                    sw.WriteLine(ahora.ToString("dd-MM-yyyy"));
                                    break;
                                case "all":
                                    ahora = DateTime.Now;
                                    sw.WriteLine(ahora.ToString("dd-MM-yyyy HH:mm:ss"));
                                    break;
                                case "close":
                                    if (msg == $"close {pass}")
                                    {
                                        CerrarServidor();
                                    }
                                    else
                                    {
                                        if (msg.Trim() == "close")
                                        {
                                            sw.WriteLine("No has indicado una contraseña");
                                        }
                                        else
                                        {
                                            sw.WriteLine("Contraseña incorrecta");
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"No se ha reconocido el comando {msg} en la lista de comandos disponibles");
                                    Console.WriteLine("Comandos disponibles: time | date | all | close *****");
                                    break;
                            }
                            Console.WriteLine($"El cliente usó {msg}");
                        }
                    }
                    catch (IOException)
                    {
                        msg = null;
                    }
                }
            }
        }

        string ProgramData = Environment.GetEnvironmentVariable("ProgramData");
        public string GestionarPassword(string NombreArchivo)
        {
            try
            {
                string Pass = "";
                DirectoryInfo d;
                StreamReader sr;
                d = new DirectoryInfo(ProgramData);
                Directory.SetCurrentDirectory(d.Name);
                string archivo = "password.txt";
                if (File.Exists(archivo))
                {
                    using (sr = new StreamReader(ProgramData + "\\" + archivo))
                    {
                        Pass = sr.ReadLine();
                    }
                }
                return Pass;
            }
            catch (Exception e) when (e is IOException || e is FileNotFoundException)
            {
                //Devuelvo una pass por defecto en caso de error con el archivo
                return "password";
            }
        }

        public void CerrarServidor()
        {
            Console.WriteLine("Cerrando Servidor");
            ServerRunning = false;
            s.Close();
        }
    }
}
