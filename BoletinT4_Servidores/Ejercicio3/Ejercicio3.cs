namespace Ejercicio3
{
    public class Ejercicio3
    {
        public delegate double Peticion(double x);

        public static double pedirDouble(double x)
        {
            Console.WriteLine("Dame un numero decimal");
            if (double.TryParse(Console.ReadLine(), out x))
            {
                return x;
            }
            return 0.0;
        }
        public static double pedirExp(double exp)
        {
            Console.WriteLine("Dame tu exponente 2 o 3");
            if (double.TryParse(Console.ReadLine(), out exp) && exp == 2 || exp == 3)
            {
                Console.WriteLine("Exponente recogido correctamente");
                return exp;
            }
            return 0.0;
        }
        public static void Main(string[] args)
        {
            Peticion p = new (pedirDouble);
            Peticion e = new (pedirExp);
            Console.WriteLine($"Cuadrado de tu num es:{e}");
        }
    }
}
