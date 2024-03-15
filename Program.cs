using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFT_optimize
{
    class Program
    {
        static void Main(string[] args)
        {
            String str;
            int N = 35, i=0;
            var rand = new Random();
            double max_A = 10;

            double[] arr = new double[N]; // random signal
            var dft = DFT.Calc_DFT(arr); // usual DFT
            var mdft = DFT.optimized_DFT(arr); // optimized DFT

            // Signal generation and showing
            Console.WriteLine("Signal");
            for(i=0; i<N; i++)
            {
                arr[i] = max_A * rand.NextDouble();
                Console.Write($"{arr[i]} ");
            }
            Console.Write("\n\n");

            // DFT writing
            Console.WriteLine("DFT");
            for (i = 0; i < dft.Length; i++)
            {
                Console.Write(dft[i].ToString() + " ");
            }
            Console.WriteLine("\nOptimized DFT");
            for (i = 0; i < mdft.Length; i++)
            {
                Console.Write(mdft[i].ToString() + " ");
            }
            Console.WriteLine();

            str = Console.ReadLine();
        }
    }
}