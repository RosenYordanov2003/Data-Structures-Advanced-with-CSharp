using System;

namespace Exam.Expressionist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expressionist expressionist = new Expressionist();

            Expression root = new Expression()
            {
                Value = "+",
                Id = "1",
                Type = ExpressionType.Operator
            };
            expressionist.AddExpression(root);
            expressionist.AddExpression(new Expression("2", "5", ExpressionType.Value, null, null),root.Id);
            expressionist.AddExpression(new Expression("3", "10", ExpressionType.Value, null, null),root.Id);

            Console.WriteLine(expressionist.Evaluate());
        }
    }
}
