
namespace SyntaxParser
{
    public static class Program
    {
        public static void Main()
        {
            var text = "sin(0.5) + cos(1.5) - ln(-123.0004) * log(1)/ exp(2) * pow(4,2)";//"log(2,3) / pow(1,5) + sin(-1) ";

            var list = Parser.Parse(text);
            foreach (var item in list)
            {
                Console.WriteLine(item.Value);
            }

            new Analyzer().Analyze(list); 
        }

    }
}