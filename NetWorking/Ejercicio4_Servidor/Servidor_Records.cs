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
        List<string> words;
        List<Record> records;
        private readonly object l = new object();

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
                            lock (l)
                            {
                                sw.WriteLine(words[NumeroAleatorio(words.Count)]);
                            }
                            break;
                        case "sw":
                            if (comandoEntero.Length == 2)
                            {
                                lock (l)
                                {
                                    if (GuardaEnArchivo(comandoEntero[1], rutaPalabras))
                                    {
                                        words.Add(comandoEntero[1]);
                                        sw.WriteLine("OK");
                                    }
                                    else
                                    {
                                        sw.WriteLine("ERROR");
                                    }
                                }
                            }
                            else
                            {
                                sw.WriteLine("ERROR");
                            }
                            break;
                        case "gr":
                            lock (l)
                            {
                                records = LeerArchivoBinario(rutaRecords);
                                foreach (Record record in records)
                                {
                                    sw.WriteLine($"{record.Nombre} - {record.Segundos}");
                                }
                            }
                            break;
                        case "sr":
                            if (comandoEntero.Length == 2)
                            { // TDO trycatch
                                sw.WriteLine("Introduce nombre: ");
                                string nombre = sr.ReadLine();
                                sw.WriteLine("Introduce el tiempo: ");
                                int tiempo = int.Parse(sr.ReadLine());
                                Record record = new Record(nombre, tiempo);
                                if (EscribirRecord(record, rutaRecords))
                                {
                                    sw.WriteLine("ACCEPT");
                                }
                                else
                                {
                                    sw.WriteLine("REJECT");
                                }
                            }
                            break;
                        case "close":
                            if (comandoEntero.Length == 2)
                            {
                                StopServer();
                            }
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

        public static List<Record> LeerArchivoBinario(string ruta)
        {
            List<Record> records = new List<Record>();
            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        string nombre = br.ReadString();
                        int cantidadSegundos = br.ReadInt32();
                        records.Add(new Record(nombre, cantidadSegundos));
                    }
                }
            }
            catch (IOException) { }
            return records;
        }


        public List<Record> AñadirRecord(List<Record> records, Record recordComparar)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Segundos > recordComparar.Segundos)
                {
                    records[i] = recordComparar;
                }
            }
            return records;
        }

        public bool EscribirRecord(Record record, string ruta)
        {
            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.OpenOrCreate))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    AñadirRecord(records, record);
                    foreach (Record recorda in records)
                    {
                        bw.Write($"{recorda.Nombre} {recorda.Segundos}");
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        static Random random = new Random();
        public static int NumeroAleatorio(int max)
        {
            return random.Next(max);
        }

        public bool GuardaEnArchivo(string word, string pathFile)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathFile, true))
                {
                    sw.Write($",{word}");
                }
            }
            catch (IOException e)
            {
                return false;
            }
            return true;
        }
    }
}
