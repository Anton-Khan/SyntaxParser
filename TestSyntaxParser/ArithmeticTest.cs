using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxParserAPI;
using System;

namespace TestSyntaxParser
{
    [TestClass]
    public class ArithmeticTest
    {
        [TestMethod]
        public void Test1()
        {
            var text = "sin(0.5) + cos(1.5) - ln(123.0004) * log(12,4)/ exp(2) * pow(4,2)";
            var expected = -7.836583189400245;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test2()
        {
            var text = "log(2,3) / pow(1,5) + sin(-1)";
            var expected = 0.7434915159132596748;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test3()
        {
            var text = "pow( pow( log(5, 125)+1 , log((12/3), 16) ) , (2) )";
            var expected = 256;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test4()
        {
            var text = "pow( pow( log(5, 125)+1 , log((12/3), 16)+5 ) , (2) )";
            double expected = 268435456;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test5()
        {
            var text = "-1--(-5)+log(--3,--9)";
            var expected = -4;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        private bool DoTestTemplate(string text, double expected, double expectedDelta)
        {
            var parsedTokens = Parser.Parse(text);
            if (new Analyzer().Analyze(parsedTokens))
            {
                var postfixPolishNotation = new PostfixPolishNotation(parsedTokens);
                var result = postfixPolishNotation.Solve(true);
                Console.WriteLine(result);
                return Math.Abs(expected - result) < expectedDelta;
            }
            else
            {
                throw new System.Exception("Could not pass Analyzer");
            }
        }
    }
}
