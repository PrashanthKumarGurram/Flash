using System.Collections.Generic;
using System.Linq;

namespace Flash
{
    public class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(TokenKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override TokenKind Kind { get; }
        public string Text { get; }
        public int Position { get; }
        public object Value {get;}

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}