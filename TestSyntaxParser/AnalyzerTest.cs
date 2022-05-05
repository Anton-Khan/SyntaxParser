using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxParser;

namespace TestSyntaxParser
{
    [TestClass]
    public class AnalyzerTest
    {
        [TestMethod]
        public void AnalizeEmptyExpression()
        {
            var input = InputStorage.Empty.TokenList.Tokens;

            bool expected = false;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnalizeOnlyOperations()
        {
            var input = InputStorage.OnlyOperations.TokenList.Tokens;

            bool expected = false;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnalizeSimpleExpression()
        {
            var input = InputStorage.SimpleExpression.TokenList.Tokens;

            bool expected = true;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnalizeFloatExpression()
        {
            var input = InputStorage.SimpleFloatExpression.TokenList.Tokens;

            bool expected = true;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnalizeFloatFuncWithConstantExpression()
        {
            var input = InputStorage.SimpleFloatFuncWithConstExpression.TokenList.Tokens;

            bool expected = true;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnalizeFuncExpression()
        {
            var input = InputStorage.FuncExpression.TokenList.Tokens;

            bool expected = true;
            bool actual = new Analyzer().Analyze(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
