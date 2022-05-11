using System;
using System.Collections.Generic;
using System.Globalization;

namespace SyntaxParserAPI
{
    /// <summary>Класс представляет список токенов в обратной польской нотации.</summary>
    public class PostfixPolishNotation : DijkstraStackMachine
    {
        private bool isBuilded = false;
        /// <summary>Инициализирует новый экземпляр класса PostfixPolishNotation.</summary>
        /// <param name="tokens">Список токенов в инфиксной нотации.</param>
        /// <remarks>Получая на вход список токенов в инфиксной нотации, собирает из них обратную польскую с помощью <see cref="SyntaxParserAPI.DijkstraStackMachine"/>.</remarks>
        public PostfixPolishNotation(List<Token> tokens) : base()
        {
            Tokens = BuildPostfixPolishNotation(tokens);
            isBuilded = true;
        }
        /// <summary>Возврщает cписок токенов в обратной польской нотации.</summary>
        /// <value>Список токенов в обратной польской нотации.</value>
        public List<Token> Tokens { get; private set; }

        /// <summary>Решает обратную польскую нотацию.</summary>
        /// <param name="showIntermediateCalculations">если <c>true</c> [Выводит в консоль промежуточные вычисления].</param>
        /// <returns>Число, полученное в результате вычислений <see cref="System.Double" />.</returns>
        /// <exception cref="SyntaxParserAPI.StackMachineException">PostfixPolishNotation is not builded yet</exception>
        public double Solve(bool showIntermediateCalculations)
        {
            if (isBuilded)
            {
                return SolvePostfixPolishNotation(showIntermediateCalculations);
            }
            else
            {
                throw new StackMachineException("PostfixPolishNotation is not builded yet");
            }
        }

        private double SolvePostfixPolishNotation(bool showIntermediateCalculations)
        {
            Stack<double> stack = new Stack<double>();
            int pointer = 0;
            int showPointer = 0;

            while (Tokens.Count > pointer)
            {
                if (Lexem.IsOperand(Tokens[pointer].Lexem))
                {
                    HandleOperand(stack, ref pointer);
                }
                else if(Tokens[pointer].Lexem == Lexem.OP || Lexem.IsFunctionOfTwo(Tokens[pointer].Lexem))
                {
                    HandleOperationOfTwo(stack, ref pointer, showIntermediateCalculations, ref showPointer);
                }
                else if (Lexem.IsFucntion(Tokens[pointer].Lexem) || Tokens[pointer].Lexem == Lexem.UNARYMINUS)
                {
                    HandleOperationOfOne(stack, ref pointer, showIntermediateCalculations, ref showPointer);
                }
                else
                {
                    throw new StackMachineException("Can\'t handle token " + Tokens[pointer].ToString());
                }
            }

            if (stack.Count == 1)
            {
                return stack.Pop();
            }
            else
            {
                throw new StackMachineException("Stack has not only one value at the end of calculation");
            }
        }

        private void HandleOperand(Stack<double> stack, ref int pointer)
        {
            try
            {
                double value = 0;
                if (Tokens[pointer].Lexem == Lexem.PI)
                {
                    value = Math.PI;
                }
                else if (Tokens[pointer].Lexem == Lexem.E)
                {
                    value = Math.E;
                }
                else
                {
                    value = double.Parse(Tokens[pointer].Value, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                }
                stack.Push(value);
                pointer++;
            }
            catch (Exception e)
            {
                throw new StackMachineException("Can\'t parse value -> " + Tokens[pointer].ToString() + "\n" + e.Message);
            }
        }

        private void HandleOperationOfTwo(Stack<double> stack, ref int pointer, bool showIntermediateCalculations, ref int showPointer)
        {
            try
            {
                var value2 = stack.Pop();
                var value1 = stack.Pop();

                switch (Tokens[pointer].Value)
                {
                    case "+": stack.Push(value1 + value2); break;
                    case "-": stack.Push(value1 - value2); break;
                    case "*": stack.Push(value1 * value2); break;
                    case "/": stack.Push(value1 / value2); break;
                    case "pow": stack.Push(Math.Pow(value1, value2)); break;
                    case "log": stack.Push(Math.Log(value2, value1)); break;
                }
                if (showIntermediateCalculations)
                {
                    Console.WriteLine(showPointer + ")".PadRight(6 - showPointer++.ToString().Length) + $"{value1} {Tokens[pointer].Value} {value2} = {stack.Peek()}");
                }
                pointer++;
            }
            catch (Exception e)
            {
                throw new StackMachineException("Can\'t apply operation of Two -> " + Tokens[pointer].ToString() + "\n" + e.Message);
            }
        }

        private void HandleOperationOfOne(Stack<double> stack, ref int pointer, bool showIntermediateCalculations, ref int showPointer)
        {
            try
            {
                var value = stack.Pop();

                switch (Tokens[pointer].Value)
                {
                    case "sin": stack.Push(Math.Sin(value)); break;
                    case "cos": stack.Push(Math.Cos(value)); break;
                    case "tan": stack.Push(Math.Tan(value)); break;
                    case "ln": stack.Push(Math.Log2(value)); break;
                    case "exp": stack.Push(Math.Exp(value)); break;
                    case "~": stack.Push(-value); break;
                }
                if (showIntermediateCalculations)
                {
                    Console.WriteLine(showPointer + ")".PadRight(6 - showPointer++.ToString().Length) + $"{Tokens[pointer].Value} {value} = {stack.Peek()}");
                }
                pointer++;
            }
            catch (Exception e)
            {
                throw new StackMachineException("Can\'t apply operation of One -> " + Tokens[pointer].ToString() + "\n" + e.Message);
            }
        }
    }
}
