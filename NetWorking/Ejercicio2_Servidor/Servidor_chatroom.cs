using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_Servidor
{
    internal class Servidor_chatroom
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
        private List<Cliente> clientes = new();
        Cliente nuevoCliente;

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
                    string welcome = "Bienvenido al servicio de chatroom";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    string? nombre = "";
                    sw.WriteLine("Introduzca su nombre de usuario");
                    try
                    {
                        nombre = sr.ReadLine().Trim();
                        nuevoCliente = new Cliente(ieClient.Address, nombre, sw);
                        sw.WriteLine($"Su nombre es:{nombre}");
                        clientes.Add(nuevoCliente);
                        while (msg != null)
                        {
                            switch (msg)
                            {

                            }
                        }

                    }
                    catch (ArgumentNullException ex)
                    {
                        sw.WriteLine("User no válido, desconectando del servidor");
                        msg = null;
                    }

                }
            }
        }
    }
}
