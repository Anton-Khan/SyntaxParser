
namespace SyntaxParser
{
    /*
    "sin(0.5) + cos(1.5) - ln(123.0004) * log(12,4)/ exp(2) * pow(4,2)"
    "log(2,3) / pow(1,5) + sin(-1)"
    "pow( pow( log(5, 125)+1 , log((12/3), 16) ) , (2) )"
    "pow( pow( log(5, 125)+1 , log((12/3), 16)+5 ) , (2) )"
    "pow(pow(log(5, 125)+1 , log((12/3), 16)+5 ),(2)) / log(3,9)"
    */


    public static class Program
    {
        public static void Main(string[] args)
        {
            string? input = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Enter new Expression:\n");
                input = Console.ReadLine();
                Console.WriteLine();
            }
            else
            {
                foreach (string item in args)
                {
                    input += item;
                }
                Console.WriteLine(input+"\n");
            }

            try
            {
                if (input == null)
                {
                    throw new Exception("Input equals null");
                }

                var parsedTokens = Parse(input);

                Analyze(parsedTokens);

                var postfixPolishNotation = ExpressionToPostfixPolishNotation(parsedTokens);

                var result = SolvePostfixPolishNotation(postfixPolishNotation);

                Console.WriteLine($"Result = {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<Token> Parse(string input)
        {
            var parsedTokens = Parser.Parse(input);

            Console.WriteLine("\tParsing Module");
            for (int i = 0; i < parsedTokens.Count; i++)
            {
                Console.WriteLine(i + ")".PadRight(6 - i.ToString().Length) + parsedTokens[i].ToString());
            }
            Console.WriteLine();
            return parsedTokens;
        }

        private static void Analyze(List<Token> parsedTokens)
        {
            Console.WriteLine("\tSyntax analyzing Module");
            if (!new Analyzer().Analyze(parsedTokens))
            {
                throw new Exception("Wrong Expression");
            }
            Console.WriteLine();
        }

        private static PostfixPolishNotation ExpressionToPostfixPolishNotation(List<Token> parsedTokens)
        {
            var postfixPolishNotation = new PostfixPolishNotation(parsedTokens);

            Console.WriteLine("\tBuilding Postfix Polish Notation Module");
            for (int i = 0; i < postfixPolishNotation.Tokens.Count; i++)
            {
                Console.WriteLine(i + ")".PadRight(6 - i.ToString().Length) + postfixPolishNotation.Tokens[i].ToString());
            }
            Console.WriteLine();
            return postfixPolishNotation;
        }

        private static double SolvePostfixPolishNotation(PostfixPolishNotation ppn)
        {
            Console.WriteLine("\tSolving Postfix Polish Notation Module");
            var result = ppn.Solve(true);
            Console.WriteLine();
            return result;
            
        }
    }
}