namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T element)
        {
            root = this.Insert(root, element);
        }

        private TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            if (node == null)
            {
                return new TreeNode<T>(element);
            }
            else if (node.IsLeaf())
            {
                return MergeNodes(node, new TreeNode<T>(element));
            }
            else if (node.LeftKey.CompareTo(element) > 0)
            {
                var newNode = Insert(node.LeftChild, element);

                return newNode == node.LeftChild ? node : MergeNodes(node, newNode);
            }
            else if (node.RightKey!= null && node.RightKey.CompareTo(element) < 0)
            {
                var newNode = Insert(node.RightChild, element);
                return newNode == node.RightChild ? node : MergeNodes(node, newNode);
            }
            else
            {
                var newNode = Insert(node.MiddleChild, element);
                return newNode == node.MiddleChild ? node : MergeNodes(node, newNode);
            }
        }

        private TreeNode<T> MergeNodes(TreeNode<T> currentNode, TreeNode<T> newNode)
        {
            if (currentNode.IsTwoNode())
            {
                //When current left key is bigger than new key it moves to the right
                if (currentNode.LeftKey.CompareTo(newNode.LeftKey) > 0)
                {
                    currentNode.RightKey = currentNode.LeftKey;
                    currentNode.LeftKey = newNode.LeftKey;

                    currentNode.RightChild = currentNode.MiddleChild;
                    currentNode.MiddleChild = newNode.MiddleChild;
                    currentNode.LeftChild = newNode.LeftChild;
                }
                else
                {
                    currentNode.RightKey = newNode.LeftKey;
                    currentNode.MiddleChild = newNode.LeftChild;
                    currentNode.RightChild = newNode.MiddleChild;
                }
                return currentNode;
            }
            else if (newNode.LeftKey.CompareTo(currentNode.LeftKey) < 0)
            {
                var node = new TreeNode<T>(root.LeftKey)
                {
                    LeftChild = newNode,
                    MiddleChild = currentNode,
                };
                currentNode.LeftChild = currentNode.MiddleChild;
                currentNode.MiddleChild = currentNode.RightChild;
                currentNode.LeftKey = currentNode.RightKey;
                currentNode.RightKey = default;
                currentNode.RightChild = null;

                return node;
            }
            else if (newNode.LeftKey.CompareTo(currentNode.RightKey) < 1)
            {
                newNode.MiddleChild = new TreeNode<T>(currentNode.RightKey)
                {
                    LeftChild = newNode.MiddleChild,
                    MiddleChild = currentNode.RightChild
                };
                newNode.LeftChild = currentNode;
                currentNode.RightKey = default;
                currentNode.RightChild = null;

                return newNode;
            }
            else
            {
                var node = new TreeNode<T>(currentNode.RightKey)
                {
                    LeftChild = currentNode,
                    MiddleChild = newNode,
                };
                newNode.LeftChild = currentNode.RightChild;
                currentNode.RightKey = default;
                currentNode.RightChild = null;

                return node;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
