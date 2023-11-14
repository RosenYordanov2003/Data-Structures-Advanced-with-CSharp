namespace _01.RedBlackTree
{
    using System;
    using System.Runtime.InteropServices.ComTypes;

    public class RedBlackTree<T> where T : IComparable
    {
        private const bool RedColor = true;
        private const bool BlackColor = false;

        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = RedColor;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
        }

        public Node root;

        public RedBlackTree()
        {

        }
        public RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }


        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, this.root);
        }


        public RedBlackTree<T> Search(T element)
        {
            Node nodeToSearch = Search(this.root, element);

            return new RedBlackTree<T>(nodeToSearch);
        }

      

        public void Insert(T value)
        {
            this.root = this.Insert(this.root, value);
            if (root != null && root.Color == BlackColor)
            {
                root.Color = BlackColor;
            }
        }

        
        public int Count()
        {
            return this.Count(this.root);
        }

        public void Delete(T key)
        {
            CheckForEmptyTree();
            this.root = Delete(root, key);
        }

        public void DeleteMin()
        {
            CheckForEmptyTree();
            this.root = DeleteMin(root);
            if (root != null && root.Color == BlackColor)
            {
                root.Color = BlackColor;
            }
        }
        public void DeleteMax()
        {
            CheckForEmptyTree();

            if (this.root != null && root.Color == RedColor)
            {
                this.root.Color = BlackColor;
            }

            this.root = DeleteMax(this.root);
        }
        private Node Delete(Node node, T value)
        {
            if (IsLesser(value, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                {
                    node = MoveRedLeft(node);
                }
                node.Left = Delete(node.Left, value);
            }
            else 
            {
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }
                if (node.Value.CompareTo(value) == 0 && node.Right == null)
                {
                    return null;
                }
                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = MoveRedRight(node);
                }
                if (node.Value.CompareTo(value) == 0 && node.Right != null)
                {
                    node.Value = FindSmallestElementInSubtree(node.Right);
                    node.Right = DeleteMin(node.Right);
                }
                else
                {
                  node.Right = Delete(node.Right, value);
                }
            }
             
            return FixUp(node);
        }

        private T FindSmallestElementInSubtree(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.Value;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }
            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }
        private Node Search(Node node, T element)
        {
            while (node != null)
            {
                if (IsLesser(element, node.Value))
                {
                    node = node.Left;
                }
                else if (IsLesser(node.Value, element))
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }
            return node;
        }
        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }
            else if (IsLesser(value, node.Value))
            {
                node.Left = this.Insert(node.Left, value);
            }
            else
            {
                node.Right = this.Insert(node.Right, value);
            }

            //Rotations
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                node = FlipColors(node);
            }

            return node;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }
            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = MoveRedLeft(node);
            }
            node.Left = DeleteMin(node.Left);

            return FixUp(node);
        }
        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }
            EachInOrder(action, node.Left);
            action(node.Value);
            EachInOrder(action, node.Right);
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                node = FlipColors(node);
            }
            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }
            if (node.Right == null)
            {
                return null;
            }
            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = MoveRedRight(node);
            }
            node.Right = DeleteMax(node.Right);

            return FixUp(node);
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }

      

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }

        private bool IsLesser(T value1, T value2)
        {
            return value1.CompareTo(value2) < 0;
        }
        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Color == RedColor;
        }
        private void CheckForEmptyTree()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
        }
        private Node FlipColors(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;

            return node;
        }
        private Node RotateLeft(Node node)
        {
            Node tempNode = node.Right;
            node.Right = tempNode.Left;
            tempNode.Left = node;
            tempNode.Color = tempNode.Left.Color;
            tempNode.Left.Color = RedColor;

            return tempNode;
        }

        private Node RotateRight(Node node)
        {
            Node tempNode = node.Left;
            node.Left = tempNode.Right;
            tempNode.Right = node;
            tempNode.Color = tempNode.Right.Color;
            tempNode.Right.Color = RedColor;

            return tempNode;
        }
    }
}