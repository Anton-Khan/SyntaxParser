using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SyntaxParserAPI
{
    /// <summary>Класс позволяющий разбить выражиние на токены.</summary>
    public static class Parser
    {
        /// <summary>Разбивает входящую строку на токены, при условии, что выражение содержит только лексемы языка.</summary>
        /// <param name="text">Выражение из допустимых лексем.</param>
        /// <returns>
        ///   <para>Возвращяет список полученных токенов.
        /// <see cref="System.Collections.Generic.List{Token}" />;
        /// </para>
        /// </returns>
        /// <exception cref="System.Exception">Input isn\'t expression at all!\nCan\'t match Lexem!\nIterator &gt;= Length of Input</exception>
        public static List<Token> Parse(string text)
        {
            List<Token> tokens = new List<Token>();
            text = "("+text.Replace(" ", "") + ") ";
            int iterator = 0;
            string subject = "";
            Token? tempToken = null;

            while (text.Length > 0)
            {
                subject += text[iterator];
                if (subject == " ")
                    break;
               
                var matches = Match(subject);

                if (matches.Lexem != Lexem.NullableLexem)
                {
                    tempToken = matches;
                }
                else if (tempToken != null)
                {
                    if (tempToken.Value == "-" && (tokens.Last().Lexem == Lexem.OP || tokens.Last().Lexem == Lexem.L_B || tokens.Last().Lexem == Lexem.COMMA || tokens.Last().Lexem == Lexem.UNARYMINUS) )
                    {
                        tempToken = new Token(Lexem.UNARYMINUS, "~");
                    }

                    AddNewToken(tokens, tempToken, ref text, ref subject, ref iterator);
                    tempToken = null;
                }

                iterator++;
                if (iterator >= text.Length)
                {
                    throw new Exception("Input isn\'t expression at all!\nCan\'t match Lexem!\nIterator >= Length of Input");
                }
            }
            return tokens;
        }

        private static void AddNewToken(List<Token> tokens, Token tempToken, ref string text, ref string subject, ref int iterator)
        {
            tokens.Add(tempToken);
            text = text.Substring(tempToken.Value.Length);
            subject = "";
            iterator = -1;
        }

        private static List<Token> TryMatch(string subject)
        {
            List<Token> matches = new List<Token>();

            foreach(var lexem in Lexem.Values)
            {
                if (Regex.IsMatch(subject, lexem.Regexp))
                {
                    matches.Add(new Token(lexem, subject));
                }
            }
            return matches;
        }

        private static Token Match(string subject)
        {
            var matchedTokens = TryMatch(subject);

            if (matchedTokens.Count == 0)
            {
                return new Token(Lexem.NullableLexem, "");
            }

            if (matchedTokens.Count > 1)
            {
                return matchedTokens.First();
            }
            
            return matchedTokens.First();
        }
    }
}
