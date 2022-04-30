using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxParser;
using System;
using System.Collections.Generic;

namespace TestSyntaxParser
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParseEmptyString()
        {
            string text = "";
            var result = Parser.Parse(text);

            Assert.AreEqual(result.Count, 0, "Null String creates some Tokens");
        }

        [TestMethod]
        public void ParseSimpleExpression()
        {
            string text = "1 + 5 * 3";
            TokenList expected = new TokenList( new List<Token>( new Token[] 
            { 
                new Token(Lexem.DIGIT, "1"), 
                new Token(Lexem.OP, "+"), 
                new Token(Lexem.DIGIT, "5"),
                new Token(Lexem.OP, "*"), 
                new Token(Lexem.DIGIT, "3")
            }));

            TokenList actual = new TokenList( Parser.Parse(text) );

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseFloatExpression()
        {
            string text = "0.5 + 1.5 * (-123.0004)";
            TokenList expected = new TokenList(new List<Token>(new Token[]
            {
                new Token(Lexem.DEC, "0.5"),
                new Token(Lexem.OP, "+"),
                new Token(Lexem.DEC, "1.5"),
                new Token(Lexem.OP, "*"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DEC, "-123.0004"),
                new Token(Lexem.R_B, ")"),
            }));

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }

        [TestMethod]
        public void ParseFuncExpression()
        {
            string text = "sin(0.5) + cos(1.5) - ln(-123.0004) * log(1)/ exp(2) * pow(4,2)";
            TokenList expected = new TokenList(new List<Token>(new Token[]
            {
                new Token(Lexem.SIN, "sin"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DEC, "0.5"),
                new Token(Lexem.R_B, ")"),
                new Token(Lexem.OP, "+"),

                new Token(Lexem.COS, "cos"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DEC, "1.5"),
                new Token(Lexem.R_B, ")"),
                new Token(Lexem.OP, "-"),

                new Token(Lexem.LN, "ln"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DEC, "-123.0004"),
                new Token(Lexem.R_B, ")"),
                new Token(Lexem.OP, "*"),

                new Token(Lexem.LOG, "log"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DIGIT, "1"),
                new Token(Lexem.R_B, ")"),
                new Token(Lexem.OP, "/"),

                new Token(Lexem.EXP, "exp"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DIGIT, "2"),
                new Token(Lexem.R_B, ")"),
                new Token(Lexem.OP, "*"),

                new Token(Lexem.POW, "pow"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.DIGIT, "4"),
                new Token(Lexem.COMMA, ","),
                new Token(Lexem.DIGIT, "2"),
                new Token(Lexem.R_B, ")"),
            }));

            TokenList actual = new TokenList(Parser.Parse(text));

            Assert.AreEqual(true, actual.SameAs(expected));
        }
    }

    public class TokenList
    {
        public TokenList(List<Token> tokens)
        {
            Tokens = tokens;
        }
        public List<Token> Tokens { get; set; }

        public bool SameAs(TokenList other)
        {
            if (Tokens.Count != other.Tokens.Count)
            {
                throw new ArgumentException("Lengths are different");
            }

            bool isDifferent = false;
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (!other.Tokens[i].SameAs(Tokens[i]))
                {
                    isDifferent = true;
                }
            }

            return !isDifferent;
        }

    }

    
}