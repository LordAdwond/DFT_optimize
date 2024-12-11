using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFT_optimize
{
    class DFT
    {
        static public Complex[] Calc_DFT(double[] arr, int l, int a=0)
        {
            int N = l;
            double[] data = new double[l];
            Complex[] dft = new Complex[N];
            int n = 0, k = 0;
            double pi = Math.PI;
            double Real = 0, Imag = 0;

            for(int i=0; i<l; i++)
            {
                data[i] = arr[i + a + l];
            }
            
            var ForDFT = Parallel.For(0, N, (i, state) => {
                Real = Imag = 0;

                dft[i] = new Complex(0, 0);

                for (k = 0; k < N; k++)
                {
                    Real += data[i] * Math.Cos(2 * pi * i * k / N);
                    Imag -= data[i] * Math.Sin(2 * pi * i * k / N);
                }

                dft[i].real = Real;
                dft[i].imag = Imag;
            });

            return dft;
        }

        static public double[,] optimized_DFT(double[] arr, int l, int a=0)
        {
            double[,] dft = new double[l, 2];
            int q = (int)Math.Log(l, 2);
            int q1 = q / 2;
            int q2 = q - q1;
            int p = (int)(Math.Pow(2, q2));
            int s = (int)(Math.Pow(2, q1));
            double A = 0, B = 0;

            var ForDFT = Parallel.For(0, l, (k, state) => {
                dft[k, 0] = 0;
                dft[k, 1] = 0;

                for (int u=0; u<s; u+=8)
                {
                    A = 0;
                    B = 0;

                    for (int v = 0; v < p; v+=8)
                    {
                        A = 0.5 * (arr[a + (s - 1) * v + u] + arr[a + (s - 1) * v + u + 1]) * Math.Cos(2 * Math.PI * (s - 1) * v / l);
                        B = 0.5 * (arr[a + (s - 1) * v + u] + arr[a + (s - 1) * v + u + 1]) * Math.Sin(2 * Math.PI * (s - 1) * v / l);
                    }

                    dft[k, 0] += A * Math.Cos(2 * Math.PI * k * u / l) - B * Math.Sin(2 * Math.PI * k * u / l);
                    dft[k, 1] += B * Math.Cos(2 * Math.PI * k * u / l) + A * Math.Sin(2 * Math.PI * k * u / l);
                }

                dft[k, 0] /= l;
                dft[k, 1] /= l;
            });

            return dft;
        }

        public static Complex[] FFT_Cooley_Tukey(double[] arr, int l, int a = 0)
        {
            double[] input = new double[l];
            int i = 0;

            for(i=0; i<l; i++)
            {
                input[i] = arr[i+a+l];
            }

            Complex[] data = new Complex[l];
            for (i = 0; i<l; i++)
            {
                data[i] = new Complex(input[i], 0);
            }

            FFTRecursive(data);
            return data;
        }

        private static void FFTRecursive(Complex[] data)
        {
            int n = data.Length;
            if (n <= 1) return;

            // Розбиття на парні та непарні індекси
            Complex[] even = new Complex[n / 2];
            Complex[] odd = new Complex[n / 2];

            var ForDFT = Parallel.For(0, n / 2, (i, state) => {
                even[i] = data[2 * i];
                odd[i] = data[2 * i + 1];
            });

            FFTRecursive(even);
            FFTRecursive(odd);

            var ForDFT1 = Parallel.For(0, n/2, (k, state) => {
                Complex t = Complex.Exp(new Complex(-2 * Math.PI * odd[k].imag * k / n, 0)) * odd[k];
                data[k] = even[k] + t;
                data[k + n / 2] = even[k] - t;
            });
        }

    }
}
