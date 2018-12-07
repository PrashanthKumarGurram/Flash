using NUnit.Framework;
using Flash;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void Test()
        {
            //Arrange
            var data = "1 + 2 * 3 / 4 ( )";
            //Act 
            var tokens = new Lexer(data).GetTokens().ToList();
            //Assert
            Assert.AreEqual(17, tokens.Count());
            Assert.IsTrue(AreSame(new SyntaxToken(TokenKind.NumberToken,0,"1",1), tokens[0]));            

        }

        private bool AreSame(SyntaxToken expected, SyntaxToken result)
        {
            return expected.Kind == result.Kind && 
                    expected.Position == result.Position &&
                    string.Equals(expected.Text,result.Text) &&
                    expected.Value == expected.Value;
        }
    }
}