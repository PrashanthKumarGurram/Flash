using System.Collections.Generic;
using System.Linq;
using Flash.Syntax;

namespace Flash
{
    public class Lexer
    {
        private string _text;
        private int _position;
        private List<string> _diagnostics = 
            new List<string>(); 

        public Lexer(string text)
        {
            _text = text;
        }

        private char Current => 
            (_position >= _text.Length) ? '\0' : _text[_position];

        private void Next()
        {
            _position++;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;
        public SyntaxToken NextToken()
        {
            if(_position >= _text.Length)
                return new SyntaxToken(
                    TokenKind.EndOfFileToken, _position, "\0", null);

            if(char.IsDigit(Current))
            {
                var start = _position;
                while(char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = this._text.Substring(start, length);
                if(!int.TryParse(text,out var value))
                    _diagnostics.Add($"The number {text} isn't a valid Int32");
                return new SyntaxToken(TokenKind.NumberToken,start,text,value);
            }

            if(char.IsWhiteSpace(Current))
            {
                var start = _position;
                while(char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = this._text.Substring(start, length);
                return new SyntaxToken(TokenKind.WhiteSpaceToken,start,text,null);
            }

            if(Current == '+')
                return new SyntaxToken(TokenKind.PlusToken,_position++,"+",null);
            else if(Current == '-')
                return new SyntaxToken(TokenKind.MinusToken,_position++,"-",null);
            if(Current == '*')
                return new SyntaxToken(TokenKind.StarToken,_position++,"*",null);
            else if(Current == '/')
                return new SyntaxToken(TokenKind.SlashToken,_position++,"/",null);
            if(Current == '(')
                return new SyntaxToken(TokenKind.OpenParenthesisToken,_position++,"(",null);
            else if(Current == ')')
                return new SyntaxToken(TokenKind.CloseParenthesisToken,_position++,")",null);
            
            _diagnostics.Add($"ERROR : Bad input char {Current}");
            return new SyntaxToken(
                TokenKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }        
    }
}