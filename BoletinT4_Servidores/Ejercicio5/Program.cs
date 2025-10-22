namespace Ejercicio5
{
    public class Program
    {
        public delegate void Mydelegate();
        public static void MenuGenerator(string[] opciones, Mydelegate[] funciones)
        {
            int opcion = 0;
            bool flag;
            Console.WriteLine("Elige una función");
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"{i + 1}.- {opciones[i]}");
            }
            Console.WriteLine($"{opciones.Length + 1}.- Salir");
            Console.WriteLine("Elige tu opción");
            flag = int.TryParse(Console.ReadLine(), out opcion);
            do
            {

            } while (opcion != opciones.Length + 1);
        }
        static void Main(string[] args)
        {
            string[] opciones = { "Falar", "Leer", "Pensar" };
            Mydelegate[] funciones = { };
            MenuGenerator(opciones, funciones);
        }
    }
}
