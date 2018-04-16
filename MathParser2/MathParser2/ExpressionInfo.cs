namespace MathParser2
{
    /// <summary>
    /// Holds a mathematical expression and can parse it into parts.
    /// </summary>
    public class ExpressionInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the string expression.
        /// </summary><remarks>
        /// Note: The expression has all whitespace removed, as well 
        /// as any redundant enclosing brackets.
        /// </remarks>
        public string Expression { get; set; } = "";

        /// <summary>
        /// Gets or sets whether the expression is a single constant value.
        /// </summary><remarks>
        /// For example: "12.3", but not "12.3+34.5".
        /// </remarks>
        public bool IsSingleConstantValue { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the expression is a single variable value.
        /// </summary><remarks>
        /// For example: "a" or "price", but not "price * 2".
        /// </remarks>
        public bool IsSingleVariableValue { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the expression contains any operators.
        /// </summary><remarks>
        /// For example: "a + b".
        /// </remarks>
        public bool IsCompoundExpression { get; set; } = false;

        #endregion

        #region Public methods

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExpressionInfo()
        {
        }

        /// <summary>
        /// Constructor from an expression.
        /// </summary>
        public ExpressionInfo(string expression)
        {
            Expression = expression;
        }

        #endregion
    }
}
