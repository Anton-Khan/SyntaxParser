

namespace SyntaxParser
{
    public class Analyzer
    {
        private List<Token> tokens;
        private int iterator;

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
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    return true;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    return false;
                }
               
            }
            catch (Exception e)
            {
                ShowError(e);
                return false;
            }
        }
        private bool Expression()
        {
            try
            {
                if (Value())
                {
                    while (true)
                    {
                        try
                        {
                            if (!Op())
                            {
                                return true;
                            }
                        }
                        catch (LangException t)
                        {
                            Console.WriteLine("(OP VALUE) in Expression ended {" + t.Message + "}");
                            return true;
                        }
                        if (!Value())
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (LangException e)
            {
                ShowError(e);
                return false;   
            }
        }


        private bool Value()
        {
            while(true)
            {
                try
                {
                    if(!UnaryMinus())
                    {
                        break;
                    }
                }
                catch (LangException)
                {
                }
            }

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
                else if (PI())
                {
                    return true;
                }
                else if (E())
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
                ShowError(e);
                return false;
            }
        }

        private bool BracketBody()
        {
            return LeftBracket() && Expression() && RightBracket();
        }

        private bool FuncOne()
        {
            return FuncOneQuantifier() && BracketBody();
        }

        private bool FuncOneQuantifier()
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
            else if (Exp())
            {
                return true;
            }

            return false;
        }

        private void ShowError(Exception e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(e.Message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private bool FuncTwo()
        {
            return FuncTwoQuantifier() && LeftBracket() && Expression() & Comma() && Expression() && RightBracket();
        }
        private bool FuncTwoQuantifier()
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
        private bool Digit()
        {
            return Match(GetNextToken(), Lexem.INTEGER);
        }

        private bool Decimal()
        {
            return Match(GetNextToken(), Lexem.DOUBLE);
        }

        private bool E()
        {
            return Match(GetNextToken(), Lexem.E);
        }

        private bool PI()
        {
            return Match(GetNextToken(), Lexem.PI);
        }
        private bool Comma()
        {
            return Match(GetNextToken(), Lexem.COMMA);
        }
        private bool Op()
        {
            return Match(GetNextToken(), Lexem.OP);
        }
        private bool UnaryMinus()
        {
            return Match(GetNextToken(), Lexem.UNARYMINUS);
        }
        private bool LeftBracket()
        {
            return Match(GetNextToken(), Lexem.L_B);
        }
        private bool RightBracket()
        {
            return Match(GetNextToken(), Lexem.R_B);
        }
        private bool Sin()
        {
            return Match(GetNextToken(), Lexem.SIN);
        }
        private bool Cos()
        {
            return Match(GetNextToken(), Lexem.COS);
        }
        private bool Tan()
        {
            return Match(GetNextToken(), Lexem.TAN);
        }
        private bool Log()
        {
            return Match(GetNextToken(), Lexem.LOG);
        }
        private bool Ln()
        {
            return Match(GetNextToken(), Lexem.LN);
        }
        private bool Exp()
        {
            return Match(GetNextToken(), Lexem.EXP);
        }
        private bool Pow()
        {
            return Match(GetNextToken(), Lexem.POW);
        }

        private bool Match(Token currentToken, Lexem requiredLexem)
        {
            if (currentToken.Lexem == requiredLexem)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine(iterator + ") " + currentToken.Lexem + " " + currentToken.Value);
                Console.BackgroundColor = ConsoleColor.Black;
                return true;
            }
            Console.WriteLine(requiredLexem.ToString() + " expected, but " + currentToken.Lexem + "(" + currentToken.Value + ") found -> " + iterator);
            iterator--;
            return false;
        }

        private Token GetNextToken()
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
