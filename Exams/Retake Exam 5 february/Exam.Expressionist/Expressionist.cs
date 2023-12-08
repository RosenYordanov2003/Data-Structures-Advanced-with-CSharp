using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Exam.Expressionist
{
    public class Expressionist : IExpressionist
    {
        private Expression root;
        private readonly Dictionary<string, Expression> allExpressions;

        public Expressionist()
        {
            allExpressions = new Dictionary<string, Expression>();
        }
        public void AddExpression(Expression expression)
        {
            if (root != null)
            {
                throw new ArgumentException();
            }
            root = expression;
            allExpressions.Add(expression.Id, root);
        }

        public void AddExpression(Expression expression, string parentId)
        {
            if (!allExpressions.ContainsKey(parentId))
            {
                throw new ArgumentException();
            }
            Expression parrentExpression = allExpressions[parentId];
            if (parrentExpression.LeftChild != null && parrentExpression.RightChild != null)
            {
                throw new ArgumentException();
            }
            if (parrentExpression.LeftChild == null)
            {
                parrentExpression.LeftChild = expression;
            }
            else
            {
                parrentExpression.RightChild = expression;
            }
            expression.Parent = parrentExpression;
            allExpressions.Add(expression.Id, expression);
        }

        public bool Contains(Expression expression)
        {
            return allExpressions.ContainsKey(expression.Id);
        }

        public int Count()
        {
            return allExpressions.Count;
        }

        public string Evaluate()
        {
            if (root == null || (root.LeftChild == null && root.RightChild == null))
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();

            Evaluate(sb, root);

            return sb.ToString().TrimEnd();
        }

        private void Evaluate(StringBuilder sb, Expression node)
        {
            if (node != null)
            {
                if (node.Type == ExpressionType.Value)
                {
                    sb.Append(node.Value);
                }
                else if (node.Type == ExpressionType.Operator)
                {
                    sb.Append("(");
                    Evaluate(sb ,node.LeftChild);
                    sb.Append(" " + node.Value + " ");
                    Evaluate(sb, node.RightChild);
                    sb.Append(")");
                }
            }
        }

        public Expression GetExpression(string expressionId)
        {
            if (!allExpressions.ContainsKey(expressionId))
            {
                throw new ArgumentException();
            }
            return allExpressions[expressionId];
        }

        public void RemoveExpression(string expressionId)
        {
            if (!allExpressions.ContainsKey(expressionId))
            {
                throw new ArgumentException();
            }
            Expression expressionToDelete = allExpressions[expressionId];

            if (root.Id == expressionId)
            {
                allExpressions.Clear();
                root = null;
            }
            else
            {
                Expression parentExpression = expressionToDelete.Parent;
                if (parentExpression.LeftChild.Id == expressionToDelete.Id)
                {
                    parentExpression.LeftChild = parentExpression.RightChild;
                    parentExpression.RightChild = null;
                    expressionToDelete.Parent = null;
                }
                else
                {
                    parentExpression.RightChild = null;
                    expressionToDelete.Parent = null;
                }
                DeleteExpression(expressionToDelete);
            }
        }

        private void DeleteExpression(Expression expression)
        {
            if (expression == null)
            {
                return;
            }

            allExpressions.Remove(expression.Id);
            if (expression.LeftChild != null)
            {
                DeleteExpression(expression.LeftChild);
            }
            if (expression.RightChild != null)
            {
                DeleteExpression(expression.RightChild);
            }
        }
    }
}
