using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace BoletinT4_Servidores
{
    public class Ejercicio2
    {

        public static void funcionCat(string[] args)
        {
            StreamReader sr;
            string[] modificador;
            int modNumero = 0;

            if (args.Length <= 2 && args.Length > 0)
            {
                if (args.Length == 1) //No hay modificador
                {
                    sr = new StreamReader(args[0]);
                    Console.WriteLine(sr.ReadToEnd());
                    sr.Close();
                }
                else //Hay modificador
                {
                    sr = new StreamReader(args[1]);
                    modificador = args[0].Split("-n");
                    modNumero = int.Parse(modificador[1]);
                    for (int i = modNumero; i > 0; i--) //Ver si ya acabo de leerse el archivo y el usuario pidio lineas de mas
                    {
                        if (sr.ReadLine != null)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("El formato es incorrecto");
            }


        }

    }
}
//2.Comando cat: Se le pasa un archivo de texto. Muestrea el contenido del
//mismo en la consola. Si justo tras el comando tiene el argumento -n con un
//número a continuación solo mostrará la cantidad de líneas indicadas por
//dicho número (en este caso evita leer el archivo entero si no es necesario).
//Ejemplos:
//cat myfile.txt
//cat -n5 c:\windows\win.ini