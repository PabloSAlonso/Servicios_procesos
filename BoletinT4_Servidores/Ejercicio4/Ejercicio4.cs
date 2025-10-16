#define ej3
using Ejercicio4;
using System.Reflection.Emit;

namespace Ejercicio4
{
    public class Ejercicio4
    {
        public static bool encontrarIndex(int n)
        {
            return n >= 5;
        }
        static void Main(string[] args)
        {
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };

#if ej1        
            if (Array.Exists(notas, num => num >= 5))
            {
                Console.WriteLine("Hay aprobaos");
            }
#elif ej2
            foreach (int nota in Array.FindAll(notas, num => num >= 5))
            {
                Console.WriteLine(nota);
            }
#elif ej3
            Console.WriteLine(Array.FindIndex(notas, num => num >= 5));

#elif ej4



#elif ej5



#elif ej6

#elif ej7

#elif ej8

#elif ej9

#elif ej10

#elif ej11

#endif
        }

    }
}
//Ejercicio 4
//Crea estos dos vectores en un Main:
//int[] notas = { 5, 2, 8, 1, 9, 4 };
//string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };
//Usa métodos de la clase Array junto con lambdas para obtener directamente
//estos elementos del vector notas:
//• Saber de si hay algún aprobado (Si existe o no) en notas.
//• Mostrar los aprobados de notas.
//• Indicar la posición en el array del último aprobado
//• Mostrar la nota del último aprobado.
//• Cuanto tienen nota par.
//Y ahora haz lo siguiente con el vector palabras:
//• Cual es la primera palabra de más de 3 caracteres.
//• Mostrar todas las palabras en mayúsculas.
//• Indica la posición de la primera palabra que empiece por E.
