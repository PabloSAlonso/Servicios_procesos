using Ejercicio1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_Servidor
{
    internal class Principal
    {
        static void Main(string[] args)
        {
            //string pass = new MultiThread_Service().GestionarPassword("password.txt");
            //Console.WriteLine($"Pass:{pass}");
            new MultiThread_Service().InitServer();
        }
    }
}
