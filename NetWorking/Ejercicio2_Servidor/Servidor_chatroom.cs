using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_Servidor
{
    internal class Servidor_chatroom//Cierre abrupto al inicio. Todos los puertos ocupados. Locks.
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { set; get; } = 0;

        int[] OpcionesPuerto = { 135, 135, 31416 };
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
                        //                Console.WriteLine($"Servidor iniciado. " +
                        //$"Escuchando en {ie.Address}:{ie.Port}");
                        s.Listen(1);
                        PuertoLibre = true;
                    }
                    catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        Console.WriteLine($"Puerto {OpcionesPuerto[i]} en uso");
                        i++;
                    }
                } while (!PuertoLibre && i < OpcionesPuerto.Length - 1);
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
                        hilo.Start();
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Fin de servidor");
                }
            }
        }

        public void notificarUsuarios(List<Cliente> clientes, string mensaje, StreamWriter sw)
        {
            lock (l)
            {

                foreach (Cliente cliente in clientes)
                {
                    if (cliente.sw != sw)
                    {
                        cliente.sw.WriteLine(mensaje);
                    }
                }
            }
        }

        private Socket s;
        private List<Cliente> clientes = new();
        static readonly Object l = new object();

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
                    Cliente clienteEliminado = null;
                    sw.AutoFlush = true;
                    string welcome = "Bienvenido al servicio de chatroom";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    string? nombre = "";
                    try
                    {
                        sw.WriteLine("Introduzca su nombre de usuario");
                        nombre = sr.ReadLine() == null ? " " : sr.ReadLine();
                        Cliente nuevoCliente = new Cliente(ieClient.Address, nombre, sw);
                        lock (l)
                        {
                            clientes.Add(nuevoCliente);
                        }
                        notificarUsuarios(clientes, nuevoCliente.ToString() + " se ha unido al servidor", sw);
                        sw.WriteLine($"Su nombre es:{nombre}, ya puede empezar a chatear con el resto de usuarios");
                        while (msg != null)
                        {
                            msg = sr.ReadLine();
                            switch (msg)
                            {
                                case "exit":
                                    msg = null;
                                    break;

                                case "list":
                                    lock (l)
                                    {

                                        foreach (Cliente cliente in clientes)
                                        {
                                            if (cliente.sw != sw)
                                            {
                                                nuevoCliente.sw.WriteLine(cliente.ToString());
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    if (msg != null)
                                    {
                                        notificarUsuarios(clientes, $"{nuevoCliente.ToString()}:{msg}", sw);
                                    }
                                    break;
                            }
                        }
                    }
                    catch (Exception ex) when (ex is ArgumentNullException || ex is IOException)
                    {
                        sw.WriteLine("Desconectandose del servidor");
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine("Error inesperado, contacte con soporte");
                    }
                    lock (l)
                    {
                        for (int i = 0; i < clientes.Count; i++)
                        {
                            if (clientes[i].sw == sw)
                            {
                                clienteEliminado = clientes[i];
                                clientes.RemoveAt(i);
                            }
                        }
                    }
                    notificarUsuarios(clientes, clienteEliminado.ToString() + " ha abandonado el servidor", sw);
                    sw.WriteLine("Desconectado del servidor");
                }
            }
        }
    }
}
