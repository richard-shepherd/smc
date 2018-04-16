namespace MathParser2
{
    /// <summary>
    /// Info returned after performing a 'split' operation on
    /// an expression.
    /// </summary>
    public class SplitInfo
    {
        /// <summary>
        /// The original expression.
        /// </summary>
        public Expression OriginalExpression { get; set; } = null;

        /// <summary>
        /// True if the expression was successfully split, false otherwise.
        /// </summary>
        public bool WasSplit { get; set; } = false;

        /// <summary>
        /// The left hand side of the expression if it was successfully split.
        /// </summary>
        public Expression LHS { get; set; } = null;

        /// <summary>
        /// The right hand side of the expression if it was successfully split.
        /// </summary>
        public Expression RHS { get; set; } = null;

        /// <summary>
        /// The operator, if the expression was successfully split.
        /// </summary>
        public Operator Operator { get; set; } = null;
    }
}
