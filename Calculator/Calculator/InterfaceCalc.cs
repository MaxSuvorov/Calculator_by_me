using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface InterfaceCalc
    {
        void Put_A(double a);

        void Clear_A();

        double Multiplication(double b);

        double Division(double b);

        double Sum(double b);

        double Subtraction(double b);

        double Sqrt();

        double Square();

        double Den();
    }
}
