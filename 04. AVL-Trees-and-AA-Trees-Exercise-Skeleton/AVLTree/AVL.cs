namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            Node current = this.Root;
            while (current != null)
            {
                int result = current.Value.CompareTo(element);

                if (result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Delete(T element)
        {
            this.Root = this.Delete(element, this.Root);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }
            Node temp = this.FindSmallestNode(this.Root);
            this.Root = this.Delete(temp.Value, this.Root);
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(element, this.Root);
        }
        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(action, this.Root);
        }
        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }
            int result = node.Value.CompareTo(element);
            if (result > 0)
            {
                node.Left = this.Delete(element, node.Left);
            }
            else if (result < 0)
            {
                node.Right = this.Delete(element, node.Right);
            }
            else
            {
                if (node.Right == null && node.Left == null)
                {
                    return null;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                }
                else if (node.Left == null)
                {
                    node = node.Right;
                }
                else
                {
                    Node smallestNode = this.FindSmallestNode(node.Right);
                    node.Value = smallestNode.Value;
                    node.Right = this.Delete(node.Value, node.Right);
                }
            }
            node = Balace(node);
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            return node;
        }

        private Node FindSmallestNode(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }
            return FindSmallestNode(node.Left);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                return new Node(element);
            }
            int result = node.Value.CompareTo(element);

            if (result > 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else
            {
                node.Right = this.Insert(element, node.Right);
            }

            node = this.Balace(node);
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            return node;
        }
        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }
            this.EachInOrder(action, node.Left);
            action(node.Value);
            this.EachInOrder(action, node.Right);
        }
        private int GetHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }
        private Node Balace(Node node)
        {
            int balanceFactor = GetHeight(node.Left) - GetHeight(node.Right);
            if (balanceFactor > 1)
            {
                int leftChildBalanceFactor = GetHeight(node.Left.Left) - GetHeight(node.Left.Right);
                if (leftChildBalanceFactor < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                node = RotateRight(node);
            }
            else if (balanceFactor < -1)
            {
                int rightChildBalanceFactor = GetHeight(node.Right.Left) - GetHeight(node.Right.Right);
                if (rightChildBalanceFactor > 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                node = RotateLeft(node);
            }
            return node;
        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            return temp;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            node.Height =  Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            return temp;
        }
    }
}
