using static Ejercicio5.Program;

namespace Ejercicio5
{
    public class Program
    {
        public static int pedirEntero()
        {
            int num = 0;
            bool flag;
            do
            {
                //flag = true;
                Console.WriteLine("Introduce un numero entero");
                flag = int.TryParse(Console.ReadLine(), out num);

            } while (!flag);

            return num;
        }
        public delegate void MyDelegate();
        public static bool MenuGenerator(string[] opciones, MyDelegate[] funciones)
        {
            if (opciones == null || funciones == null)
            {
                Console.WriteLine("Error: Los parámetros no pueden ser null.");
                return false;
            }

            if (opciones.Length != funciones.Length)
            {
                Console.WriteLine("Error: Los vectores deben tener la misma longitud.");
                return false;
            }

            int opcion = 0;
            bool flag;

            do
            {
                Console.WriteLine("Menú de opciones");
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.WriteLine($"{i + 1}.- {opciones[i]}");
                }

                Console.WriteLine($"{opciones.Length + 1}.- Salir");
                Console.WriteLine("\nElige una función");
                opcion = pedirEntero();
                if (opcion >= 1 && opcion <= opciones.Length)
                {
                    funciones[opcion - 1]();
                    Console.WriteLine();
                }
                else if (opcion == opciones.Length + 1)
                {
                    Console.WriteLine("Saliendo del menu...");
                }
                else
                {
                    Console.WriteLine("Opcion no válida");
                }

            } while (opcion != opciones.Length + 1);

            return true;
        }
        //static void f1()
        //{
        //    Console.WriteLine("A");
        //}
        //static void f2()
        //{
        //    Console.WriteLine("B");
        //}
        //static void f3()
        //{
        //    Console.WriteLine("C");
        //}
        static void Main(string[] args)
        {
            MenuGenerator(
                new string[] { "Op1", "Op2", "Op3" },
                new MyDelegate[] { () => Console.WriteLine("A"), () => Console.WriteLine("B"), () => Console.WriteLine("C") }
            );

            Console.WriteLine("Programa finalizado");
        }
    }
}
