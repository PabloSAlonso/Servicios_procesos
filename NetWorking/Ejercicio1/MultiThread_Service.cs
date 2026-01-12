using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ejercicio1
{
    internal class MultiThread_Service
    {
        public bool ServerRunning { set; get; } = true;
        public int[] Port { get; set; } = { 31416, 31417, 16178, 18290 };

        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port[0]);
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
                    string welcome = "Bienvenido al Servicio de Fecha y Hora, Comandos: time, date, all, close password";
                    string pass = "";
                    int longitud = 0;
                    (pass, longitud) = GestionarPassword("password.txt");
                    sw.WriteLine(welcome);
                    string? msg = "";
                    string comando;
                    DateTime ahora;
                    try
                    {
                        msg = sr.ReadLine();
                        comando = msg.Split(' ')[0];
                        if (msg != null)
                        {
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
                                        sw.WriteLine("El servidor ha sido finalizado");
                                        ServerRunning = false;
                                        //Cerrar conexion del servidor
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
                                    msg = null;
                                    break;
                            }
                            Console.WriteLine($"El cliente dijo {msg}");

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
        public (string, int) GestionarPassword(string NombreArchivo)
        {
            try
            {
                string Pass = "";
                int LongitudPass = 0;
                DirectoryInfo d;
                StreamReader sr;
                d = new DirectoryInfo(ProgramData);
                foreach (FileInfo file in d.GetFiles())
                {
                    Console.WriteLine(ProgramData + "\\" + file.Name);
                    if (file.FullName == NombreArchivo)
                    {
                        using (sr = new StreamReader(ProgramData + "\\" + file.FullName))
                        {
                            Pass = sr.ReadToEnd().Trim();
                            LongitudPass = Pass.Length;
                        }
                    }
                }
                return (Pass, LongitudPass);

            }
            catch (Exception e) when (e is IOException || e is FileNotFoundException)
            {
                //Devuelvo una pass por defecto en caso de error con el archivo
                return ("password", 8);
            }
        }
        static void Main(string[] args)
        {
            (string pass, int longitud) = new MultiThread_Service().GestionarPassword("password.txt");
            Console.WriteLine($"Pass:{pass}, Longitud{longitud}");
            new MultiThread_Service().InitServer();
        }
    }
}
