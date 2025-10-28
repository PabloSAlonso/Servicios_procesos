#define ejemplos4
namespace Ejemplo1
{
    //Uso de Thread
    internal class Program
    {

#if ejemplos1
        static object l = new object();
        static void Main(string[] args) // Hilo principal, foreground por defecto.
        {
            Thread thread = new Thread(WriteDown);
            thread.IsBackground = true; // Punto clave. Prueba a cambiarlo a false.
            thread.Start();
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 1);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(50);
            }
        } // Cuando acaba el programa se cierra interrumpiendo el hilo background.
        static void WriteDown()
        {
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 20);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(200);
            }
        }
#endif

#if ejemplos2
        static bool running = true; // Booleana compartida para controlar los bucles
        static readonly object l = new object();

        static void charA()
        {
            int contA = 1;
            while (running)
            {
                lock (l)
                {
                    if (running)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" A:{contA}");
                        contA++;
                        if (contA > 1000)
                        {
                            running = false;
                        }
                    }

                }
            }
        }
        static void charB()
        {
            int contB = 1; //while-lock-if estructura sencilla para asegurar que cuando un proceso acaba los otros tambien van a acabar en ese momento 
            while (running)
            {
                lock (l)
                {
                    if (running)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" B:{contB}");
                        contB++;
                        if (contB > 1000)
                        {
                            running = false;
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Thread threadA = new Thread(charA);
            Thread threadB = new Thread(charB);
            threadA.Start();
            threadB.Start();
            threadA.Join(); //Espera a q acabe el hilo
            //threadB.Join();
            Console.ReadKey();
        }
    }
#endif

#if ejemplos3
    //static void Main(string[] args)
    //{
    //    Thread thread = new Thread(WriteDown);
    //    thread.Start();
    //    // writeUp
    //    int i = 1;
    //    while (running)
    //    {
    //        lock (l)
    //        {
    //            Console.SetCursorPosition(1, 1);
    //            Console.Write("{0,4}", i);
    //            i++;
    //            Thread.Sleep(1000);
    //            if (i >= 1000)
    //            {
    //                running = false;
    //            }
    //        }
    //    }
    //    Console.ReadKey();

    //}
    //static void WriteDown()
    //{
    //    int i = 1;
    //    while (running)
    //    {
    //        lock (l)
    //        {
    //            Console.SetCursorPosition(1, 20);
    //            Console.Write("{0,4}", i);
    //            i++;
    //            Thread.Sleep(1000);
    //            if (i >= 1000)
    //            {
    //                running = false;
    //            }
    //        }
#endif

#if ejemplos4
        static void Main(string[] args)
        {
            for (char i = 'A'; i < 'D'; i++)
            {
                char oneChar = i;
                new Thread(() =>
                {
                    for (int j = 0; j < 5; j++)
                        Console.Write(oneChar); //Si escribimos directamente la i, al ser esta comun para todos los hilos va a crear conflictos
                }).Start();
            }
            Console.ReadKey();
        }
#endif

    }
}
