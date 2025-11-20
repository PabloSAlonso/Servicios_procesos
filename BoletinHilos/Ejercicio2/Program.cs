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
        private static Random numeroRandom = new Random();
        public static int numAleatorio(int maximo)
        {
            return numeroRandom.Next(1, maximo + 1);
        }

        static readonly object l = new();
        public static void caballosAvanzan(object y)
        {
            int x = 0;
            bool caballosCorren = true;
            while (caballosCorren)
            {
                lock (l)
                {
                    if (caballosCorren)
                    {
                        Console.SetCursorPosition(x += numAleatorio(10), (int)y);
                        Console.WriteLine("*");
                        if (x >= 50)
                        {
                            caballosCorren = false;
                        }
                    }
                }
                Thread.Sleep(numAleatorio(1000));
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
            int y = 0;
            do
            {
                Console.WriteLine("=== BIENVENIDO AL HIPÓDROMO VIVAS ===");
                Console.WriteLine("¿Cuantos caballos van a correr?");
                cantidadCaballos = pedirEntero();
                Thread[] caballos = new Thread[cantidadCaballos];
                Console.WriteLine($"Selecciona uno de los {cantidadCaballos} caballos (Introduzca 0 para salir)");
                while (caballoElegido < 1 || caballoElegido > cantidadCaballos)
                {
                    Console.WriteLine("Mete un numero dentro del rango de caballos");
                    caballoElegido = pedirEntero();
                }
                Console.Clear();
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Thread(caballosAvanzan);
                    caballos[i].Start(y);
                    y += 3;
                }
                

            } while (caballoElegido != 0);
        }
    }
}
