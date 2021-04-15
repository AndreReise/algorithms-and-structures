using System;
using System.Collections.Generic;
using System.Linq;


namespace algorithms_and_structures.Structures.BalancedTree
{
    public partial class BalancedTree<T> : ITree<T> where T : IComparable
    {
        public List<T> GetPreOrderList()
        {
            var list = new List<T>();

            GetPreOrderListUtils(this.Root , ref list);

            return list;
        }

        private void GetPreOrderListUtils(TreeNode<T> node, ref List<T> list)
        {
            if (node != null)
            {
                list.Add(node.Value);

                GetPreOrderListUtils(node.Left, ref list);
                GetPreOrderListUtils(node.Right, ref list);
            }
        }
        public void PrintPreOrder()
        {
            if (Root == null) throw new Exception("Root is null");

            var list = this.GetPreOrderList();

            Console.WriteLine();
            foreach (var item in list)
            {
                Console.Write(item + "  ");
            }
            
        }


        public List<T> PrintSorted()
        {
            List<T> list = new List<T>();
            ConvertToList(this.Root, ref list);

            Console.WriteLine("\nAscending order\n");

            foreach (var x in list.OrderBy(x => x))
            {
                Console.Write(x + " ");
            }

            Console.WriteLine("\n\nDescending order\n");

            foreach (var x in list.OrderByDescending(x => x))
            {
                Console.Write(x + " ");
            }

            return list.OrderBy(x => x).ToList();

        }


        public void InsertBalancedTree(BalancedTree<T> tree)
        {
            List<T> list = new List<T>();

            ConvertToList(tree.Root, ref list);

            foreach (var item in list)
            {
                this.Root = Insert(this.Root, item);
            }
        }

        private void ConvertToList(TreeNode<T> node, ref List<T> list)
        {
            if (node == null) return;

            if (node.Left != null)
            {
                ConvertToList(node.Left, ref list);
            }

            if (node.Right != null)
            {
                ConvertToList(node.Right, ref list);
            }

            list.Add(node.Value);
        }
    }
}
