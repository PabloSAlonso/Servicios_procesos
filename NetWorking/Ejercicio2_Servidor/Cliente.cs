using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_Servidor
{
    public class Cliente
    {
        public IPAddress IP { get; set; }
        public string nombre_usuario { get; set; }
        public StreamWriter sw {  get; set; }

        public Cliente(IPAddress IP, string nombre,  StreamWriter sw)
        {
            this.IP = IP;
            this.nombre_usuario = nombre;
            this.sw = sw;
        }

        public override string ToString()
        {
            return $"{nombre_usuario}@{IP}";
        }
    }
}
