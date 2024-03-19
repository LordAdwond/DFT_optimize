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
            int N = 11525, i=0;
            var rand = new Random();
            double max_A = 10;

            double[] arr = new double[N]; // random signal
            var dft = DFT.Calc_DFT(arr); // usual DFT
            var mdft = DFT.optimized_DFT(arr); // optimized DFT

            str = Console.ReadLine();
        }
    }
}