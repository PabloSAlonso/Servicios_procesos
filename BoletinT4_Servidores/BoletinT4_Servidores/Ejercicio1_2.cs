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
            string linea = "";
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
                            if (int.TryParse(modificador[1], out numFilas))
                                for (int i = 0; i < numFilas && linea != null; i++)
                                {
                                    linea = sr.ReadLine();  
                                    Console.WriteLine(linea);
                                }
                        }
                        else
                        {
                            Console.WriteLine("El modificador disponible es -n y debe ir un entero pegado a él");
                        }
                    }

                }
                else //Está vacío o hay lineas de mas
                {
                    Console.WriteLine("Formato: cat -nN ruta");
                }
            }
            catch (IOException)
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