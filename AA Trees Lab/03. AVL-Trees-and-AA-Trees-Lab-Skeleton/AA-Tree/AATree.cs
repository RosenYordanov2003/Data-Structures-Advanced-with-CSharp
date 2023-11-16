namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Value = element;
                Level = 0;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public int Level { get; set; }
        }

        private Node root;

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, root);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if (node.Value.CompareTo(element) > 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else
            {
                node.Right = this.Insert(element, node.Right);
            }
            node = Skew(node);
            node = Split(node);

            return node;
        }

        private Node Split(Node node)
        {
            if (node.Right != null && node.Right.Right != null && node.Level == node.Right.Level && node.Level == node.Right.Right.Level)
            {
                Node temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;
                temp.Level++;

                return temp;
            }
            return node;
        }

        private Node Skew(Node node)
        {
            if (node.Left != null && node.Level == node.Left.Level)
            {
                Node temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;
                return temp;
            }
            return node;
        }

        public bool Contains(T element)
        {
            Node current = root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
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

        public void InOrder(Action<T> action)
        {
            this.InOrder(this.root, action);
        }

        private void InOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            InOrder(node.Left, action);
            action(node.Value);
            InOrder(node.Right, action);
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrder(this.root, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            action(node.Value);
            PreOrder(node.Left, action);
            PreOrder(node.Right, action);
        }

        public void PostOrder(Action<T> action)
        {
            this.PostOrder(this.root, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return ;
            }
            PostOrder(node.Left, action);
            PostOrder(node.Right, action);
            action(node.Value);
        }
    }
}