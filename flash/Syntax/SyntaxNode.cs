using System.Collections.Generic;

namespace Flash.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract TokenKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}