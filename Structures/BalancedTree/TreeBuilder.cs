using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms_and_structures.Structures.BalancedTree
{
    public partial class BalancedTree<T> : ITree<T> where T : IComparable
    {
        public TreeNode<T> Insert(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                Size++;
                return new TreeNode<T>(value);
            }

            int comparer = node.Value.CompareTo(value);

            //Value is bigger than node`s value
            if (comparer == -1)
            {
                node.Right = Insert(node.Right, value);
            }

            //Value is equal to node`s value
            else if (comparer == 0)
            {
                _consistDuplicates = true;
                _duplicatesList.Add(value);
                node.Right = Insert(node.Right, value);
            }

            //Value is less than node`s value
            else if (comparer == 1)
            {
                node.Left = Insert(node.Left, value);
            }

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && value.CompareTo(node.Left.Value) == -1)
            {
                return RightRotation(node);
            }

            if (balance < -1 && value.CompareTo(node.Right.Value) == 1)
            {
                return LeftRotation(node);
            }

            if (balance > 1 && value.CompareTo(node.Left.Value) == 1)
            {
                node.Left = LeftRotation(node.Left);
                return RightRotation(node);
            }

            if (balance < -1 && value.CompareTo(node.Right.Value) == -1)
            {
                node.Right = RightRotation(node.Right);
                return LeftRotation(node);
            }

            return node;
        }

        public TreeNode<T> DeleteNode(TreeNode<T> node, T value)
        {
            if (node == null)
                return node;

            int comparer = node.Value.CompareTo(value);

            if (comparer == -1)
            {
                node.Right = DeleteNode(node.Right, value);
            }
            else if (comparer == 1)
            {
                node.Left = DeleteNode(node.Left, value);
            }
            else
            {
                Size--;
                //if node has one child or none
                if (node.Left == null || node.Right == null)
                {
                    TreeNode<T> tmp = null;

//                    tmp = node.Left == null ? node.Right : node.Left;
                    if (tmp == node.Left)
                        tmp = node.Right;
                    else
                        tmp = node.Left;

                    //no child
                    if (tmp == null)
                    {
                        tmp = node;
                        node = null;
                    }
                    else
                    {
                        node = tmp;
                    }
                }
                else
                {
                    //node with 2 children
                    //try to get the smallest node in right subtree
                    var tmp = GetMinValuedNode(node.Right);
                    node.Value = tmp.Value;
                    node.Right = DeleteNode(node.Right, tmp.Value);
                }
            }

            //if there are no nodes in tree
            if (node == null) return Root;

            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.Left) >= 0)
            {
                return RightRotation(node);
            }

            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotation(node.Left);
                return RightRotation(node);
            }

            if (balance < -1 && GetBalance(node.Right) <= 0)
            {
                return LeftRotation(node);
            }

            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotation(node.Right);
                return LeftRotation(node);
            }


            return node;

        }

        private int GetBalance(TreeNode<T> node)
        {
            if (node == null)
                return 0;

            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private int GetHeight(TreeNode<T> node)
        {
            if (node == null) return 0;

            return node.Height;
        }

        private TreeNode<T> RightRotation(TreeNode<T> y)
        {
            var x = y.Left;
            var T1 = x.Right;

            x.Right = y;
            y.Left = T1;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }

        private TreeNode<T> LeftRotation(TreeNode<T> x)
        {
            var y = x.Right;
            var T1 = y.Left;

            y.Left = x;
            x.Right = T1;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            return y;
        }

        private TreeNode<T> GetMinValuedNode(TreeNode<T> nodeRight)
        {
            var currentNode = nodeRight;

            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode;
        }
    }
}
