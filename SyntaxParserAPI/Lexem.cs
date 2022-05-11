using System;
using System.Collections.Generic;

namespace SyntaxParserAPI
{
    /// <summary>Содержит все основные лексемы и группы лексем языка.</summary>
    public class Lexem
    {
        /// <summary>Инициализирует новый экземпляр класса Lexem.</summary>
        /// <param name="regexp">Шаблон регулярного выражения.</param>
        /// <param name="name">Название лексемы.</param>
        public Lexem(String regexp, String name)
        {
            this.Regexp = regexp;
            this.Name = name;
        }

        /// <summary>Возврщает шаблон регулярного выражения.</summary>
        /// <value>Шаблон регулярного выражения.</value>
        public String Regexp { get; }
        /// <summary>Возвращает название лексемы.</summary>
        /// <value>Название лексемы.</value>
        public String Name { get; }

        /// <summary>Стандартная лексема Integer</summary>
        /// <remarks>Является операндом.</remarks>
        public static readonly Lexem INTEGER = new Lexem(@"^(0|[1-9][0-9]*)$", nameof(INTEGER));// ^[-]?((0|[1-9])[0-9]*[.])?[0-9]+
        /// <summary>Стандартная лексема Double</summary>
        /// <remarks>Является операндом.</remarks>
        public static readonly Lexem DOUBLE = new Lexem(@"^(0|[1-9][0-9]*)([.][0-9]*)?$", nameof(DOUBLE));
        /// <summary>Стандартная лексема Comma</summary>
        /// <remarks>Запятая.</remarks>
        public static readonly Lexem COMMA = new Lexem(@"^,$", nameof(COMMA));
        /// <summary>Стандартная лексема OP</summary>
        /// <remarks>Является операцией и содержит "+ - * /" .</remarks>
        public static readonly Lexem OP = new Lexem(@"^(\+|-|\*|\/)$", nameof(OP));
        /// <summary>Стандартная лексема L_B</summary>
        /// <remarks>Является скобкой (левой).</remarks>
        public static readonly Lexem L_B = new Lexem(@"^\($", nameof(L_B));
        /// <summary>Стандартная лексема R_B</summary>
        /// <remarks>Является скобкой (правой).</remarks>
        public static readonly Lexem R_B = new Lexem(@"^\)$", nameof(R_B));
        /// <summary>Стандартная лексема Sin</summary>
        /// <remarks>Является функцией одного переменного.</remarks>
        public static readonly Lexem SIN = new Lexem(@"^sin$", nameof(SIN));
        /// <summary>Стандартная лексема Cos</summary>
        /// <remarks>Является функцией одного переменного.</remarks>
        public static readonly Lexem COS = new Lexem(@"^cos$", nameof(COS));
        /// <summary>Стандартная лексема Tan</summary>
        /// <remarks>Является функцией одного переменного.</remarks>
        public static readonly Lexem TAN = new Lexem("^tan$", nameof(TAN));
        /// <summary>Стандартная лексема Log</summary>
        /// <remarks>Является функцией двух переменных.</remarks>
        public static readonly Lexem LOG = new Lexem("^log$", nameof(LOG));
        /// <summary>Стандартная лексема Ln (Log2 в библиотеке Math) </summary>
        /// <remarks>Является функцией одного переменного.</remarks>
        public static readonly Lexem LN = new Lexem("^ln$", nameof(LN));
        /// <summary>Стандартная лексема Exp</summary>
        /// <remarks>Является функцией одного переменного.</remarks>
        public static readonly Lexem EXP = new Lexem("^exp$", nameof(EXP));
        /// <summary>Стандартная лексема Pow</summary>
        /// <remarks>Является функцией двух переменных.</remarks>
        public static readonly Lexem POW = new Lexem("^pow$", nameof(POW));
        /// <summary>Стандартная лексема PI. Математическая константа pi.</summary>
        /// <remarks>Является операндом.</remarks>
        public static readonly Lexem PI = new Lexem("^PI$", nameof(PI));
        /// <summary>Стандартная лексема E. Математическая константа e.</summary>
        /// <remarks>Является операндом.</remarks>
        public static readonly Lexem E = new Lexem("^E$", nameof(E));

        /// <summary>Возвращает полный список доступных лексем.</summary>
        /// <value>Доступные лексемы.</value>
        public static IEnumerable<Lexem> Values
        {
            get
            {
                yield return INTEGER;
                yield return DOUBLE;
                yield return COMMA;
                yield return OP;
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
            }
        }

        /// <summary>Проверяет, является ли полученная лексема функцией.</summary>
        /// <param name="function">Лексема требующая проверки.</param>
        /// <returns>
        ///   <c>true</c> если полученная лексема функция. ; иначе, <c>false</c>.</returns>
        public static bool IsFucntion(Lexem function)
        {
            return function == Lexem.SIN || function == Lexem.COS || function == Lexem.TAN || function == Lexem.LN || function == Lexem.LOG || function == Lexem.EXP || function == Lexem.POW;
        }
        /// <summary>Проверяет, является ли полученная лексема операндом.</summary>
        /// <param name="operand">Лексема требующая проверки.</param>
        /// <returns>
        ///   <c>true</c> если полученная лексема операнд. ; иначе, <c>false</c>.</returns>
        public static bool IsOperand(Lexem operand)
        {
            return operand == Lexem.INTEGER || operand == Lexem.DOUBLE || operand == Lexem.PI || operand == Lexem.E;
        }
        /// <summary>Проверяет, является ли полученная лексема скобкой.</summary>
        /// <param name="bracket">Лексема требующая проверки.</param>
        /// <returns>
        ///   <c>true</c> если полученная лексема скобка. ; иначе, <c>false</c>.</returns>
        public static bool IsBracket(Lexem bracket)
        {
            return bracket == Lexem.L_B || bracket == Lexem.R_B;
        }
        /// <summary>Проверяет, является ли полученная лексема функцией двух переменных.</summary>
        /// <param name="function">Лексема требующая проверки.</param>
        /// <returns>
        ///   <c>true</c> если полученная лексема функция двух переменных. ; иначе, <c>false</c>.</returns>
        public static bool IsFunctionOfTwo(Lexem function)
        {
            return function == Lexem.LOG || function == Lexem.POW;
        }

        /// <summary>Служебная лексема NullableLexem.Означает пустую лексему.</summary>
        public static readonly Lexem NullableLexem = new Lexem("", nameof(NullableLexem));
        /// <summary>Служебная лексема UNARYMINUS.Нужна для преобразования минуса в выражении в унарный минус.</summary>
        public static readonly Lexem UNARYMINUS = new Lexem("", nameof(UNARYMINUS));
        /// <summary>Служебная лексема END.Означает конец выражения.</summary>
        public static readonly Lexem END = new Lexem("", nameof(END));
        /// <summary>Конвертирует лексему в строку.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => Name;
    }
}
