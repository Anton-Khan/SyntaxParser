using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public static class SyntaxAnalyzer
    {
        private static List<Token> tokens;
        private static int iterator;
        public static void Analyze(List<Token> tokenList)
        {
            tokens = tokenList;
            iterator = 0;

            try
            {
                Expression();
            }
            catch (LangException e)
            {
                Console.WriteLine(e.Message);
            }


            tokens.Clear();
            iterator = 0;
        }

        private static void Expression()
        {
            Value();
            try
            {
                while (true)
                {
                    Op();
                    Value();
                }
            }
            catch (LangException e)
            {
                iterator--;
                throw new LangException(
                    e.Message + "\n"
                    );
            }
        }

        private static void Value()
        {
            try
            {
                Digit();
            }
            catch (LangException digit)
            {
                try
                {
                    iterator--;
                    Decimal();
                }
                catch (LangException dec)
                {
                    try
                    {
                        iterator--;
                        BracketBody();
                    }
                    catch (LangException bb)
                    {
                        try
                        {
                            iterator--;
                            FuncOne();
                        }
                        catch (LangException fo)
                        {
                            try
                            {
                                iterator--;
                                FuncTwo();
                            }
                            catch (LangException ft)
                            {
                                throw new LangException(
                                    "Value doesn't Exist\n" +
                                    digit.Message + "\n" +
                                    dec.Message + "\n" +
                                    bb.Message + "\n" +
                                    fo.Message + "\n" +
                                    ft.Message + "\n"
                                    );

                            }
                        }
                    }
                }
            }
        }

        private static void BracketBody()
        {
            LeftBracket();
            Expression();
            RightBracket();
        }

        private static void FuncOne()
        {
            FuncOneQuantifier();
            BracketBody();
        }

        private static void FuncTwo()
        {
            FuncTwoQuantifier();
            LeftBracket();
            Expression();
            Comma();
            Expression();
            RightBracket();
        }

        private static void FuncOneQuantifier()
        {
            try
            {
                Sin();
            }catch (LangException sin)
            {
                try
                {
                    iterator--;
                    Cos();
                }
                catch (LangException cos)
                {
                    try
                    {
                        iterator--;
                        Tan();
                    }catch (LangException tan)
                    {
                        try
                        {
                            iterator--;
                            Ln();
                        }
                        catch (LangException ln)
                        {
                            try
                            {
                                iterator--;
                                Exp();
                            }
                            catch (LangException exp)
                            {
                                throw new LangException(
                                    "FuncOneQuantifier doesn't Exist" +
                                    sin.Message + "\n" +
                                    cos.Message + "\n" +
                                    tan.Message + "\n" +
                                    ln.Message + "\n" +
                                    exp.Message + "\n"
                                    );
                            }
                        }
                    }
                }
            }
        }

        private static void FuncTwoQuantifier()
        {
            try
            {
                Log();
            }
            catch (LangException log)
            {
                try
                {
                    iterator--;
                    Pow();
                }
                catch (LangException pow)
                {
                    throw new LangException("FuncTwoQuantifier doesn't Exist\n" + log.Message + "\n" + pow.Message );
                }
                
            }
        }

        private static void Digit()
        {
            Match(GetNextToken(), Lexem.DIGIT);
        }

        private static void Decimal()
        {
            Match(GetNextToken(), Lexem.DEC);
        }
        private static void Comma()
        {
            Match(GetNextToken(), Lexem.COMMA);
        }
        private static void Op()
        {
            Match(GetNextToken(), Lexem.OP);
        }
        private static void LeftBracket()
        {
            Match(GetNextToken(), Lexem.L_B);
        }
        private static void RightBracket()
        {
            Match(GetNextToken(), Lexem.R_B);
        }
        private static void Sin()
        {
            Match(GetNextToken(), Lexem.SIN);
        }
        private static void Cos()
        {
            Match(GetNextToken(), Lexem.COS);
        }
        private static void Tan()
        {
            Match(GetNextToken(), Lexem.TAN);
        }
        private static void Log()
        {
            Match(GetNextToken(), Lexem.LOG);
        }
        private static void Ln()
        {
            Match(GetNextToken(), Lexem.LN);
        }
        private static void Exp()
        {
            Match(GetNextToken(), Lexem.EXP);
        }
        private static void Pow()
        {
            Match(GetNextToken(), Lexem.POW);
        }

        private static void Match(Token currentToken, Lexem requiredLexem)
        {
            if (currentToken.Lexem != requiredLexem)
            {
                throw new LangException(requiredLexem.ToString() + " expected, but " + currentToken.Lexem + "(" + currentToken.Value + ") found -> " + iterator);
            }
            Console.WriteLine(currentToken.Lexem + " " + currentToken.Value + " -> " + iterator);
        }

        private static Token GetNextToken()
        {
            if (tokens.Count > iterator)
            {
                var nextToken = tokens[iterator];
                iterator++;
                return nextToken;
            }
            else
            {
                Console.WriteLine("\nGood!");
                throw new LangException("Out of Tokens -> " + iterator);
                
            }
        }


    }
}
