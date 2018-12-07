namespace Flash
{
    public class SyntaxToken
    {
        public SyntaxToken(TokenKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public TokenKind Kind { get; }
        public string Text { get; }
        public int Position { get; }
        public object Value {get;}
    }
}