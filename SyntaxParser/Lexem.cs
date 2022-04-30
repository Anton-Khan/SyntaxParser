﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
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

        public static readonly Lexem DIGIT = new Lexem(@"^[-]?(0|[1-9][0-9]*)$", nameof(DIGIT));// ^[-]?((0|[1-9])[0-9]*[.])?[0-9]+
        public static readonly Lexem DEC = new Lexem(@"^[-]?(0|[1-9][0-9]*)([.][0-9]*)?$", nameof(DEC));
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

        public static readonly Lexem COMPARE_OP = new Lexem(@"^(>=|<=|>|<|!=|==)$", "COMPARE_OP");

        public static IEnumerable<Lexem> Values
        {
            get
            {
                yield return DIGIT;
                yield return DEC;
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

                yield return COMPARE_OP;
            }
        }

        public static readonly Lexem NullableLexem = new Lexem("", nameof(NullableLexem));
        public override string ToString() => Name;
    }
}
