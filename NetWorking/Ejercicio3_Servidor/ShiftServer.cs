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
        public bool ServicioEjecutandose { set; get; } = true;
        private Socket s;

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
            return int.Parse(pin) | -1;
        }

        public int GestionarPuerto(int puertoInicial)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, puertoInicial);
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool libre = false;
                do
                {
                    try
                    {
                        s.Bind(iPEndPoint);
                        s.Listen(1);
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
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);

        }

        public void ProtocoloCliente(Socket sClient)
        {

        }
    }
}
