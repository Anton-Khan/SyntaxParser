using System.Collections.Generic;

namespace SyntaxParserAPI
{
    /// <summary>Класс собирающай токены из инфиксной в обратную польскую нотацию.</summary>
    public class DijkstraStackMachine
    {
        private Stack<Token> stack;
        private List<Token> result;
        private int pointer;

        /// <summary>Инициализирует новый экземпляр класса DijkstraStackMachine.</summary>
        protected DijkstraStackMachine()
        {
            stack = new Stack<Token>();
            result = new List<Token>();
            pointer = 0;
        }

        /// <summary>Строит обратную польскую нотацию из токенов в инфиксной нотации.</summary>
        /// <param name="tokens">Токены в инфиксной нотации.</param>
        /// <returns>Возвращает токены в обратной польской нотации. <see cref="List{Token}" />;<br /></returns>
        /// <exception cref="StackMachineException">Can\'t handle token " + tokens[pointer].ToString()</exception>
        protected List<Token> BuildPostfixPolishNotation(List<Token> tokens)
        {
            tokens.Add(new Token(Lexem.END, "END"));

            while (tokens[pointer].Lexem != Lexem.END)
            {
                if (Lexem.IsOperand(tokens[pointer].Lexem))
                {
                    AddOperandToResult(tokens[pointer]);
                }
                else if (tokens[pointer].Lexem == Lexem.OP || tokens[pointer].Lexem == Lexem.UNARYMINUS)
                {
                    AddOperationToStack(tokens[pointer]);
                }
                else if (Lexem.IsFucntion(tokens[pointer].Lexem))
                {
                    AddFunctionToStack(tokens[pointer]);
                }
                else if (Lexem.IsBracket(tokens[pointer].Lexem))
                {
                    AddBrackets(tokens[pointer]);
                }
                else if (tokens[pointer].Lexem == Lexem.COMMA)
                {
                    AddComma(tokens[pointer]);
                }
                else
                {
                    throw new StackMachineException("Can\'t handle token " + tokens[pointer].ToString());
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
            while (stack.Peek().Lexem != Lexem.L_B)
            {
                result.Add(stack.Pop());
            }
            pointer++;
        }

        private void AddOperandToResult(Token operand)
        {
            result.Add(operand);
            pointer++;
        }

        private void AddOperationToStack(Token operation)
        {
            RemoveOperationsFromStack(operation);
            stack.Push(operation);
            pointer++;
        }

        private void AddFunctionToStack(Token function)
        {
            stack.Push(function);
            pointer++;
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
                    if (stack.Count > 0 && Lexem.IsFucntion(stack.Peek().Lexem))
                    {
                        result.Add(stack.Pop());
                    }
                }
            }
            pointer++;
        }

        private void RemoveOperationsFromStack(Token operation)
        {
            while (stack.Count > 0 && (stack.Peek().Lexem == Lexem.OP || stack.Peek().Lexem == Lexem.UNARYMINUS) && WeighOperation(operation) <= WeighOperation(stack.Peek()))
            {
                if (stack.Peek().Lexem == Lexem.UNARYMINUS && operation.Lexem == Lexem.UNARYMINUS)
                {
                    break;
                }
                result.Add(stack.Pop());
            }
        }

        private int WeighOperation(Token operation)
        {
            switch (operation.Value)
            {
                case "+": return 2;
                case "-": return 2;
                case "*": return 3;
                case "/": return 3;
                case "~": return 4;
                default:
                    return 5;
            }
        }
    }
}
