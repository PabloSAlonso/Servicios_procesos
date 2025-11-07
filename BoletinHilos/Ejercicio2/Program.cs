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
            Thread[] caballos = new Thread[5];
            Console.WriteLine("=== BIENVENIDO AL HIPÓDROMO VIVAS ===");
            Console.WriteLine("Selecciona uno de los 5 caballos");
            int opcion = pedirEntero();
        }
    }
}
