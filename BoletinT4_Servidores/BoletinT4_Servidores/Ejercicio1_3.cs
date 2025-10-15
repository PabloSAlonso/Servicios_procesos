using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BoletinT4_Servidores
{

    public class Ejercicio1_3
    {
        public static void FuncionNewFile(string[] args)
        {
            if (args.Length == 2) //No modificador -a
            {
                Console.WriteLine("Entró sin modificador");
                using (StreamWriter streamWriter = new StreamWriter(args[0]))
                {
                    streamWriter.WriteLine(args[1]);
                }
            }
            else if (args.Length == 3) //Modificador -a
            {
                Console.WriteLine("Entró con modificador");
                if (args[1] == "-a") 
                {
                    using (StreamWriter streamWriter2 = new StreamWriter(args[0], true))
                    {
                        streamWriter2.WriteLine(args[2]);
                    }
                }
            }
            else
            {
                Console.WriteLine("Formato: archivo.txt \"Texto a escribir en archivo.txt\"");
                Console.WriteLine("Modificadores disponibles: -a (Añade el texto al archivo seleccionado sin sobreescribir lo anterior escrito en él)");
            }
        }
    }
}
//3.Comando newfile: Crea un archivo con el texto que haya a continuación.
//Si justo tras el comando se pone -a, añade el texto en vez de sobreescribirlo.
//Ejemplos:
//newfile myfile.txt “This text goes in the myfile.”
//newfile -a miarchivo.txt “And this one is added.”