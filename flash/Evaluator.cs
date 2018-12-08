using System;
using System.Linq;
using Flash.Syntax;

namespace Flash
{
    public class Evaluator
    {
        private ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax root)
        {
            if(root is NumberExpressionSyntax n)
                return (int) n.NumberToken.Value;
            
            if(root is BinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                switch(b.OperatorToken.Kind)
                {
                    case TokenKind.PlusToken:
                        return left + right;
                    case TokenKind.MinusToken:
                        return left - right;
                    case TokenKind.SlashToken:
                        return left / right;
                    case TokenKind.StarToken:
                        return left * right;
                    default:
                        throw new Exception(
                            $"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }

            if(root is ParenthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }

            throw new Exception(
                $"Unexpected node {root.Kind}");
        }
    }
}