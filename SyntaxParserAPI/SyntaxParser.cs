
namespace SyntaxParserAPI
{
    /// <summary>
    ///   <para>Основной класс для работы с синтаксическим парсером.</para>
    /// </summary>
    public static class SyntaxParser
    {
        /// <summary>Решает поданное на вход выражение.</summary>
        /// <param name="expression">Выражение.</param>
        /// <param name="showParsingModule">При установке в true [Показывает работу модуля Parser].</param>
        /// <param name="showAnalizingModule">При установке в true [Показывает работу модуля Analyzer].</param>
        /// <param name="showPpnBuilderModule">При установке в true [Показывает работу модуля строящего PostfixPolishNotation ].</param>
        /// <param name="showPpnSolverModule">При установке в true [Показывает работу модуля решающего PostfixPolishNotation ].</param>
        /// <remarks>
        ///   <para>Функция оследовательно обрабатыет поданное на вход выражение.<br />1. Парсит входное выражение, разбирая его на лексемы.<br />2. Полученные лексемы анализируются по правилам определенной грамматики.<br />3. По завершению анализа строится обратная польская нотация.<br />4. Обратная польская нотация последовательно решается.</para>
        /// </remarks>
        /// <example>
        ///   <code>using SyntaxParserAPI;
        ///
        /// namespace YourNamespace
        /// {
        ///     public static class Program
        ///     {
        ///         public static void Main(string[] args)
        ///         {
        ///             string? input = Console.ReadLine();
        ///
        ///             SyntaxParser.Parse(input);
        ///         }
        ///     }
        /// }</code>
        /// </example>
        public static void Parse(string? expression, bool showParsingModule = false, bool showAnalizingModule = false, bool showPpnBuilderModule = false, bool showPpnSolverModule = false)
        {
            try
            {
                if (expression == null)
                {
                    throw new Exception("Input is null");
                }

                var parsedTokens = ParseInputString(expression, showParsingModule);

                Analyze(parsedTokens, showAnalizingModule);

                var postfixPolishNotation = ExpressionToPostfixPolishNotation(parsedTokens, showPpnBuilderModule);

                var result = SolvePostfixPolishNotation(postfixPolishNotation, showPpnSolverModule);

                Console.WriteLine($"Result = {result}");
            }
            catch (LangException lng)
            {
                Console.WriteLine(lng.Message);
            }
            catch(StackMachineException stm)
            {
                Console.WriteLine(stm.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static List<Token> ParseInputString(string input, bool showParsingModule = true)
        {
            var parsedTokens = Parser.Parse(input);

            if (showParsingModule)
            {
                Console.WriteLine("\tParsing Module");
                for (int i = 0; i < parsedTokens.Count; i++)
                {
                    Console.WriteLine(i + ")".PadRight(6 - i.ToString().Length) + parsedTokens[i].ToString());
                }
                Console.WriteLine();
            }
            return parsedTokens;
        }

        private static void Analyze(List<Token> parsedTokens, bool showAnalizingModule = true)
        {
            if (showAnalizingModule)
                Console.WriteLine("\tSyntax analyzing Module");
            if (!new Analyzer().Analyze(parsedTokens, showAnalizingModule))
            {
                throw new Exception("Wrong Expression");
            }
            Console.WriteLine();
        }

        private static PostfixPolishNotation ExpressionToPostfixPolishNotation(List<Token> parsedTokens, bool showPpnBuilderModule = true)
        {
            var postfixPolishNotation = new PostfixPolishNotation(parsedTokens);

            if (showPpnBuilderModule)
            {
                Console.WriteLine("\tBuilding Postfix Polish Notation Module");
                for (int i = 0; i < postfixPolishNotation.Tokens.Count; i++)
                {
                    Console.WriteLine(i + ")".PadRight(6 - i.ToString().Length) + postfixPolishNotation.Tokens[i].ToString());
                }
                Console.WriteLine();
            }
            return postfixPolishNotation;
        }

        private static double SolvePostfixPolishNotation(PostfixPolishNotation ppn, bool showPpnSolverModule = true)
        {
            if (showPpnSolverModule)
            {
                Console.WriteLine("\tSolving Postfix Polish Notation Module");
            }
            var result = ppn.Solve(showPpnSolverModule);
            Console.WriteLine();
            return result;
        }
    }
}
