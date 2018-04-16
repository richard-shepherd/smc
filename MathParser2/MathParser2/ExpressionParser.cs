using System;
using System.Collections.Generic;
using System.Linq;

namespace MathParser2
{
    /// <summary>
    /// Helper functions to parse math (arithmetic) expression.
    /// </summary><remarks>
    /// The parsing is designed to be used to help with the creation of
    /// a "bottom up" calculation graph to calculate the expression.
    /// 
    /// In this graph, the original expression is used to create the 
    /// initial (bottom / output) node. The expression is then broken
    /// up splitting on the operator with the lowest precendence. The
    /// two remaining expressions are evaluated as parent nodes, and 
    /// their results combined using the operator found.
    /// 
    /// This continues recursively for the parent nodes until the only 
    /// available tokens are single variables or constants.
    /// 
    /// For example:
    /// 
    ///   N:a  N:2  N:b  N:3  N:c  N:17
    ///      \  |    |  /      |   / 
    ///      N:a+2  N:b+3    N:c/17  N:8
    ///          \     |       \    /
    ///         N:(a+2)*(b+3)  N:c/17-8
    ///                 \         /
    ///           N:(a+2)*(b+3)+c/17-8
    ///           
    /// </remarks>
    public class ExpressionParser
    {
        #region Static construction

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ExpressionParser()
        {
            // We set up the collection of operators we work with,
            // from lowest to highest precedence...
            addOperator(0, '-', (a, b) => a - b);
            addOperator(1, '+', (a, b) => a + b);
            addOperator(2, '/', (a, b) => a / b);
            addOperator(3, '*', (a, b) => a * b);
            addOperator(4, '^', (a, b) => Math.Pow(a, b));
        }

        /// <summary>
        /// Adds an operator to the collection.
        /// </summary>
        private static void addOperator(int precedence, char operatorText, Func<double, double, double> operatorFunction)
        {
            m_operators.Add(operatorText, new Operator(operatorText, operatorFunction, precedence));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Splits the expression by the lowest precedence operator.
        /// </summary>
        public static SplitInfo splitByLowestPrecedenceOperator(string expression)
        {
            var result = new SplitInfo();

            // We first remove all whitespace from the expression...
            expression = String.Concat(expression.Where(c => !Char.IsWhiteSpace(c)));

            // If the whole expression is in brackets, we remove them...
            expression = removeOuterBrackets(expression);
            result.Expression = expression;

            // We look through the expression for the first lowest precedence 
            // operator. Note that 0perators in brackets are not included.
            var indexOfLowestPrecendenceOperator = -1;
            Operator lowestPrecedenceOperator = null;
            var bracketDepth = 0;
            var expressionLength = expression.Length;
            for(var i=0; i<expressionLength; ++i)
            {
                var c = expression[i];

                // Are we coming in or out of a bracket?
                if (c == '(') ++bracketDepth;
                if (c == ')') --bracketDepth;

                // If we are in a bracket, we don't check for an operator
                // (as the bracket itself is higher precedence than the
                // operators it contains).
                if(bracketDepth > 0)
                {
                    continue;
                }

                // We check if the current character is an operator...
                Operator currentOperator;
                if(m_operators.TryGetValue(c, out currentOperator))
                {
                    // The current character is an operator.
                    // We check if it is the lowest precedence we've
                    // seen so far...
                    if(lowestPrecedenceOperator == null
                        ||
                        currentOperator.Precendence < lowestPrecedenceOperator.Precendence)
                    {
                        // We've found a new lowest-precedence operator...
                        lowestPrecedenceOperator = currentOperator;
                        indexOfLowestPrecendenceOperator = i;
                    }
                }
            }

            if(lowestPrecedenceOperator != null)
            {
                // We found an operator...
                result.WasSplit = true;
                result.Operator = lowestPrecedenceOperator;
                result.LHS = expression.Substring(0, indexOfLowestPrecendenceOperator);
                result.RHS = expression.Substring(indexOfLowestPrecendenceOperator + 1);
            }
            else
            {
                // We did not find an operator...
                result.WasSplit = false;
            }

            return result;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Removes outer brackets from the expression passed in.
        /// </summary>
        private static string removeOuterBrackets(string expression)
        {
            if (expression.Substring(0, 1) != "("
                ||
                expression.Substring(expression.Length - 1) != ")")
            {
                // The expression is not in brackets...
                return expression;
            }

            // The expression is in brackets, so we remove them, and
            // we remove any further brackets from the expression...
            expression = expression.Substring(1, expression.Length - 2);
            return removeOuterBrackets(expression);
        }

        #endregion

        #region Private data

        // The collection of operators we know about, keyed by their text
        // representation...
        private static Dictionary<char, Operator> m_operators = new Dictionary<char, Operator>();

        #endregion
    }
}
