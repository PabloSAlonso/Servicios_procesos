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
        static bool caballosCorren = true;
        static int ganador = 0;
        public static void caballosAvanzan(object y)
        {
            int x = 0;
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
                            ganador = (int)y;
                        }
                    }
                }
                Thread.Sleep(numAleatorio(1000));
            }
        }

        static void Main(string[] args) //TODO cuando vuelves a apostar no corrren y gana el mismo siempre...
        {
            int dinero = 1000;
            int apuesta = 0;
            int caballoElegido = 0;
            int cantidadCaballos = 0;
            int y = 0;
            Thread[] caballos;
            do
            {
                caballoElegido = 0;
                apuesta = 0;
                Console.WriteLine("=== BIENVENIDO AL HIPÓDROMO VIVAS ===");
                Console.WriteLine($"Tu saldo es de {dinero}$");
                Console.WriteLine("¿Cuantos caballos van a correr? (Introduzca 0 para salir)");
                cantidadCaballos = pedirEntero();
                caballos = new Thread[cantidadCaballos];
                Console.WriteLine($"Selecciona uno de los {cantidadCaballos} caballos ");
               
                while (caballoElegido < 1 || caballoElegido > cantidadCaballos)
                {
                    Console.WriteLine("Mete un numero dentro del rango de caballos");
                    caballoElegido = pedirEntero();
                }
                Console.WriteLine($"Cual es tu apuesta? (Saldo {dinero}$)");
                while (apuesta <= 0 || apuesta > dinero)
                {
                    Console.WriteLine("introduce un numero mayor que 0 y que puedas pagar!");
                    apuesta = pedirEntero();
                }
                dinero -= apuesta;

                Console.WriteLine($"Saldo tras la apuesta:{dinero}");
                Console.WriteLine("Enter para empezar la carrera");
                Console.ReadKey();
                Console.Clear();
                // Va cada acción en un bucle pq sino no correrian a la vez
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Thread(caballosAvanzan);
                }
                for (int i = 1; i <= caballos.Length; i++)
                {
                    caballos[i - 1].Start(y + i);
                }
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i].Join();
                }
                Console.Clear();
                Console.WriteLine($"Ha ganado el caballo {ganador}!");
                if (ganador == caballoElegido)
                {
                    Console.WriteLine("Has ganado!");
                    dinero += (apuesta * 2);
                    Console.WriteLine($"Has ganado {(apuesta * 2)}$");
                }
                else
                {
                    Console.WriteLine("Has perdido...");
                }
                Console.ReadKey();
                Console.Clear();
                if (dinero == 0)
                {
                    cantidadCaballos = 0;
                    Console.WriteLine("Te has quedado sin dinero para apostar...");
                    Console.WriteLine("Enter para salir...");
                    Console.ReadKey();
                }
            } while (cantidadCaballos != 0);
        }
    }
}
