using System.Collections.Generic;

namespace Flash
{
    public class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(
            SyntaxToken openPareanthesisToken,
            ExpressionSyntax expression,
            SyntaxToken closedParenthesisToken)
        {
            OpenPareanthesisToken = openPareanthesisToken;
            Expression = expression;
            ClosedParenthesisToken = closedParenthesisToken;
        }
        public override TokenKind Kind => TokenKind.ParenthesizedExpression;
        public SyntaxToken OpenPareanthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken ClosedParenthesisToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenPareanthesisToken;
            yield return Expression;
            yield return ClosedParenthesisToken;
        }
    }
}