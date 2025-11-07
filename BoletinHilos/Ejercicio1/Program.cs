using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Ejercicio1
{
    public class Program
    {
        //Ejercicio 1
        //a) Crea dos hilos(Thread) compitiendo que traten de incrementar(el primer hilo) o
        //decrementar(el segundo hilo) en 1 unidad una variable que comienza en 0.
        //Funcionarán de forma continua hasta que a llegue a 500 (primer hilo) o a -500
        //(segundo hilo). Además se mostrará en pantalla la variable cada vez que cambie
        //indicando quién la ha cambiado(thread 1 o thread 2 y cada uno en un color).
        //Ambos hilos deben parar en cuanto uno consiga su objetivo.
        //No uses setCursorPosition.
        //El Main, una vez que lanza los hilos, se queda en espera hasta que ambos hilos
        //finalizan, luego informa de cual ha ganado.
        //b) Las funciones de hilos serán expresiones lambda(si quieres y los ves claro haz
        //ya directamente este apartado).
        static bool flag = true;
        static readonly object l = new();
        static void Main(string[] args)
        {
            int num = 0;
            Thread threadIncrementa = new Thread(() =>
            {
                while (flag)
                {
                    lock (l)
                    {
                        if (flag)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            num++;
                            Console.Write($"{num,10}");
                            if (num > 499 || num < -499)
                            {
                                flag = false;
                            }
                        }
                    }
                }

            });
            Thread threadDecrementa = new Thread(() =>
            {
                while (flag)
                {
                    lock (l)
                    {
                        if (flag)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            num--;
                            Console.Write($"{num,10}");
                            if (num > 499 || num < -499)
                            {
                                flag = false;
                            }
                        }
                    }
                }

            });
            threadIncrementa.Start();
            threadDecrementa.Start();
            threadIncrementa.Join();
            threadDecrementa.Join();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            //threadIncrementa.Join();
            if (num == 500){
                Console.WriteLine("\nHa ganado el verde!");
            }
            if (num == -500)
            {
                Console.WriteLine("\nHa ganado el rojo!");
            }

        }
    }
}
