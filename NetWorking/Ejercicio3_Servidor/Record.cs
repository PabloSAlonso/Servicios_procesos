using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3_Servidor
{
    public class Record
    {
        private string nombre;
        private int segundos;

        public string Nombre
        {
            set
            {
                nombre = value.Substring(0, 3);
            }
            get
            {
                return nombre;
            }
        }

        public int Segundos
        {
            set
            {
                segundos = value;
            }
            get
            {
                return segundos;
            }
        }

        public Record(string nombre, int segundos)
        {
            Nombre = nombre;
            Segundos = segundos;
        }
    }
}
