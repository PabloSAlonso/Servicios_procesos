namespace BoletinT4_Servidores
{
    internal class Ejercicio1
    {

        public static void funcionLs(string argsMain)
        {
            if (argsMain.Length > 0 && Directory.Exists(argsMain))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(argsMain);
                Console.WriteLine("Directorios");
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (var directorios in directoryInfo.GetDirectories())
                {
                    Console.WriteLine(directorios);
                }
                Console.ResetColor();
                Console.WriteLine("Archivos");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var archivos in directoryInfo.GetFiles())
                {
                    Console.WriteLine($"Nombre:{archivos.Name} de tamaño {archivos.Length}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("No existeee");
            }
        }


    }
}
//1.Comando ls: se le puede pasar un directorio o una variable de entorno.
//Muestra el contenido de dicho directorio, tanto subdirectorios como archivos.
//Diferenciará por colores y en los archivos además indicará el tamaño de los
//mismos.
//Ejemplos:
//ls % appdata %
//ls e:\temp