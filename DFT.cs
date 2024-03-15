using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFT_optimize
{
    class DFT
    {
        static public Complex[] Calc_DFT(double[] arr)
        {
            Complex[] dft = new Complex[arr.Length];
            Complex neg_j = new Complex(0, -1);

            for (int k = 0; k < arr.Length; k++)
            {
                dft[k] = new Complex(0, 0);
                for (int i = 0; i < arr.Length; i++)
                {
                    dft[k].SetValue(dft[k] + arr[i] * Complex.Exp(2 * i * k * Math.PI * neg_j / arr.Length));
                    dft[k].SetValue(dft[k] / arr.Length);
                }
            }

            return dft;
        }

        static public Complex[] Calc_DFT(double[] arr, int min_freq, int max_freq) // to calculate DFT to frequencies in selected interval
        {
            Complex[] dft = new Complex[max_freq-min_freq];
            Complex neg_j = new Complex(0, -1);

            if(min_freq<1 || max_freq>arr.Length)
            {
                throw new ArgumentException("Incorrect interval of frequencies");
            }

            for (int k = 0, K = min_freq; k < max_freq - min_freq && K<= max_freq; k++, K++)
            {
                dft[k] = new Complex(0, 0);
                for (int i = 0; i < arr.Length; i++)
                {
                    dft[k].SetValue(dft[k] + arr[i] * Complex.Exp(2 * i * K * Math.PI * neg_j / arr.Length));
                    dft[k].SetValue(dft[k] / arr.Length);
                }
            }

            return dft;
        }

        static public Complex[] optimized_DFT(double[] arr)
        {
            int q = (int)Math.Log(arr.Length, 2);
            int N = (int)Math.Pow(2, q);
            Complex[] dft = new Complex[N];
            Complex neg_j = new Complex(0, -1);

            for (int k = 0; k < N; k++)
            {
                dft[k] = new Complex(0, 0);
                for (int i = 0; i < N / 4; i++)
                {
                    Complex Multiplier = arr[i] + arr[i + N / 4] * Complex.Exp(-Math.PI * k * neg_j / 4);
                    Multiplier = Multiplier + arr[i + N / 2] * Complex.Exp(Math.PI * k * neg_j / 2) + arr[i + 3 * N / 4] * Complex.Exp(3 * Math.PI * k * neg_j / 4);
                    dft[k].SetValue(dft[k] + Multiplier * Complex.Exp(2 * i * k * Math.PI * neg_j / arr.Length) / N);
                }
            }

            return dft;
        }


    }
}
