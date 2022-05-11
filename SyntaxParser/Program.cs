using SyntaxParserAPI;

namespace SyntaxParserApp
{
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

            SyntaxParser.Parse(input, true, true, true, true);            
        }
    }
}

/*
    "sin(0.5) + cos(1.5) - ln(123.0004) * log(12,4)/ exp(2) * pow(4,2)"
    "log(2,3) / pow(1,5) + sin(-1)"
    "pow( pow( log(5, 125)+1 , log((12/3), 16) ) , (2) )"
    "pow( pow( log(5, 125)+1 , log((12/3), 16)+5 ) , (2) )"
    "pow(pow(log(5, 125)+1 , log((12/3), 16)+5 ),(2)) / log(3,9)"
    "-1--(-5)+log(--3,--9)"
    */
