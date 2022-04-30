
namespace SyntaxParser
{
    public static class Program
    {
        public static void Main()
        {
            var text = "5.7 + 3 + log(4)";

            var list = Parser.Parse(text);
            foreach (var item in list)
            {
                Console.WriteLine(item.Value);
            }
        }

    }
}