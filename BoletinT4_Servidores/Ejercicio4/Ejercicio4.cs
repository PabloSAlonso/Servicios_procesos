#define ej7
using Ejercicio4;
using System.Reflection.Emit;

namespace Ejercicio4
{
    public class Ejercicio4
    {
        public static void Main(string[] args)
        {
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };

#if ej1        
            if (Array.Exists(notas, num => num >= 5))
            {
                Console.WriteLine("Hay aprobados :)");
            }
#elif ej2
            foreach (int nota in Array.FindAll(notas, num => num >= 5))
            {
                Console.WriteLine("Aprobados:{nota});
            }
#elif ej3
            int n = Array.FindLastIndex(notas, num => num >= 5); 
            Console.WriteLine($"Posicion del ultimo aprobado {n+1}");

#elif ej4
            int n = Array.FindLast(notas, num =>  num >= 5);
            Console.WriteLine($"Nota del ultimo aprobado {n}");

#elif ej5
            int pares = Array.FindAll(notas, num => num % 2 == 0).Length;
            Console.WriteLine($"La cantidad de pares es {pares}");


#elif ej6
            string tresCaracteres = Array.Find(palabras, palabra => palabra.Length >= 3);
            Console.WriteLine($"La primera palabra de 3 o mas caracteres es {tresCaracteres}");

#elif ej7
            Array.ForEach(palabras, palabra => Console.WriteLine(palabra.ToUpper()));//TODO revisar

#elif ej8
            int indice = Array.FindIndex(palabras, palabra => palabra.StartsWith("E"));
            Console.WriteLine($"Posicion palabra que empieza por E es:{indice+1}");

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
