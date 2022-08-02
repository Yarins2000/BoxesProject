namespace Models
{
    public class BinarySearchTree<K, V> where K:IComparable<K>
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
                if(root.Left == null)
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

        public void TraverseInOrder()
        {
            TraverseInOrder(Root);
        }
        private void TraverseInOrder(TreeNode<K, V>? root)
        {
            if (root != null)
            {
                TraverseInOrder(root.Left);
                Console.Write(root.Data + " ");
                TraverseInOrder(root.Right);
            }
        }

        public V GetValue(K key)
        {
            if (Root == null)
                return default(V);
            return GetValue(key, Root);
        }
        private V GetValue(K key, TreeNode<K, V> root)
        {
            if (Root == null)
                return default(V);

            if (root.Data.Equals(key))
                return root.Value;

            else if (key.CompareTo(root.Data) <= 0)
                return GetValue(key, root.Left);
            else
                return GetValue(key, root.Right);
        }

        public override string ToString()
        {
            return ToString(Root);
        }
        private string ToString(TreeNode<K, V>? root)
        {
            string res = "";
            if (root != null)
            {
                TraverseInOrder(root.Left);
                //Console.Write(root.Data + " ");
                res += $"Data: {root.Data}, Value: {root.Value}\n";
                TraverseInOrder(root.Right);
            }
            return res;
        }
    }
}
