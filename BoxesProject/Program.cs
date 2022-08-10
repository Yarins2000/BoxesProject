using Models;
using DAL;
using Newtonsoft.Json;

namespace BoxesProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region stam init
            //var tree = new BinarySearchTree<double, string>();
            //tree.AddNode(5, null);
            //tree.AddNode(6, null);
            //tree.AddNode(4, null);
            //tree.AddNode(8, null);
            //tree.AddNode(10, null);
            //tree.AddNode(3, null);
            //tree.AddNode(4.5, null);
            //tree.AddNode(4.3, null);
            //tree.AddNode(4.7, null);
            //tree.AddNode(4.8, null);
            //tree.AddNode(4.6, null);
            //tree.AddNode(4.2, null);
            //tree.AddNode(4.65, null);
            //tree.TraverseInOrder(Console.WriteLine);
            //Console.WriteLine();
            //Console.WriteLine("====================");
            //tree.RemoveNode(4.5);
            //tree.TraverseInOrder();

            //foreach (var item in tree.TraverseInOrderByEnumerator())
            //{
            //    Console.WriteLine(item.Data);
            //}
            #endregion

            var tree = DBMock.Instance.Tree;
            tree.TraverseInOrder(Console.WriteLine);
            Console.WriteLine();

            var xtree = tree.GetSuitableNodesByRange(4, 7);
            var l = xtree.ToList();
            foreach (var item in xtree)
            {
                Console.WriteLine(item.Data);
            }
        }
    }
    /*
     * public bool FindLilBiggerValue(Tkey minKey, Tkey maxKey, out Tvalue value) // O(log n)
        {
            if (IsEmpty())
            {
                value = default(Tvalue);
                return false;
            }
            value = FindLilBiggerValue(minKey, maxKey, _root);
            return value != null;
        }
        Tvalue FindLilBiggerValue(Tkey minKey, Tkey maxKey, TreeNode node) // O(log n)
        {
            if (node == null) return default;
            int compMIN = node.CompareTo(minKey);
            int compMAX = node.CompareTo(maxKey);

            if (compMIN >= 0 && compMAX <= 0) // If the dimensions match the 
                if (node.Left != null && node.Left.Key.CompareTo(minKey) > 0)
                    return FindLilBiggerValue(minKey, maxKey, node.Left);
                else
                    return node.Value;
            else if (compMIN < 0)
                return FindLilBiggerValue(minKey, maxKey, node.Right);
            else if (compMAX > 0)
                return FindLilBiggerValue(minKey, maxKey, node.Left);
            return default;
        }

    public IEnumerable GetRange(Tkey minKey, Tkey maxKey) => GetRange(_root, minKey, maxKey);
        IEnumerable GetRange(TreeNode node, Tkey minKey, Tkey maxKey)
        {
            if (node == null) yield break;

            if (minKey.CompareTo(node.Key) < 0) 
                foreach (TreeNode leftNode in GetRange(node.Left, minKey, maxKey))
                    yield return leftNode;

            // If NODE's kEY lies in range, then YIELD RETURNS NODE
            if (minKey.CompareTo(node.Key) <= 0 && maxKey.CompareTo(node.Key) >= 0)
                yield return node;

            // Recursively call the right subtree
            foreach (TreeNode rightNode in GetRange(node.Right, minKey, maxKey))
                yield return rightNode;
        }
     */
}