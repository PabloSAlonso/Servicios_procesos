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

        static readonly object l = new();
        public static void caballosAvanzan(object y)
        {
            bool flagComun = true;
            while (flagComun)
            {
                lock (l)
                {
                    if (flagComun)
                    {

                    }
                }
            }
        }

        public static void iniciarCaballos(Thread[] caballos)
        {
            for (int i = 0; i < caballos.Length; i++)
            {
                caballos[i] = new Thread(caballosAvanzan);
            }
        }

        static void Main(string[] args)
        {
            int caballoElegido = 0;
            int cantidadCaballos = 0;
            do
            {
                Console.WriteLine("=== BIENVENIDO AL HIPÓDROMO VIVAS ===");
                Console.WriteLine("¿Cuantos caballos van a correr?");
                cantidadCaballos = pedirEntero();
                Thread[] caballos = new Thread[cantidadCaballos];
                Console.WriteLine($"Selecciona uno de los {cantidadCaballos} caballos (Introduzca 0 para salir)");


            } while (caballoElegido != 0);
        }
    }
}
