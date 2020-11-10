using System;
using System.Data.SqlTypes;
using System.Linq;

namespace WordCount
{
    class Program
    {
        static int Main(string[] args)
        {
            String file = "";
            Boolean exsists = false;
            Boolean extension = false;
            Console.WriteLine("sajt:");

            while (!(exsists && extension)) {
                Console.WriteLine("Kerek egy fajlnevet:");
                file = Console.ReadLine();

                exsists   = System.IO.File.Exists(System.IO.Path.GetFullPath(file));
                extension = System.IO.Path.GetExtension(System.IO.Path.GetFullPath(file)) == ".txt";
            }

            Statistics stat = new Statistics();
            try
            {
                stat.Load(file);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            stat.CountWords();
            var pairs = stat.WordCount.OrderByDescending(e => e.Value);

            foreach (var pair in pairs)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

            return 0;

        }
    }
}
