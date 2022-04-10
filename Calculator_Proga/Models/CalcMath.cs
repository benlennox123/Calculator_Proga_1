using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Proga.Models
{
    static class CalcMath
    {
        public static double Add(double a, double b) => a+b;
        public static double Sub(double a, double b) => a-b;
        public static double Mul(double a, double b) => a*b;
        public static double Div(double a, double b) => a/b;
        public static double Proc(double a, double b) => b*a*0.01;
        public static double Squ(double a) => Math.Sqrt(a);
        public static double Pow(double a) => Math.Pow(a,2);
        public static double Dec(double a) => 1/a;
    }
}
