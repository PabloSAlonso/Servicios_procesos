namespace Ejercicio2
{
    public class Program
    {
        public static int pedirEntero()
        {
            int num = 0;
            bool flag;
            do
            {
                Console.WriteLine("Introduce un numero entero");
                flag = int.TryParse(Console.ReadLine(), out num); 

            } while (!flag);

            return num;
        }

       
        static void Main(string[] args)
        {
            int opcion;
            Thread[] caballos = new Thread[5];
            for (int i = 0; i < caballos.Length; i++)
            {
                //caballos[i] = new Thread();
                Console.WriteLine();
            }
            do
            {
                Console.WriteLine("=== BIENVENIDO AL HIPÓDROMO VIVAS ===");
                Console.WriteLine("Selecciona uno de los 5 caballos (Introduzca 0 para salir)");
                opcion = pedirEntero();
                Thread miCaballo = caballos[opcion];

            } while (opcion != 0);
        }
    }
}
