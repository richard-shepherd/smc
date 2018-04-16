using System;

namespace MathParser2
{
    /// <summary>
    /// Represents one mathematical operator.
    /// Holds the text representation of the operator as well
    /// as a function which applies the operator to two double
    /// values.
    /// </summary>
    public class Operator
    {
        #region Properties

        /// <summary>
        /// Gets the text representation of the operator.
        /// </summary>
        public char OperatorText { get; private set; }

        /// <summary>
        /// Gets a function which applies the operator to two doubles.
        /// </summary>
        public Func<double, double, double> OperatorFunction { get; private set; }

        /// <summary>
        /// Gets the operator's precedence. (Lower values are lower precedence.)
        /// </summary>
        public int Precendence { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Operator(char operatorText, Func<double, double, double> operatorFunction, int precedence)
        {
            OperatorText = operatorText;
            OperatorFunction = operatorFunction;
            Precendence = precedence;
        }

        #endregion
    }
}
