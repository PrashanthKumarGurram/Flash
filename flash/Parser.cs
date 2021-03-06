using System;
using System.Collections.Generic;
using Flash.Syntax;

namespace Flash
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        private List<string> _diagnostics = new List<string>();
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.Kind != TokenKind.WhiteSpaceToken &&
                   token.Kind != TokenKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != TokenKind.EndOfFileToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }

            return _tokens[index];
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Match(TokenKind kind)
        {
            if(Current.Kind == kind)
                return NextToken();
            _diagnostics.Add($"ERROR : Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFileToken = Match(TokenKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expression, endOfFileToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }
        private ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            while (Current.Kind == TokenKind.PlusToken ||
                    Current.Kind == TokenKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpression(left, operatorToken, right);
            }

            return left;
        }
        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == TokenKind.SlashToken ||
                    Current.Kind == TokenKind.StarToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpression(left, operatorToken, right);
            }

            return left;
        }
        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == TokenKind.OpenParenthesisToken)
            {
                var openParenth = NextToken();
                var expression = ParseExpression();
                var closeParenth = Match(TokenKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(openParenth, expression, closeParenth);
            }

            var numberToken = Match(TokenKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}