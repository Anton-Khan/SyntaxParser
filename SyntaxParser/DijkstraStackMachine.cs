using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxParser
{
    public class DijkstraStackMachine
    {
        private Stack<Token> stack;
        private List<Token> result;
        private int pointer;

        public DijkstraStackMachine()
        {
            this.stack = new Stack<Token>();
            this.result = new List<Token>();
            this.pointer = 0;
        }

        protected List<Token> BuildPostfixPolishNotation(List<Token> tokens)
        {
            tokens.Add(new Token(Lexem.END, "END"));

            while (tokens[pointer].Lexem != Lexem.END)
            {
                if (tokens[pointer].Lexem == Lexem.INTEGER || tokens[pointer].Lexem == Lexem.DOUBLE)
                {
                    AddOperandToResult(tokens[pointer]);
                }
                else if (tokens[pointer].Lexem == Lexem.OP)
                {
                    AddOperationToStack(tokens[pointer]);
                }
                else if (IsFucntion(tokens[pointer]))
                {
                    AddFunctionToStack(tokens[pointer]);
                }
                else if (tokens[pointer].Lexem == Lexem.L_B || tokens[pointer].Lexem == Lexem.R_B)
                {
                    AddBrackets(tokens[pointer]);
                }
                else if (tokens[pointer].Lexem == Lexem.COMMA)
                {
                    AddComma(tokens[pointer]);
                }
            }

            while (stack.Count > 0 )
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        private void AddComma(Token comma)
        {
            if (comma.Lexem == Lexem.COMMA)
            {
                while (stack.Peek().Lexem != Lexem.L_B)
                {
                    result.Add(stack.Pop());
                }
                pointer++;
            }
            else
            {
                throw new StackMachineException("Not an comma");
            }
        }

        private void AddOperandToResult(Token operand)
        {
            if (operand.Lexem == Lexem.INTEGER || operand.Lexem == Lexem.DOUBLE )
            {
                result.Add(operand);
                pointer++;
            }
            else
            {
                throw new StackMachineException("Not an operand");
            }
        }

        private void AddOperationToStack(Token operation)
        {
            if (operation.Lexem == Lexem.OP)
            {
                RemoveOperationsFromStack(operation);
                stack.Push(operation);
                pointer++;
            }
            else
            {
                throw new StackMachineException("Not an operation");
            }
        }

        private void AddFunctionToStack(Token function)
        {
            if (IsFucntion(function))
            {
                stack.Push(function);
                pointer++;
            }
            else
            {
                throw new StackMachineException("Not an function");
            }
        }

        private void AddBrackets(Token bracket)
        {
            if (bracket.Lexem == Lexem.L_B)
            {
                stack.Push(bracket);
            }
            else if (bracket.Lexem == Lexem.R_B)
            {
                
                while (stack.Peek().Lexem != Lexem.L_B)
                {
                    result.Add(stack.Pop());
                }

                if (stack.Peek().Lexem == Lexem.L_B)
                {
                    stack.Pop();
                    if (stack.Count > 0 && IsFucntion(stack.Peek()))
                    {
                        result.Add(stack.Pop());
                    }
                }
                
            }
            else
            {
                throw new StackMachineException("Not a bracket");
            }
            pointer++;
        }

        protected bool IsFucntion(Token function)
        {
            return function.Lexem == Lexem.SIN || function.Lexem == Lexem.COS || function.Lexem == Lexem.TAN || function.Lexem == Lexem.LN || function.Lexem == Lexem.LOG || function.Lexem == Lexem.EXP || function.Lexem == Lexem.POW;
        }

        private void RemoveOperationsFromStack(Token operation)
        {
            while (stack.Count > 0 && stack.Peek().Lexem == Lexem.OP && WeighOperand(operation) <= WeighOperand(stack.Peek()))
            {
                result.Add(stack.Pop());
            }
        }

        private int WeighOperand(Token operand)
        {
            switch (operand.Value)
            {
                case "+": return 2;
                case "-": return 2;
                case "*": return 3;
                case "/": return 3;
                default:
                    return 4;
            }
        }
    }
}
