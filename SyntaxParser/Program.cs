
namespace SyntaxParser
{
    public static class Program
    {
        public static void Main()
        {
            var text = "log(2,3) / ln(1)";

            var list = Parser.Parse(text);
            foreach (var item in list)
            {
                Console.WriteLine(item.Value);
            }

            SyntaxAnalyzer.Analyze(list);
        }

    }
}