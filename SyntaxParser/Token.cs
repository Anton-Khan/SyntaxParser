using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public class Token
    {
        public Token(Lexem lexem, string value)
        {
            Lexem = lexem;
            Value = value;
        }

        public Lexem Lexem { get; private set; }
        public String Value { get; set; }

        public bool SameAs(Token other)
        {
            return Lexem == other.Lexem && Value == other.Value;
        }

        public override string ToString()
        {
            return Lexem.ToString().PadRight(8) + Value;
        }
    }
}
