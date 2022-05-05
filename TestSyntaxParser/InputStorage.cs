using SyntaxParser;
using System;
using System.Collections.Generic;

namespace TestSyntaxParser
{
    public static class InputStorage
    {
        public static readonly Input Empty = new Input("", new TokenList(new List<Token>()));

        public static readonly Input SimpleExpression = new Input(
            "1 + 5 * (-3)",
            new TokenList(new List<Token>(new Token[]
            {
                new Token(Lexem.INTEGER, "1"),
                new Token(Lexem.OP, "+"),
                new Token(Lexem.INTEGER, "5"),
                new Token(Lexem.OP, "*"),
                new Token(Lexem.L_B, "("),
                new Token(Lexem.INTEGER, "-3"),
                new Token(Lexem.R_B, ")"),
            }))
        );

        public static readonly Input SimpleFloatExpression = new Input(
            "0.5 + 1.5 * (-123.0004)",
            new TokenList(new List<Token>(new Token[]
            {
                    new Token(Lexem.DOUBLE, "0.5"),
                    new Token(Lexem.OP, "+"),
                    new Token(Lexem.DOUBLE, "1.5"),
                    new Token(Lexem.OP, "*"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.DOUBLE, "-123.0004"),
                    new Token(Lexem.R_B, ")"),
            }))
        );

        public static readonly Input SimpleFloatFuncWithConstExpression = new Input(
            "0.5 + 1.5 * (-123.0004) / pow(PI,E) - log(2,8)",
            new TokenList(new List<Token>(new Token[]
            {
                    new Token(Lexem.DOUBLE, "0.5"),
                    new Token(Lexem.OP, "+"),
                    new Token(Lexem.DOUBLE, "1.5"),
                    new Token(Lexem.OP, "*"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.DOUBLE, "-123.0004"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "/"),
                    new Token(Lexem.POW, "pow"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.PI, "PI"),
                    new Token(Lexem.COMMA, ","),
                    new Token(Lexem.E, "E"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "-"),
                    new Token(Lexem.LOG, "log"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.INTEGER, "2"),
                    new Token(Lexem.COMMA, ","),
                    new Token(Lexem.INTEGER, "8"),
                    new Token(Lexem.R_B, ")"),
            }))
        );

        public static readonly Input FuncExpression = new Input(
            "sin(0.5) + cos(1.5) - ln(-123.0004) * log(3,2.4)/ exp(2) * pow(4,2)",
            new TokenList(new List<Token>(new Token[]
            {
                    new Token(Lexem.SIN, "sin"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.DOUBLE, "0.5"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "+"),
            
                    new Token(Lexem.COS, "cos"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.DOUBLE, "1.5"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "-"),
            
                    new Token(Lexem.LN, "ln"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.DOUBLE, "-123.0004"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "*"),
            
                    new Token(Lexem.LOG, "log"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.INTEGER, "3"),
                    new Token(Lexem.COMMA, ","),
                    new Token(Lexem.DOUBLE, "2.4"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "/"),
            
                    new Token(Lexem.EXP, "exp"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.INTEGER, "2"),
                    new Token(Lexem.R_B, ")"),
                    new Token(Lexem.OP, "*"),
            
                    new Token(Lexem.POW, "pow"),
                    new Token(Lexem.L_B, "("),
                    new Token(Lexem.INTEGER, "4"),
                    new Token(Lexem.COMMA, ","),
                    new Token(Lexem.INTEGER, "2"),
                    new Token(Lexem.R_B, ")"),
            }))
        );

        public static readonly Input OnlyOperations = new Input(
            "+ - * /",
            new TokenList(new List<Token>(new Token[]
            {
                new Token(Lexem.OP, "+"),
                new Token(Lexem.OP, "-"),
                new Token(Lexem.OP, "*"),
                new Token(Lexem.OP, "/"),

            }))
        );
    }

    public class Input
    {
        public Input(string text, TokenList tokenList)
        {
            Text = text;
            TokenList = tokenList;
        }

        public string Text { get; private set; }
        public TokenList TokenList { get; private set; }
    }

    public class TokenList
    {
        public TokenList(List<Token> tokens)
        {
            Tokens = new List<Token>();
            Tokens.AddRange((Token[])tokens.ToArray().Clone());
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
