using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public class PostfixPolishNotation : DijkstraStackMachine
    {
        private bool isBuilded = false;
        public PostfixPolishNotation(List<Token> tokens) : base()
        {
            Tokens = BuildPostfixPolishNotation(tokens);
            isBuilded = true;
        }

        public List<Token> Tokens { get; private set; }

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
                if (Tokens[pointer].Lexem == Lexem.INTEGER || Tokens[pointer].Lexem == Lexem.DOUBLE || Tokens[pointer].Lexem == Lexem.PI || Tokens[pointer].Lexem == Lexem.E)
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
                            value = double.Parse(Tokens[pointer].Value.Replace(".", ","));
                        }
                        stack.Push(value);
                        pointer++;
                    }
                    catch (Exception e)
                    {
                        throw new StackMachineException("Can\'t parse value -> " + Tokens[pointer].ToString() + "\n" + e.Message);
                    }
                }
                else if(Tokens[pointer].Lexem == Lexem.OP || IsFunctionOfTwo(Tokens[pointer]))
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
                            //Console.WriteLine($"{showPointer++}) {value1} {Tokens[pointer].Value} {value2} = {stack.Peek()}");
                            Console.WriteLine(showPointer + ")".PadRight(6 - showPointer++.ToString().Length) + $"{value1} {Tokens[pointer].Value} {value2} = {stack.Peek()}");
                            
                        }
                        pointer++;
                    }
                    catch (Exception e)
                    {
                        throw new StackMachineException("Can\'t apply operation of Two -> " + Tokens[pointer].ToString() + "\n" + e.Message);
                    }
                }
                else if (IsFucntion(Tokens[pointer]))
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
                        }
                        if (showIntermediateCalculations)
                        {
                            //Console.WriteLine($"{showPointer++}) {Tokens[pointer].Value} {value} = {stack.Peek()}");
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

            if (stack.Count == 1)
            {
                return stack.Pop();
            }
            else
            {
                throw new StackMachineException("Stack has not only one value");
            }
        }

        private bool IsFunctionOfTwo(Token function)
        {
            return function.Lexem == Lexem.LOG || function.Lexem == Lexem.POW;
        }
    }
}
