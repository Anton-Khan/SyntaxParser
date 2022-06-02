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

        [TestMethod]
        public void Test6()
        {
            var text = "pow(sin(4), 2) + pow(cos(4), 2)";
            var expected = 1;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test7()
        {
            var text = "(pow(1,3) + pow(12, 3) + pow(9, 3) + pow(10,3))/2";
            var expected = 1729;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void Test8()
        {
            var text = "sqrt(sqrt(sqrt(sqrt(pow(pow(pow(pow(3 ,2) ,2) ,2) ,2)))))";
            var expected = 3;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void TestCircleLength()
        {
            var text = "2 * PI * 4";
            var expected = 25.13274122871834590770114706623602307357735519;
            var expectedDelta = 0.0000000000001;

            var same = DoTestTemplate(text, expected, expectedDelta);

            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public void TestInfiniteUnaryMinus()
        {
            var text = "------------------------------------1";
            var expected = 1;
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
