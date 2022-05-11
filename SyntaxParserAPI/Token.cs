using System;

namespace SyntaxParserAPI
{
    /// <summary>Класс содержит информацию о полученной лексеме и значении.</summary>
    public class Token
    {
        /// <summary>Инициализирует новый экземпляр класса Token.</summary>
        /// <param name="lexem">Шаблон регулярного выражения <see cref="SyntaxParserAPI.Lexem" /> .</param>
        /// <param name="value">Реальное значение Токена в виде <see cref="System.String" /> .</param>
        public Token(Lexem lexem, string value)
        {
            Lexem = lexem;
            Value = value;
        }

        /// <summary>Возврщает шаблон регулярного выражения .</summary>
        /// <value>Шаблон регулярного выражения <see cref="SyntaxParserAPI.Lexem" />.</value>
        public Lexem Lexem { get; private set; }
        /// <summary>Реальное значение Токена.</summary>
        /// <value>Реальное значение Токена <see cref="System.String"  />.</value>
        public String Value { get; set; }
        /// <summary>Проверяет по значению, является ли полученный токен одинаковым с this.</summary>
        /// <param name="other">Другой токен.</param>
        /// <returns>true если токены одинаковы; иначе false</returns>
        public bool SameAs(Token other)
        {
            return Lexem == other.Lexem && Value == other.Value;
        }

        /// <summary>Конвертирует токен в строку.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Lexem.ToString().PadRight(8) + Value;
        }
    }
}
