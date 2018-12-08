using NUnit.Framework;
using Flash;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class LexerTest
    {
        [TestCase("0",TokenKind.NumberToken)]
        [TestCase("1",TokenKind.NumberToken)]
        [TestCase("2",TokenKind.NumberToken)]
        [TestCase("3",TokenKind.NumberToken)]
        [TestCase("4",TokenKind.NumberToken)]
        [TestCase("5",TokenKind.NumberToken)]
        [TestCase("6",TokenKind.NumberToken)]
        [TestCase("7",TokenKind.NumberToken)]
        [TestCase("8",TokenKind.NumberToken)]
        [TestCase("9",TokenKind.NumberToken)]
        [TestCase("1234",TokenKind.NumberToken)]
        [TestCase(" ",TokenKind.WhiteSpaceToken)]
        [TestCase("  ",TokenKind.WhiteSpaceToken)]
        [TestCase("+",TokenKind.PlusToken)]
        [TestCase("-",TokenKind.MinusToken)]
        [TestCase("/",TokenKind.SlashToken)]
        [TestCase("*",TokenKind.StarToken)]
        [TestCase("(",TokenKind.OpenParenthesisToken)]
        [TestCase(")",TokenKind.CloseParenthesisToken)]
        [TestCase("b",TokenKind.BadToken)]
        [TestCase("",TokenKind.EndOfFileToken)]
        public void LexerTokenKind_Test(string test, TokenKind expectedTokenKind)
        {                       
            Assert.AreEqual(expectedTokenKind, new Lexer(test).NextToken().Kind);
        }

        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("2", "2")]
        [TestCase("3", "3")]
        [TestCase("4", "4")]
        [TestCase("5", "5")]
        [TestCase("6", "6")]
        [TestCase("7", "7")]
        [TestCase("8", "8")]
        [TestCase("9", "9")]
        [TestCase("1234", "1234")]
        [TestCase(" ", " ")]
        [TestCase("  ", "  ")]
        [TestCase("+", "+")]
        [TestCase("+", "+")]
        [TestCase("-", "-")]
        [TestCase("/", "/")]
        [TestCase("*", "*")]
        [TestCase("(", "(")]
        [TestCase(")", ")")]
        [TestCase("", "\0")]
        [TestCase("?", "?")]
        public void LexerTokenText_Test(string test, string expectedText)
        {                       
            Assert.AreEqual(expectedText, new Lexer(test).NextToken().Text);
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("4", 4)]
        [TestCase("5", 5)]
        [TestCase("6", 6)]
        [TestCase("7", 7)]
        [TestCase("8", 8)]
        [TestCase("9", 9)]
        [TestCase("1234", 1234)]
        [TestCase(" ", null)]
        [TestCase("  ", null)]
        [TestCase("+", null)]
        [TestCase("-", null)]
        [TestCase("/", null)]
        [TestCase("*", null)]
        [TestCase("(", null)]
        [TestCase(")", null)]
        [TestCase("", null)]
        [TestCase("?", null)]
        public void LexerTokenValue_Test(string test, object expectedValue)
        {                       
            Assert.AreEqual(expectedValue, new Lexer(test).NextToken().Value);
        }

        [TestCase("?")]
        [TestCase("|")]
        [TestCase("~")]
        [TestCase("@")]
        [TestCase("#")]
        [TestCase("$")]
        [TestCase("%")]
        [TestCase("^")]
        [TestCase("&")]
        [TestCase("_")]
        [TestCase("=")]
        [TestCase("\\")]
        [TestCase("}")]
        [TestCase("{")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase(".")]
        [TestCase(",")]
        [TestCase("\"")]
        [TestCase(":")]
        [TestCase(";")]
        [TestCase("'")]        
        public void LexerDiagnostic_Test(string test)
        {
            var lexer = new Lexer(test);
            var token = lexer.NextToken();
            Assert.AreEqual(token.Kind, TokenKind.BadToken);
            Assert.IsTrue(lexer.Diagnostics.Any());
            var msg = lexer.Diagnostics.FirstOrDefault();
            Assert.AreEqual($"ERROR : Bad input char {test}", msg);
        }

        [TestCase("999999999999999")]
        public void LexerDiagnosticInt32_Test(string test)
        {
            var lexer = new Lexer(test);
            var token = lexer.NextToken();
            Assert.AreEqual(token.Kind, TokenKind.NumberToken);
            Assert.IsTrue(lexer.Diagnostics.Any());
            var msg = lexer.Diagnostics.FirstOrDefault();
            Assert.AreEqual($"The number {test} isn't a valid Int32", msg);
        }
    }
}