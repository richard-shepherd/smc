using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser2
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = "((a + 2) * (b - 3) + c/4 - 8)";
            //var expression = "(((a + 7)))";

            var info1 = ExpressionParser.splitByLowestPrecedenceOperator(expression);
            var info2 = ExpressionParser.splitByLowestPrecedenceOperator(info1.LHS);
            var info3 = ExpressionParser.splitByLowestPrecedenceOperator(info1.RHS);


            var a = 1;
        }

    }
}
