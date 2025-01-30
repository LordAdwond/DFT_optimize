using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace DFT_optimize
{
    class Program
    {
        static void Main(string[] args)
        {
            String str;
            Stopwatch stopwatch = new Stopwatch();
            int N = 0, i=0, k=0;
            int l = 64, step = 500;
            int dl = 2 * l;
            int acc = 1;
            bool to_continue = true;

            // data reading
            N = File.ReadAllLines("data/channel0.txt").Length;
            double[,] samples = new double[4, N];

            for (int channel = 0; channel < 1; channel++)
            {
                String[] signal_str_values = File.ReadAllLines($"data/channel{channel}.txt");
                N = signal_str_values.Length;
                double[] arr = new double[N];
                for (i = 0; i < N; i++)
                {
                    Double.TryParse(signal_str_values[i], out samples[channel, i]);
                }
                
            }

            Console.WriteLine("Testing DFT");
            stopwatch.Start();
            for (int channel=0; channel<1; channel++)
            {
                double[] arr = new double[N];
                for (i = 0; i < N; i++)
                {
                    try
                    {
                        arr[i] = samples[channel, i];
                    }
                    catch(Exception e)
                    {
                        to_continue = false;
                        break;
                    }
                }
                if(to_continue)
                {
                    for (int a = 0; a < N - dl * acc; a += step)
                    {
                        try
                        {
                            var dft = DFT.Calc_DFT(arr, dl * acc, a); // usual DFT
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"DFT needed {stopwatch.ElapsedMilliseconds} milliseconds");

            Console.WriteLine("Testing modifide DFT");
            to_continue = true;
            stopwatch.Start();
            for (int channel = 0; channel < 1; channel++)
            {
                double[] arr = new double[N];
                for (i = 0; i < N; i++)
                {
                    try
                    {
                        arr[i] = samples[channel, i];
                    }
                    catch (Exception e)
                    {
                        to_continue = false;
                        break;
                    }
                }
                if(to_continue)
                {
                    for (int a = 0; a < N - dl * acc; a += step)
                    {
                        try
                        {
                            var dft = DFT.optimized_DFT(arr, dl * acc, 10, a); // optimized DFT
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Modified DFT needed {stopwatch.ElapsedMilliseconds} milliseconds");

            /*
            Console.WriteLine("Testing FFT by Cooley-Tuley method");
            stopwatch.Start();
            for (int channel = 0; channel < 4; channel++)
            {
                double[] arr = new double[N];
                for (i = 0; i < N - dl * acc; i++)
                {
                    arr[i] = samples[channel, i];
                }
                for (int a = 0; a < N - dl * acc; a += step)
                {
                    try
                    {
                        var dft = DFT.FFT_Cooley_Tukey(arr, dl * acc, a); // Cooley-Tuley method
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Modified DFT needed {stopwatch.ElapsedMilliseconds} milliseconds");
            */

            str = Console.ReadLine();
        }
    }
}