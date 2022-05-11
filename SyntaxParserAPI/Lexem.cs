using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParserAPI
{
    public class Lexem
    {
        public Lexem(String regexp, String name)
        {
            this.Regexp = regexp;
            this.Name = name;
        }

        public String Regexp { get; }
        public String Name { get; }

        public static readonly Lexem INTEGER = new Lexem(@"^(0|[1-9][0-9]*)$", nameof(INTEGER));// ^[-]?((0|[1-9])[0-9]*[.])?[0-9]+
        public static readonly Lexem DOUBLE = new Lexem(@"^(0|[1-9][0-9]*)([.][0-9]*)?$", nameof(DOUBLE));
        public static readonly Lexem COMMA = new Lexem(@"^,$", nameof(COMMA));
        public static readonly Lexem ASSIGN_OP = new Lexem(@"^=$", nameof(ASSIGN_OP));
        public static readonly Lexem OP = new Lexem(@"^(\+|-|\*|\/)$", nameof(OP));
        public static readonly Lexem L_B = new Lexem(@"^\($", nameof(L_B));
        public static readonly Lexem R_B = new Lexem(@"^\)$", nameof(R_B));
        public static readonly Lexem SIN = new Lexem(@"^sin$", nameof(SIN));
        public static readonly Lexem COS = new Lexem(@"^cos$", nameof(COS));
        public static readonly Lexem TAN = new Lexem("^tan$", nameof(TAN));
        public static readonly Lexem LOG = new Lexem("^log$", nameof(LOG));
        public static readonly Lexem LN = new Lexem("^ln$", nameof(LN));
        public static readonly Lexem EXP = new Lexem("^exp$", nameof(EXP));
        public static readonly Lexem POW = new Lexem("^pow$", nameof(POW));
        public static readonly Lexem PI = new Lexem("^PI$", nameof(PI));
        public static readonly Lexem E = new Lexem("^E$", nameof(E));
        public static readonly Lexem COMPARE_OP = new Lexem(@"^(>=|<=|>|<|!=|==)$", "COMPARE_OP");
        
        public static IEnumerable<Lexem> Values
        {
            get
            {
                yield return INTEGER;
                yield return DOUBLE;
                yield return COMMA;
                yield return OP;
                yield return ASSIGN_OP;
                yield return L_B;
                yield return R_B;
                yield return SIN;
                yield return COS;
                yield return TAN;
                yield return LOG;
                yield return LN;
                yield return EXP;
                yield return POW;
                yield return PI;
                yield return E;
                yield return COMPARE_OP;
            }
        }

        public static bool IsFucntion(Lexem function)
        {
            return function == Lexem.SIN || function == Lexem.COS || function == Lexem.TAN || function == Lexem.LN || function == Lexem.LOG || function == Lexem.EXP || function == Lexem.POW;
        }
        public static bool IsOperand(Lexem operand)
        {
            return operand == Lexem.INTEGER || operand == Lexem.DOUBLE || operand == Lexem.PI || operand == Lexem.E;
        }
        public static bool IsBracket(Lexem bracket)
        {
            return bracket == Lexem.L_B || bracket == Lexem.R_B;
        }
        public static bool IsFunctionOfTwo(Lexem function)
        {
            return function == Lexem.LOG || function == Lexem.POW;
        }

        public static readonly Lexem NullableLexem = new Lexem("", nameof(NullableLexem));
        public static readonly Lexem UNARYMINUS = new Lexem("", nameof(UNARYMINUS));
        public static readonly Lexem END = new Lexem("", nameof(END));
        public override string ToString() => Name;
    }
}
