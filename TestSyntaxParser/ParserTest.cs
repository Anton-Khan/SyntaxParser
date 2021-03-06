using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxParserAPI;
using System.Collections.Generic;

namespace TestSyntaxParser
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParseEmptyString()
        {
            string text = InputStorage.Empty.Text;
            var result = Parser.Parse(text);

            Assert.AreEqual(result.Count, InputStorage.Empty.TokenList.Tokens.Count, "Null String creates some Tokens");
        }

        [TestMethod]
        public void ParseOnlyOperations()
        {
            var input = InputStorage.OnlyOperations;
            string text = input.Text;
            TokenList expected = input.TokenList;

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseSimpleExpression()
        {
            var input = InputStorage.SimpleExpression;
            string text = input.Text;
            TokenList expected = input.TokenList;

            TokenList actual = new TokenList( Parser.Parse(text) );

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseFloatExpression()
        {
            var input = InputStorage.SimpleFloatExpression;
            string text = input.Text;
            TokenList expected = input.TokenList;

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseFloatFuncWithConstantExpression()
        {
            var input = InputStorage.SimpleFloatFuncWithConstExpression;
            string text = input.Text;
            TokenList expected = input.TokenList;

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseFuncExpression()
        {
            var input = InputStorage.FuncExpression;
            string text = input.Text;
            TokenList expected = input.TokenList;

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }
    }
   
}