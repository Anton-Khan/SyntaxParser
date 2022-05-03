using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public class Analyzer
    {
        private static List<Token> tokens;
        private static int iterator;

        public Analyzer()
        {
            tokens = new List<Token>();
            iterator = 0;
        }

        public bool Analyze(List<Token> list)
        {
            try
            {
                tokens.AddRange((Token[])list.ToArray().Clone());
                if (Expression() && iterator == tokens.Count)
                {
                    Console.WriteLine("OK!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Bad!");
                    return false;
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        private static bool Expression()
        {
            try
            {
                if (Value())
                {
                    while (true)
                    {
                        try
                        {
                            Op();
                        }
                        catch (LangException t)
                        {
                            Console.WriteLine("(OP VALUE) in Expression ended  " + t.Message);
                            return true;
                        }
                        if (!Value())
                        {
                            break;
                        }
                    }

                    return true;
                }
                return false;
            }
            catch (LangException e)
            {
                Console.WriteLine(e.Message);
                return false;   
            }
        }


        private static bool Value()
        {
            try
            {
                if (Digit())
                {
                    return true;
                }
                else if (Decimal())
                {
                    return true;
                }
                else if (BracketBody())
                {
                    return true;
                }
                else if (FuncOne())
                {
                    return true;
                }
                else if (FuncTwo())
                {
                    return true;
                }

                return false;
            }
            catch (LangException e )
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private static bool BracketBody()
        {
            return LeftBracket() && Expression() && RightBracket();
        }

        private static bool FuncOne()
        {
            return FuncOneQuantifier() && BracketBody();
        }

        private static bool FuncOneQuantifier()
        {
            if (Sin())
            {
                return true;
            }
            else if (Cos())
            {
                return true;
            }
            else if (Tan())
            {
                return true;
            }
            else if (Ln())
            {
                return true;
            }
            else if (Log())
            {
                return true;
            }
            else if (Exp())
            {
                return true;
            }

            return false;
        }

        private static bool FuncTwo()
        {
            return FuncTwoQuantifier() && LeftBracket() && Expression() && Comma() && Expression() && RightBracket();
        }
        private static bool FuncTwoQuantifier()
        {
            if (Log())
            {
                return true;
            }
            else if (Pow())
            {
                return true;
            }

            return false;
        }
        private static bool Digit()
        {
            return Match(GetNextToken(), Lexem.DIGIT);
        }

        private static bool Decimal()
        {
            return Match(GetNextToken(), Lexem.DEC);
        }
        private static bool Comma()
        {
            return Match(GetNextToken(), Lexem.COMMA);
        }
        private static bool Op()
        {
            return Match(GetNextToken(), Lexem.OP);
        }
        private static bool LeftBracket()
        {
            return Match(GetNextToken(), Lexem.L_B);
        }
        private static bool RightBracket()
        {
            return Match(GetNextToken(), Lexem.R_B);
        }
        private static bool Sin()
        {
            return Match(GetNextToken(), Lexem.SIN);
        }
        private static bool Cos()
        {
            return Match(GetNextToken(), Lexem.COS);
        }
        private static bool Tan()
        {
            return Match(GetNextToken(), Lexem.TAN);
        }
        private static bool Log()
        {
            return Match(GetNextToken(), Lexem.LOG);
        }
        private static bool Ln()
        {
            return Match(GetNextToken(), Lexem.LN);
        }
        private static bool Exp()
        {
            return Match(GetNextToken(), Lexem.EXP);
        }
        private static bool Pow()
        {
            return Match(GetNextToken(), Lexem.POW);
        }

        private static bool Match(Token currentToken, Lexem requiredLexem)
        {
            if (currentToken.Lexem == requiredLexem)
            {
                Console.WriteLine(currentToken.Lexem + " " + currentToken.Value + " -> " + iterator);
                return true;
            }
            Console.WriteLine(requiredLexem.ToString() + " expected, but " + currentToken.Lexem + "(" + currentToken.Value + ") found -> " + iterator);
            iterator--;
            return false;
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
                throw new LangException("Out of Tokens -> " + iterator);
            }
        }
    }
}
