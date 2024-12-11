using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFT_optimize
{
    class Complex
    {
        public double real, imag;
        public Complex(double a, double b)
        {
            this.real = a;
            this.imag = b;
        }

        // arithmetic operations and related
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.real + b.real, a.imag + b.imag);
        }
        public static Complex operator +(double a, Complex b)
        {
            return new Complex(a + b.real, b.imag);
        }
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.real - b.real, a.imag - b.imag);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.real * b.real - a.imag * b.imag, a.real * b.imag + a.imag * b.real);
        }
        public static Complex operator *(double a, Complex b)
        {
            return new Complex(a * b.real, a * b.imag);
        }
        public static Complex operator /(Complex a, Complex b)
        {
            return new Complex((a.real * b.real + a.imag * b.imag) / Abs(b), (a.imag * b.real - a.real * b.imag) / Abs(b));
        }
        public static Complex operator /(double a, Complex b)
        {
            return new Complex(b.real / a, b.imag / a);
        }
        public static Complex operator /(Complex b, double a)
        {
            return new Complex(b.real / a, b.imag / a);
        }
        public void SetValue(Complex other)
        {
            this.real = other.real;
            this.imag = other.imag;
        }

        // math functions
        public static double Abs(Complex z)
        {
            return Math.Sqrt(z.real * z.real + z.imag * z.imag);
        }
        public static double Arg(Complex z)
        {
            return Math.Acos(z.real / Abs(z));
        }
        public static Complex Pow(Complex z, int n)
        {
            return n > 0 ? z * Pow(z, n - 1) : new Complex(1, 0);
        }
        public static Complex Exp(Complex z)
        {
            return new Complex(Math.Exp(z.real) * Math.Cos(z.imag), Math.Exp(z.real) * Math.Sin(z.imag));
        }
        private static double Factorial(int n)
        {
            return n > 0 ? n * Factorial(n - 1) : 1;
        }

        // class methods
        override public String ToString()
        {
            return $"({this.real}, {this.imag})";
        }
    }
}
