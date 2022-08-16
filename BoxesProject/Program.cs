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

            BoxManager bm = new();
            bm.Traverse(Console.WriteLine);
            //var list = bm.SuitableBoxListByAmount(8, 7, 22, out bool flag);
            //bm.UpdateTreeAfterPurchase(list);
            
            BoxManagerUI bmUI = new();
            //bmUI.Start();

            //bmUI.ChooseBoxesForGift();

        }
    }
    /*public void RemoveNode(k key)
        {
            RemoveNode(root, key);
        }
        private Node<k, v> RemoveNode(Node<k, v> root, k key)
        {
            if (root == null)
                return root;
            if (root.Key.CompareTo(key) > 0)
                root.Left = RemoveNode(root.Left, key);
            else if (root.Key.CompareTo(key) < 0)
            {
                root.Right = RemoveNode(root.Right, key);
            }
            //we found the node
            else
            {
                //Node has no children
                if (root.Left == null && root.Right == null)
                {
                    //update root to null
                    root = null;
                }
                //node has two children
                else if (root.Left != null && root.Right != null)
                {
                    var maxNode = FindMax(root.Right);
                    //copy the value
                    root.Key = maxNode.Key;
                    root.Value = maxNode.Value;
                    root.Right = RemoveNode(root.Right, maxNode.Key);
                }
                //node has one children
                else
                {
                    var child = root.Left != null ? root.Left : root.Right;
                    root = child;
                }
            }
            return root;
        }
     */
}