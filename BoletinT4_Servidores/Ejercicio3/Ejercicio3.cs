namespace Ejercicio3
{
    public class Ejercicio3
    {
        public delegate double Peticion(double x);

        public static double PedirDouble()
        {
            double x;
            Console.WriteLine("Dame un numero decimal");
            if (double.TryParse(Console.ReadLine(), out x))
            {
                return x;
            }
            return 0.0;
        }

        public static int PedirEntero()
        {
            int x;
            if (int.TryParse(Console.ReadLine(), out x))
            {
                return x;
            }
            return 0;
        }

        public static void Main(string[] args)
        {
            double num = PedirDouble();
            int exp = 0;
            Peticion e = p => p;
            while (exp != 2 && exp != 3)
            {
                Console.WriteLine("Dame un 2 para cuadrado o 3 para cubo");
                exp = PedirEntero();
                if (exp == 2)
                {
                    e = num => num * num;
                }
                else if (exp == 3)
                {
                    e = (num) => num * num * num;
                }
                Console.WriteLine(e(num));

            }

        }
    }
}
