using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace BoletinT4_Servidores
{
    public class Ejercicio1_2
    {

        public static void FuncionCat(string[] args)
        {
            //StreamReader sr;
            string[] modificador;
            int numFilas = 0;
            try
            {
                if (args.Length == 1) //No hay modificador
                {
                    using (StreamReader sr = new StreamReader(args[0]))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
                else if (args.Length == 2)//Hay modificador
                {
                    using (StreamReader sr = new StreamReader(args[1]))
                    {
                        string mod = args[0];
                        if (mod.StartsWith("-n"))
                        {
                            modificador = args[0].Split("-n");
                            numFilas = int.Parse(modificador[1]);
                            for (int i = numFilas; i > 0; i--)
                            {
                                string linea = sr.ReadLine();
                                if (linea != null)
                                {
                                    Console.WriteLine(linea);
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("Junto al modifcador -n debe ir un entero");
                        }


                    }

                }
                else
                {
                    Console.WriteLine("Formato: cat -nN ruta");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Archivo inexistente o corrupto");
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