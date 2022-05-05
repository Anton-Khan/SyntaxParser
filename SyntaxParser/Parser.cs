using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public static class Parser
    {
        public static List<Token> Parse(string text)
        {
            List<Token> tokens = new List<Token>();
            text = text.Replace(" ", "") + " ";
            int iterator = 0;
            string subject = "";
            Token? tempToken = null;

            while (text.Length > 0)
            {
                subject = subject + text[iterator];
                if (subject == " ")
                    break;

                var matches = Match(subject);

                if (matches.Item2.Lexem != Lexem.NullableLexem)
                {
                    tempToken = matches.Item2;
                }
                else if (tempToken != null)
                {
                    tokens.Add(tempToken);
                    text = text.Substring(tempToken.Value.Length);
                    subject = "";
                    iterator = -1;
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

        private static (bool, Token) Match(string subject)
        {
            var matchedTokens = TryMatch(subject);

            if (matchedTokens.Count == 0)
            {
                return (false, new Token(Lexem.NullableLexem, ""));
            }

            if (matchedTokens.Count > 1)
            {
                return (true, matchedTokens.First());
            }
            
            return (true, matchedTokens.First());
        }

    }
}
