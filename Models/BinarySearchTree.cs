namespace Models
{
    public class BinarySearchTree<K, V> where K : IComparable<K>
    {
        internal TreeNode<K, V>? Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }
        public void AddNode(K data, V value)
        {
            if (Root == null)
                Root = new TreeNode<K, V>(data, value);
            else
                AddNode(data, value, Root);
        }
        private void AddNode(K data, V value, TreeNode<K, V> root)
        {
            if (data.CompareTo(root.Data) <= 0)
            {
                if (root.Left == null)
                    root.Left = new TreeNode<K, V>(data, value);
                else
                    AddNode(data, value, root.Left);
            }
            else
            {
                if (root.Right == null)
                    root.Right = new TreeNode<K, V>(data, value);
                else
                    AddNode(data, value, root.Right);
            }
        }

        private TreeNode<K, V> Get(K key)
        {
            if (Root == null)
                return null;
            return Get(key, Root);
        }
        private TreeNode<K, V> Get(K key, TreeNode<K, V> root)
        {
            if (root == null)
                return null;
            if (root.Data.Equals(key))
                return root;

            else if (key.CompareTo(root.Data) <= 0)
                return Get(key, root.Left);
            else
                return Get(key, root.Right);
        }

        private V GetValue(K key)
        {
            if (Root == null)
                return default;
            return GetValue(key, Root);
        }
        private V GetValue(K key, TreeNode<K, V> root)
        {
            if (root == null)
                return default;
            if (root.Data.Equals(key))
                return root.Value;

            else if (key.CompareTo(root.Data) <= 0)
                return GetValue(key, root.Left);
            else
                return GetValue(key, root.Right);
        }

        private void RemoveNodeWithoutChildren(TreeNode<K, V> deleteNode)
        {
            if (deleteNode.Left == null && deleteNode.Right == null)
            {
                var parent = GetPerent(deleteNode.Data);
                if (parent.Right == deleteNode)
                    parent.Right = null;
                else
                    parent.Left = null;
            }
        }
        private void RemoveNodeWithOneChild(TreeNode<K, V> deleteNode)
        {
            var perent = GetPerent(deleteNode.Data);
            if (perent != null)
            {
                if (deleteNode.Left != null && deleteNode.Right == null)
                {
                    if (perent.Right == deleteNode)   // if parent's RIGHT is the node we want to remove
                        perent.Right = deleteNode.Left;  // parent's RIGHT becomes the deleteNode LEFT
                    else
                        perent.Left = deleteNode.Left; // else parent's LEFT becomes the deleteNode LEFT
                    return;
                }

                else if (deleteNode.Left == null && deleteNode.Right != null)
                {
                    if (perent.Right == deleteNode)  // if parent's LEFT is the node we want to remove
                        perent.Right = deleteNode.Right; // parent's RIGHT becomes the deleteNode RIGHT
                    else
                        perent.Left = deleteNode.Right; // else parent's LEFT becomes the deleteNode RIGHT
                }
            }
        }
        private void RemoveNodeWithTwoChildren(TreeNode<K, V> deleteNode)
        {
            if (deleteNode.Left != null && deleteNode.Right != null)
            {
                var minimum = GetMinimumNode(deleteNode.Right);
                var parant = GetPerent(deleteNode.Data);

                var minParent = GetPerent(minimum.Data);  //find minimum's parent
                if (minParent != deleteNode)
                {
                    minParent.Left = null;
                    if (minimum.Right != null) // if minimum has a right node
                    {
                        minParent.Left = minimum.Right; // minimum's parent takes the node of the minimum's RIGHT node
                    }
                }
                minimum.Left = deleteNode.Left;    // minimum takes the RIGHT and the LEFT of the deleteNode
                if(deleteNode.Right != minimum)
                    minimum.Right = deleteNode.Right;
                

                if (parant.Right == deleteNode)   // connecting the node's parent with the minimum
                    parant.Right = minimum;
                else
                    parant.Left = minimum;
            }
        }

        public void RemoveNode(K data)
        {
            var deleteNode = Get(data);
            if(deleteNode != null && deleteNode != Root)
            {
                RemoveNodeWithoutChildren(deleteNode);
                RemoveNodeWithOneChild(deleteNode);
                RemoveNodeWithTwoChildren(deleteNode);
                deleteNode = null;
            }
        }

        private TreeNode<K, V> GetMinimumNode(TreeNode<K, V> t)
        {
            if (t.Left == null)
                return t;
            return GetMinimumNode(t.Left);
        }

        // Get the parent of the given value
        private TreeNode<K, V> GetPerent(K data)
        {
            if (Root != null)
                return GetPerent(data, Root);
            return null;
        }
        private TreeNode<K, V> GetPerent(K data, TreeNode<K, V> t)
        {
            if (t == null)
                return null;

            if (t.Left != null && t.Left.Data.Equals(data))
                return t;
            else if (t.Right != null && t.Right.Data.Equals(data))
                return t;
            else if (data.CompareTo(t.Data) < 0)
                return GetPerent(data, t.Left);
            else
                return GetPerent(data, t.Right);
        }

        public void TraverseInOrder()
        {
            TraverseInOrder(Root);
        }
        private void TraverseInOrder(TreeNode<K, V>? root)
        {
            if (root != null)
            {
                TraverseInOrder(root.Left);
                Console.Write(root.Data + ", ");
                TraverseInOrder(root.Right);
            }
        }

    }
}
