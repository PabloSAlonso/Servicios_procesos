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

        public void ReadNames(String ruta)
        {
            try
            {
                StreamReader sr = new StreamReader(ruta);
                string[] nombres_leidos = sr.ReadToEnd().Split(";");
                for (int i = 0; i < nombres_leidos.Length; i++)
                {
                    users[i] = nombres_leidos[i];
                }
            }
            catch (IOException io)
            {
                Console.WriteLine("Error de archivo");
            }
        }

        public int ReadPin(String ruta)
        {
            try
            {
                StreamReader sr = new StreamReader(ruta);
                return int.Parse(sr.ReadLine());
            }
            catch (Exception e) when (e is IOException || e is ArgumentNullException)
            {

                Console.WriteLine("Error de archivos o Pin");
                return -1;
            }
        }

        public bool ServicioEjecutandose { set; get; } = true;
        public int Port { set; get; } = 31416;
        private Socket s;
        public int GestionarPuerto()
        {
            int i = 1024;
            bool PuertoLibre = false;
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                do
                {
                    try
                    {
                        //s.Bind(IPEndPoint );
                        s.Listen(1);
                    }
                    catch (SocketException se) when (se.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        Console.WriteLine($"Puerto {i} ocupado");
                        i++;
                    }
                } while (!PuertoLibre);
            }
            return i;
        }

        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
        }

        public void ProtocoloCliente(Socket sClient)
        {

        }
    }
}
