using System;
using System.Collections.Generic;

namespace Flash
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
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
            
            return new SyntaxToken(kind, Current.Position, null, null); 
        }

        public ExpressionSyntax Parse()
        {
            var left = ParsePrimaryExpression();

            while(Current.Kind == TokenKind.PlusToken ||
                Current.Kind == TokenKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpression(left, operatorToken, right); 
            }
                        
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            var numberToken = Match(TokenKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}