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
            int q = (int)Math.Log(arr.Length, 2);
            int N = (int)Math.Pow(2, q);
            Complex[] dft = new Complex[N];
            int n = 0, k = 0;
            double pi = Math.PI;
            double Real = 0, Imag = 0;
            
            for (n = 0; n < N; n++)
            {
                Real = Imag = 0;
                
                dft[n] = new Complex(0, 0);

                for(k = 0; k < N; k++)
                {
                    Real += arr[k] * Math.Cos(pi * n * k / N);
                    Imag -= arr[k] * Math.Cos(pi * n * k / N);
                }

                dft[n].real = Real;
                dft[n].imag = Imag;
            }
            

            return dft;
        }


        static public Complex[] optimized_DFT(double[] arr)
        {
            int q = (int)Math.Log(arr.Length, 2);
            int N = (int)Math.Pow(2, q);
            int N4 = N / 4, N2 = N / 2, N34 = 3 * N / 4;

            Complex[] dft = new Complex[N];
            int n = 0, k = 0;
            double coef = 0.25 * Math.PI / N;
            double coef_n = 0;
            double sqr2 = Math.Sqrt(2);
            double Real = 0, Imag = 0;

            var ForDFT = Parallel.For(0, N, (i, state) => {
            Real = Imag = 0;
            dft[n] = new Complex(0, 0);
            coef_n = coef * n;
            for (k = 0; k < Math.Pow(2, q - 2); k++)
            {
                Real += arr[k] * sqr2 * Math.Cos(coef_n * (4 * k - N)) + arr[k + N4] * Math.Cos(coef_n * (4 * k + N));
                Real += arr[k + N2] * Math.Cos(2 * coef_n * (2 * k + N)) + arr[k + N34] * Math.Cos(coef_n * (4 * k + 3 * N));

                Imag -= arr[k] * sqr2 * Math.Sin(coef_n * (4 * k + 3 * N)) + arr[k + N4] * Math.Sin(coef_n * (4 * k + N));
                Imag -= arr[k + N2] * Math.Sin(2 * coef_n * (2 * k + N)) + arr[k + N34] * Math.Sin(coef_n * (4 * k + 3 * N));
            }

                dft[n].real = Real;
                dft[n].imag = Imag;
            });

            return dft;
        }


    }
}
