using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            for (int i = 2; i < 50; ++i)
            Console.WriteLine(i + ": " + EsPrimo(i));
        }
        public static bool EsPrimo(int i){
        
            if (i < 2) return false;
            int p = 2;
            while ((p * p <= i) && (i % p) != 0)
            {
                ++p;
            }
            return p * p > i;
        }
    }
}
