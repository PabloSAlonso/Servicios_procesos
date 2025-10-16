namespace Ejercicio4
{
    internal class Ejercicio4
    {
        static void Main(string[] args)
        {
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };
            if (Array.Exists(notas, num => num >= 5))
            {
                Console.WriteLine("Hay aprobaos");
            }

            foreach (int nota in Array.FindAll(notas, num => num >= 5))
            {
                Console.WriteLine(nota);
            }
        }

    }
}
