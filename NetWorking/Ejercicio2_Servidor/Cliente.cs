using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_Servidor
{
    public class Cliente
    {
        public int IP { get; set; }
        public string nombre_usuario { get; set; }
        public StreamWriter sw {  get; set; }
    }
}
