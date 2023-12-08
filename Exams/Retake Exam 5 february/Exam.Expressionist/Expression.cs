namespace Exam.Expressionist
{
    public class Expression
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public ExpressionType Type { get; set; }
        public Expression LeftChild { get; set; }
        public Expression RightChild { get; set; }
        public Expression Parent { get; set; }
        public Expression()
        {
        }

        public Expression(string id, string value, ExpressionType type, Expression leftChild, Expression rightChild)
        {
            Id = id;
            Value = value;
            Type = type;
            LeftChild = leftChild;
            RightChild = rightChild;
        }
    }
}
