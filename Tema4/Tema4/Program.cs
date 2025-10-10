namespace Tema4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string directory;
            string separator;
            string fullName;
            StreamWriter s;
            // If only use Windows, you don’t need to do this
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                directory = Environment.GetEnvironmentVariable("HOME");
                separator = "/";
            }
            else
            {
                directory = Environment.GetEnvironmentVariable("homepath");
                separator = "\\";
            }
            fullName = directory + separator + "testfile.txt";
            // Open File
            s = new StreamWriter(fullName);
            // Write File
            for (int i = 1; i <= 10; i++)
            {
                s.Write("{0,-2}", i);
            }
            s.WriteLine();
            for (int i = 1; i <= 10; i++)
            {
                s.WriteLine($"Line :{i,3}");
            }
            // Close File. Needs try-catch.
            s.Close();


            using (s = new StreamWriter(fullName, true))
            {
                s.WriteLine("Appending text to file");
            }


            string line;
            StreamReader sr;
            sr = new StreamReader(fullName);
            line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();

        }
    }
}
